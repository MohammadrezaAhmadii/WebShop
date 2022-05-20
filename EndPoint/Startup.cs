using Application.Interfaces.Context;
using Application.Interfaces.FacadPattern;
using Application.Services.Categories.Commands.AddNewCategory;
using Application.Services.Categories.Commands.RemoveCategory;
using Application.Services.Categories.FacadPattern;
using Application.Services.Categories.Queries.GetAllCategory;
using Application.Services.Categories.Queries.GetCategories;
using Application.Services.Categories.Queries.GetMenuItem;
using Application.Services.Products.Queries.GetProductSiteById;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint
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
            services.AddScoped<IGetCategories, GetCategories>();
            services.AddScoped<IGetProductSiteById, GetProductSiteById>();
            services.AddScoped<IGetAllCategory, GetAllCategory>();
            services.AddScoped<IDataBaseContext, DataBaseContext>();
            services.AddScoped<IAddNewCategory, AddNewCategory>();
            services.AddScoped<IRemoveCategory, RemoveCategory>();
            services.AddScoped<IFacadPattern, ProductFacad>();
            services.AddScoped<IGetMenuItem, GetMenuItem>();
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(
            Configuration.GetConnectionString("TestWebDbContext")));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
