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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies")]
    //[Authorize/*(Roles = "Manager")*/]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet/*, Authorize*/]
        public IActionResult AllCompanies()
        {
            try
            {
                var claims = User.Claims;
                var company= _repository.Company.GetAllCompanies();
                //var companiesDto = _mapper.Map<Company>(company);
                //var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(company);

                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCompanyById(int id)
        {
            try
            {
                var company = _repository.Company.GetCompanyById(id);
                //var companiesDto = _mapper.Map<Company>(company);
                //var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(company);

                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult NewCompany(Company company)
        {
            try
            {
                _repository.Company.NewCompany(company);
                _repository.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateCompanies(Company company)
        {
            try
            {
                _repository.Company.UpdateCompany(company);
                _repository.Save();

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompanies(int id)
        {
            try
            {
                _repository.Company.DeleteCompany(id);
                _repository.Save();

                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }
    }
}