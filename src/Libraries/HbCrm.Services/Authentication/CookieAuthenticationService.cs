using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using HbCrm.Core.Domain.Admin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using HbCrm.Services.Admin;
using System.Linq;
using HbCrm.Core.Caching;
using HbCrm.Services.Web;

namespace HbCrm.Services.Authentication
{
    public class CookieAuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminService _adminService;
        private readonly ICacheManager _cache;
        private readonly IWorkContext _workContext;

        public CookieAuthenticationService(IHttpContextAccessor httpContextAccessor,
            IAdminService adminService,
            ICacheManager cache,
            IWorkContext workContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _adminService = adminService;
            _cache = cache;
            _workContext = workContext;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="admin">登录的账号</param>
        /// <param name="isPersistent">登录信息持久化到客户端，true 是，false 否</param>
        public async void SignIn(Core.Domain.Admin.SysAdmin admin, bool isPersistent)
        {

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, admin.UserName, ClaimValueTypes.String, HbCrmAuthenticationDefaults.ClaimsIssuer));
            ClaimsIdentity identity = new ClaimsIdentity(claims, HbCrmAuthenticationDefaults.AdminAuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationProperties properties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
                //ExpiresUtc = DateTimeOffset.MaxValue 最大值 。默认值14天
            };
            await _httpContextAccessor.HttpContext.SignInAsync(HbCrmAuthenticationDefaults.AdminAuthenticationScheme, principal, properties);

            _workContext.Admin = admin;
        }

        /// <summary>
        /// 登出
        /// </summary>
        public async void SignOut()
        {
            //移除缓存
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null && httpContext.User.Identity.IsAuthenticated && !string.IsNullOrWhiteSpace(httpContext.User.Identity.Name))
            {
                Claim adminClaim = httpContext.User.FindFirst(
                         claim => claim.Type == ClaimTypes.Name
                      && claim.Issuer == HbCrmAuthenticationDefaults.ClaimsIssuer);

                if (adminClaim != null)
                {
                    string key = string.Format(HbCrmCachingDefaults.AdminUserNameCacheKey, adminClaim.Value);                    
                    _cache.Remove(key);
                }

            }
            await httpContext.SignOutAsync(HbCrmAuthenticationDefaults.AdminAuthenticationScheme);
        }

        /// <summary>
        /// 获取身份验证的账号
        /// </summary>
        /// <returns></returns>
        public HbCrm.Core.Domain.Admin.SysAdmin GetAuthenticatedAdmin()
        {
            return _workContext.Admin;
        }

    }
}
