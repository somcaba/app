using Yacaba.Domain.Models;

namespace Yacaba.Domain.Requests {
    public class GymUpdateRequest : GymCreateRequest {

        public required Int64 Id { get; init; }

    }
}
