using HbCrm.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HbCrm.Api
{
    public static class Builder
    {
        public static void AddHbCrmApi(this IServiceCollection services, IHostingEnvironment env, IConfiguration config)
        {

            HbCrmApiConfiguration hbCrmApiConfiguration = config.GetSection("HbCrmApiConfiguration").Get<HbCrmApiConfiguration>();
            services.TryAddSingleton<HbCrmApiConfiguration>(hbCrmApiConfiguration);

            Jwt jwt = hbCrmApiConfiguration.Jwt;
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = jwt.AuthenticateScheme;
                options.DefaultChallengeScheme = jwt.AuthenticateScheme;

            }).AddJwtBearer(jwt.AuthenticateScheme,
                  (jwtBearerOptions) =>
                  {
                      jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwt.Key)),//秘钥
                          ValidateIssuer = true,
                          ValidIssuer = jwt.Issuer,
                          ValidateAudience = true,
                          ValidAudience = "",
                          ValidateLifetime = true,
                          ClockSkew = TimeSpan.FromMinutes(5)
                      };
                  });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public static void UseHbCrmApi(this IApplicationBuilder app, IHostingEnvironment env, IConfiguration config)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "default",
                   template: "api/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
