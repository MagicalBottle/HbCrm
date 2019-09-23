using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Domain.Admin
{
    /// <summary>
    /// 用户角色关系映射
    /// </summary>
    public partial class SysAdminRole:EditorEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        public virtual SysAdmin SysAdmin { get; set; }

        public virtual SysRole SysRole { get; set; }
    }
}
