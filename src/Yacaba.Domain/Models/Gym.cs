namespace Yacaba.Domain.Models {
    public class Gym {

        public Int64 Id { get; set; }

        public required String Name { get; set; }

        public String? Image { get; set; }

        public String? Contact { get; set; }
        public Address? Address { get; set; }
        public GpsLocation? Location { get; set; }

        public Boolean IsOffical { get; set; }

        public Int64? OrganisationId { get; set; }
        public Organisation? Organisation { get; set; }

        public ICollection<Wall> Walls { get; set; } = [];

    }
}
