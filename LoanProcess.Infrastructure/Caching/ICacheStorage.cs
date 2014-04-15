

namespace LoanProcess.Infrastructure.Caching
{
    using System;

    public interface ICacheStorage : IDisposable
    {
        void Remove(string key);

        void Store(CacheItem cacheItem);

        T Retrieve<T>(string key);

        ////Hashtable Load();

        void Flush();
    }
}
