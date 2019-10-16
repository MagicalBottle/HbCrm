using HbCrm.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HbCrm.Data
{
    public partial class HbCrmContext : DbContext, IDbContext
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

        /// <summary>
        /// 返回一个泛型的DbSet，用来查询或者保存实体的实例
        /// </summary>
        /// <typeparam name="TEntity">实体的类型</typeparam>
        /// <returns>给定的实体类型的实例的DbSet</returns>
        protected virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }


        /// <summary>
        /// 生成一个脚本，脚本用来生成当前的所有实体对应的表
        /// </summary>
        /// <returns>A SQL script</returns>
        public virtual string GenerateCreateScript()
        {
            return this.Database.GenerateCreateScript();
        }

        /// <summary>
        /// 执行sql得到linq查询语句，以便用linq操作
        /// </summary>
        /// <typeparam name="TQuery">要得到的类型</typeparam>
        /// <param name="sql">执行的sql</param>
        /// <returns>linq查询语句</returns>
        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class
        {
            return this.Query<TQuery>().FromSql(sql);
        }

        /// <summary>
        /// 执行sql得到linq查询语句，以便用linq操作
        /// </summary>
        /// <typeparam name="TQuery">要得到的类型</typeparam>
        /// <param name="sql">执行的sql</param>
        /// <param name="parameters">执行sql中的参数值</param>
        /// <returns>linq查询语句</returns>
        public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return this.Set<TEntity>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">要执行的sql</param>
        /// <param name="doNotEnsureTransaction">true，不使用事务；false 使用事务。默认false，使用事务</param>
        /// <param name="timeout">执行sql的超时时间，和连接超时时间不同。通常在数据库连接字符串中设置</param>
        /// <param name="parameters">执行的sql中用到的参数</param>
        /// <returns>执行的条数</returns>
        public int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            //设置执行超时时间
            var previousTimeout = this.Database.GetCommandTimeout();
            this.Database.SetCommandTimeout(timeout);
            var result = 0;
            if (!doNotEnsureTransaction)
            {
                using (var transaction = this.Database.BeginTransaction())
                {
                    result = this.Database.ExecuteSqlCommand(sql, parameters);
                    transaction.Commit();
                }
            }
            else
            {
                result = this.Database.ExecuteSqlCommand(sql, parameters);
            }
            //执行完后，要恢复原来的设置
            this.Database.SetCommandTimeout(previousTimeout);

            return result;
        }

        /// <summary>
        /// 将实体从context中分离
        /// </summary>
        /// <typeparam name="TEntity">实体的类型</typeparam>
        /// <param name="entity">要分离的实体实例</param>
        public void Detach<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var entityEntry = this.Entry(entity);
            if (entityEntry == null)
            {
                return;
            }
            entityEntry.State = EntityState.Detached;
        }

        /// <summary>
        /// 开启关闭懒加载
        /// </summary>
        /// <param name="enabled">true 开启，false 关闭</param>
       public void LazyLoadingEnabled(bool enabled)
        {
            base.ChangeTracker.LazyLoadingEnabled = enabled;  
        }
        #endregion


        #region Utilities

        /// <summary>
        /// 把参数添加到sql中
        /// </summary>
        /// <param name="sql">要执行的sql</param>
        /// <param name="parameters">参数</param>
        /// <returns>sql</returns>
        protected virtual string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is DbParameter parameter))
                {
                    continue;
                }

                sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";
                if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                {
                    sql = $"{sql} output";
                }
            }
            return sql;
        }

        /// <summary>
        /// 开启事务执行
        /// </summary>
        /// <param name="action"></param>
        /// <returns>result 大于0成功</returns>
        public int BeginTransaction(Action action)
        {
            int result = -1;
            if (Database.CurrentTransaction == null)
            {
                using (var transaction = Database.BeginTransaction())
                {
                    try
                    {
                        action.Invoke();
                        transaction.Commit();
                        result = 1;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            else
            {
                action.Invoke();
                result = 1;
            }
            return result;
        }

        #endregion
    }
}
