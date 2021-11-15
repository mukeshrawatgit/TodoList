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
        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = config["Authentication:JWT:Issuer"],
                        ValidAudience = config["Authentication:JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["Authentication:JWT:SecurityKey"]))
                    };
                });
            services.AddAuthorization();

        }

        /// <summary>
        /// Configures identity services.
        /// </summary>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(config =>
            {
                // User defined password policy settings.
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
              .AddEntityFrameworkStores<TodoContext>()
              .AddDefaultTokenProviders();



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
