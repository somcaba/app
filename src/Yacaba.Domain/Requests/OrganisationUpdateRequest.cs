using Yacaba.Domain.Models;

namespace Yacaba.Domain.Requests {
    public class OrganisationUpdateRequest : OrganisationCreateRequest {

        public required Int64 Id { get; init; }

    }
}
