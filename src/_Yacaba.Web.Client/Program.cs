using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Yacaba.Shared.Services;
using Yacaba.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the Yacaba.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

//builder.Services.AddHttpClient("Yacaba.Api")
//    .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
//    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

//// Supply HttpClient instances that include access tokens when making requests to the server project.
//builder.Services.AddScoped(provider => {
//    var factory = provider.GetRequiredService<IHttpClientFactory>();
//    return factory.CreateClient("Yacaba.Api");
//});

//builder.Services.AddOidcAuthentication(options => {
//    options.ProviderOptions.ClientId = "yacaba-web-client";
//    options.ProviderOptions.Authority = "https://localhost:5099/";
//    options.ProviderOptions.ResponseType = "code";

//    // Note: response_mode=fragment is the best option for a SPA. Unfortunately, the Blazor WASM
//    // authentication stack is impacted by a bug that prevents it from correctly extracting
//    // authorization error responses (e.g error=access_denied responses) from the URL fragment.
//    // For more information about this bug, visit https://github.com/dotnet/aspnetcore/issues/28344.
//    //
//    options.ProviderOptions.ResponseMode = "query";
//    options.AuthenticationPaths.RemoteRegisterPath = "https://localhost:5099/Identity/Account/Register";

//    // Add the "roles" (OpenIddictConstants.Scopes.Roles) scope and the "role" (OpenIddictConstants.Claims.Role) claim
//    // (the same ones used in the Startup class of the Server) in order for the roles to be validated.
//    // See the Counter component for an example of how to use the Authorize attribute with roles
//    options.ProviderOptions.DefaultScopes.Add("roles");
//    options.UserOptions.RoleClaim = "role";
//});

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

await builder.Build().RunAsync();
