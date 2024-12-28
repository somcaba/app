namespace Yacaba.Domain.Models {

    public record OrganisationId(Int64 Value) { }

    public class Organisation {

        public OrganisationId Id { get; set; } = default!;

        public String Name { get; set; } = default!;
        public String? Image { get; set; }
        public Boolean IsOffical { get; set; }

    }
}
