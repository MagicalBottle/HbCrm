using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Models.Admin
{
    public class RoleQueryParamInput:BaseQueryParamInput
    {
        /// <summary>
        /// 角色显示名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色状态
        /// </summary>
        public int RoleStatus { get; set; } = -1;

        /// <summary>
        /// 角色说明
        /// </summary>
        public string RoleRemark { get; set; }

    }
}
