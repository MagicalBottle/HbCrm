using HbCrm.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HbCrm.Data
{
    public partial class HbCrmContext : DbContext
    {

        #region Ctor
        public HbCrmContext(DbContextOptions<HbCrmContext> options)
            : base(options)
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// nopcommerence的写法
        /// 进一步配置模型
        /// ①可以使用链式操作配置模型 modelBuilder.Entity<Blog>().ToTable("blogs").Property(b => b.BlogId).HasColumnName("blog_id");
        /// ②可以采用实现IEntityTypeConfiguration或者IQueryTypeConfiguration接口的类，然后调用modelBuilder.ApplyConfiguration(实现接口的配置类)
        ///     实现接口的类中实现方法Configure()在方法中配置模型
        ///     因为有两个接口，所以这里自定义一个接口IMappingConfiguration，用来统一上面两个接口。
        ///     也可以不用定义接口，但是要分别遍历两个接口的实现类，直接modelBuilder.ApplyConfiguration(实现接口的配置类)
        ///     
        ///     ***modelBuilder.ApplyConfigurationsFromAssembly();或者直接用这个方法，自动读取****
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //动态加载所有的配置
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                      (type.BaseType?.IsGenericType ?? false)
                      && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                          || type.BaseType.GetGenericTypeDefinition() == typeof(QueryTypeConfiguration<>)));
            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration.ApplyConfiguration(modelBuilder);
                //如果不使用统一接口IMappingConfiguration
                //遍历的时候区分一下接口
                //(NopEntityTypeConfiguration) Activator.CreateInstance(typeConfiguration);
                //NopEntityTypeConfiguration:IEntityTypeConfiguration
                //modelBuilder.ApplyConfiguration(typeConfiguration);
            }
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
