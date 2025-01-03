using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Yacaba.Domain.Models;

namespace Yacaba.Api {
    public class YacabaOdataModelBuilder {

        private readonly ODataConventionModelBuilder _modelBuilder = new();

        public YacabaOdataModelBuilder() {
            EntitySetConfiguration<Organisation> organisationEntitySet = _modelBuilder.EntitySet<Organisation>("organisations");

            EntityTypeConfiguration<Organisation> organisationEntityType = organisationEntitySet.EntityType;
            organisationEntityType.HasKey(p => p.OrganisationId);
            organisationEntityType.Ignore(p => p.Id);
            organisationEntityType.Property(p => p.OrganisationId).Name = nameof(Organisation.Id);

            //var organisation = _modelBuilder.StructuralTypes.First(t => t.ClrType == typeof(Organisation));
            //organisation.AddProperty(typeof(Organisation).GetProperty("OrganisationId")).Name = "Id";

            _modelBuilder.EnableLowerCamelCase();
        }

        public IEdmModel GetEdmModel() => _modelBuilder.GetEdmModel();

    }
}
