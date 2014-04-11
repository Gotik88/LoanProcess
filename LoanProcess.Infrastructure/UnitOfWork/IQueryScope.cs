// ============================================================================
// <copyright file="IQueryScope.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.UnitOfWork
{
    using System;
    using System.Threading;

    /// <summary>
    /// The interface serves as a scope for a batch of related operations.
    /// </summary>
    public interface IQueryScope : IDisposable
    {
        /// <summary>
        /// Gets the lock object for safe multithreading operations.
        /// </summary>
        ReaderWriterLockSlim Lock { get; }

        /// <summary>
        /// Gets the execution context for this scope.
        /// </summary>
        IExecutionContext ExecutionContext { get; }
    }
}
