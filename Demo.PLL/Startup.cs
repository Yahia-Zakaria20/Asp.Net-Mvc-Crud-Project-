using Demo.BLL.Interfaces;
using Demo.BLL.Repositrys;
using Demo.DAL.Data.Context;
using Demo.PL.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.BLL;
using Demo.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Demo.PL.Servisec.EmailServisec;
using Demo.PL.Models;



namespace Demo.PLL
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
            services.AddControllersWithViews();


         //   services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));


            services.AddTransient<IEmailSetting, EmailSetting>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<CompanyDbcontext>(option =>
            {
                option.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefultConection"));
            });

            services.AddAutoMapper(o => o.AddProfile(new MappingProfiles()));
	

			services.AddIdentity<ApplicationUser, IdentityRole>(config =>  //To config inputs in user
            {
                config.User.RequireUniqueEmail = true;    //the configration
                config.Password.RequiredLength = 5;       //the configration
                
            }).AddEntityFrameworkStores<CompanyDbcontext>() //to allowe the store services in packege 
            .AddDefaultTokenProviders(); // to generate to to some servisec ex reset password change email change phonenumber

            services.ConfigureApplicationCookie(config => //to configre the built in token (Coockies)
            {
                config.AccessDeniedPath = "/Home/Error";
                config.LoginPath = "/Account/SignIn";
                config.ExpireTimeSpan = TimeSpan.FromDays(10);
            });

            
                //.AddCookie("hamad", config =>
                //{
                //    config.LoginPath = "/Account/SignIn";
                //    config.ExpireTimeSpan = TimeSpan.FromSeconds(10);

                //}); //User Difined Schema

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
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
