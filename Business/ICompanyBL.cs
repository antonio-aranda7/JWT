using Entities.Models;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business
{
    public interface ICompanyBL
    {
        Response DeleteCompanies(int id);
        List<Company> GetCompanies();
        Company GetCompaniesById(int id);
        Response NewCompanies(Company company);
        Response UpdateCompanies(Company company);
    }
}