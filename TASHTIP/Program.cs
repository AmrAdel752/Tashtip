 
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Employee;
using TASHTIP.EF.UsersRolePolicy.Seeds;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();


            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("MetersConnection");

            try
            {

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await DefaultRoles.SeedRoleAsync(roleManager);
                await DefaultUsers.SeedUserAdminAsync(userManager);

                logger.LogInformation("Data Information");
                logger.LogInformation("Application Started");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "error acured seeds data");
            }

            host.Run();


        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
        //            webBuilder.UseWebRoot("wwwroot");
        //            webBuilder.UseStartup<Startup>();

        //        });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                  webBuilder.UseWebRoot("wwwroot");
                  webBuilder.UseStartup<Startup>();

              });
    }
}
