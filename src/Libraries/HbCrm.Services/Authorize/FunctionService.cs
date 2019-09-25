using HbCrm.Core.Data;
using HbCrm.Core.Domain.Authorize;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HbCrm.Services.Authorize
{
   public  class FunctionService: IFunctionService
    {
        private readonly IRepository<SysFunction> _functionRepository;
        private readonly IRepository<SysFunctionRole> _functionRoleRepository;

        public FunctionService(
            IRepository<SysFunction> functionRepository,
            IRepository<SysFunctionRole> functionRoleRepository
            )
        {
            _functionRepository = functionRepository;
            _functionRoleRepository = functionRoleRepository;
        }


        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>角色所拥有的权限集合</returns>
        public  virtual List<SysFunction> GetFunctionsByRoleId(int roleId)
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
