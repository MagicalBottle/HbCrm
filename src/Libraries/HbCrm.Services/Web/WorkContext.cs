using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Services.Admin;
using HbCrm.Core.Domain.Admin;
using System.Security.Claims;
using HbCrm.Services.Authentication;
using HbCrm.Core.Caching;

namespace HbCrm.Services.Web
{
    public class WorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminService _adminService;
        private readonly ICacheManager _cache;
        private SysAdmin _cachedAdmin;

        public WorkContext(IHttpContextAccessor httpContextAccessor,
            IAdminService adminService,
            ICacheManager cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _adminService = adminService;
            _cache = cache;
        }
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public SysAdmin Admin
        {
            get
            {
                if (_cachedAdmin != null)
                {
                    return _cachedAdmin;
                }


                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null && httpContext.User.Identity.IsAuthenticated && !string.IsNullOrWhiteSpace(httpContext.User.Identity.Name))
                {
                    Claim adminClaim = httpContext.User.FindFirst(
                             claim => claim.Type == ClaimTypes.Name
                          && claim.Issuer == HbCrmAuthenticationDefaults.ClaimsIssuer);

                    if (adminClaim != null)
                    {

                        string key = string.Format(HbCrmCachingDefaults.AdminUserNameCacheKey, adminClaim.Value);
                        _cachedAdmin= _cache.Get<SysAdmin>(key, () =>
                        {
                            return _adminService.GetAdminAllInforByUserName(adminClaim.Value);
                        });
                    }
                    return _cachedAdmin;

                }
                return null;

            }
            set
            {
                if (value != null)
                {
                    //值不为空的时候，缓存
                    string key = string.Format(HbCrmCachingDefaults.AdminUserNameCacheKey, value.UserName);
                    _cache.Set<SysAdmin>(key, value);
                }
                _cachedAdmin = value;
            }
        }

        public HttpContext HttpContext
        {
            get {
                return _httpContextAccessor.HttpContext;
            }
        }
    }
}
