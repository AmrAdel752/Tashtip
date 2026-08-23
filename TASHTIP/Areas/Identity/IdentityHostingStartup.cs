using System; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Web.Areas.Identity.IdentityHostingStartup))]
namespace Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //services.AddDbContext<DataContext>(options =>
                //options.UseSqlServer(context.Configuration.GetConnectionString("ERPConnecation")));

                //services.AddDefaultIdentity<ApplicationUser>(options =>
                //            {
                //                options.SignIn.RequireConfirmedAccount = false;
                //                options.Password.RequireLowercase = false;
                //                options.Password.RequireUppercase = false;
                //            })
                //        .AddEntityFrameworkStores<DataContext>();

 
 



            });
        }
    }
}
