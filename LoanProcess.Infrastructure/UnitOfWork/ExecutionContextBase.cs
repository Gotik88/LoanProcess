// ============================================================================
// <copyright file="ExecutionContextBase.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.UnitOfWork
{
    using System;

    /// <summary>
    /// The base implementation of the <see cref="IExecutionContext"/> interface.
    /// </summary>
    public class ExecutionContextBase : IExecutionContext
    {
        /// <summary>
        /// The current execution context.
        /// </summary>
        [ThreadStatic]
        private static ExecutionContextBase _current;

        /// <summary>
        /// The next execution context.
        /// </summary>
        private ExecutionContextBase _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContextBase"/> class.
        /// </summary>
        public ExecutionContextBase()
        {
            Status = ExecutionStatus.Initialized;
        }

        /// <summary>
        /// Gets a value indicating whether the opertaion can be executed.
        /// </summary>
        public virtual bool CanExecute
        {
            get
            {
                return _current == null || _current.Status != ExecutionStatus.Aborted;
            }
        }

        /// <summary>
        /// Gets a context execution status.
        /// </summary>
        public ExecutionStatus Status { get; private set; }

        /// <summary>
        /// Tries to start operation execution.
        /// It is possible to retrying execution several times.
        /// Interface implementation is responsible for counting the attempts.
        /// </summary>
        public virtual void StartExecute()
        {
            if (_current != this)
            {
                _next = _current;
                _current = this;
            }

            Status = ExecutionStatus.Started;
        }

        /// <summary>
        /// Completes operation execution.
        /// </summary>
        public virtual void CompleteExecute()
        {
            _current = _next;
            _next = null;
            Status = ExecutionStatus.Completed;
        }

        /// <summary>
        /// Stops operation execution normally.
        /// It should be possible to start execution again after stopping.
        /// Interface implementation is responsible for context reset.
        /// </summary>
        public virtual void StopExecute()
        {
            Status = ExecutionStatus.Stopped;
        }

        /// <summary>
        /// Aborts operation execution after error.
        /// It is impossible to restart execution after abort.
        /// </summary>
        public virtual void AbortExecute()
        {
            var next = this;
            while (next._next != null)
            {
                next.Status = ExecutionStatus.Aborted;
                next = next._next;
            }

            _current = _next;
            _next = null;
        }
    }
}
