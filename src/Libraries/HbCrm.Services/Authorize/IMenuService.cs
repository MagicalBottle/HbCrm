using HbCrm.Core;
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

        /// <summary>
        /// 分页获取菜单
        /// </summary>
        /// <param name="pageNumber">页数（默认第1页）</param>
        /// <param name="pageSize">每页条数（默认10条）</param>
        /// <param name="menuMame">菜单名称（默认为空）</param>
        /// <param name="menuSystermName">菜单系统名称（默认为空）</param>
        /// <param name="sortName">排序的字段（默认为空）</param>
        /// <param name="sortOrder">排序方式 asc desc</param>
        /// <returns></returns>
        IPagedList<SysMenu> GetMenus( int pageNumber = 1, int pageSize = 10, string menuName = null, string menuSystermName = null, string sortName = "Id", string sortOrder ="ASC");

        /// <summary>
        /// 获取所有的菜单
        /// </summary>
        /// <returns></returns>
        List<SysMenu> GetAllMenus();

        /// <summary>
        /// 获取菜单下的所有子菜单（只获取一级）
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<SysMenu> GetLevelMenus(int parentId = 0);

        /// <summary>
        /// 是否存在同名系统菜单
        /// </summary>
        /// <param name="menuSystermName">系统菜单名称</param>
        /// <returns></returns>
        bool ExistMenuByMenuSystermName(string menuSystermName);

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu">菜单实体</param>
        /// <returns>-1插入失败</returns>
        int AddMenu(SysMenu menu);
    }
}
