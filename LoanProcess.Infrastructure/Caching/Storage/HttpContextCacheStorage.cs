
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace LoanProcess.Infrastructure.Caching.Storage
{
    public class HttpContextCacheStorage : ICacheStorage
    {

        public void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public void Store(CacheItem cacheItem)
        {
            HttpContext.Current.Cache.Insert(cacheItem.Key, cacheItem.Value);
        }

        public T Retrieve<T>(string key)
        {
            var storedItems = HttpContext.Current.Cache.Get(key) ?? default(T);
            return (T)storedItems;
        }

        public void Flush()
        {
            RemoveByCondition(_ => true);
        }

        /// <summary>
        /// Never loop through the cache directly and remove items.As you'll get an error "collection was modified, enumeration may not execute".
        /// </summary>
        private void RemoveByCondition(Func<CacheItem, bool> condition = null)
        {
            if (condition == null)
            {
                condition = _ => true; // default behaviour
            }

            var cacheItems = new List<CacheItem>();
            var enumerator = HttpContext.Current.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                cacheItems.Add(new CacheItem(enumerator.Key.ToString(), null));
            }

            foreach (var cacheItem in cacheItems.Where(cacheItem => condition(cacheItem)))
            {
                HttpContext.Current.Cache.Remove(cacheItem.Key);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
