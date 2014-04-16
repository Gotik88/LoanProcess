// ============================================================================
// <copyright file="CacheItem.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using LoanProcess.Infrastructure.Caching.Expiration;

    public class CacheItem
    {
        public CacheItem(string key, object value, params ICacheExpiration[] expirations)
        {
            Key = key;
            Value = value;
            LastAccessedTime = DateTime.Now;

            InitializeExpirations(expirations);
            InitializeFlushing();
        }

        public void AddExpiration(ICacheExpiration expiration)
        {
            Expirations.Add(expiration);
        }

        public void AddExpirations(ICacheExpiration[] expirations)
        {
            expirations.ToList().ForEach(AddExpiration);
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

        private void InitializeExpirations(ICacheExpiration[] expirations)
        {
            Expirations = expirations.Any() ? expirations : CacheItemExpirationsFactory.GetCacheExpirations(this);
        }

        private void InitializeFlushing()
        {

        }
    }
}
