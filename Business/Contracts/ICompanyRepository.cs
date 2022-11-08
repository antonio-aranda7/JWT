using Entities.Models;
using CompanyEmployees.Repository;
using System;
using System.Collections.Generic;
using static Humanizer.In;

namespace CompanyEmployees.Contracts
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);

        //Company GetCompanyById(int Id, bool trackChanges);
    }
}
