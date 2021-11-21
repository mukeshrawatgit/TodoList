using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data.Contexts;
using TodoList.Data.Interfaces;
using TodoList.Data.Repository;
using TodoList.Model;
using TodoList.Service.Interfaces;
using TodoList.Service.Services;

namespace TodoList.API.Extensions
{
    public static class ServiceExtensions
    {

        /// <summary>
        /// Configures JSON Web Token authentication scheme.
        /// </summary>
        public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {

          //  var secret = config.GetSection("Authentication:JWT:").GetSection("SecurityKey").Value;

           // var key = Encoding.ASCII.GetBytes(config["Authentication:JWT:SecurityKey"]);
            var secret = config["Authentication:JWT:SecurityKey"];

            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                     ValidIssuer = "https://localhost:27667/",
                    ValidAudience = "https://localhost:27667/"
                };
            });

            return services;
         

        }

       

        /// <summary>
        /// Configures repository and service.
        /// </summary>
        public static void ConfigureRepository(this IServiceCollection services)
        {
          services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<ITodoItemService, TodoItemService>();
           
        }
    }
}
