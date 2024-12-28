using OrganisationEntity = Yacaba.Domain.Models.Organisation;

namespace Yacaba.Domain.Events.Organisation {
    public record OrganisationUpdated(OrganisationEntity Previous, OrganisationEntity Current) { }
}
