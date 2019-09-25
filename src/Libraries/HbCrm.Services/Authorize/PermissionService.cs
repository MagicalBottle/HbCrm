using HbCrm.Core.Data;
using HbCrm.Services.Web;
using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Authorize;
using System.Linq;
using HbCrm.Core.Caching;
using HbCrm.Core.Domain.Admin;

namespace HbCrm.Services.Authorize
{
    public class PermissionService : IPermissionService
    {
        private readonly IWorkContext _workContext;
        private readonly IFunctionService _functionService;
        private readonly ICacheManager _cache;
        
        public PermissionService(IWorkContext workContext,
            IFunctionService functionService,
            ICacheManager cache)
        {
            _workContext = workContext;
            _functionService= functionService;
            _cache = cache;
        }

        /// <summary>
        /// 判定权限
        /// </summary>
        /// <param name="functionSystermName">权限名称</param>
        /// <returns>true 有此权限；false 无此权限</returns>
        public bool Authorize(string functionSystermName)
        {
            if (string.IsNullOrWhiteSpace(functionSystermName))
            {
                return false;
            }
           return this.Authorize(functionSystermName, _workContext.Admin);
        }
        /// <summary>
        ///  判定权限
        /// </summary>
        /// <param name="functionSystermName">权限名称</param>
        /// <param name="admin">当前用户</param>
        /// <returns>true 有此权限；false 无此权限</returns>
        public bool Authorize(string functionSystermName, SysAdmin admin)
        {
            if (string.IsNullOrWhiteSpace(functionSystermName))
            {
                return false;
            }
            foreach (var f in admin.Functions)
            {
                if (functionSystermName.Equals(f.FunctionSystermName,StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false ;
        }
        
    }
}
