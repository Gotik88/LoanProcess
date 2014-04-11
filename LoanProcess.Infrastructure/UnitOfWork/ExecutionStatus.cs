// ============================================================================
// <copyright file="ExecutionStatus.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.UnitOfWork
{
    /// <summary>
    /// The current status of an execution context.
    /// </summary>
    public enum ExecutionStatus
    {
        /// <summary>
        /// The execution context is initialized and ready to start execution.
        /// </summary>
        Initialized,

        /// <summary>
        /// The operation was started successful.
        /// </summary>
        Started,

        /// <summary>
        /// The operation was completed successful.
        /// </summary>
        Completed,

        /// <summary>
        /// The operation was stopped after error and can be executed again.
        /// </summary>
        Stopped,

        /// <summary>
        /// The operation aborted and can't be executed again.
        /// </summary>
        Aborted
    }
}
