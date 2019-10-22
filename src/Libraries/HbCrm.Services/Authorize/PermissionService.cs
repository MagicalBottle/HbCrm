using HbCrm.Core.Data;
using HbCrm.Services.Web;
using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Authorize;
using System.Linq;
using HbCrm.Core.Caching;
using HbCrm.Core.Domain.Admin;
using HbCrm.Core.Domain.DataEnumerate;

namespace HbCrm.Services.Authorize
{
    public class PermissionService : IPermissionService
    {
        private readonly IWorkContext _workContext;
        private readonly IMenuService _menuService;
        private readonly ICacheManager _cache;

        public PermissionService(IWorkContext workContext,
            IMenuService menuService,
        ICacheManager cache)
        {
            _workContext = workContext;
            _menuService = menuService;
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
            foreach (var f in admin.Menus.Where(m=>m.MenuType== MenuType.Function))
            {
                if (functionSystermName.Equals(f.MenuSystermName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
