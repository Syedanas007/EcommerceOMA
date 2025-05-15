using CompanyService.Models;

namespace CompanyService.Factory
{
    public interface ICompanyFactory
    {
        Company CreateCompany(string name, string streetAddress, string city, string state, string postalAddress, string zip, string contactNumber);
    }
}
