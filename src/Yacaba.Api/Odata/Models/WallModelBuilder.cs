using Microsoft.OData.ModelBuilder;
using Yacaba.Core.Odata.ModelBuilder;
using Yacaba.Domain.Models;

namespace Yacaba.Api.Odata.Models {
    public class WallModelBuilder : IModelBuilder {
        public void Configure(ODataModelBuilder modelBuilder) {
            EntitySetConfiguration<Wall> wallEntitySet = modelBuilder.EntitySet<Wall>("walls");
            EntityTypeConfiguration<Wall> wallEntityType = wallEntitySet.EntityType;
            wallEntityType.HasKey(p => p.Id);

            wallEntityType.Ignore(p => p.GymId);
            wallEntityType.Expand(SelectExpandType.Automatic, nameof(Wall.Gym).ToLower());
        }
    }
}
