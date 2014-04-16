// ============================================================================
// <copyright file="ObjectCacheStorage.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Storage
{
    using System;
    using System.Linq;

    using System.Runtime.Caching;

    internal class ObjectCacheStorage : CacheStorageBase
    {
        public ObjectCacheStorage(long maxSize = 0)
            : base(maxSize) { }

        private static ObjectCache Container
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public override object this[string key]
        {
            get { return Container[key]; }
            set { Container[key] = value; }
        }

        public long GetCount()
        {
            return Container.Count();
        }

        public override bool Contains(string key)
        {
            return Container.Contains(key);
        }

        public override void Remove(string key)
        {
            Container.Remove(key);
        }

        public override T Retrieve<T>(string key)
        {
            var storedItems = Container.Get(key) ?? default(T);
            return (T)storedItems;
        }

        public virtual StoreResult Store(CacheItem cacheItem)
        {
            StoreResult result;
            LockObject.AcquireWriterLock(-1);

            try
            {
                Container.Add(cacheItem.Key, cacheItem.Value, null); // we don't need any CacheItemPolicy, as we will use it from CacheItem
                result = StoreResult.Success;
            }
            finally
            {
                LockObject.ReleaseWriterLock();
            }

            return result;
        }

        public override void Flush()
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
    }
}
