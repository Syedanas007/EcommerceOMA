using CompanyService.Models;

namespace CompanyService.Factory
{
    public class CompanyFactory : ICompanyFactory
    {
        public Company CreateCompany(string name, string streetAddress, string city, string state, string postalAddress, string zip, string contactNumber)
        {
            return new Company
            {
                Id = Guid.NewGuid(),
                Name = name,
                StreetAddress = streetAddress,
                City = city,
                State = state,
                PostalAddress = postalAddress,
                Zip = zip,
                ContactNumber = contactNumber
            };
        }
    }
}
