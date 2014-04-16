// ============================================================================
// <copyright file="CacheExtensions.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Expiration
{
    using System;

    public static class CacheExtensions
    {
        public static T Get<T>(this CacheManagerBase cacheManager, string key, Func<T> getValue)
        {
            if (cacheManager.Contains(key) && !cacheManager.Get<T>(key).HasExpired())
            {
                return (T)cacheManager.Get<T>(key).Value;
            }

            var result = getValue();
            cacheManager.Add(key, result);

            return result;
        }

        public static CacheItem Add(this CacheManagerBase cacheManager, string key, object value)
        {
            return cacheManager.Add(key, value);
        }

        #region Time based Expirations

        public static CacheItem WithSlidingExpiration(this CacheItem cacheItem, TimeSpan expirationTime)
        {
            cacheItem.AddExpiration(new SlidingTimeExpiration(cacheItem.LastAccessedTime, expirationTime));
            return cacheItem;
        }

        public static CacheItem WithAbsoluteExpiration(this CacheItem cacheItem, TimeSpan expirationTime)
        {
            cacheItem.AddExpiration(new AbsoluteTimeExpiration(expirationTime));
            return cacheItem;
        }

        #endregion

        #region Notification based Expirations

        public static CacheItem WithSqlDependencyExpiration(this CacheItem cacheItem, string tableName)
        {
            cacheItem.AddExpiration(new SqlDependencyCacheWatcher(tableName));
            return cacheItem;
        }

        public static CacheItem WithFileDependencyExpiration(this CacheItem cacheItem, string fullFileName)
        {
            cacheItem.AddExpiration(new FileSystemCacheWatcher(fullFileName));
            return cacheItem;
        }

        #endregion
    }
}
