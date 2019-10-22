using HbCrm.Core.Data;
using HbCrm.Core.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HbCrm.Core;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace HbCrm.Services.Admin
{
    public class RoleService : IRoleService
    {

        private readonly IRepository<SysRole> _roleRepository;
        private readonly IRepository<SysAdminRole> _adminRoleRepository;
        private readonly IRepository<SysAdmin> _adminRepository;

        public RoleService(IRepository<SysRole> roleRepository,
             IRepository<SysAdminRole> adminRoleRepository,
             IRepository<SysAdmin> adminRepository)
        {
            _roleRepository = roleRepository;
            _adminRoleRepository = adminRoleRepository;
            _adminRepository = adminRepository;
        }

        /// <summary>
        /// 获取所有的角色，按照id正序
        /// </summary>
        /// <returns></returns>
        public List<SysRole> GetAllRoles()
        {
            var query = from m in _roleRepository.TableNoTracking
                        orderby m.Id
                        select m;
            return query.ToList();
        }

        /// <summary>
        /// 分页获取菜单
        /// </summary>
        /// <param name="pageNumber">页数（默认第1页）</param>
        /// <param name="pageSize">每页条数（默认10条）</param>
        /// <param name="sortName">排序的字段（默认为空）</param>
        /// <param name="sortOrder">排序方式 asc desc</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="roleStatus">角色状态</param>
        /// <param name="roleRemark">角色说明</param>
        /// <returns></returns>
        public IPagedList<SysRole> GetRoles(int pageNumber = 1,
            int pageSize = 10,
            string sortName = "Id",
            string sortOrder = "DESC",
            string roleName = null,
            int roleStatus = 1,
            string roleRemark = null)
        {
            //null 传播 https://github.com/StefH/System.Linq.Dynamic.Core/wiki/NullPropagation

            var query = from m in _roleRepository.TableNoTracking
                        select m;

            if (!string.IsNullOrEmpty(roleName))
            {
                //Contains 会包含 or name=''   indexof 不会  找不到-1
                //转换成mysql locate() 只要找到返回的结果都大于0
                query = query.Where(m => m.RoleName.IndexOf(roleName) > -1);
            }
            
            query = query.Where(m => m.Status == roleStatus);


            if (!string.IsNullOrEmpty(roleRemark))
            {
                query = query.Where(m => m.RoleRemark.IndexOf(roleRemark) > -1);
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

            return new PagedList<SysRole>(query, pageNumber, pageSize);
        }


        /// <summary>
        /// 是否存在账号名称
        /// </summary>
        /// <param name="userName">角色名称</param>
        /// <param name="excludeId">排除那个Id的角色名称</param>
        /// <returns>true 存在，false 不存在</returns>
        public bool ExistRoleName(string roleName, int excludeId = 0)
        {
            bool isExist = true;

            var query = from m in _roleRepository.TableNoTracking
                        select m;

            if (excludeId > 0)
            {
                query = query.Where(m => m.Id != excludeId);
            }

            isExist = query.Any(m => m.RoleName == roleName);

            return isExist;

        }

        /// <summary>
        ///  新增
        /// </summary>
        /// <param name="admin">角色实体</param>
        /// <param name="roleIds">角色包含了账号id</param>
        /// <returns>-1,实体插入失败，-2角色关系插入失败</returns>
        public int AddRole(SysRole role, List<int> adminIds)
        {
            int result = -1;
            result = _roleRepository.BeginTransaction(() =>
            {
                result = _roleRepository.Insert(role);
                if (adminIds != null && adminIds.Count > 0)
                {
                    foreach (var id in adminIds)
                    {
                        var ar = new SysAdminRole()
                        {
                            AdminId = id,
                            RoleId = role.Id,
                            CreateBy = role.CreateBy,
                            CreatebyName = role.CreatebyName,
                            CreateDate = role.CreateDate,
                            LastUpdateBy = role.LastUpdateBy,
                            LastUpdateByName = role.LastUpdateByName,
                            LastUpdateDate = role.LastUpdateDate
                        };
                        role.AdminRoles.Add(ar);
                    }
                    result = _adminRoleRepository.Insert(role.AdminRoles);
                }
            });
            return result;
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysRole GetRoleById(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var query = from c in _roleRepository.TableNoTracking
                        orderby c.Id
                        where c.Id == id
                        select c;
            var sysRole = query.FirstOrDefault();

            //对应角色
            var queryAdmin = from r in _adminRepository.TableNoTracking
                             join ar in _adminRoleRepository.Table on r.Id equals ar.AdminId
                             join a in _roleRepository.Table on ar.RoleId equals a.Id
                             where a.Id == id
                             orderby r.Id
                             select r;
            var sysAdmins = queryAdmin.ToList();
            sysRole.Admins = sysAdmins;

            return sysRole;
        }


        /// <summary>
        ///  更新一个角色
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <param name="adminIds">包含的人员id</param>
        /// <returns></returns>
        public int UpdateRole(SysRole role, List<int> adminIds)
        {
            int result = -1;
            result = _roleRepository.BeginTransaction(() =>
            {

                result = _roleRepository.Update(role,
                    m => m.RoleName, m => m.RoleStatus, m => m.RoleRemark, m => m.LastUpdateBy, m => m.LastUpdateByName, m => m.LastUpdateDate);

                var adminRoles = _adminRoleRepository.Table.Where(m => m.RoleId == role.Id).ToList();

                #region 删除
                //没有设置 全部删除
                if (adminIds == null || adminIds.Count <= 0)
                {
                    _adminRoleRepository.Delete(adminRoles);
                }
                else
                {
                    List<SysAdminRole> remveadminRoles = new List<SysAdminRole>();
                    foreach (var adminRole in adminRoles)
                    {
                        //没有包含，删除数据库
                        if (!adminIds.Any(id => id == adminRole.AdminId))
                        {
                            remveadminRoles.Add(adminRole);
                        }
                    }
                    _adminRoleRepository.Delete(remveadminRoles);
                }
                #endregion

                #region 插入
                if (adminIds != null && adminIds.Count > 0)
                {
                    foreach (var id in adminIds)
                    {
                        if (!adminRoles.Any(m => m.AdminId == id))
                        {
                            //不存在新增
                            var ar = new SysAdminRole()
                            {
                                AdminId = id,
                                RoleId = role.Id,
                                LastUpdateBy = role.LastUpdateBy,
                                LastUpdateByName = role.LastUpdateByName,
                                LastUpdateDate = role.LastUpdateDate,
                                CreateBy = role.LastUpdateBy,
                                CreatebyName = role.LastUpdateByName,
                                CreateDate = role.LastUpdateDate
                            };
                            role.AdminRoles.Add(ar);
                        }
                    }
                    result = _adminRoleRepository.Insert(role.AdminRoles);
                }
                #endregion
            });
            return result;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public int DeleteRoleById(int id)
        {
            //设置了级联删除，自动删除对应的表adminrole表的记录
            int result = -1;
            var role = _roleRepository.Table.Where(m => m.Id == id);

            result = _roleRepository.Delete(role);
            return result;
        }

    }
}
