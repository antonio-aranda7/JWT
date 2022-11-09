using Entities.Models;
using CompanyEmployees.Repository;
using System;
using System.Collections.Generic;
using static Humanizer.In;

namespace CompanyEmployees.Contracts
{
    public interface ICompanyRepository //: IRepositoryBase<Company>
    {
        IEnumerable<Company> GetAllCompanies();

        Company GetCompanyById(int Id);

        //Company GetCompanyByName(string Name);

        void NewCompany(Company company);

        void UpdateCompany(Company company);

        void DeleteCompany(int id);

    }
}
