using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Yacaba.Core.Odata.LinkProvider;
using Yacaba.Core.Odata.ModelBuilder;

namespace Yacaba.Core.Odata {

    public static class OdataConfigurationBuilderExtensions {

        public static OdataModelBuilder AddOdataModelBuilder(this IServiceCollection services, params Assembly[] assemblies) {
            var collection = new ServiceCollection();
            Type typeToFind = typeof(IModelBuilder);

            var moduleBuilderToRegisters = assemblies.SelectMany(p => p.GetTypes())
                .Where(p =>
                    p.IsAbstract == false &&
                    typeToFind.IsAssignableFrom(p)
                ).ToList();

            foreach (Type? module in moduleBuilderToRegisters) {
                collection.AddSingleton(typeToFind, module);
            }

            collection.AddSingleton(services => {
                OdataModelBuilder instance = ActivatorUtilities.CreateInstance<OdataModelBuilder>(services);
                instance.Initialize();
                return instance;
            });

            OdataModelBuilder result = collection.BuildServiceProvider().GetRequiredService<OdataModelBuilder>();
            return result;
        }

        public static void AddOdataLinkProvider(this IServiceCollection services, Assembly assembly) {

            Type typeToFind = typeof(ILinkProvider);

            var moduleBuilderToRegisters = assembly.GetTypes()
                .Where(p =>
                    p.IsAbstract == false &&
                    typeToFind.IsAssignableFrom(p)
                ).ToList();

            foreach (Type? module in moduleBuilderToRegisters) {
                services.AddSingleton(typeToFind, module);
            }

            services.AddSingleton<ILinkProviderService, DefaultLinkProviderService>();
        }

    }
}
