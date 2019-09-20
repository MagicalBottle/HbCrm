using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Models.Admin
{
    public class AdminLoginModel
    {
        /// <summary>
        /// 登录账号名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码 密文
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// cookie持久化
        /// </summary>
        public bool IsPersistent { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
