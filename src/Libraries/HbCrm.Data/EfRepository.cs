using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HbCrm.Core;
using HbCrm.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace HbCrm.Data
{
    public partial class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields

        /// <summary>
        /// context
        /// </summary>
        private readonly IDbContext _context;

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

        public EfRepository(IDbContext context)
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
        public void Update(TEntity entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            try
            {
                this.Entities.Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        public void Update(IEnumerable<TEntity> entities)
        {

            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            try
            {
                this.Entities.UpdateRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Delete(TEntity entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            try
            {
                this.Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        public void Delete(IEnumerable<TEntity> entities)
        {

            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            try
            {
                this.Entities.RemoveRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }


        /// <summary>
        /// 开启关闭懒加载
        /// </summary>
        /// <param name="enabled">true 开启，false 关闭</param>
        public void LazyLoadingEnabled(bool enabled)
        {
            _context.LazyLoadingEnabled(enabled);
        }


        /// <summary>
        /// 将实体从context中分离
        /// </summary>
        /// <typeparam name="TEntity">实体的类型</typeparam>
        /// <param name="entity">要分离的实体实例</param>
        public void Detach(TEntity entity)
        {
            _context.Detach(entity);
        }

        /// <summary>
        /// 开启事务执行
        /// </summary>
        /// <param name="action"></param>
        /// <returns>result 大于0成功</returns>
        public int BeginTransaction(Action action)
        {
           return _context.BeginTransaction(action);
        }

        #endregion
    }
}
