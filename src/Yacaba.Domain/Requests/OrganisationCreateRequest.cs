namespace Yacaba.Domain.Requests {
    public class OrganisationCreateRequest {

        public required String Name { get; init; }
        public String? Image { get; init; }

    }
}
