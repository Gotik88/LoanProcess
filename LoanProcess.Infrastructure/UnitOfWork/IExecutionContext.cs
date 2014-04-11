// ============================================================================
// <copyright file="IExecutionContext.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.UnitOfWork
{
    /// <summary>
    /// The execution context for a batch of related operations.
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// Gets a value indicating whether the opertaion can be executed.
        /// </summary>
        bool CanExecute { get; }

        /// <summary>
        /// Gets a context execution status.
        /// </summary>
        ExecutionStatus Status { get; }

        /// <summary>
        /// Tries to start operation execution.
        /// It is possible to retrying execution several times.
        /// </summary>
        void StartExecute();

        /// <summary>
        /// Completes operation execution.
        /// </summary>
        void CompleteExecute();

        /// <summary>
        /// Stops operation execution normally.
        /// It should be possible to start execution again after stopping.
        /// Interface implementation is responsible for context reset.
        /// </summary>
        void StopExecute();

        /// <summary>
        /// Aborts operation execution after error.
        /// It is impossible to restart execution after abort.
        /// </summary>
        void AbortExecute();
    }
}
