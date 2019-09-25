using HbCrm.Core.Domain.Authorize;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Services.Authorize
{
    public interface IFunctionService
    {
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>角色所拥有的权限集合</returns>
        List<SysFunction> GetFunctionsByRoleId(int roleId);
    }
}
