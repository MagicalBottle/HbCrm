using HbCrm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HbCrm.Web.Areas.Admin.Models
{
    public class ReponseOutPut
    {
        /// <summary>
        /// 状态, 0失败 ，1成功 默认1
        /// </summary>
        public ReutnStatus Status { get; set; } = ReutnStatus.Success;
        
        /// <summary>
        /// 错误编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 提示信息 默认Success
        /// </summary>
        public string Message { get; set; } = "Success";
    }
    public class PagedListReponseOutPut<T>
    {
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
        /// 错误编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 提示信息 默认Success
        /// </summary>
        public string Message { get; set; } = "Success";
    }

    /// <summary>
    /// 返回数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListReponseOutPut<T>
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<T> Rows { get; set; } = new List<T>();

        /// <summary>
        /// 总条数
        /// </summary>
        public int Total => Rows.Count();

        /// <summary>
        /// 状态, 0失败 ，1成功 默认1
        /// </summary>
        public ReutnStatus Status { get; set; } = ReutnStatus.Success;
        
        /// <summary>
        /// 错误编码
        /// </summary>
        public string Code { get; set; }

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
