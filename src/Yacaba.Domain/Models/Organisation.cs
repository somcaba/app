namespace Yacaba.Domain.Models {

    public class Organisation {

        public Int64 Id { get; set; } = default!;
        
        public String Name { get; set; } = default!;
        public String? Image { get; set; }
        public Boolean IsOffical { get; set; }

    }
}
