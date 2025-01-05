using Microsoft.OData.ModelBuilder;
using Yacaba.Core.Odata.LinkProvider;
using Yacaba.Core.Odata.ModelBuilder;

namespace Yacaba.Core.Odata.Models {
    public class InstanceLinkModelBuilder : IModelBuilder {
        public void Configure(ODataModelBuilder modelBuilder) {
            ComplexTypeConfiguration<InstanceLink> config = modelBuilder.ComplexType<InstanceLink>();
        }
    }
}
