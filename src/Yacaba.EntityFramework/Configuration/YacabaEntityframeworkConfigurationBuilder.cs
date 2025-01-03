using Yacaba.Domain.Stores;
using Yacaba.EntityFramework.Stores;

namespace Microsoft.Extensions.DependencyInjection {
    public static class YacabaEntityframeworkConfigurationBuilder {

        public static void AddYacabaEntityFrameworkStores(this IServiceCollection services) {
            services.AddTransient<IOrganisationStore, EntityframeworkOrganisationStore>();
        }

    }
}
