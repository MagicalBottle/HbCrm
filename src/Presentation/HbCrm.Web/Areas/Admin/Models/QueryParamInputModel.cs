using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Models
{
    public abstract class QueryParamInputModel
    {
        /// <summary>
        /// 每页条数 默认10
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 查询页数 默认1  
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// 排序的字段
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// 排序方式 asc desc
        /// </summary>
        public string SortOrder { get; set; }
    }
}
