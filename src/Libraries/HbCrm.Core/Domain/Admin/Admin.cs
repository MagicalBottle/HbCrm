using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Domain.Admin
{

    /// <summary>
    /// 管理员
    /// </summary>
    public partial class Admin : EditorEntity
    {

        #region Ctor

        public Admin()
        {
            Guid = new Guid().ToString();
        }
        #endregion

        #region Properties

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Admin 的 Guid
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 登录账号名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码 密文
        /// </summary>
        public string Password { get; set; }

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
        public string MobilePhone { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        #endregion



    }
}
