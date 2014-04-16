// ============================================================================
// <copyright file="CacheStorageBase.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Storage
{
    using System.Threading;

    internal abstract class CacheStorageBase : ICacheStorage
    {
        protected ReaderWriterLock _lockObject;

        private long _maxSize;

        protected CacheStorageBase(long maxSize)
        {
            this._lockObject = new ReaderWriterLock();
            this._maxSize = maxSize;
        }

        public ReaderWriterLock LockObject
        {
            get
            {
                return this._lockObject;
            }
        }

        public abstract object this[string key] { get; set; }

        public abstract bool Contains(string key);

        public abstract void Remove(string key);

        public abstract StoreResult Store(CacheItem cacheItem);

        public abstract void Flush();

        public abstract T Retrieve<T>(string key);

        public virtual void Dispose()
        {
            this._lockObject = null;
            this.Flush();
        }
    }
}
