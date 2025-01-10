using Yacaba.Domain.Models;

namespace Yacaba.Domain.Requests {
    public class WallUpdateRequest : WallCreateRequest {

        public required Int64 Id { get; init; }

    }
}
