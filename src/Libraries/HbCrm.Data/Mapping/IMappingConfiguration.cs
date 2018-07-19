using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Data.Mapping
{
    /// <summary>
    /// 配置context模型
    /// </summary>
   public partial interface IMappingConfiguration
    {
        /// <summary>
        /// 请求配置模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
