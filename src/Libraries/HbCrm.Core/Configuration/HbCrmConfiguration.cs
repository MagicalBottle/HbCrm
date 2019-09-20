using HbCrm.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Configuration
{
    public class HbCrmConfiguration
    {
        /// <summary>
        /// 数据库配置
        /// </summary>
        public DatabaseOption DatabaseOption { get; set; }

        /// <summary>
        /// 网站域名
        /// </summary>
        public string Domian{ get; set; }
    }
}
