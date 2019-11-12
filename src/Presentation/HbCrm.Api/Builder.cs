using HbCrm.Api.Configuration;
using HbCrm.Api.Services.Authorize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace HbCrm.Api
{
    public static class Builder
    {
        public static void AddHbCrmApi(this IServiceCollection services, IHostingEnvironment env, IConfiguration config)
        {

            HbCrmApiConfiguration hbCrmApiConfiguration = config.GetSection("HbCrmApiConfiguration").Get<HbCrmApiConfiguration>();
            services.TryAddSingleton<HbCrmApiConfiguration>(hbCrmApiConfiguration);

            #region JWT
            Jwt jwt = hbCrmApiConfiguration.Jwt;
            //源码分析https://www.cnblogs.com/lex-wu/p/10512424.html
            // https://github.com/aspnet/Security/issues/1310
            //就是身份验证失败也会继续下一个中间件执行，所以通常不需要重写这里的方法
            //那么只能放到权限验证中判断
            //401等不正常访问处理 http://www.voidcn.com/article/p-ckgqcgds-btn.html
            //https://blog.csdn.net/jasonsong2008/article/details/89226705
            //16，JwtBearerEvents 中的OnAuthenticationFailed 设置 StatusCode得到Kestrell抛出StatusCode cannot be set because the response has already started 异常。
            //这也是因为 [Authority] 属性会调用Challenge，那里会设置statuscode，和 error head。 所以从返回的header中读取错误信息即可。OnAuthenticationFailed应该永远不使用。
            //https://www.jianshu.com/p/f9d9d51b489b
            //http://www.voidcn.com/article/p-ckgqcgds-btn.html  UseStatusCodePages
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = jwt.AuthenticateScheme;
                options.DefaultChallengeScheme = jwt.AuthenticateScheme;

            }).AddJwtBearer(jwt.AuthenticateScheme,
                  (jwtBearerOptions) =>
                  {
                      //jwtBearerOptions.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
                      //{
                      //    //此处为权限验证失败后（401）触发的事件，因为只处理401，所以不在这里处理，用UseStatusCodePages通已处理非200的
                      //    OnChallenge = (context) =>
                      //    {
                      //        var c = context;
                      //        return Task.CompletedTask;
                      //    },
                      //    ////此处为出现异常后触发的事件
                      //    OnAuthenticationFailed = (context) =>
                      //    {
                      //        var c = context;
                      //        return Task.CompletedTask;
                      //    }
                      //};
                      jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwt.Key)),//秘钥
                          ValidateIssuer = true,
                          ValidIssuer = jwt.Issuer
                          //ValidateAudience = true,
                          //ValidAudience = "",
                          //ValidateLifetime = true,
                          //ClockSkew = TimeSpan.FromMinutes(5)                      
                      };
                  });
            #endregion

            services.AddSingleton<IAuthorizationHandler, ApiAuthorizationHandler>();
            services.AddAuthorization(options =>
               options.AddPolicy("Something",
               policy => policy.RequireClaim("Permission", "CanViewPage", "CanViewAnything")));

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

            app.UseStatusCodePages((context) =>
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Clear();
                    response.WriteAsync("Status code page, status code: " +context.HttpContext.Response.StatusCode);
                }
                return Task.CompletedTask;
            });

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
