using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Interface;
using DatingApp.API.ViewModels;
using DatingApp.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _service;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repository,IConfiguration config)
        {
            _service = repository;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserVM newuser)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            newuser.Username = newuser.Username.ToLower();

            if(await _service.UserExists(newuser.Username))
            {
                return BadRequest("Username already exists");
            }

            var user = new User{
                Username =newuser.Username
            };

            var createdUser = await _service.Register(user, newuser.Password);

            return StatusCode(201);
        }


        //JSON WEB TOKEN 
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginVM user)
        {
            var userservice = await _service.Login(user.Username.ToLower(), user.Password);    

            if (userservice == null)
            {
                return Unauthorized();
            }

            //if user exists ...

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userservice.ID.ToString()),
                new Claim(ClaimTypes.Name,userservice.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
         














    }
}