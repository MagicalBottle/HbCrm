using HbCrm.Core;
using HbCrm.Core.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Services.Admin
{
   public interface IRoleService
    {
        /// <summary>
        /// 获取所有的角色，按照id正序
        /// </summary>
        /// <returns></returns>
        List<SysRole> GetAllRoles();

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
        IPagedList<SysRole> GetRoles(
            int pageNumber = 1,
            int pageSize = 10,
            string sortName = "Id",
            string sortOrder = "DESC",
            string roleName = null,
            int roleStatus =-1,
            string roleRemark = null);

        /// <summary>
        /// 是否存在账号名称
        /// </summary>
        /// <param name="userName">角色名称</param>
        /// <param name="excludeId">排除那个Id的角色名称</param>
        /// <returns>true 存在，false 不存在</returns>
        bool ExistRoleName(string roleName, int excludeId = 0);


        /// <summary>
        ///  新增
        /// </summary>
        /// <param name="admin">角色实体</param>
        /// <param name="roleIds">角色包含了账号id</param>
        /// <returns>-1,实体插入失败，-2角色关系插入失败</returns>
        int AddRole(SysRole role, List<int> adminIds);

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysRole GetRoleById(int id);

        /// <summary>
        ///  更新一个角色
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <param name="adminIds">包含的人员id</param>
        /// <returns></returns>
        int UpdateRole(SysRole role, List<int> adminIds);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        int DeleteRoleById(int id);

        /// <summary>
        /// 获取角色，包含角色对应的菜单权限
        /// </summary>
        /// <param name="">角色id</param>
        /// <returns></returns>
         SysRole GetRoleWithMenus(int id);

        /// <summary>
        ///  更新角色的权限
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="adminIds">包含的菜单id</param>
        /// <returns></returns>
        int UpdatePermission(SysRole role, List<int> menuIds);
    }
}
