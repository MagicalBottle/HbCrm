using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Services.Authorize
{
    public interface IPermissionService
    {
        /// <summary>
        /// 判定权限
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <returns>true 有此权限；false 无此权限</returns>
        bool Authorize(string name);
    }
}
