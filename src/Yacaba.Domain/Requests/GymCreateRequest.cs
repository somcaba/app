namespace Yacaba.Domain.Requests {
    public class GymCreateRequest {

        public required String Name { get; init; }
        public String? Image { get; init; }

    }
}
