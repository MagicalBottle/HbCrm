using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Domain.Authorize
{
    public class SysFunction:EditorEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 功能显示名称
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 功能系统名称
        /// </summary>
        public string FunctionSystermName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int FunctionSort { get; set; }

        /// <summary>
        /// 所属菜单id
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 菜单说明
        /// </summary>
        public string FunctionRemark { get; set; }

        /// <summary>
        /// 功能权限角色关联表
        /// </summary>
        public virtual List<SysFunctionRole> FunctionRoles { get; set; }

    }
}
