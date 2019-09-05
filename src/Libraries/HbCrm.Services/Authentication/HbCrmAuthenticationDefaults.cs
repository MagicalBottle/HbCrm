using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Services.Authentication
{
   public static class HbCrmAuthenticationDefaults
    {
        /// <summary>
        /// 后台管理员验证
        /// </summary>
        public static string AdminAuthenticationScheme => "AdminAuthentication";

        /// <summary>
        /// 前台用户验证
        /// </summary>
        public static string CustomerAuthenticationScheme => "CustomerAuthentication";

        /// <summary>
        /// 证书颁发者
        /// </summary>
        public static string ClaimsIssuer => "HbCrm";

        /// <summary>
        /// 后台登录页面
        /// </summary>
        public static PathString LoginPath => new PathString("/Login");
        
        /// <summary>
        ///后台没有权限跳转的页面
        /// </summary>
        public static PathString AccessDeniedPath => new PathString("/Forbidden");

        /// <summary>
        /// 前台登录页面
        /// </summary>
        public static PathString SigninPath => new PathString("/Signin");

    }
}
