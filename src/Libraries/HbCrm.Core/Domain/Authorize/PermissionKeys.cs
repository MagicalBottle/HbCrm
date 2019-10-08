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

        #region Admin人员管理
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
        #endregion

        #region Menu 菜单管理
        /// <summary>
        /// 查看管理员
        /// </summary>
        public const string MenuView = "MenuView";

        /// <summary>
        /// 添加管理员
        /// </summary>
        public const string MenuAdd = "MenuAdd";

        /// <summary>
        /// 编辑管理员
        /// </summary>
        public const string MenuEdit = "MenuEdit";

        /// <summary>
        /// 删除管理员
        /// </summary>
        public const string MenuDelete = "MenuDelete";
        #endregion 

        public static List<PermissionRecord> AllPermissions = new List<PermissionRecord>
        {
            new PermissionRecord(){Name="AdminDashboard",Description="后台首页" },

            new PermissionRecord(){Name="AdminView",Description="查看管理员" },
            new PermissionRecord(){Name="AdminAdd",Description="添加管理员" },
            new PermissionRecord(){Name="AdminEdit",Description="编辑管理员"},
            new PermissionRecord(){Name="AdminDelete",Description="删除管理员"},

            new PermissionRecord(){Name="MenuView",Description="查看菜单" },
            new PermissionRecord(){Name="MenuAdd",Description="添加菜单" },
            new PermissionRecord(){Name="MenuEdit",Description="编辑菜单"},
            new PermissionRecord(){Name="MenuDelete",Description="删除菜单"}

        };
        #endregion
    }
}
