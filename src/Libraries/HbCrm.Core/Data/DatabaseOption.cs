using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Core.Data
{
    /// <summary>
    /// 数据库配置
    /// </summary>
   public class DatabaseOption
    {
        public DbTypes DbType { get; set; }
        public string ConnectionString { get; set; }
    }
}
