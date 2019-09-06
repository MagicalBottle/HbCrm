using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core
{
    /// <summary>
    /// 创建 修改记录
    /// </summary>
   public  class EditorEntity
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        public virtual int CreateBy { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string CreatebyName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public virtual int LastUpdateBy { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public virtual string LastUpdateByName { get; set; }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public virtual DateTime? LastUpdateDate { get; set; }
    }
}
