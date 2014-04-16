// ============================================================================
// <copyright file="ICacheManager.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching
{
    using System;
    using LoanProcess.Infrastructure.Caching.Expiration;

    public abstract class CacheManagerBase
    {
        internal abstract CacheItem Add(string key, object value, params ICacheExpiration[] expirations);

        internal abstract bool Contains(string key);

        internal abstract void Clear();

        internal abstract CacheItem Get<T>(string key);

        internal abstract void Remove(string key);

        internal abstract void RemoveByCondition(Func<CacheItem> predicate);
    }
}
