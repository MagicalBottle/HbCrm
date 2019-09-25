using HbCrm.Core.Domain.Authorize;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Services.Authorize
{
    public interface IMenuService 
    {
        /// <summary>
        /// 根据角色id获取对应的菜单
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>菜单集合</returns>
        List<SysMenu> GetMenusByRoleId(int roleId);
    }
}
