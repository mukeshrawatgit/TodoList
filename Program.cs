using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data;

namespace TodoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args)
            //   .ConfigureAppConfiguration((hc, config) =>
            //   {
            //       var env = hc.HostingEnvironment;
            //       config.SetBasePath(Directory.GetCurrentDirectory())
            //           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //           .AddEnvironmentVariables();
            //   })
            //   .ConfigureLogging((hc, logging) =>
            //   {
            //       logging.AddConfiguration(hc.Configuration.GetSection("Logging"));
            //       logging.AddConsole();
            //       logging.AddDebug();
            //       logging.AddEventSourceLogger();
            //   })
             .Build();
            InitializeDatabase(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private static void InitializeDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    SeedData.InitializeAsync(services).Wait();
                }
                catch (Exception e)
                {
                   
                }
            }
        }
    }


}
