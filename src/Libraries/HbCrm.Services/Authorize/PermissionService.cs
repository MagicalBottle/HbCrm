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
        private readonly IRepository<SysFunction> _functionRepository;
        private readonly IRepository<SysFunctionRole> _functionRoleRepository;
        private readonly ICacheManager _cache;
        
        public PermissionService(IWorkContext workContext,
            IRepository<SysFunction> functionRepository,
            IRepository<SysFunctionRole> functionRoleRepository,
            ICacheManager cache)
        {
            _workContext = workContext;
            _functionRepository = functionRepository;
            _functionRoleRepository = functionRoleRepository;
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
            foreach (var role in admin.AdminRoles)
            {
                if (Authorize(functionSystermName, role.RoleId))
                {
                    return true;
                }
            }
            return false ;
        }

        /// <summary>
        /// 判定权限
        /// </summary>
        /// <param name="functionSystermName">权限名称</param>
        /// <param name="roleId">角色ID</param>
        /// <returns>true 有此权限；false 无此权限</returns>
        protected bool Authorize(string functionSystermName, int roleId)
        {            
            string key = string.Format(HbCrmCachingDefaults.PermissionsRoleIdCacheKey, roleId);
            var functions = _cache.Get(key, () => 
            {
                return GetFunctionsByRoleId(roleId);
            });

            if (functions == null||functions.Count<=0)
            {
                return false;
            }

            foreach (var f in functions)
            {
                if (functionSystermName.Equals(f.FunctionSystermName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>角色所拥有的权限集合</returns>
        protected virtual List<SysFunction> GetFunctionsByRoleId(int roleId)
        {
                var query = from f in _functionRepository.Table
                            join fr in _functionRoleRepository.Table on f.Id equals fr.FunctionId
                            where fr.RoleId == roleId
                            orderby fr.Id
                            select f;

                return query.ToList();
            
        }
    }
}
