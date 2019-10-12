using System;
using System.Collections.Generic;
using System.Text;
using HbCrm.Core.Domain.Authorize;

namespace HbCrm.Core.Domain.Admin
{


    /// <summary>
    /// 管理员
    /// </summary>
    public partial class SysAdmin : EditorEntity
    {

        #region Ctor

        public SysAdmin()
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

        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChar { get; set; }

        /// <summary>
        /// 用户角色关联表
        /// </summary>
        public virtual List<SysAdminRole> AdminRoles { get; set; }

        #region ignoe
        /// <summary>
        /// 对应角色集合
        /// </summary>
        public List<SysRole> Roles { get; set; }

        /// <summary>
        /// 用户对应功能集合
        /// </summary>
        public List<SysFunction> Functions { get; set; }

        /// <summary>
        /// 用户对应菜单集合
        /// </summary>
        public List<SysMenu> Menus { get; set; }

        #endregion

        #endregion



    }
}
