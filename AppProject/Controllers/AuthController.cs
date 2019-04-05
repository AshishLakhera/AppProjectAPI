using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppProject.Model;
using AppProject.Repositories;
using ClassLibrary1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AppProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Entities _db;
        public AuthController(Entities Db)
        {
            _db = Db;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            LoginRepositories model = new LoginRepositories(_db);
            bool IsValidUser= model.ValidateUser(user);
            if (IsValidUser)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@3456"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserEmail),
          //  new Claim(ClaimTypes.Role, "Manager")
        };
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:4200",
                    audience: "http://localhost:4200",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}