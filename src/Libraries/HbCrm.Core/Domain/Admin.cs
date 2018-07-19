using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Domain
{
    /// <summary>
    /// 管理员
    /// </summary>
    public partial class Admin:BaseEntity
    {

        #region Ctor

        public Admin()
        {

        }
        #endregion

        #region Properties

        /// <summary>
        /// Admin 的 Guid
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// 登录账号名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileNumber { get; set; }
        #endregion



    }
}
