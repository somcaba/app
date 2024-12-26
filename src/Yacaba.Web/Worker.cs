using System.Globalization;
using OpenIddict.Abstractions;
using Yacaba.Web.Data;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Yacaba.Web;

public class Worker : IHostedService {

    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken) {
        using IServiceScope scope = _serviceProvider.CreateScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        await RegisterApplicationsAsync(scope.ServiceProvider, cancellationToken);
        await RegisterScopesAsync(scope.ServiceProvider, cancellationToken);

        static async Task RegisterApplicationsAsync(IServiceProvider provider, CancellationToken cancellationToken) {
            IOpenIddictApplicationManager manager = provider.GetRequiredService<IOpenIddictApplicationManager>();

            // API
            if (await manager.FindByClientIdAsync("resource_server_1", cancellationToken) == null) {
                var descriptor = new OpenIddictApplicationDescriptor {
                    ClientId = "resource_server_1",
                    ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                    Permissions =
                    {
                        Permissions.Endpoints.Introspection
                    }
                };

                await manager.CreateAsync(descriptor, cancellationToken);
            }

            // Blazor Hosted
            if (await manager.FindByClientIdAsync("blazorcodeflowpkceclient", cancellationToken) is null) {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor {
                    ClientId = "blazorcodeflowpkceclient",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "Blazor code PKCE",
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:44348/callback/logout/local")
                    },
                    RedirectUris =
                    {
                        new Uri("https://localhost:44348/callback/login/local")
                    },
                    ClientSecret = "codeflow_pkce_client_secret",
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.EndSession,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + "api1"
                    },
                    Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                }, cancellationToken);
            }
        }

        static async Task RegisterScopesAsync(IServiceProvider provider, CancellationToken cancellationToken) {
            IOpenIddictScopeManager manager = provider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByNameAsync("api1", cancellationToken) is null) {
                await manager.CreateAsync(new OpenIddictScopeDescriptor {
                    DisplayName = "Dantooine API access",
                    DisplayNames =
                    {
                        [CultureInfo.GetCultureInfo("fr-FR")] = "Accès à l'API de démo"
                    },
                    Name = "api1",
                    Resources =
                    {
                        "resource_server_1"
                    }
                }, cancellationToken);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
