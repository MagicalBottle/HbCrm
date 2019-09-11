using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HbCrm.Services.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using HbCrm.Data.Mapping;
using HbCrm.Data;
using Microsoft.Extensions.Configuration;
using HbCrm.Core.Data;
using HbCrm.Services.Admin;
using HbCrm.Core.Data;
using HbCrm.Data;

namespace HbCrm.Web
{
    public static class Builder
    {
        public static void UseHbCrm(this IServiceCollection services, IHostingEnvironment env, IConfiguration config)
        {
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbContext, HbCrmContext>();
            services.AddSingleton<DatabaseOption>(config.GetSection("Database").Get<DatabaseOption>());
            services.AddDbContext<HbCrmContext>((provider, option) =>
            {
                DatabaseOption databaseOption = provider.GetRequiredService<DatabaseOption>();
                switch (databaseOption.DbType)
                {
                    case DbTypes.MsSql:
                        option.UseSqlServer(databaseOption.ConnectionString);
                        break;
                    case DbTypes.MySql:
                        option.UseMySql(databaseOption.ConnectionString);
                        break;
                    default:
                        throw new NotSupportedException("The database type "+databaseOption.DbType+" no support!");
                        
                }
            });
            services.AddAuthentication(HbCrmAuthenticationDefaults.AdminAuthenticationScheme)
                   .AddCookie(HbCrmAuthenticationDefaults.AdminAuthenticationScheme, option =>
                   {
                       option.LoginPath = HbCrmAuthenticationDefaults.LoginPath;
                       option.AccessDeniedPath = HbCrmAuthenticationDefaults.AccessDeniedPath;
                   })
                   .AddCookie(HbCrmAuthenticationDefaults.CustomerAuthenticationScheme, option =>
                   {
                       option.LoginPath = HbCrmAuthenticationDefaults.SigninPath;
                   });
            services.AddMvc();

        }

        public static void UseHbCrm(this IApplicationBuilder app, IHostingEnvironment env, IConfiguration config)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "Admin",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
