using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Services.Authorize
{
   public class AdminAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Policy { get; set; }
    }
}
