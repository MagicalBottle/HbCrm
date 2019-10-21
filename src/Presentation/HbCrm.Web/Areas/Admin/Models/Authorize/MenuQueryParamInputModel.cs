using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Models.Authorize
{
    public class MenuQueryParamInputModel : BaseQueryParamInput
    {
        /// <summary>
        /// 菜单显示名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单系统名称
        /// </summary>
        public string MenuSystermName { get; set; }


    }
}
