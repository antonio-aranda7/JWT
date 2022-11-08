using CompanyEmployees.Contracts;
using Entities.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using static Humanizer.In;

namespace CompanyEmployees.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(DutchContext dutchContext)
            : base(dutchContext)
        {
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(companie => companie.Name)
                .ToList();
        }

        /*public Company GetCompanyById(int Id)
        {
            return FindByCondition(company => company.Id.Equals(Id))
                    .FirstOrDefault();
        }*/

    }
}
