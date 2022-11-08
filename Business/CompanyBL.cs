using Entities.Models;
using Data;
using System.Linq;
using System;
using System.Collections.Generic;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Business
{
    public class CompanyBL : ICompanyBL
    {
        private readonly DutchContext _ctx;

        public CompanyBL(DutchContext ctx)
        {
            _ctx = ctx;
        }

        public List<Company> GetCompanies()
        {
            var result = _ctx.Companies.ToList();
            return result;
        }


        public Company GetCompaniesById(int id)
        {

            var companie = _ctx.Companies.Find(id);

            if (companie == null)
            {
                return null;
            }

            return companie;
        }



        public Response NewCompanies(Company company)
        {
            try
            {
                _ctx.Companies.Add(company);
                _ctx.SaveChanges();

                return new Response
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Created",
                    Token = ""
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Token = ""
                };
            }

        }



        public Response UpdateCompanies(Company company)
        {
            try
            {
                _ctx.Entry(company).State = EntityState.Modified;
                _ctx.SaveChanges();

                return new Response
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Updated",
                    Token = ""
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Token = ""
                };
            }

        }

        public Response DeleteCompanies(int id)
        {
            try
            {
                var companie = _ctx.Companies.Find(id);
                var result = _ctx.Companies.Remove(companie);
                _ctx.SaveChanges();

                return new Response
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Deleted",
                    Token = ""
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Token = ""
                };
            }

        }
    }
}