using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Yacaba.Core.Odata.ModelBuilder;

namespace Yacaba.Core.Odata {

    public static class OdataModuleBuilderExtensions {

        public static OdataModelBuilder AddOdataModelBuilder(this IServiceCollection services, Assembly assembly) {
            var collection = new ServiceCollection();
            Type typeToFind = typeof(IModelBuilder);

            var moduleBuilderToRegisters = assembly.GetTypes()
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

    }
}
