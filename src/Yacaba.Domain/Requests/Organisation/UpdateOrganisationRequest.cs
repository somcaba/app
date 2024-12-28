using Yacaba.Domain.Models;

namespace Yacaba.Domain.Requests.Organisation {
    public class UpdateOrganisationRequest : CreateOrganisationRequest {

        public required OrganisationId Id { get; init; }

    }
}
