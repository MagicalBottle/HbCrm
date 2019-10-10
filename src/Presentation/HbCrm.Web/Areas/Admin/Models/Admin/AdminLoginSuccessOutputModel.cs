using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Models.Admin
{
    public class AdminLoginSuccessOutputModel
    {
        /// <summary>
        /// 登录状态
        /// </summary>
        public LoginStatus LoginStatus { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string ReturnUrl { get; set; }

    }

    public enum LoginStatus
    {
        Error = 0,
        Success = 1,
        Locked = 2
    }
}
