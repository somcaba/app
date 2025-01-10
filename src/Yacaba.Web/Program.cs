using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Scalar.AspNetCore;
using Yacaba.Api;
using Yacaba.Core.Odata;
using Yacaba.Core.Odata.ModelBuilder;
using Yacaba.Core.Odata.Serializer;
using Yacaba.EntityFramework;
using Yacaba.EntityFramework.Identity;
using Yacaba.Web;
using Yacaba.Web.Components;
using Yacaba.Web.Components.Account;
using static OpenIddict.Abstractions.OpenIddictConstants;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAntiforgery(options => {
    options.HeaderName = "X-XSRF-TOKEN";
    options.Cookie.Name = "__Yacaba-X-XSRF-TOKEN";
    options.Cookie.SameSite = SameSiteMode.Strict;
    if (Environment.GetEnvironmentVariable("NON_SECURE_ENVIRONMENT") != "true") {
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    }
});

builder.Services.AddAuthentication(options => {
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies();

builder.AddNpgsqlDbContext<ApplicationDbContext>(connectionName: "postgresdb", configureDbContextOptions: options => {
    options.UseOpenIddict();
});
builder.EnrichNpgsqlDbContext<ApplicationDbContext>(settings => {
    settings.DisableRetry = false;
    settings.CommandTimeout = 30;
});

//String connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<ApplicationDbContext>(options => {
//    options.UseNpgsql(connectionString);
//    options.UseOpenIddict();
//});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddOpenIddict()
    // Register the OpenIddict core components.
    .AddCore(options => {
        // Configure OpenIddict to use the Entity Framework Core stores and models.
        // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
        options.UseEntityFrameworkCore()
                .UseDbContext<ApplicationDbContext>();

        // Enable Quartz.NET integration.
        //options.UseQuartz();
    })

    // Register the OpenIddict client components.
    .AddClient(options => {
        // Note: this sample uses the code flow, but you can enable the other flows if necessary.
        options.AllowAuthorizationCodeFlow();

        // Register the signing and encryption credentials used to protect
        // sensitive data like the state tokens produced by OpenIddict.
        options.AddDevelopmentEncryptionCertificate()
                .AddDevelopmentSigningCertificate();

        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
        options.UseAspNetCore()
                .EnableStatusCodePagesIntegration()
                .EnableRedirectionEndpointPassthrough();

        // Register the System.Net.Http integration and use the identity of the current
        // assembly as a more specific user agent, which can be useful when dealing with
        // providers that use the user agent as a way to throttle requests (e.g Reddit).
        options.UseSystemNetHttp()
                .SetProductInformation(typeof(Program).Assembly);

        // Register the Web providers integrations.
        //
        // Note: to mitigate mix-up attacks, it's recommended to use a unique redirection endpoint
        // URI per provider, unless all the registered providers support returning a special "iss"
        // parameter containing their URL as part of authorization responses. For more information,
        // see https://datatracker.ietf.org/doc/html/draft-ietf-oauth-security-topics#section-4.4.
        options.UseWebProviders()
                .AddGitHub(options => {
                    options.SetClientId("c4ade52327b01ddacff3")
                            .SetClientSecret("da6bed851b75e317bf6b2cb67013679d9467c122")
                            .SetRedirectUri("callback/login/github");
                });
    })

    // Register the OpenIddict server components.
    .AddServer(options => {
        // Enable the authorization, logout, token and userinfo endpoints.
        options.SetAuthorizationEndpointUris("connect/authorize")
                .SetEndSessionEndpointUris("connect/logout")
                .SetTokenEndpointUris("connect/token")
                .SetUserInfoEndpointUris("connect/userinfo");

        // Mark the "email", "profile" and "roles" scopes as supported scopes.
        options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

        // Note: the sample uses the code and refresh token flows but you can enable
        // the other flows if you need to support implicit, password or client credentials.
        options.AllowAuthorizationCodeFlow()
                .AllowRefreshTokenFlow();

        // Register the signing and encryption credentials.
        options.AddDevelopmentEncryptionCertificate()
                .AddDevelopmentSigningCertificate();

        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
        options.UseAspNetCore()
                .EnableAuthorizationEndpointPassthrough()
                .EnableEndSessionEndpointPassthrough()
                .EnableStatusCodePagesIntegration()
                .EnableTokenEndpointPassthrough();
    })

    // Register the OpenIddict validation components.
    .AddValidation(options => {
        // Import the configuration from the local OpenIddict server instance.
        options.UseLocalServer();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

OdataModelBuilder modelBuilder = builder.Services.AddOdataModelBuilder(
    typeof(_ApiAssembly).Assembly,
    typeof(OdataConfigurationBuilderExtensions).Assembly
);

builder.Services.AddControllersWithViews()
    .AddApplicationPart(typeof(_ApiAssembly).Assembly)
    .AddControllersAsServices()
    .AddOData(options => {
        options.EnableAttributeRouting = true;
        options.TimeZone = TimeZoneInfo.Utc;
        options.EnableQueryFeatures();
        options.RouteOptions.EnableNonParenthesisForEmptyParameterFunction = true;
        options.RouteOptions.EnableActionNameCaseInsensitive = true;
        options.RouteOptions.EnableControllerNameCaseInsensitive = true;
        options.RouteOptions.EnablePropertyNameCaseInsensitive = true;
        options.RouteOptions.EnableKeyAsSegment = true;
        options.RouteOptions.EnableKeyInParenthesis = false;
        options.AddRouteComponents(routePrefix: "api", model: modelBuilder.GetEdmModel(), configureServices: services => {
            //services.AddSingleton<IFilterBinder, CustomFilterBinder>();
            services.AddOdataLinkProvider(typeof(_ApiAssembly).Assembly);
            services.AddSingleton<IODataSerializerProvider, CustomODataSerializerProvider>();
        });
    })
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, true));
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });


//builder.Services.AddAutoMapper(options => {
//    options.AddExpressionMapping();
//}, typeof(_DomainAssembly).Assembly);

builder.Services.AddMediatR(options => {
    options.RegisterServicesFromAssembly(typeof(_ApiAssembly).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(_ApiAssembly).Assembly);

builder.Services.AddYacabaEntityFrameworkStores();

builder.Services.AddRazorPages();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("CookieAuthenticationPolicy", builder => {
        builder.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
        builder.RequireAuthenticatedUser();
    });

builder.Services.AddOpenApi();

// Register the worker responsible for seeding the database.
// Note: in a real world application, this step should be part of a setup script.
builder.Services.AddHostedService<Worker>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
    app.UseODataRouteDebug();

    app.MapOpenApi();
    app.MapScalarApiReference();

    IdentityModelEventSource.ShowPII = true;
} else {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseODataQueryRequest();
app.UseODataBatching();

app.UseRouting();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Yacaba.Web.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapRazorPages();
app.MapControllers();

app.Run();