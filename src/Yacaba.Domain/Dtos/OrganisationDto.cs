namespace Yacaba.Domain.Dtos {
    public class OrganisationDto {
        public Int64 Id { get; set; }

        public String Name { get; set; } = default!;
        public String? Image { get; set; }
        public Boolean IsOffical { get; set; }
    }
}
