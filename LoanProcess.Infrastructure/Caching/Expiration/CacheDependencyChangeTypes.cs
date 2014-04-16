// ============================================================================
// <copyright file="CacheDependencyChangeTypes.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Expiration
{
    public enum CacheDependencyChangeTypes
    {
        Created,

        Deleted,

        Changed,

        Renamed
    }
}
