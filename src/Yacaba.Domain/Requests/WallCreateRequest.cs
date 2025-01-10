using Yacaba.Domain.Models;

namespace Yacaba.Domain.Requests {
    public class WallCreateRequest {

        public required String Name { get; init; }
        public required String Image { get; init; }
        public required WallType WallType { get; init; }
        public required Int32 Angle { get; init; }
        public required Int64 Gym { get; init; }
    }
}
