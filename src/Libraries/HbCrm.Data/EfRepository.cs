using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using HbCrm.Core;
using HbCrm.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HbCrm.Data
{
    public partial class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields

        /// <summary>
        /// context
        /// </summary>
        private readonly HbCrmContext _context;

        /// <summary>
        /// 实体的集合
        /// </summary>
        private DbSet<TEntity> _entities;

        #endregion


        #region Properties

        public virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<TEntity>();
                }
                return _entities;
            }
        }

        /// <summary>
        /// 实体的集合
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;


        /// <summary>
        /// 实体的集合（不被跟踪），意味着就算查询得到这些实体被改变了，也不会更新到数据库中。相当于只读数据
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        #endregion


        #region Ctor

        public EfRepository(HbCrmContext context)
        {
            _context = context;
        }
        #endregion


        #region Utilities

        /// <summary>
        /// 回滚更新出错的实体在context中的状态
        /// </summary>
        /// <param name="exception">DbUpdateException异常</param>
        /// <returns>异常信息</returns>
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //更新到数据库出错（实际在数据库没有更新）要回滚context中这些数据的状态
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                    .ToList();
                entries.ForEach(entry => entry.State = EntityState.Unchanged);
            }

            _context.SaveChanges();
            return exception.ToString();
        }

        #endregion

        #region Methods

        public EntityEntry<TEntity> Entry(TEntity entity)
        {
            return _context.Entry<TEntity>(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体唯一标识</param>
        /// <returns></returns>
        public TEntity GetById(object id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int Insert(TEntity entity)
        {
            int result = -1;
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Entities.Add(entity);
            result = _context.SaveChanges();

            return result;
        }

        /// <summary>
        /// 插入多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        public int Insert(IEnumerable<TEntity> entities)
        {
            int result = -1;
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            this.Entities.AddRange(entities);
            result = _context.SaveChanges();

            return result;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(TEntity entity)
        {

            int result = -1;
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Entities.Update(entity);
            result = _context.SaveChanges();
            return result;
        }

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        public int Update(IEnumerable<TEntity> entities)
        {
            int result = -1;

            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            this.Entities.UpdateRange(entities);
            result = _context.SaveChanges();

            return result;
        }

        /// <summary>
        /// 更新实体，指定要更新的属性
        /// </summary>
        /// <param name="entity">更新实体</param>
        /// <param name="properties">更新的属性</param>
        /// <returns>大于0成功</returns>
        public int Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties) 
        {

            #region 封装成方法更新指定列
            //var entry = _context.Entry(entity);
            //entry.State = EntityState.Modified;
            //var dic = new Dictionary<string, object>() {
            //        { "UserName", "wangwu" }, { "NickName", "王五" }, { "Password", "123456" },
            //         { "Email", "123456" },{ "WeChar", "123456" }
            //    };

            //foreach (var p in entry.Properties)
            //{
            //    bool isModified = false;
            //    foreach (var keyvalue in dic)
            //    {
            //        if (p.Metadata.Name.Equals(keyvalue.Key, StringComparison.InvariantCultureIgnoreCase))
            //        {
            //            p.CurrentValue = dic[keyvalue.Key];
            //            p.IsModified = true;
            //            isModified = true;
            //            break;
            //        }
            //    }
            //    if (!isModified)
            //    {
            //        p.IsModified = isModified;
            //    }
            //}

            //entry.Context.SaveChanges();
            #endregion

            int result = -1;
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var dbEntityEntry = _context.Entry(entity);
            if (properties.Any())
            {
                foreach (var property in properties)
                {
                    dbEntityEntry.Property(property).IsModified = true;
                }
            }
            else
            {
                foreach (var rawProperty in dbEntityEntry.Entity.GetType().GetTypeInfo().DeclaredProperties)
                {
                    var originalValue = dbEntityEntry.Property(rawProperty.Name).OriginalValue;
                    var currentValue = dbEntityEntry.Property(rawProperty.Name).CurrentValue;
                    foreach (var property in properties)
                    {
                        if (originalValue != null && !originalValue.Equals(currentValue))
                            dbEntityEntry.Property(property).IsModified = true;
                    }

                }
            }

            result = _context.SaveChanges();

            return result;
        }


        /// <summary>
        /// 更新实体集合，指定要更新的属性
        /// </summary>
        /// <param name="entities">更新实体集合</param>
        /// <param name="properties">更新的属性</param>
        /// <returns>大于0成功</returns>
        public int UpdateRange(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] properties)
        {
            int result = -1;
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var entity in entities)
            {
                var dbEntityEntry = _context.Entry(entity);
                //Notice that:当更新实体指定属性时，若实体从数据库中查询而出，此时实体已被跟踪，则无需处理，若实例化对象而更新对象指定属性，此时需要将其状态修改为Unchanged即需要附加
                //if (!isNoTracking) { dbEntityEntry.State = EntityState.Unchanged; }
                if (properties.Any())
                {
                    foreach (var property in properties)
                    {
                        dbEntityEntry.Property(property).IsModified = true;
                    }
                }
                else
                {
                    foreach (var rawProperty in dbEntityEntry.Entity.GetType().GetTypeInfo().DeclaredProperties)
                    {
                        var originalValue = dbEntityEntry.Property(rawProperty.Name).OriginalValue;
                        var currentValue = dbEntityEntry.Property(rawProperty.Name).CurrentValue;
                        foreach (var property in properties)
                        {
                            if (originalValue != null && !originalValue.Equals(currentValue))
                                dbEntityEntry.Property(property).IsModified = true;
                        }

                    }
                }
            }

            result = _context.SaveChanges();

            return result;
        }


        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int Delete(TEntity entity)
        {

            int result = -1;
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Entities.Remove(entity);
            result = _context.SaveChanges();

            return result;
        }
        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        public int Delete(IEnumerable<TEntity> entities)
        {

            int result = -1;
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            this.Entities.RemoveRange(entities);
            result = _context.SaveChanges();

            return result;
        }


        /// <summary>
        /// 开启关闭懒加载
        /// </summary>
        /// <param name="enabled">true 开启，false 关闭</param>
        public void LazyLoadingEnabled(bool enabled)
        {
            _context.ChangeTracker.LazyLoadingEnabled = enabled;
        }


        /// <summary>
        /// 将实体从context中分离
        /// </summary>
        /// <typeparam name="TEntity">实体的类型</typeparam>
        /// <param name="entity">要分离的实体实例</param>
        public void Detach(TEntity entity)
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
        /// 开启事务执行
        /// </summary>
        /// <param name="action"></param>
        /// <returns>result 大于0成功</returns>
        public int BeginTransaction(Action action)
        {

            int result = -1;
            if (_context.Database.CurrentTransaction == null)
            {
                using (var transaction = _context.Database.BeginTransaction())
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


        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">要执行的sql</param>
        /// <param name="parameters">执行的sql中用到的参数</param>
        /// <returns>执行的条数</returns>
        public int ExecuteSqlCommand(RawSqlString sql, params object[] parameters)
        {
            var result = 0;
            result = _context.Database.ExecuteSqlCommand(sql, parameters);
            return result;
        }

        #endregion
    }
}
