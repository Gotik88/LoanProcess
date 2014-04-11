
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace LoanProcess.Infrastructure.Caching.Storage
{
    internal class HttpContextCacheStorage : CacheStorageBase
    {
        public HttpContextCacheStorage(long maxSize = 0)
            : base(maxSize) { }

        private static Cache Container
        {
            get
            {
                return HttpContext.Current.Cache;
            }
        }

        public override object this[string key]
        {
            get { return Container[key]; }
            set { Container[key] = value; }
        }

        public override bool Contains(string key)
        {
            var enumerator = Container.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        public override void Remove(string key)
        {
            Container.Remove(key);
        }

        public override StoreResult Store(CacheItem cacheItem)
        {
            StoreResult result;
            LockObject.AcquireWriterLock(-1);

            try
            {
                Container.Insert(cacheItem.Key, cacheItem.Value);
                result = StoreResult.Success;
            }
            finally
            {
                LockObject.ReleaseWriterLock();
            }

            return result;
        }

        public override T Retrieve<T>(string key)
        {
            var storedItems = Container.Get(key) ?? default(T);
            return (T)storedItems;
        }

        public override void Flush()
        {
            LockObject.AcquireWriterLock(-1);

            try
            {
                RemoveByCondition(_ => true);
            }
            finally
            {
                LockObject.ReleaseWriterLock();
            }
        }

        /// <summary>
        /// Never loop through the cache directly and remove items.As you'll get an error "collection was modified, enumeration may not execute".
        /// </summary>
        private static void RemoveByCondition(Func<CacheItem, bool> condition)
        {
            if (condition == null)
            {
                condition = _ => true; // default behaviour
            }

            var cacheItems = new List<CacheItem>();
            var enumerator = Container.GetEnumerator();

            while (enumerator.MoveNext())
            {
                cacheItems.Add(new CacheItem(enumerator.Key.ToString(), null));
            }

            foreach (var cacheItem in cacheItems.Where(cacheItem => condition(cacheItem)))
            {
                Container.Remove(cacheItem.Key);
            }
        }
    }
}
