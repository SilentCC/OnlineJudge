using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OnlineJudgeServer.Models;
using OnlineJudgeServer.Services;
using OnlineJudgeServer.Settings;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace OnlineJudgeServer
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
            services.AddTransient<ExecuteCplusProgram>()
                .AddTransient<ExecutePythonProgram>()
                .AddTransient<ExecuteCSharpProgram>()
                .AddTransient<ExecuteJavaProgram>();
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder =>
                {
                    builder.WithOrigins("https://localhost:5001", "http://localhost:5000", "https://oj.kanghekeji.cn",
                            "http://oj.kanghekeji.cn")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            var connection =
                @"Server=localhost;Database=onlineJudge;port=3306;user=root;password=Dage_123456.;Convert Zero Datetime=True;";
            services.AddDbContext<OnlineJudgeContext>(options => options.UseMySql(connection,mysqlOptions =>
            {
                mysqlOptions.ServerVersion(new Version(5, 7, 31), ServerType.MySql); 
            }));

            services.Configure<OnlineJudgeServerSettings>(this.Configuration.GetSection("OnlineJudgeServerSettings"));

            services.AddControllers();
            services.AddControllersWithViews().AddNewtonsoftJson();
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

            app.UseRouting();
            app.UseCors();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            /*app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });*/
            
            app.UseEndpoints(endpoints=>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
