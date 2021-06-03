using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.CompanyManagement;
using BLL.WorkerManagement;
using DAL.Repositories.CompanyRepo;
using DAL.Repositories.WorkerRepo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.Helpers;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStringObj = Configuration.GetSection("ConnectionStrings");
            var connectionString = connectionStringObj.Get<DataBaseConnection>().DefaultConnection; 
            
            services.AddSingleton(_ => Configuration);
            services.AddTransient<IWorkerRepository, WorkerRepository>(_ => new WorkerRepository(connectionString));
            services.AddTransient<ICompanyRepository, CompanyRepository>(_ => new CompanyRepository(connectionString));

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IWorkerService, WorkerService>();
            
            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}