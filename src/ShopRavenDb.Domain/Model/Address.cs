namespace ShopRavenDb.Domain.Model
{
    public class Address
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }
    }
}
