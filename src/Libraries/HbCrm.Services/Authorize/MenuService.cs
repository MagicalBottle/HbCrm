using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Data;
using HbCrm.Core.Domain.Authorize;
using System.Linq;

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
    }
}
