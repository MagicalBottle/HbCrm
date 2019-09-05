using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HbCrm.Services.Authentication;
using Microsoft.AspNetCore.Http;

namespace HbCrm.Web
{
    public static class Builder
    {
        public static void UseHbCrm(this IServiceCollection services, IHostingEnvironment env)
        {
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

        public static void UseHbCrm(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
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
