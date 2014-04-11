// ============================================================================
// <copyright file="IQueryScopeProvider.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.UnitOfWork
{
    public interface IQueryScopeProvider
    {
        /// <summary>
        /// Gets a value indicating whether the instance of the <see cref="IQueryScope"/> interface
        /// should be recreated every time when an exception occured during query processing.
        /// </summary>
        bool RecreateScope { get; }

        /// <summary>
        /// Creates an operation scope for a batch of operations.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <returns>The operation scope isntance.</returns>
        IQueryScope CreateScope(IExecutionContext context);
    }
}
