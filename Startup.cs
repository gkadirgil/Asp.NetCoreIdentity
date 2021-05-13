using ASP.NetCoreIdentity.CustomValidation;
using ASP.NetCoreIdentity.Models;
using Microsoft.AspNetCore.Authentication;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NetCoreIdentity
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
            services.AddTransient<IAuthorizationHandler, ExpireDateExchangeHandler>();
            services.AddRazorPages();

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"]);
            });

            services.AddAuthorization(options=> 
            {
                options.AddPolicy("AnkaraPolicy",policy=>
                {
                    policy.RequireClaim("city","ankara"); // 1.Kýsýtlama
                    //policy.RequireRole("Editor");//2. Kýsýtlama=> Kullanýcýnýn city:ankara ve role:editor olmasý zorunlu
                }); //AddPolic: Kýsýtlama Oluþturulur. RequireClaim: Oluþturulan Kýsýtlamada Zorunlu istenecek Claimleri ata.

                options.AddPolicy("ViolancePolicy", policy=>
                {
                    policy.RequireClaim("violance");
                });

                options.AddPolicy("ExchangePolicy", policy =>
                 {
                     policy.AddRequirements(new ExpireDateExchangeRequirement());
                 });

            });





            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcçdefgðhýijklmnoöpqrsþtuüvwxyzABCÇDEFðGHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789-._";

                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireDigit = false;
            })
            .AddUserValidator<CustomUserValidator>()
            .AddPasswordValidator<CustomPasswordValidator>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddDefaultTokenProviders();

            CookieBuilder cookieBuilder = new CookieBuilder();

            cookieBuilder.Name = "MyBlog";
            cookieBuilder.HttpOnly = false;
            cookieBuilder.SameSite = SameSiteMode.Lax;
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;


            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/Login");
                opt.LogoutPath = new PathString("/Member/Logout");
                opt.Cookie = cookieBuilder;
                opt.SlidingExpiration = true;
                opt.ExpireTimeSpan = System.TimeSpan.FromDays(60);
                opt.AccessDeniedPath = new PathString("/Member/AccessDenied");
            });

            services.AddScoped<IClaimsTransformation, ClaimProvider.ClaimProvider>();
            services.AddMvc();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePages();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
