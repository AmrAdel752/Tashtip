 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Claims;
using TASHTIP.EF.Entities.Employee;
using TASHTIP.EF.UsersRolePolicy;
using TASHTIP.InfraDB;
using TASHTIP.InfraDB.ContextDB;
using TASHTIP.RepoUOWCore.Services;
using Web.Models.Filter;

namespace Web
{
    public class Startup
    { 
        public Startup(IConfiguration configuration )
        {
            Configuration = configuration; 
            
        }
        

        public IConfiguration Configuration { get; }
          
        
        public void ConfigureServices(IServiceCollection services) 
        {


            #region Dependency Injection (Interface Repo)

           // services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddAuthorization(options =>
            {
                foreach (string permission in Permissions.GenerateAllPermissions())
                {
                    options.AddPolicy(permission, policy =>
                        policy.RequireClaim("Permission", permission));
                    options.AddPolicy($"No.{permission}", policy =>
                        policy.RequireClaim("NoPermission", permission));
                }
            });
            #endregion

            #region all connection DataBase

            services.AddDbContext<EmployeeDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TASHTIPConnection")) ;
            });

            services.AddDbContext<GeneralDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TASHTIPConnection"));
            });

            #endregion

            #region Proccess Special By Identity , User , Login , Register , Logout

            ////////////     services.AddAuthentication()
            ////////////.AddCookie(options =>
            ////////////{
            ////////////    options.LoginPath = "/Account/Login/";
            ////////////    options.AccessDeniedPath = "/Home/Home/";
            ////////////});

            //services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            //{
            //    // user setting
            //    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    options.User.RequireUniqueEmail = false;

            //    options.SignIn.RequireConfirmedAccount = false;
            //    options.SignIn.RequireConfirmedEmail = false;

            //    // password setting 
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireDigit = false;
            //    options.Password.RequiredUniqueChars = 0;

            //})
            //.AddEntityFrameworkStores<EmployeeDBContext>()
            //.AddDefaultTokenProviders()
            //.AddDefaultUI();

            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            //services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    // user setting
            //    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    options.User.RequireUniqueEmail = false;

            //    options.SignIn.RequireConfirmedAccount = false;
            //    options.SignIn.RequireConfirmedEmail = false;

            //    // password setting 
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireDigit = false;
            //    options.Password.RequiredUniqueChars = 0;

            //});
            #endregion

            //services.AddRazorPages().AddRazorRuntimeCompilation();
            //services.AddDatabaseDeveloperPageExceptionFilter();
            //services.Configure<SecurityStampValidatorOptions>(options => { options.ValidationInterval = TimeSpan.Zero; });
            //services.AddControllersWithViews();

            //services.AddSession(options =>
            // {
            //     options.IdleTimeout = TimeSpan.FromSeconds(10);
            //     options.Cookie.HttpOnly = true;
            //     options.Cookie.IsEssential = true;
            // });

            //services.AddMvc(); 


            #region  
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<EmployeeDBContext>()
                    .AddDefaultTokenProviders()
                    .AddDefaultUI();
            services.Configure<SecurityStampValidatorOptions>(options => { options.ValidationInterval = TimeSpan.Zero; });

            services.Configure<IdentityOptions>(options =>
            {
                // user setting
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;

                // password setting 
                //options.Password.RequireLowercase = false;
                //options.Password.RequireUppercase = false;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireDigit = false;
                //options.Password.RequiredUniqueChars = 0;

                // lockout setting
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.AllowedForNewUsers = true;
            });
             #endregion


            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.Configure<SecurityStampValidatorOptions>(options => { options.ValidationInterval = TimeSpan.Zero; });
            services.AddControllersWithViews();



            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.AddMvc();
            services.AddMvcCore();

            #region Business logic services (TASHTIP.RepoUOWCore)

            services.AddScoped<NotificationsService>();
            services.AddScoped<RequestsService>();
            services.AddScoped<GalleryService>();
            services.AddScoped<ReviewsService>();
            services.AddScoped<AppointmentsService>();
            services.AddScoped<DashboardStatsService>();

            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {  
            /*----------All Middleware Used in Application -------------*/
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }
            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<SomeHub>("/path");
            //});
            app.UseHttpsRedirection();
            app.UseStaticFiles(); 
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseResponseCaching();  
            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Home}/{id?}");
                endpoints.MapRazorPages();
            });



        }
    }
}
