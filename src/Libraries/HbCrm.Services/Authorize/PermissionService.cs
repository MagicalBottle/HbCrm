using HbCrm.Core.Data;
using HbCrm.Services.Web;
using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Authorize;
using System.Linq;

namespace HbCrm.Services.Authorize
{
    public class PermissionService : IPermissionService
    {
        private readonly IWorkContext _workContext;
        private readonly IRepository<SysFunction> _functionRepository;
        private readonly IRepository<SysFunctionRole> _functionRoleRepository;


        public PermissionService(IWorkContext workContext,
            IRepository<SysFunction> functionRepository,
            IRepository<SysFunctionRole> functionRoleRepository)
        {
            _workContext = workContext;
            _functionRepository = functionRepository;
            _functionRoleRepository = functionRoleRepository;
        }

        /// <summary>
        /// 判定权限
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <returns>true 有此权限；false 无此权限</returns>
        public bool Authorize(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
           return this.Authorize(name, _workContext.Admin);
        }
        /// <summary>
        ///  判定权限
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <param name="admin">当前用户</param>
        /// <returns></returns>
        public bool Authorize(string name,HbCrm.Core.Domain.Admin.SysAdmin admin)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            foreach (var role in admin.AdminRoles)
            {
                if (Authorize(name, role.RoleId))
                {
                    return true;
                }
            }
            return false ;
        }

        protected bool Authorize(string name, int roleId)
        {
            var functions = GetFunctionsByRoleId(roleId);
            foreach (var f in functions)
            {
                if (f.FunctionSystermName == name)
                {
                    return true;
                }
            }
            return false;
        }

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
