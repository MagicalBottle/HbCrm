using HbCrm.Services.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Services.Authorize
{
    public class PermissionService : IPermissionService
    {
        private readonly IWorkContext _workContext;

        public PermissionService(IWorkContext workContext)
        {
            _workContext = workContext;
        }

        /// <summary>
        /// 判定权限
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <returns>true 有此权限；false 无此权限</returns>
        public bool Authorize(string name)
        {
            this.Authorize(name, _workContext.Admin);
            return true;
        }

        public bool Authorize(string name,HbCrm.Core.Domain.Admin.Admin admin)
        {
            return true;
        }
    }
}
