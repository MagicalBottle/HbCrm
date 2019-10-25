using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Data;
using HbCrm.Core.Domain.Admin;
using HbCrm.Core.Domain.Authorize;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using HbCrm.Core;
using HbCrm.Core.Domain.DataEnumerate;

namespace HbCrm.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<SysAdmin> _adminRepository;
        private readonly IRepository<SysAdminRole> _adminRoleRepository;
        private readonly IRepository<SysRole> _roleRepository;

        private readonly IRepository<SysMenu> _menuRepository;
        private readonly IRepository<SysMenuRole> _menuRoleRepository;
        


        public AdminService(IRepository<SysAdmin> adminRepository,
            IRepository<SysAdminRole> adminRoleRepository,
            IRepository<SysRole> roleRepository,
             IRepository<SysMenu> menuRepository,
              IRepository<SysMenuRole> menuRoleRepository)
        {
            _adminRepository = adminRepository;
            _adminRoleRepository = adminRoleRepository;
            _roleRepository = roleRepository;

            _menuRepository = menuRepository;
            _menuRoleRepository = menuRoleRepository;
            
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


        public SysAdmin GetAdminAllInforByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;
            //用户信息
            var queryAdmin = from c in _adminRepository.Table
                             orderby c.Id
                             where c.UserName == userName
                             select c;
            var sysAdmin = queryAdmin.FirstOrDefault();

            if (sysAdmin == null)
                return null;

            //对应角色
            var queryRoles = from r in _roleRepository.Table
                             join ar in _adminRoleRepository.Table on r.Id equals ar.RoleId
                             join a in _adminRepository.Table on ar.AdminId equals a.Id
                             where a.UserName == userName && r.Status== (int)RoleStatus.Active
                             orderby r.Id
                             select r;
            var sysRoles = queryRoles.ToList();
            sysAdmin.Roles = sysRoles;

            //对应菜单
            var queryMenu = from m in _menuRepository.Table
                            join mr in _menuRoleRepository.Table on m.Id equals mr.MenuId
                            join r in _roleRepository.Table on mr.RoleId equals r.Id
                            join ar in _adminRoleRepository.Table on mr.RoleId equals ar.RoleId
                            join a in _adminRepository.Table on ar.AdminId equals a.Id
                            where a.UserName == userName && r.Status == (int)RoleStatus.Active
                            orderby m.Id
                            select m;
            var sysMenus = queryMenu.ToList();
            sysAdmin.Menus = sysMenus;

            return sysAdmin;
        }

        /// <summary>
        /// 分页获取菜单
        /// </summary>
        /// <param name="pageNumber">页数（默认第1页）</param>
        /// <param name="pageSize">每页条数（默认10条）</param>
        /// <param name="sortName">排序的字段（默认为空）</param>
        /// <param name="sortOrder">排序方式 asc desc</param>
        /// <param name="userName">登录名</param>
        /// <param name="nickName">昵称</param>
        /// <param name="email">邮箱</param>
        /// <param name="mobilePhone">手机号</param>
        /// <param name="qQ">qq</param>
        /// <param name="weChar">微信号</param>
        /// <returns></returns>
        public IPagedList<SysAdmin> GetAdmins(int pageNumber = 1,
            int pageSize = 10,
            string sortName = "Id",
            string sortOrder = "DESC",
            string userName = null,
            string nickName = null,
            string email = null,
            string mobilePhone = null,
            string qQ = null,
            string weChar = null)
        {
            //null 传播 https://github.com/StefH/System.Linq.Dynamic.Core/wiki/NullPropagation

            var query = from m in _adminRepository.TableNoTracking
                        select m;

            if (!string.IsNullOrEmpty(userName))
            {
                //Contains 会包含 or name=''   indexof 不会  找不到-1
                //转换成mysql locate() 只要找到返回的结果都大于0
                query = query.Where(m => m.UserName.IndexOf(userName) > -1);
            }

            if (!string.IsNullOrEmpty(nickName))
            {
                query = query.Where(m => m.NickName.IndexOf(nickName) > -1);
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(m => m.Email.IndexOf(email) > -1);
            }

            if (!string.IsNullOrEmpty(mobilePhone))
            {
                query = query.Where(m => m.MobilePhone.IndexOf(mobilePhone) > -1);
            }

            if (!string.IsNullOrEmpty(qQ))
            {
                query = query.Where(m => m.QQ.IndexOf(qQ) > -1);
            }

            if (string.IsNullOrWhiteSpace(sortName))
            {
                sortName = "Id";
            }
            if (sortOrder.ToUpper() == "ASC")
            {
                query = query.OrderBy(sortName + " ASC");
            }
            if (sortOrder.ToUpper() == "DESC")
            {
                query = query.OrderBy(sortName + " DESC");
            }


            return new PagedList<SysAdmin>(query, pageNumber, pageSize);
        }

        /// <summary>
        /// 是否存在账号名称
        /// </summary>
        /// <param name="userName">账号名称</param>
        /// <param name="excludeId">排除那个Id的名称</param>
        /// <returns>true 存在，false 不存在</returns>
        public bool ExistAdminUserName(string userName, int excludeId = 0)
        {
            bool isExist = true;

            var query = from m in _adminRepository.TableNoTracking
                        select m;

            if (excludeId > 0)
            {
                query = query.Where(m => m.Id != excludeId);
            }

            isExist = query.Any(m => m.UserName == userName);

            return isExist;

        }

        /// <summary>
        /// 新增一个账号
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int AddAdmin(SysAdmin admin)
        {

            int result = -1;
            try
            {
                result = _adminRepository.Insert(admin);
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        /// <summary>
        ///  新增一个账号
        /// </summary>
        /// <param name="admin">账号实体</param>
        /// <param name="roleIds">账号分配了的角色id</param>
        /// <returns>-1,实体插入失败，-2角色关系插入失败</returns>
        public int AddAdmin(SysAdmin admin, List<int> roleIds)
        {
            int result = -1;
            result = _adminRepository.BeginTransaction(() =>
            {
                result = _adminRepository.Insert(admin);
                if (roleIds != null && roleIds.Count > 0)
                {
                    foreach (var id in roleIds)
                    {
                        var ar = new SysAdminRole()
                        {
                            AdminId = admin.Id,
                            RoleId = id,
                            CreateBy = admin.CreateBy,
                            CreatebyName = admin.CreatebyName,
                            CreateDate = admin.CreateDate,
                            LastUpdateBy = admin.LastUpdateBy,
                            LastUpdateByName = admin.LastUpdateByName,
                            LastUpdateDate = admin.LastUpdateDate
                        };
                        admin.AdminRoles.Add(ar);
                    }
                    result = _adminRoleRepository.Insert(admin.AdminRoles);
                }
            });
            return result;
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysAdmin GetAdminById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var query = from c in _adminRepository.TableNoTracking
                        orderby c.Id
                        where c.Id == id
                        select c;
            var sysAdmin = query.FirstOrDefault();

            if (sysAdmin == null)
            {
                return null;
            }

            //对应角色
            var queryRoles = from r in _roleRepository.TableNoTracking
                             join ar in _adminRoleRepository.Table on r.Id equals ar.RoleId
                             join a in _adminRepository.Table on ar.AdminId equals a.Id
                             where a.Id == id
                             orderby r.Id
                             select r;
            var sysRoles = queryRoles.ToList();
            sysAdmin.Roles = sysRoles;

            return sysAdmin;
        }

        /// <summary>
        ///  更新一个账号
        /// </summary>
        /// <param name="admin">账号实体</param>
        /// <param name="roleIds">账号分配了的角色id</param>
        /// <returns></returns>
        public int UpdateAdmin(SysAdmin admin, List<int> roleIds)
        {
            int result = -1;
            result = _adminRepository.BeginTransaction(() =>
            {

                result = _adminRepository.Update(admin,
                    m => m.UserName, m => m.NickName, m => m.Password, m => m.Email, m => m.MobilePhone, m => m.QQ, m => m.WeChar, m => m.LastUpdateBy, m => m.LastUpdateByName, m => m.LastUpdateDate);

                var adminRoles = _adminRoleRepository.Table.Where(m => m.AdminId == admin.Id).ToList();

                #region 删除
                //没有设置 全部删除
                if (roleIds == null || roleIds.Count <= 0)
                {
                    _adminRoleRepository.Delete(adminRoles);
                }
                else
                {
                    List<SysAdminRole> remveadminRoles = new List<SysAdminRole>();
                    foreach (var adminRole in adminRoles)
                    {
                        //没有包含，删除数据库
                        if (!roleIds.Any(id => id == adminRole.RoleId))
                        {
                            remveadminRoles.Add(adminRole);
                        }
                    }
                    _adminRoleRepository.Delete(remveadminRoles);
                }
                #endregion

                #region 插入
                if (roleIds != null && roleIds.Count > 0)
                {
                    foreach (var id in roleIds)
                    {
                        if (!adminRoles.Any(m => m.RoleId == id))
                        {
                            //不存在新增
                            var ar = new SysAdminRole()
                            {
                                AdminId = admin.Id,
                                RoleId = id,
                                LastUpdateBy = admin.LastUpdateBy,
                                LastUpdateByName = admin.LastUpdateByName,
                                LastUpdateDate = admin.LastUpdateDate,
                                CreateBy = admin.LastUpdateBy,
                                CreatebyName = admin.LastUpdateByName,
                                CreateDate = admin.LastUpdateDate
                            };
                            admin.AdminRoles.Add(ar);
                        }
                    }
                    result = _adminRoleRepository.Insert(admin.AdminRoles);
                }
                #endregion
            });
            return result;
        }

        /// <summary>
        /// 获取所有的管理员，按照id正序
        /// </summary>
        /// <returns></returns>
        public List<SysAdmin> GetAllAdmins()
        {
            var query = from m in _adminRepository.TableNoTracking
                        orderby m.Id
                        select m;
            return query.ToList();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            if (id <= 0)
            {
                return 0;
            }
            int result = -1;
            var model = _adminRepository.Table.Where(m =>m.Id==id).ToList();
            try
            {
                result = _adminRepository.Delete(model);
            }
            catch (Exception ex)
            {
            }
            return result;
        }


        /// <summary>
        /// 查找账号的菜单权限
        /// </summary>
        /// <param name="id">账号id</param>
        /// <returns></returns>
        public List<SysMenu> GetMenus(int id)
        {
            List<SysMenu> menus = new List<SysMenu>();

            if (id <= 0)
            {
                return null;
            }
            
            var queryMenu = from m in _menuRepository.TableNoTracking
                            join mr in _menuRoleRepository.TableNoTracking on m.Id equals mr.MenuId
                            join r in _roleRepository.TableNoTracking on mr.RoleId equals r.Id
                            join ar in _adminRoleRepository.TableNoTracking on r.Id equals ar.RoleId
                            join a in _adminRepository.TableNoTracking on ar.AdminId equals a.Id
                            where a.Id == id
                            orderby m.Id
                            select m;

            menus = queryMenu.ToList();
            
            return menus;
        }

    }
}
