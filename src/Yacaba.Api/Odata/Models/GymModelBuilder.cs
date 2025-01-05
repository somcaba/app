using Microsoft.OData.ModelBuilder;
using Yacaba.Core.Odata.ModelBuilder;
using Yacaba.Domain.Models;

namespace Yacaba.Api.Odata.Models {
    public class GymModelBuilder : IModelBuilder {
        public void Configure(ODataModelBuilder modelBuilder) {
            EntitySetConfiguration<Gym> gymEntitySet = modelBuilder.EntitySet<Gym>("gyms");
            EntityTypeConfiguration<Gym> gymEntityType = gymEntitySet.EntityType;
            gymEntityType.HasKey(p => p.Id);

            gymEntityType.Ignore(p => p.OrganisationId);
            gymEntityType.Expand(SelectExpandType.Automatic, nameof(Gym.Organisation).ToLower());
        }
    }
}
