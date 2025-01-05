using Microsoft.OData.ModelBuilder;
using Yacaba.Core.Odata.ModelBuilder;
using Yacaba.Domain.Models;

namespace Yacaba.Api.Odata.Models {
    public class OrganisationModelBuilder : IModelBuilder {
        public void Configure(ODataModelBuilder modelBuilder) {
            EntitySetConfiguration<Organisation> organisationEntitySet = modelBuilder.EntitySet<Organisation>("organisations");
            EntityTypeConfiguration<Organisation> organisationEntityType = organisationEntitySet.EntityType;
            organisationEntityType.HasKey(p => p.Id);
        }
    }
}
