namespace Yacaba.Domain.Models {
    public class Address {

        public String Line1 { get; set; } = default!;
        public String? Line2 { get; set; } = default!;
        public String? Line3 { get; set; } = default!;

        public String PostalCode { get; set; } = default!;
        public String Locality { get; set; } = default!;
        public String CountryCode { get; set; } = default!;

    }
}
