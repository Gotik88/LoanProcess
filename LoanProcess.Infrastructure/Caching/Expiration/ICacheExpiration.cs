// ============================================================================
// <copyright file="ICacheExpiration.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Expiration
{
    public interface ICacheExpiration
    {
        /// <summary>
        ///	Specifies if CacheItem has expired or not.
        /// </summary>
        /// <returns>Returns true if the item has expired, otherwise false.</returns>
        bool HasExpired();
    }
}
