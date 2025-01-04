using Microsoft.OData.ModelBuilder;

namespace Yacaba.Core.Odata.ModelBuilder {
    public interface IModelBuilder {

        void Configure(ODataModelBuilder modelBuilder);

    }
}
