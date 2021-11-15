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

namespace TodoList.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;



        public AccountController( UserManager<User> userManager, SignInManager<User> signInManager,
            IConfiguration config)
        {
          
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        //Method Used to register dummy users.
        [HttpPost()]
        public async Task<ActionResult<bool>> Register()
        {


            var testUser1 = new TodoList.Model.User
            {
                Email = "test",
                UserName = "test",

            };

            var testUser2 = new TodoList.Model.User
            {
                Email = "test1",
                UserName = "test1",

            };


           var checkuser1= await _userManager.FindByNameAsync("test");
            if (checkuser1 == null)
            {
                await _userManager.CreateAsync(testUser1, "pwd123");
            }
            var checkuser2 = await _userManager.FindByNameAsync("test1");
            if (checkuser2 == null)
            {
                await _userManager.CreateAsync(testUser2, "pwd1231");
            }

            return Ok();


        }


        //Login functionality
        [HttpPost()]
        public async Task<ActionResult<string>> Login(Models.LoginUser iuser)
        {
                    //sign in
            var signInResult = await _signInManager.PasswordSignInAsync(iuser.User, iuser.Password, true, false);

            if (signInResult.Succeeded)
            {
                var secretKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(_config.GetSection("Authentication:JWT:SecurityKey").Value));             
                var signInCreds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer:_config.GetSection("Authentication:JWT:Issuer").Value,
                    audience:_config.GetSection("Authentication:JWT:Audience").Value,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signInCreds
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }

    }
}
