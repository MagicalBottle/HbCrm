using HbCrm.Core.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Domain.Authorize
{
   public class SysMenuRole: EditorEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 功能权限ID
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public virtual SysMenu SysMenu { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual SysRole SysRole { get; set; }
    }
}
