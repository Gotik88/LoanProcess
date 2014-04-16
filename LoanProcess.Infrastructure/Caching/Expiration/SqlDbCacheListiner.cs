// ============================================================================
// <copyright file="SqlDbCacheListiner.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Expiration
{
    using System.Data.Sql;

    public class SqlDbCacheListiner : CacheDependencyListenerBase
    {
        private readonly CacheManagerBase _cacheManager;

        public SqlDbCacheListiner(CacheManagerBase cacheManager, SqlNotificationRequest request)
        {
            _cacheManager = cacheManager;
            // should implement SqlNotificationRequest and attach to it property
        }
    }
}
