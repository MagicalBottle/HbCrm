using HbCrm.Core.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Domain.Authorize
{
    public class SysFunctionRole:EditorEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 功能权限ID
        /// </summary>
        public int FunctionId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        public virtual SysFunction SysFunction { get; set; }

        public virtual SysRole SysRole { get; set; }
    }
}
