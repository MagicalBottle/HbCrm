using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using HbCrm.Core.Domain.Admin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using HbCrm.Services.Admin;
using System.Linq;

namespace HbCrm.Services.Authentication
{
    public class CookieAuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminService _adminService;

        private HbCrm.Core.Domain.Admin.SysAdmin _cachedAdmin;

        public CookieAuthenticationService(IHttpContextAccessor httpContextAccessor, IAdminService adminService)
        {
            _httpContextAccessor = httpContextAccessor;
            _adminService = adminService;
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
            };
            await _httpContextAccessor.HttpContext.SignInAsync(HbCrmAuthenticationDefaults.AdminAuthenticationScheme, principal, properties);

            _cachedAdmin = admin;
        }

        /// <summary>
        /// 登出
        /// </summary>
        public async void SignOut()
        {
            _cachedAdmin = null;
            await _httpContextAccessor.HttpContext.SignOutAsync(HbCrmAuthenticationDefaults.AdminAuthenticationScheme);
        }

        /// <summary>
        /// 获取身份验证的账号
        /// </summary>
        /// <returns></returns>
        public HbCrm.Core.Domain.Admin.SysAdmin GetAuthenticatedAdmin()
        {
            if (_cachedAdmin != null)
            {
                return _cachedAdmin;
            }

            AuthenticateResult authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(HbCrmAuthenticationDefaults.AdminAuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
            {
                return null;
            }


            HbCrm.Core.Domain.Admin.SysAdmin admin = null;
            Claim adminClaim = authenticateResult.Principal.FindFirst(
                claim => claim.Type == ClaimTypes.Name
             && claim.Issuer == HbCrmAuthenticationDefaults.ClaimsIssuer);

            if (adminClaim != null)
            {
                admin = _adminService.GetAdminByUserName(adminClaim.Value);
            }

            if (admin == null)
            {
                return null;
            }
            _cachedAdmin = admin;
            return admin;

        }

    }
}
