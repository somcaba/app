namespace Yacaba.Domain.Models {

    public class Organisation {

        public Int64 Id { get; set; } = default!;

        public required String Name { get; set; }

        public String? Image { get; set; }
        public String? Contact { get; set; }
        public Boolean IsOffical { get; set; }

        public ICollection<Gym> Gyms { get; set; } = [];

    }
}
