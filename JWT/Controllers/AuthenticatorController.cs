using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business;
using Microsoft.Extensions.Configuration;
using Data;
using Entities.Models;
using Entities.Dtos;
using System;

namespace net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatorController : ControllerBase
    {
        private readonly DutchContext _ctx;
        private readonly IConfiguration _config;

        public AuthenticatorController(IConfiguration config,DutchContext ctx)
        {
            _config = config;
            _ctx = ctx;
        }

        [HttpPost]
        [Route("Validator")]
        public IActionResult Validator([FromBody] User request)
        {

            var authenticationBL = new AuthenticationBL(_config, _ctx);
            var result = authenticationBL.Validate(request);
            return StatusCode(result.Code, new { token = result.Token });

        }


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
