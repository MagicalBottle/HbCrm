﻿using HbCrm.Core.Domain.DataEnumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Models.Authorize
{
    public class MenuInput
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
        /// 一级菜单ID
        /// </summary>
        public int FirstParentMenuId { get; set; }

        /// <summary>
        /// 二级菜单ID
        /// </summary>
        public int SecondParentMenuId { get; set; }

        /// <summary>
        /// 三级菜单ID
        /// </summary>
        public int ThirdParentMenuId { get; set; }

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
        /// 菜单类型 1，链接；2功能
        /// </summary>
        public MenuType MenuType { get; set; }

    }
}
