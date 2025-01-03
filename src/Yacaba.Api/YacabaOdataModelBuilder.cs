using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Yacaba.Domain.Models;

namespace Yacaba.Api {
    public class YacabaOdataModelBuilder {

        private readonly ODataConventionModelBuilder _modelBuilder = new();

        public YacabaOdataModelBuilder() {
            EntitySetConfiguration<Organisation> organisationEntitySet = _modelBuilder.EntitySet<Organisation>("organisations");

            EntityTypeConfiguration<Organisation> organisationEntityType = organisationEntitySet.EntityType;
            organisationEntityType.HasKey(p => p.Id);
            
            _modelBuilder.EnableLowerCamelCase();
        }

        public IEdmModel GetEdmModel() => _modelBuilder.GetEdmModel();

    }
}
