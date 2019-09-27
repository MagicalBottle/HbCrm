using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Data;
using HbCrm.Core.Domain.Authorize;
using System.Linq;
using HbCrm.Core.Extensions;

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

            var menus =ObjectExtensions.CloneByStream( inputMenus);
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
                        menu.Deep = dicTemp[menu.ParentMenuId].Deep + 1;
                        dicTemp[menu.ParentMenuId].ChildrenMenus.Add(menu);
                    }
                }
                dicTemp.Add(menu.Id, menu);
            }
            dicTemp = null;
            return dicReturn.Values.ToList();
        }
    }
}
