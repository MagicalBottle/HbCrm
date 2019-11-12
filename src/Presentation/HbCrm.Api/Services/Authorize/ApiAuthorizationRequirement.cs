using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Api.Services.Authorize
{
    public class ApiAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Policy { get; set; }
    }
}
