using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoList.Model;
using TodoList.Models;

namespace TodoList.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class AccountController : Controller
    {

        //private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        public List<LoginUser> userslst = new List<LoginUser>();
        private readonly string _secret;
        private readonly string _expDate;

        public AccountController(
            IConfiguration config)
        {
          
         //   _userManager = userManager;
           // _signInManager = signInManager;
            _config = config;
            _secret = config["Authentication:JWT:SecurityKey"];
            _expDate = "1440";

            var testUser1 = new LoginUser
            {
              User="test",
              Password="pwd123"

            };

            var testUser2 = new LoginUser
            {
                User = "test1",
                Password = "pwd1231"

            };
            userslst.Add(testUser2);
            userslst.Add(testUser1);
        }

      

        //Login functionality
        [HttpPost()]
        public async Task<ActionResult<string>> Login([FromBody]Models.LoginUser iuser)
        {
            //sign in
            var signInResult =  userslst.Any(x=>x.User == iuser.Username && x.Password == x.Password);

            if (signInResult)
            {

                var tokenlist = GenerateSecurityToken(iuser.Username);
                return Ok(new { Token = tokenlist });
            }

            return Unauthorized();
        }

        private string GenerateSecurityToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                })
            ,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

    }
}
