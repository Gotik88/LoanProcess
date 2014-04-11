// ============================================================================
// <copyright file="IExecutionContextProvider.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.UnitOfWork
{
    public interface IExecutionContextProvider
    {
        /// <summary>
        /// Creates the execution context.
        /// </summary>
        /// <returns>The execution context instance.</returns>
        IExecutionContext CreateContext();
    }
}
