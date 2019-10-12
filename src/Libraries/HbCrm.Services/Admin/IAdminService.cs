using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core;
using HbCrm.Core.Domain.Admin;

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
            string sortOrder = "ASC",
            string userName = null,
            string nickName = null,
            string email = null,
            string mobilePhone = null,
            string qQ = null,
            string weChar = null);
    }
}
