using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.API.Controllers;
using TodoList.API.Extensions;
using TodoList.Data.Contexts;
using TodoList.Model;

namespace TodoList
{
    public class Startup
    {
       // private readonly ILogger<Startup> _logger;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //_logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            //In Memory database
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()), ServiceLifetime.Singleton, ServiceLifetime.Singleton);
           
           services.ConfigureJwtAuthentication(Configuration);
             services.ConfigureRepository();
            services.AddSwaggerGen();
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));
            // services.ConfigureIdentity();
            services.AddMvc();
           




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("ApiCorsPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoList Api v1");
            });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

       
    }
}
