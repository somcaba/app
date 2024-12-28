using OrganisationEntity = Yacaba.Domain.Models.Organisation;

namespace Yacaba.Domain.Events.Organisation {
    public record OrganisationDeleted(OrganisationEntity Previous) { }
}
