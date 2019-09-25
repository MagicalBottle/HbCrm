using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HbCrm.Core.Caching
{
    /// <summary>
    /// 缓存管理。因为微软IDistributedCache分布式缓存没有提供根据前缀获取的方法，也无法获取全部keys，那就无法根据前缀删除或全部删除。所以引用了第三方，或者自己封装
    /// </summary>
    public interface ICacheManager: IDisposable
    {

        /// <summary>
        /// 获取缓存，如果没有缓存，则生成缓存
        /// </summary>
        /// <typeparam name="T">缓存的类型</typeparam>
        /// <param name="key">key</param>
        /// <returns>缓存值</returns>
        T Get<T>(string key);

        /// <summary>
        /// 获取缓存，如果没有缓存，则生成缓存
        /// </summary>
        /// <typeparam name="T">缓存的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="acquire">如果缓存不存在，生成缓存的方法</param>
        /// <param name="cacheTime">缓存时间（单位分钟）。如果为0则不缓存，如果为null则用默认时间</param>
        /// <returns>缓存值</returns>
        T Get<T>(string key, Func<T> acquire, int? cacheTime = null);

        /// <summary>
        /// 获取缓存，如果没有缓存，则生成缓存
        /// </summary>
        /// <typeparam name="T">缓存的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="acquire">如果缓存不存在，生成缓存的方法</param>
        /// <param name="cacheTime">缓存时间（单位分钟）。如果为0则不缓存，如果为null则用默认时间</param>
        /// <returns>缓存值</returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int? cacheTime = null);

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存时间（单位分钟）。如果为0则不缓存，如果为null则用默认时间</param>
        void Set(string key, object data, int? cacheTime = null);


        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <typeparam name="T">缓存的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存时间（单位分钟）。如果为0则不缓存，如果为null则用默认时间</param>
        void Set<T>(string key, T data, int? cacheTime = null);

        /// <summary>
        /// 是否存在key值对应的缓存值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true，存在；false，不存在</returns>
        bool IsSet(string key);

        /// <summary>
        /// 移除key值对应的缓存
        /// </summary>
        /// <param name="key">key</param>
        void Remove(string key);

        /// <summary>
        /// 根据前缀删除缓存
        /// </summary>
        /// <param name="prefix">前缀</param>
        void RemoveByPrefix(string prefix);

        /// <summary>
        /// 清空所有的缓存
        /// </summary>
        void Clear();
    }
}
