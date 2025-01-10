namespace Yacaba.Domain.Models {
    public class Wall {

        public Int64 Id { get; set; }

        public required String Name { get; set; }
        public required String Image { get; set; }
        public required WallType WallType { get; set; }
        public required Int32 Angle { get; set; }

        public Int64 GymId { get; set; }
        public required Gym Gym { get; set; }

    }
}
