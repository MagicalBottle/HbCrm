using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace HbCrm.Core.Data
{
    public partial interface IRepository<TEntity> where TEntity : class
    {
        #region Properties
        
        /// <summary>
        /// 实体的集合
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// 实体的集合（不被跟踪），意味着就算查询得到这些实体被改变了，也不会更新到数据库中。相当于只读数据
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体唯一标识</param>
        /// <returns></returns>
        TEntity GetById(object id);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Insert(TEntity entity);

        /// <summary>
        /// 插入多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(TEntity entity);

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// 开启关闭懒加载
        /// </summary>
        /// <param name="enabled">true 开启，false 关闭</param>
        void LazyLoadingEnabled(bool enabled);

        /// <summary>
        /// 将实体从context中分离
        /// </summary>
        /// <typeparam name="TEntity">实体的类型</typeparam>
        /// <param name="entity">要分离的实体实例</param>
        void Detach(TEntity entity);

        #endregion
    }
}
