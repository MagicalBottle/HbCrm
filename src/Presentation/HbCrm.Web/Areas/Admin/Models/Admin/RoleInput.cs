using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Models.Admin
{
    public class RoleInput
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
        /// 人员id集合
        /// </summary>
        public List<int> AdminIds { get; set; }
    }
}
