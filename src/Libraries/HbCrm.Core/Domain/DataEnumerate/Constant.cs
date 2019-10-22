using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Domain.DataEnumerate
{
   public enum MenuType
    {
        /// <summary>
        /// 连接
        /// </summary>
        Menu=1,

        /// <summary>
        /// 功能
        /// </summary>
        Function=2
    }

    public enum RoleStatus
    {
        /// <summary>
        /// 启用
        /// </summary>
        Active = 1,

        /// <summary>
        /// 停用
        /// </summary>
        Locked = 2
    }

    public enum AdminStatus
    {
        /// <summary>
        /// 启用
        /// </summary>
        Active = 1,

        /// <summary>
        /// 停用
        /// </summary>
        Locked = 2
    }
}
