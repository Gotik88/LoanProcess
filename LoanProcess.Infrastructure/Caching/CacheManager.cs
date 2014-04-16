// ============================================================================
// <copyright file="CacheManager.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LoanProcess.Infrastructure.Caching.Expiration;
    using LoanProcess.Infrastructure.Caching.Storage;

    internal sealed class CacheManager : CacheManagerBase
    {
        private readonly ICacheStorage _cacheStorage;

        readonly IList<CacheItem> _cacheItems = new List<CacheItem>();

        public CacheManager()
        {
            _cacheStorage = new HttpContextCacheStorage();
        }

        internal override CacheItem Add(string key, object value, params ICacheExpiration[] expirations)
        {
            var cacheItem = new CacheItem(key, value, expirations);
            var result = _cacheStorage.Store(cacheItem);

            if (result != StoreResult.Success) return null;
            _cacheItems.Add(cacheItem);
            return cacheItem;
        }

        internal override bool Contains(string key)
        {
            return _cacheStorage.Contains(key);
        }

        internal override void Clear()
        {
            _cacheStorage.Flush();
        }

        internal override CacheItem Get<T>(string key)
        {
            var value = (object)_cacheStorage.Retrieve<T>(key);
            return value != null ? _cacheItems.SingleOrDefault(i => i.Key.Equals(key)) : null;
        }

        internal override void Remove(string key)
        {
            _cacheStorage.Remove(key);
        }

        internal override void RemoveByCondition(Func<CacheItem> predicate)
        {
        }
    }
}
