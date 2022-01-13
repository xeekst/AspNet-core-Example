using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ANC_Authorize_Policy
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Home/NotPermission");
                options.LoginPath = new PathString("/Home/Login");
                options.ExpireTimeSpan = TimeSpan.FromSeconds(10);
            });
            services.AddAuthorization(options =>
            {
                //声明一个名为Administrator的声明，判断角色的claimType为Role的值是否equal Administrator、SuperAdministrator
                options.AddPolicy("AdministratorOnly", policy => policy.RequireClaim("Role", "Administrator", "SuperAdministrator"));
                //使用非equal逻辑的声明策略 —— 声明一个生日日期大于某个日期的策略
                options.AddPolicy("DateOfBirthMoreThan2021", policy => policy.RequireAssertion(context => context.User.HasClaim(c =>
                    c.Type == ClaimTypes.DateOfBirth && DateTime.Parse(c.Value) > DateTime.Parse("2021-01-15"))));
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
