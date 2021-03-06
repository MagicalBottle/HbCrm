﻿using HbCrm.Core.Domain.DataEnumerate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Domain.Authorize
{
    /// <summary>
    /// 菜单类
    /// </summary>
    [Serializable]
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
        /// 菜单链接
        /// </summary>
        public string MenuUrl { get; set; }

        /// <summary>
        /// 上级菜单ID
        /// </summary>
        public int ParentMenuId { get; set; }

        /// <summary>
        /// 菜单类型 1，链接；2功能。操作的时候看<see cref="MenuType"/>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 菜单类型 1，链接；2功能
        /// </summary>
        public MenuType MenuType
        {
            get { return (MenuType)Type; }
            set { Type = (int)value; }
        }        

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

        /// <summary>
        /// 菜单角色关联表
        /// </summary>
        public virtual List<SysMenuRole> MenuRoles { get; set; }

        #region ignore
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<SysMenu> ChildrenMenus { get; set; } = new List<SysMenu>();

        /// <summary>
        /// 父菜单
        /// </summary>
        public SysMenu ParentMenu { get; set; }

        /// <summary>
        /// 深度，默认0
        /// </summary>
        public int Deep { get; set; }

        /// <summary>
        /// 选中菜单。true 选中，false 不选中。默认 false
        /// </summary>
        public bool Active { get; set; } = false;
        #endregion

    }


}
