using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace LoanProcess.Infrastructure.Caching.Container
{
    public class ObjectCacheContainer : ICacheContainer
    {
        private ICacheStorage _cacheStorage;

        public ObjectCacheContainer(ICacheStorage cacheStorage)
        {
            _cacheStorage = cacheStorage;
        }

        private static ObjectCache Container
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public object this[string key]
        {
            get { return Container[key]; }
            set { Container[key] = value; }
        }

        public long GetCount()
        {
            return Container.Count();
        }

        public bool Contains(string key)
        {
            return Container.Contains(key);
        }

        public void Remove(string key)
        {
            Container.Remove(key);
        }

        public void Push(CacheItem cacheItem)
        {
            Container.Add(cacheItem.Key, cacheItem.Value, null);
        }

        public T Pull<T>(string key)
        {
            var storedItems = Container.Get(key) ?? default(T);
            return (T)storedItems;
        }

        public void Flush()
        {
            RemoveByCondition(_ => true);
        }

        /// <summary>
        /// Never loop through the cache directly and remove items.As you'll get an error "collection was modified, enumeration may not execute".
        /// </summary>
        public void RemoveByCondition(Func<CacheItem, bool> condition = null)
        {
            if (condition == null)
            {
                condition = _ => true; // default behaviour
            }

            var cacheItems = Container.AsEnumerable().Select(item => new CacheItem(item.Key, null)).ToList();
            foreach (var cacheItem in cacheItems.Where(cacheItem => condition(cacheItem)))
            {
                Container.Remove(cacheItem.Key);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            _cacheStorage.Dispose();
            _cacheStorage = null;
        }
    }
}
