using System;

namespace LoanProcess.Infrastructure.Caching
{
    public interface ICacheContainer : IDisposable
    {
        object this[string key] { get; set; }

        bool Contains(string key);

        void Push(CacheItem cacheItem);

        T Pull<T>(string key);

        void Remove(string key);

        long GetCount();

        void Flush();

        void RemoveByCondition(Func<CacheItem, bool> predicate);
    }
}
