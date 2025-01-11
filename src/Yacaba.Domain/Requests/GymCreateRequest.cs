using Yacaba.Domain.Models;

namespace Yacaba.Domain.Requests {
    public class GymCreateRequest {

        public required String Name { get; init; }
        public String? Image { get; init; }
        public String? Contact { get; init; }
        public Address? Address { get; init; }
        public GpsLocation? Location { get; init; }

        public Int64? Organisation { get; init; }

    }
}
