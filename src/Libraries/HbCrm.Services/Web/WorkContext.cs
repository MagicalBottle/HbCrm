using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Services.Admin;
using System.Security.Claims;
using HbCrm.Services.Authentication;

namespace HbCrm.Services.Web
{
    public class WorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminService _adminService;
        private HbCrm.Core.Domain.Admin.SysAdmin _cachedAdmin;

        public WorkContext(IHttpContextAccessor httpContextAccessor, IAdminService adminService)
        {
            _httpContextAccessor = httpContextAccessor;
            _adminService = adminService;
        }
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public HbCrm.Core.Domain.Admin.SysAdmin Admin
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
                        _cachedAdmin = _adminService.GetAdminByUserName(adminClaim.Value);
                    }
                    return _cachedAdmin;

                }
                return null;

            }
            set
            {
                _cachedAdmin = value;
            }
        }
    }
}
