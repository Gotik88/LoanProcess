// ============================================================================
// <copyright file="ICacheStorage.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Storage
{
    using System;

    public interface ICacheStorage : IDisposable
    {
        object this[string key] { get; set; }

        StoreResult Store(CacheItem cacheItem);

        T Retrieve<T>(string key);

        bool Contains(string key);

        void Remove(string key);

        void Flush();
    }
}
