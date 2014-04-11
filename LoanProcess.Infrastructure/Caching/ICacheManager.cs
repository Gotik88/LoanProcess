// ============================================================================
// <copyright file="ICacheManager.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

using System;
using System.ComponentModel;

namespace LoanProcess.Infrastructure.Caching
{
    public interface ICacheManager
    {
        ////ICacheContainer Container { get; }

        /*object this[string key] { get; }

        void Add(string key, object value);

        void Add(string key, object value, CacheItemPriority scavengingPriority);

        bool Contains(string key);

        int CacheItemsCount { get; }

        void Flush();

        object Get(string key);

        void Remove(string key);

        void RemoveByCondition(Func<CacheItem> predicate);*/

        object Get(string key);

        bool Contains(string key);
    }
}
