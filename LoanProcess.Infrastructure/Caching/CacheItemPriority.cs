// ============================================================================
// <copyright file="CacheItemPriority.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching
{
    public enum CacheItemPriority
    {
        None,

        Low,

        Normal,

        High,

        NotRemovable
    }
}
