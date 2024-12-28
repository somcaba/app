namespace Yacaba.Domain.Requests.Organisation {
    public class CreateOrganisationRequest {

        public required String Name { get; init; }
        public String? Image { get; init; }

    }
}
