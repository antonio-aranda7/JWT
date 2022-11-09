using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business;
using Microsoft.Extensions.Configuration;
using Data;
using Entities.Models;
using Entities.Dtos;
using System;
using CompanyEmployees.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using CompanyEmployees.JwtFeatures;

namespace net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatorController : ControllerBase
    {
        private readonly DutchContext _ctx;
        private readonly IConfiguration _config;
        private readonly JwtHandler _jwtHandler;

        public AuthenticatorController(IConfiguration config,DutchContext ctx, JwtHandler jwtHandler)
        {
            _config = config;
            _ctx = ctx;
            _jwtHandler = jwtHandler;
        }

        [HttpPost]
        [Route("Validator")]
        /**/public IActionResult Validator([FromBody] User request)
        {

            var authenticationBL = new AuthenticationBL(_config, _ctx);
            var result = authenticationBL.Validate(request);
            //return StatusCode(result.Code, new { token = result.Token });

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = result.Token });

        }
        /*public IActionResult Login(User user)
        {
            var authenticationBL = new AuthenticationBL(_config, _ctx);
            var results = authenticationBL.UserLogin();

            var user = _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null || !_userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }*/


        [HttpPost]
        [Route("registration")]
        public Response NewUser(User user)
        {
            try
            {
                var users = new AuthenticationBL(_config, _ctx);
                var result = users.NewUser(user);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
