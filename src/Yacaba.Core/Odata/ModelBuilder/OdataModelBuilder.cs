using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace Yacaba.Core.Odata.ModelBuilder {
    public class OdataModelBuilder {

        private readonly ODataConventionModelBuilder _modelBuilder = new();
        private readonly IEnumerable<IModelBuilder> _moduleBuilders;

        public OdataModelBuilder(
            IEnumerable<IModelBuilder> moduleBuilders
        ) {
            _moduleBuilders = moduleBuilders;
        }

        public void Initialize() {
            foreach (var module in _moduleBuilders) {
                module.Configure(_modelBuilder);
            }
            _modelBuilder.EnableLowerCamelCase();
        }

        public IEdmModel GetEdmModel() => _modelBuilder.GetEdmModel();

    }
}
