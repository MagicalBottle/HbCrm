using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Domain.Authorize
{
    public class PermissionKeys
    {
        #region fileds
        /// <summary>
        /// 后台首页，看板，一些统计数据
        /// </summary>
        public const string AdminDashboard = "AdminDashboard";

        /// <summary>
        /// 查看管理员
        /// </summary>
        public const string AdminView = "AdminView";

        /// <summary>
        /// 添加管理员
        /// </summary>
        public const string AdminAdd = "AdminAdd";

        /// <summary>
        /// 编辑管理员
        /// </summary>
        public const string AdminEdit = "AdminEdit";

        /// <summary>
        /// 删除管理员
        /// </summary>
        public const string AdminDelete = "AdminDelete";

        public static List<PermissionRecord> AllPermissions = new List<PermissionRecord>
        {
            new PermissionRecord(){Name="AdminDashboard",Description="后台首页" },
            new PermissionRecord(){Name="AdminView",Description="查看管理员" },
            new PermissionRecord(){Name="AdminAdd",Description="添加管理员" },
            new PermissionRecord(){Name="AdminEdit",Description="编辑管理员"},
            new PermissionRecord(){Name="AdminDelete",Description="删除管理员"}

        };
        #endregion
    }
}
