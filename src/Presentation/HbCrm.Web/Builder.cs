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
using HbCrm.Data;
using Microsoft.Extensions.Configuration;
using HbCrm.Core.Data;
using HbCrm.Services.Admin;
using Microsoft.Extensions.DependencyInjection.Extensions;
using HbCrm.Core.Configuration;
using HbCrm.Core.Domain.Authorize;
using HbCrm.Services.Authorize;
using Microsoft.AspNetCore.Authorization;
using HbCrm.Services.Web;
using EasyCaching.Core;
using EasyCaching.InMemory;
using HbCrm.Core.Caching;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using FluentValidation.AspNetCore;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using FluentValidation;
using AutoMapper;

namespace HbCrm.Web
{
    public static class Builder
    {
        public static void UseHbCrm(this IServiceCollection services, IHostingEnvironment env, IConfiguration config)
        {
            services.AddEasyCaching(option => option.UseInMemory("hbcrm_memory_cache"));
            services.AddScoped<ICacheManager, MemoryCacheManager>();//封装一层，缓存用的是EasyCaching
            services.AddScoped<IDbContext, HbCrmContext>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            HbCrmConfiguration hbCrmConfiguration = config.GetSection("HbCrmConfiguration").Get<HbCrmConfiguration>();
            services.TryAddSingleton<HbCrmConfiguration>(hbCrmConfiguration);

            services.AddDbContext<HbCrmContext>((provider, option) =>
            {
                //option.UseLazyLoadingProxies();
                DatabaseOption databaseOption = provider.GetRequiredService<HbCrmConfiguration>().DatabaseOption;
                switch (databaseOption.DbType)
                {
                    case DbTypes.MsSql:
                        option.UseSqlServer(databaseOption.ConnectionString);
                        break;
                    case DbTypes.MySql:
                        option.UseMySql(databaseOption.ConnectionString);
                        break;
                    default:
                        throw new NotSupportedException("The database type " + databaseOption.DbType + " no support!");

                }
            });


            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddScoped<IFunctionService, FunctionService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IWorkContext, WorkContext>();
            services.AddScoped<IAuthenticationService, CookieAuthenticationService>();
            services.AddScoped<IPermissionService, PermissionService>();

            services.AddAuthentication(HbCrmAuthenticationDefaults.AdminAuthenticationScheme)
                   .AddCookie(HbCrmAuthenticationDefaults.AdminAuthenticationScheme, option =>
                   {
                       option.LoginPath = HbCrmAuthenticationDefaults.LoginPath;
                       option.AccessDeniedPath = HbCrmAuthenticationDefaults.AccessDeniedPath;
                       option.Cookie.Name = HbCrmAuthenticationDefaults.AdminAuthenticationScheme;
                   })
                   .AddCookie(HbCrmAuthenticationDefaults.CustomerAuthenticationScheme, option =>
                   {
                       option.LoginPath = HbCrmAuthenticationDefaults.SigninPath;
                   });

            services.AddAuthorization(options =>
                PermissionKeys.AllPermissions.ForEach(keys =>
                    options.AddPolicy(keys.Name, policy => policy.Requirements.Add(new AdminAuthorizationRequirement { Policy=keys.Name}))
            ));

            services.AddSingleton<IAuthorizationHandler, AdminAuthorizationHandler>();

            var mvcBuilder = services.AddMvc();
            mvcBuilder.AddFluentValidation(option =>
                {
                    //https://docs.microsoft.com/zh-cn/aspnet/core/mvc/advanced/app-parts?view=aspnetcore-2.2
                    var assemblies = mvcBuilder.PartManager.ApplicationParts
                     .OfType<AssemblyPart>()
                     .Where(part => part.Name.StartsWith("HbCrm", StringComparison.InvariantCultureIgnoreCase))
                     .Select(part => part.Assembly);
                    option.RegisterValidatorsFromAssemblies(assemblies);//从程序集中加载继承AbstractValidator<T> 的 public类，且是非 abstract的
                    option.RunDefaultMvcValidationAfterFluentValidationExecutes = false;//去掉默认验证，只运行FluentValidation验证
                    option.ImplicitlyValidateChildProperties = true;//支持子属性是类的验证      
                });
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure; //开启全局设置，第一个验证失败立即停止验证

            services.AddAutoMapper(typeof(Builder));
        }

        public static void UseHbCrm(this IApplicationBuilder app, IHostingEnvironment env, IConfiguration config)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/page{0}.html"); //在根目录编辑各种状态码的错误页https://localhost:5001/page404.html
            }

            app.UseAuthentication();
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
