using AutoMapper;
using Business;
using CompanyEmployees.Contracts;
using CompanyEmployees.Entities.DataTransferObjects;
using Entities.Models;
using Data;
using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly DutchContext _ctx;
        private readonly ILogger _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CompaniesController(DutchContext ctx, IRepositoryManager repository, IMapper mapper, ILogger logger)
        {
            _ctx = ctx;
            _repository = repository;
            _mapper = mapper;   
            _logger = logger;   
        }

        [HttpGet]
        public List<Company> GetCompanies()
        {
            try
            {
                var Comp = new CompanyBL(_ctx);
                var result = Comp.GetCompanies();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet("GetCompanies")]
        public IActionResult AllCompanies()
        {
            try
            {
                var claims = User.Claims;
                var coompanie = new Company();
               _repository.Company.Create(coompanie);
                var company= _repository.Company.GetAllCompanies(trackChanges: false);
                //var companiesDto = _mapper.Map<Company>(company);
                //var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(company);

                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/companies/5
        [HttpGet("{id}")]
        public Company GetPlanet(int id)
        {
            try
            {
                if (_ctx.Companies == null)
                {
                    return null;
                }
                var Comp = new CompanyBL(_ctx);
                var companie =  Comp.GetCompaniesById(id);
                return companie;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public Response NewCompanies(Company company)
        {
            try
            {
                var Comp = new CompanyBL(_ctx);
                var result = Comp.NewCompanies(company);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPut]
        public Response UpdateCompanies(Company company)
        {
            try
            {
                var Comp = new CompanyBL(_ctx);
                var result = Comp.UpdateCompanies(company);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpDelete("{id}")]
        public Response DeleteCompanies(int id)
        {     
                var Comp = new CompanyBL(_ctx);
                var result = Comp.DeleteCompanies(id);
                return result;                     
        }
    }
}