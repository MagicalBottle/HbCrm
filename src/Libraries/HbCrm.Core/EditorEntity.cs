using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HbCrm.Core
{
    /// <summary>
    /// 创建 修改记录
    /// </summary>
    [Serializable]
    public  class EditorEntity
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        [Required]
        public virtual int CreateBy { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Required,MaxLength(50)]
        public virtual string CreatebyName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Required]
        public virtual int LastUpdateBy { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Required, MaxLength(50)]
        public virtual string LastUpdateByName { get; set; }

        /// <summary>
        /// 最后更新日期
        /// </summary>
        [Required]
        public virtual DateTime LastUpdateDate { get; set; }
    }
}
