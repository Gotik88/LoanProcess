

namespace LoanProcess.Infrastructure.Caching
{
    using System;

    using System.Linq;
    using LoanProcess.Infrastructure.Caching.RefreshCache;
    using System.Collections.Generic;

    using System.Collections;
    using LoanProcess.Infrastructure.Caching.CacheGone;

    public class CacheItem
    {
        public CacheItem(string key, object value)
        {
            Key = key;
            Value = value;
            LastAccessedTime = DateTime.Now;

            InitializeExpirations();
            InitializeFlushing();
        }

        internal void Replace(object value)
        {
            InitializeExpirations();
        }

        public object Value { get; private set; }

        public string Key { get; private set; }

        public DateTime LastAccessedTime { get; private set; }

        public IList<ICacheExpiration> Expirations { get; private set; }

        /// <summary>
        /// Intended to be used internally only. Returns object used to refresh expired CacheItems.
        /// </summary>
        /*public ICacheItemRefreshAction RefreshAction
        {
            get { return refreshAction; }
        }*/

        public bool HasExpired()
        {
            return Expirations.Any(expiration => expiration.HasExpired());
        }

        public void SetLastAccessedTime(DateTime lastAccessedTime)
        {
            LastAccessedTime = lastAccessedTime;
        }

        public CacheItemType GetCacheItemType()
        {
            return CacheItemType.Object;
        }

        private void InitializeExpirations()
        {
            Expirations = CacheItemExpirationsFactory.GetCacheExpirations(this);
        }

        private void InitializeFlushing()
        {

        }
    }
}
