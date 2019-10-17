using HbCrm.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HbCrm.Data
{
    /// <summary>
    /// DB context的接口
    /// </summary>
   public partial interface IDbContext
    {
        /// <summary>
        /// 返回一个泛型的DbSet，用来查询或者保存实体的实例
        /// </summary>
        /// <typeparam name="TEntity">实体的类型</typeparam>
        /// <returns>给定的实体类型的实例的DbSet</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;


        /// <summary>
        /// 返回一个泛型的DbSet，用来查询或者保存实体的实例
        /// </summary>
        /// <typeparam name="TEntity">实体的类型</typeparam>
        /// <returns>给定的实体类型的实例的DbSet</returns>
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 把所有在context中的的更改写入的数据库
        /// </summary>
        /// <returns>写入数据库的条数</returns>
        int SaveChanges();


        /// <summary>
        /// 生成一个脚本，脚本用来生成当前的所有实体对应的表
        /// </summary>
        /// <returns>A SQL script</returns>
        string GenerateCreateScript();

        /// <summary>
        /// 执行sql得到linq查询语句，以便用linq操作
        /// </summary>
        /// <typeparam name="TQuery">要得到的类型</typeparam>
        /// <param name="sql">执行的sql</param>
        /// <returns>linq查询语句</returns>
        IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class;


        /// <summary>
        /// 执行sql得到linq查询语句，以便用linq操作
        /// </summary>
        /// <typeparam name="TQuery">要得到的类型</typeparam>
        /// <param name="sql">执行的sql</param>
        /// <param name="parameters">执行sql中的参数值</param>
        /// <returns>linq查询语句</returns>
        IQueryable<TEntity> EntityFromSql<TEntity>(string sql,params object[] parameters) where TEntity : class;

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">要执行的sql</param>
        /// <param name="doNotEnsureTransaction">true，不使用事务；false 使用事务。默认false，使用事务</param>
        /// <param name="timeout">执行sql的超时时间，和连接超时时间不同。通常在数据库连接字符串中设置</param>
        /// <param name="parameters">执行的sql中用到的参数</param>
        /// <example>
        /// _dbContext.ExecuteSqlCommand("EXEC [LanguagePackImport] @LanguageId, @XmlPackage, @UpdateExistingResources",
        ///        false, 600, pLanguageId, pXmlPackage, pUpdateExistingResources);
        /// </example>
        /// <returns>执行的条数</returns>
        int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);


        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">要执行的sql</param>
        /// <param name="parameters">执行的sql中用到的参数</param>
        /// <example>
        /// _dbContext.ExecuteSqlCommand("EXEC [LanguagePackImport] @LanguageId, @XmlPackage, @UpdateExistingResources",
        ///        pLanguageId, pXmlPackage, pUpdateExistingResources);
        /// </example>
        /// <returns>执行的条数</returns>
        int ExecuteSqlCommand(RawSqlString sql,params object[] parameters);

        /// <summary>
        /// 将实体从context中分离
        /// </summary>
        /// <typeparam name="TEntity">实体的类型</typeparam>
        /// <param name="entity">要分离的实体实例</param>
        void Detach<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 开启关闭懒加载
        /// </summary>
        /// <param name="enabled">true 开启，false 关闭</param>
        void LazyLoadingEnabled(bool enabled);

        /// <summary>
        /// 开启事务执行
        /// </summary>
        /// <param name="action"></param>
        /// <returns>result 大于0成功</returns>
         int BeginTransaction(Action action);
    }
}
