using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Caching
{
   public static partial class HbCrmCachingDefaults
    {
        /// <summary>
        /// 默认的缓存时间60分钟
        /// </summary>
        public static int CacheTime => 60;


        /// <summary>
        /// 权限缓存前缀
        /// </summary>
        public static string PermissionsPrefixCacheKey => "hbcrm.permission.";

        /// <summary>
        /// 角色id-对应功能 缓存
        /// </summary>
        /// <remarks>
        /// {0} :  role ID
        /// value: functions
        /// </remarks>
        public static string PermissionsRoleIdCacheKey => "hbcrm.permission.roleid-{0}";


        /// <summary>
        /// 用户名称 缓存
        /// </summary>
        /// <remarks>
        /// {0} :  username 
        /// value: username
        /// </remarks>
        public static string AdminUserNameCacheKey => "hbcrm.admin.adminname-{0}";

    }
}
