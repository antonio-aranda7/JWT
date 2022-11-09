using Entities.Dtos;
using Entities;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System;
using Data;
using System.Linq;
using Entities.Models;
using Humanizer.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Business
{
    public class AuthenticationBL : IAuthenticationBL
    {
        private readonly DutchContext _ctx;
        private readonly IConfiguration _configuration;
        private readonly string? secretKey;

        public AuthenticationBL(IConfiguration config, DutchContext ctx)
        {
            secretKey = config.GetSection("JWT").GetSection("securityKey").ToString();
            _ctx = ctx;
            _configuration = config;
        }

        public bool UserLogin(String email, String key)
        {

            var user = _ctx.Users
                  .Where(u => u.Email == email && u.Password == key)
                  .FirstOrDefault();

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Response NewUser(User user)
        {
            try
            {
                _ctx.Users.Add(user);
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

        public List<Claim> GetClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                //new Claim(ClaimTypes.Role, "Manager"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

        };

            return claims;
        }


        public Response Validate(User user)
        {
            var results = UserLogin(user.Email, user.Password);

            if (results)
            {

                var keyBytes = Encoding.ASCII.GetBytes(secretKey/*_configuration.GetSection("JWT:securityKey").Value*/);
                var claims = new ClaimsIdentity();      
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));
                //claims.AddClaim( new Claim(ClaimTypes.Role, "Manager"));
                //claims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);


                return new Response
                {
                    Code = StatusCodes.Status200OK,
                    Message = "TokenCreado",
                    Token = tokencreado
                };

            }
            else
            {
                return new Response
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Sin Permiso",
                    Token = "Sin Permiso"
                };
            }
        }
    }
}
