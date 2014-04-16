// ============================================================================
// <copyright file="CacheDependencyChangedEventArgs.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Expiration
{
    using System;
    using LoanProcess.Infrastructure.Caching.RefreshCache.Expiration.NotificationExpiration;

    public class CacheDependencyChangedEventArgs : EventArgs
    {
        public CacheDependencyChangedEventArgs(object data, CacheDependencyChangeTypes changeType)
        {
            Data = data;
            ChangeType = changeType;
        }

        public Object Data { get; set; }

        public CacheDependencyChangeTypes ChangeType { get; set; }
    }
}
