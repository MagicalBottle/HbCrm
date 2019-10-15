using HbCrm.Core.Data;
using HbCrm.Core.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HbCrm.Services.Admin
{
   public class RoleService:IRoleService
    {

        private readonly IRepository<SysRole> _roleRepository;

        public RoleService(IRepository<SysRole> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取所有的角色，按照id正序
        /// </summary>
        /// <returns></returns>
        public List<SysRole> GetAllRoles()
        {
            var query = from m in _roleRepository.TableNoTracking
                        orderby m.Id
                        select m;
            return query.ToList();
        }
    }
}
