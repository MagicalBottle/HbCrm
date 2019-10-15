using HbCrm.Core.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Services.Admin
{
   public interface IRoleService
    {
        /// <summary>
        /// 获取所有的角色，按照id正序
        /// </summary>
        /// <returns></returns>
        List<SysRole> GetAllRoles();
    }
}
