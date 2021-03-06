﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCrm.Data.Mapping
{
    public abstract partial class QueryTypeConfiguration<TQuery> : IMappingConfiguration, IQueryTypeConfiguration<TQuery> where TQuery : class
    {

        #region Methods

        /// <summary>
        /// 开发者自定义配置
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void PostConfigure(QueryTypeBuilder<TQuery> builder)
        {

        }

        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        /// <summary>
        /// 配置模型
        /// </summary>
        /// <param name="builder"></param>
        public virtual void Configure(QueryTypeBuilder<TQuery> builder)
        {
            //开发者自定义配置，重写PostConfigure，
            //重写Configure必须调用base.Configure才会执行这里
            this.PostConfigure(builder);
        }

        #endregion

    }
}
