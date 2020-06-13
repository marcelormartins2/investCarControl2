using InvestCarControl.Areas.Identity;
using InvestCarControl.Data;
using InvestCarControl.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace InvestCarControl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(c =>
            {
                            c.AccessDeniedPath = "/Identity/Account/AccessDenied";
                            c.Cookie.Name = "IvestCarControl";
                            c.Cookie.HttpOnly = true;
                            c.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                            c.SlidingExpiration = true;
                            c.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                            c.LoginPath = "/Identity/Account/Login";
                            c.LogoutPath = "/Identity/Account/Logout";
            });

            services.AddDefaultIdentity<Parceiro>(opt =>
            {

                            // User Config
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                            // Lockout Config
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 4;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(4);

                            // SignIn Config
                            opt.SignIn.RequireConfirmedPhoneNumber = false;
                opt.SignIn.RequireConfirmedAccount = false;
                opt.SignIn.RequireConfirmedEmail = false;  // Pay Attention

                            // Password Config          // Todo: Quer que a senha padrão seja Admin@123
                            opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequiredUniqueChars = 2;


            }).AddRoles<IdentityRole>()
                //.AddDefaultUI()
                .AddEntityFrameworkStores<IdentyDbContext>();
            services.AddControllers(config =>
            {
                // using Microsoft.AspNetCore.Mvc.Authorization;
                // using Microsoft.AspNetCore.Authorization;
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //services.AddDbContext<IdentyDbContext>(options =>
            //    options.UseMySql(Configuration.GetConnectionString("IdentyDbContext"), builder =>
            //        builder.MigrationsAssembly("InvestCarControl")));

            services.AddDbContext<IdentyDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("IdentyDbContext"), builder =>
                    builder.MigrationsAssembly("InvestCarControl")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IdentyDbContext context,
            UserManager<Parceiro> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //UsuarioRoleDefault.Seed(context, userManager, roleManager).Wait();

            app.UseEndpoints(endpoints =>
            {
                //routes.MapRoute("modulos","Prontuario/{controller=Home}/{action=Index}/{id?}");
                //routes.MapRoute("pacientes","{controller=Home}/{action=Index}/{id}/{paciente}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

            });

            //app.UseMvc(routes =>
            //{
            //    //routes.MapRoute("modulos","Prontuario/{controller=Home}/{action=Index}/{id?}");
            //    //routes.MapRoute("pacientes","{controller=Home}/{action=Index}/{id}/{paciente}");

            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
