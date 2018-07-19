using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core
{
    /// <summary>
    /// 实体的抽象基类
    /// </summary>
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// 获取或设置实体的标识符
        /// </summary>
        public int Id { get; set; }
    }
}
