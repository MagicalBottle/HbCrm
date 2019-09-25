using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using EasyCaching.Core;
using System.Threading.Tasks;

namespace HbCrm.Core.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IEasyCachingProvider _provider;

        public MemoryCacheManager(IEasyCachingProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 获取缓存，如果没有缓存，则生成缓存
        /// </summary>
        /// <typeparam name="T">缓存的类型</typeparam>
        /// <param name="key">key</param>
        /// <returns>缓存值</returns>
        public T Get<T>(string key)
        {
            return _provider.Get<T>(key).Value;
        }


        /// <summary>
        /// 获取缓存，如果没有缓存，则生成缓存
        /// </summary>
        /// <typeparam name="T">缓存的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="acquire">如果缓存不存在，生成缓存的方法</param>
        /// <param name="cacheTime">缓存时间（单位分钟）。如果为0则不缓存，如果为null则用默认时间</param>
        /// <returns>缓存值</returns>
        public T Get<T>(string key, Func<T> acquire, int? cacheTime = null)
        {
            if (cacheTime <= 0)
            {
                return acquire();
            }
            return _provider.Get<T>(key, acquire, TimeSpan.FromMinutes(cacheTime ?? HbCrmCachingDefaults.CacheTime))
                .Value;
        }

        /// <summary>
        /// 获取缓存，如果没有缓存，则生成缓存
        /// </summary>
        /// <typeparam name="T">缓存的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="acquire">如果缓存不存在，生成缓存的方法</param>
        /// <param name="cacheTime">缓存时间（单位分钟）。如果为0则不缓存，如果为null则用默认时间</param>
        /// <returns>缓存值</returns>
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int? cacheTime = null)
        {
            if (cacheTime <= 0)
            {
                return await acquire();
            }

            var t = await _provider.GetAsync(key, acquire, TimeSpan.FromMinutes(cacheTime ?? HbCrmCachingDefaults.CacheTime));
            return t.Value;
        }

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存时间（单位分钟）。如果为0则不缓存，如果为null则用默认时间</param>
        public void Set(string key, object data, int? cacheTime=null)
        {
            if (cacheTime <= 0)
            {
                return;
            }
            _provider.Set(key, data, TimeSpan.FromMinutes(cacheTime ?? HbCrmCachingDefaults.CacheTime));
        }

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <typeparam name="T">缓存的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存时间（单位分钟）。如果为0则不缓存，如果为null则用默认时间</param>
        public void Set<T>(string key, T data, int? cacheTime = null)
        {
            if (cacheTime <= 0)
            {
                return;
            }
            _provider.Set(key, data, TimeSpan.FromMinutes(cacheTime ?? HbCrmCachingDefaults.CacheTime));
        }


        /// <summary>
        /// 是否存在key值对应的缓存值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true，存在；false，不存在</returns>
        public bool IsSet(string key)
        {
            return _provider.Exists(key);
        }

        /// <summary>
        /// 移除key值对应的缓存
        /// </summary>
        /// <param name="key">key</param>
        public void Remove(string key)
        {
            _provider.Remove(key);
        }
        
        /// <summary>
        /// 根据前缀删除缓存
        /// </summary>
        /// <param name="prefix">前缀</param>
        public void RemoveByPrefix(string prefix)
        {
            _provider.RemoveByPrefix(prefix);
        }


        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public void Clear()
        {
            _provider.Flush();

        }

        public void Set(string key, object data, int cacheTime)
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
            
        }
    }
}
