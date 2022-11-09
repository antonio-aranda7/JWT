using CompanyEmployees.Contracts;
using Entities.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using static Humanizer.In;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(DutchContext dutchContext)
            : base(dutchContext)
        {
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return FindAll()
                .OrderBy(companie => companie.Id)
                .ToList();
        }

        public Company GetCompanyById(int Id)
        {
            var company = FindByCondition(company => company.Id.Equals(Id)).FirstOrDefault();

            return company;
        }

        public void NewCompany(Company company)
        {
            Create(company);
        }

        public void UpdateCompany(Company company)
        {
            Update(company);
        }

        public void DeleteCompany(int id)
        {
            var company = GetCompanyById(id);

            Delete(company);
        }
    }
}
