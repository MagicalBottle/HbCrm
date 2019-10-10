using HbCrm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Models
{
    public class PagedListOutPut<T>
    {
        public PagedListOutPut()
        {            
        }
        /// <summary>
        /// 数据集合
        /// </summary>
        public IPagedList<T> Rows { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Total => Rows.TotalCount;

        /// <summary>
        /// 状态, 0失败 ，1成功 默认1
        /// </summary>
        public ReutnStatus Status { get; set; } = ReutnStatus.Success;

        /// <summary>
        /// 提示信息 默认Success
        /// </summary>
        public string Message { get; set; } = "Success";
    }    
}

/// <summary>
/// 返回标识
/// </summary>
public enum ReutnStatus
{
    /// <summary>
    /// 失败
    /// </summary>
    Error=0,

    /// <summary>
    /// 成功
    /// </summary>
    Success=1
}
