using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core;
using HbCrm.Core.Domain.Admin;
using HbCrm.Core.Domain.Authorize;

namespace HbCrm.Services.Admin
{
   public interface IAdminService
    {
        SysAdmin GetAdminByUserName(string userName);


       // SysAdmin GetAdminByUserNameNoLazy(string userName);


        SysAdmin GetAdminAllInforByUserName(string userName);

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
        IPagedList<SysAdmin> GetAdmins(int pageNumber = 1,
            int pageSize = 10,
            string sortName = "Id",
            string sortOrder = "DESC",
            string userName = null,
            string nickName = null,
            string email = null,
            string mobilePhone = null,
            string qQ = null,
            string weChar = null);

        /// <summary>
        /// 是否存在账号名称
        /// </summary>
        /// <param name="userName">账号名称</param>
        /// <param name="excludeId">排除那个Id的名称</param>
        /// <returns>true 存在，false 不存在</returns>
        bool ExistAdminUserName(string userName,int excludeId=0);

        /// <summary>
        /// 新增一个账号
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        int AddAdmin(SysAdmin admin);


        /// <summary>
        ///  新增一个账号
        /// </summary>
        /// <param name="admin">账号实体</param>
        /// <param name="roleIds">账号分配了的角色id</param>
        /// <returns></returns>
        int AddAdmin(SysAdmin admin, List<int> roleIds);


        /// <summary>
        ///  更新一个账号
        /// </summary>
        /// <param name="admin">账号实体</param>
        /// <param name="roleIds">账号分配了的角色id</param>
        /// <returns></returns>
        int UpdateAdmin(SysAdmin admin, List<int> roleIds);
        

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysAdmin GetAdminById(int id);

        /// <summary>
        /// 获取所有的管理员，按照id正序
        /// </summary>
        /// <returns></returns>
        List<SysAdmin> GetAllAdmins();

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        int Delete(int id);

        /// <summary>
        /// 查找账号的菜单权限
        /// </summary>
        /// <param name="id">账号id</param>
        /// <returns></returns>
        List<SysMenu> GetMenus(int id);
    }
}
