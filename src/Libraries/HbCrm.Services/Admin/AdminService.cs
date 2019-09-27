using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Data;
using HbCrm.Core.Domain.Admin;
using HbCrm.Core.Domain.Authorize;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HbCrm.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<SysAdmin> _adminRepository;
        private readonly IRepository<SysAdminRole> _adminRoleRepository;
        private readonly IRepository<SysRole> _roleRepository;

        private readonly IRepository<SysMenu> _menuRepository;
        private readonly IRepository<SysMenuRole> _menuRoleRepository;

        private readonly IRepository<SysFunction> _functionRepository;
        private readonly IRepository<SysFunctionRole> _functionRoleRepository;


        public AdminService(IRepository<SysAdmin> adminRepository,
            IRepository<SysAdminRole> adminRoleRepository,
            IRepository<SysRole> roleRepository,
             IRepository<SysMenu> menuRepository,
              IRepository<SysMenuRole> menuRoleRepository,
              IRepository<SysFunction> functionRepository,
              IRepository<SysFunctionRole> functionRoleRepository)
        {
            _adminRepository = adminRepository;
            _adminRoleRepository = adminRoleRepository;
            _roleRepository = roleRepository;

            _menuRepository = menuRepository;
            _menuRoleRepository = menuRoleRepository;

            _functionRepository = functionRepository;
            _functionRoleRepository = functionRoleRepository;
        }
        public SysAdmin GetAdminByUserNameNoLazy(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            _adminRepository.LazyLoadingEnabled(false);//要缓存，必须.Lazy=false 且_adminRepository.Table 且  _adminRepository.Detach(sysAdmin) 
                                                       //另一种方式就是取消懒加载 注释代码option.UseLazyLoadingProxies(); 
            var query = from c in _adminRepository.Table.Include(model => model.AdminRoles)
                        orderby c.Id
                        where c.UserName == userName
                        select c;
            var sysAdmin = query.FirstOrDefault();
            _adminRepository.Detach(sysAdmin);
            return sysAdmin;
        }


        public SysAdmin GetAdminByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            var query = from c in _adminRepository.Table
                        orderby c.Id
                        where c.UserName == userName
                        select c;
            var sysAdmin = query.FirstOrDefault();
            return sysAdmin;
        }


        public SysAdmin GetAdminAllInforByUserName (string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;
            
            //用户信息
            var queryAdmin = from c in _adminRepository.Table
                             orderby c.Id
                             where c.UserName == userName
                             select c;
            var sysAdmin = queryAdmin.FirstOrDefault();

            //对应角色
            var queryRoles = from r in _roleRepository.Table
                             join ar in _adminRoleRepository.Table on r.Id equals ar.RoleId
                             join a in _adminRepository.Table on ar.AdminId equals a.Id
                             where a.UserName == userName
                             orderby r.Id
                             select r;
            var sysRoles = queryRoles.ToList();
            sysAdmin.Roles = sysRoles;

            //对应菜单
            var queryMenu = from m in _menuRepository.Table
                            join mr in _menuRoleRepository.Table on m.Id equals mr.MenuId
                            join ar in _adminRoleRepository.Table on mr.RoleId equals ar.RoleId
                            join a in _adminRepository.Table on ar.AdminId equals a.Id
                            where a.UserName == userName
                            orderby m.Id
                            select m;
            var sysMenus = queryMenu.ToList();
            sysAdmin.Menus = sysMenus;

            //对应功能
            var queryFunction = from f in _functionRepository.Table
                            join fr in _functionRoleRepository.Table on f.Id equals fr.FunctionId
                            join ar in _adminRoleRepository.Table on fr.RoleId equals ar.RoleId
                            join a in _adminRepository.Table on ar.AdminId equals a.Id
                            where a.UserName == userName
                            orderby f.Id
                            select f;
            var sysFunctions = queryFunction.ToList();
            sysAdmin.Functions = sysFunctions;

            return sysAdmin;
        }
    }
}
