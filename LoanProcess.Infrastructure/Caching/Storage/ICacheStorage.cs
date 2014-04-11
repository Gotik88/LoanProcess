

using LoanProcess.Infrastructure.Caching.Storage;

namespace LoanProcess.Infrastructure.Caching
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
