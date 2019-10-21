using HbCrm.Core.Domain.Authorize;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Domain.Admin
{
   public partial class SysRole :EditorEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        
        /// <summary>
        /// 角色状态
        /// </summary>
        public int RoleStatus { get; set; }

        /// <summary>
        /// 角色说明
        /// </summary>
        public string RoleRemark { get; set; }

        /// <summary>
        /// 用户角色关联表
        /// </summary>
        public virtual List<SysAdminRole> AdminRoles { get; set; } = new List<SysAdminRole>();

        /// <summary>
        /// 功能角色关联表
        /// </summary>
        public virtual List<SysFunctionRole> FunctionRoles { get; set; } = new List<SysFunctionRole>();

        /// <summary>
        /// 菜单角色关联表
        /// </summary>
        public virtual List<SysMenuRole> MenuRoles { get; set; } = new List<SysMenuRole>();

        /// <summary>
        /// 包含的管理员
        /// </summary>
        public List<SysAdmin> Admins { get; set; } = new List<SysAdmin>();

    }
}
