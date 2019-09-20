using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Admin;

namespace HbCrm.Services.Authentication
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="admin">登录的账号</param>
        /// <param name="isPersistent">登录信息持久化到客户端，true 是，false 否</param>
         void SignIn(HbCrm.Core.Domain.Admin.Admin admin, bool isPersistent);

        /// <summary>
        /// 登出
        /// </summary>
        void SignOut();

        /// <summary>
        /// 获取身份验证的账号
        /// </summary>
        /// <returns></returns>
        HbCrm.Core.Domain.Admin.Admin GetAuthenticatedAdmin();
    }
}
