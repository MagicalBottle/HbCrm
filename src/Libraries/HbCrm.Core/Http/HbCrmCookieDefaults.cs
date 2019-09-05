using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Http
{
    public static class HbCrmCookieDefaults
    {
        /// <summary>
        /// cookie名称的前缀
        /// </summary>
        public static string Prefix => ".HbCrm";

        /// <summary>
        /// 后台账号cookie
        /// </summary>
        public static string AdminCookie => ".Admin";

        /// <summary>
        /// 前台用户cookie
        /// </summary>
        public static string CustomerCookie => ".Customer";
    }
}
