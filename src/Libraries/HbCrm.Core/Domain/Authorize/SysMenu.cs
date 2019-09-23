using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Domain.Authorize
{
    /// <summary>
    /// 菜单类
    /// </summary>
    public partial class SysMenu : EditorEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 菜单显示名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单系统名称
        /// </summary>
        public string MenuSystermName { get; set; }

        /// <summary>
        /// 菜单连接
        /// </summary>
        public string MenuUrl { get; set; }

        /// <summary>
        /// 上级菜单ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; }

        /// <summary>
        /// 菜单排序
        /// </summary>
        public int MenuSort { get; set; }

        /// <summary>
        /// 菜单说明
        /// </summary>
        public string MenuRemark { get; set; }

    }
}
