namespace CompanyService.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalAddress { get; set; }
        public string Zip { get; set; }
        public string ContactNumber { get; set; }
    }
}
