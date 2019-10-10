﻿using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Data;
using HbCrm.Core.Domain.Authorize;
using System.Linq;
using HbCrm.Core.Utils;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using HbCrm.Core;

namespace HbCrm.Services.Authorize
{
    public class MenuService : IMenuService
    {
        private readonly IRepository<SysMenu> _menuRepository;
        private readonly IRepository<SysMenuRole> _menuRoleRepository;

        public MenuService(IRepository<SysMenu> menuRepository,
           IRepository<SysMenuRole> menuRoleRepository)
        {
            _menuRepository = menuRepository;
            _menuRoleRepository = menuRoleRepository;
        }

        /// <summary>
        /// 根据角色id获取对应的菜单
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>菜单集合</returns>
        public List<SysMenu> GetMenusByRoleId(int roleId)
        {
            var query = from m in _menuRepository.Table
                        join mr in _menuRoleRepository.Table on m.Id equals mr.MenuId
                        where mr.RoleId == roleId
                        select m;

            return query.ToList();
        }

        #region 后台菜单列表
        /// <summary>
        /// 组织好菜单数据
        /// </summary>
        /// <param name="menus">数据库查出的原始数据</param>
        /// <returns>组织好的菜单集合</returns>
        public List<SysMenu> FormData(List<SysMenu> inputMenus)
        {
            if (inputMenus == null || inputMenus.Count <= 0)
            {
                return inputMenus;
            }
            Dictionary<int, SysMenu> dicTemp = new Dictionary<int, SysMenu>();
            Dictionary<int, SysMenu> dicReturn = new Dictionary<int, SysMenu>();

            var menus = CopyHelper.CopyDeepByJson(inputMenus);
            //按pid排序才不影响下面的判断dic.ContainsKey(menu.ParentMenuId)
            foreach (var menu in menus.OrderBy(m => m.ParentMenuId))
            {
                if (menu.ParentMenuId == 0)
                {
                    menu.Deep = 0;
                    dicReturn.Add(menu.Id, menu);
                }
                else
                {
                    //如果当前的菜单的父级是刚才添加过的，那么关联上
                    if (dicTemp.ContainsKey(menu.ParentMenuId))
                    {
                        var tempParentMenu = dicTemp[menu.ParentMenuId];
                        menu.Deep = tempParentMenu.Deep + 1;
                        tempParentMenu.ChildrenMenus.Add(menu);
                        menu.ParentMenu = tempParentMenu;
                    }
                }
                dicTemp.Add(menu.Id, menu);
            }
            dicTemp = null;
            return dicReturn.Values.ToList();
        }

        /// <summary>
        /// 标记请求的链接为选中状态
        /// </summary>
        /// <param name="menus">必须是调用<see cref="FormData"/>方法处理后的</param>
        /// <param name="httpContext">请求上下文</param>
        public void ActiveMenu(List<SysMenu> menus, HttpContext httpContext)
        {
            if (httpContext == null || httpContext.Request == null || !httpContext.Request.Path.HasValue)
            {
                return;
            }
            string[] urls = httpContext.Request.Path.ToString().Split("/", StringSplitOptions.RemoveEmptyEntries);
            string control = string.Empty;// /Admin /Admin/Home /Admin/Home/Index   /Admin/Menu /Admin/Menu/Index 
            if (urls.Length <= 0)
            {
                return;
            }
            if (urls.Length == 1)
            {
                control = "Home";
            }
            if (urls.Length >= 2)
            {
                control = urls[1];
            }
            ActiveMenu(menus, control);
        }

        /// <summary>
        /// 标记请求的链接为选中状态
        /// </summary>
        /// <param name="menus">必须是调用<see cref="FormData"/>方法处理后的</param>
        /// <param name="control">路径，控制器的部分</param>
        private void ActiveMenu(List<SysMenu> menus, string control)
        {
            foreach (var menu in menus)
            {
                //  /Admin/Menu/Edit/2  数据库保存 /Admin/Menu/Index   取[1]做对比
                string[] urls = menu.MenuUrl.Split("/", StringSplitOptions.RemoveEmptyEntries);
                if (urls.Length >= 2)
                {
                    if (string.Equals(urls[1], control, StringComparison.InvariantCultureIgnoreCase))
                    {
                        ActiveMenu(menu);
                    }
                }

                var childrenMenus = menu.ChildrenMenus;
                if (childrenMenus != null && childrenMenus.Count > 0)
                {
                    ActiveMenu(childrenMenus, control);
                }

            }
        }

        /// <summary>
        /// 标记当前菜单，以及他的祖先菜单
        /// </summary>
        /// <param name="menu"></param>
        private void ActiveMenu(SysMenu menu)
        {
            menu.Active = true;
            if (menu.ParentMenu != null)
            {
                ActiveMenu(menu.ParentMenu);
            }
        }

        #endregion



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
        public IPagedList<SysMenu> GetMenus(int pageNumber = 1, int pageSize = 10, string menuName = null, string menuSystermName = null, string sortName = "Id", string sortOrder = "ASC")
        {
            var query = from m in _menuRepository.TableNoTracking

                        select m;

            if (sortOrder.ToUpper() == "ASC")
            {
                query = query.OrderBy(m => m.MenuName);
            }
            if (sortOrder.ToUpper() == "DESC")
            {
                query = query.OrderByDescending(m =>
                    m.GetType().GetProperty(sortName).Name
                );
            }


            return new PagedList<SysMenu>(query, pageNumber, pageSize);
        }

    }
}
