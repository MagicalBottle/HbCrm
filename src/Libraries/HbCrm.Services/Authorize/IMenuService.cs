using HbCrm.Core.Domain.Authorize;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// 组织好菜单数据
        /// </summary>
        /// <param name="menus">数据库查出的原始数据</param>
        /// <returns>组织好的菜单集合</returns>
        List<SysMenu> FormData(List<SysMenu> menus);

        /// <summary>
        /// 标记请求的链接为选中状态
        /// </summary>
        /// <param name="menus">必须是调用<see cref="FormData"/>方法处理后的</param>
        /// <param name="httpContext">请求上下文</param>
        void ActiveMenu(List<SysMenu> menus, HttpContext httpContext);
    }
}
