using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yacaba.Web;
using Yacaba.Web.Components;
using Yacaba.Web.Components.Account;
using Yacaba.Web.Data;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);

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
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(options => {
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(connectionString);
    options.UseOpenIddict();
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
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

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options => options.AddPolicy("CookieAuthenticationPolicy", builder => {
    builder.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
    builder.RequireAuthenticatedUser();
}));

// Register the worker responsible for seeding the database.
// Note: in a real world application, this step should be part of a setup script.
builder.Services.AddHostedService<Worker>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
} else {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

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
