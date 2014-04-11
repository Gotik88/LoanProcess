// ============================================================================
// <copyright file="RecoverableExecutionContext.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.UnitOfWork
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The implementation of the <see cref="IExecutionContext"/>
    /// for recoverable executions.
    /// </summary>
    public class RecoverableExecutionContext : ExecutionContextBase
    {
        /// <summary>
        /// The limit of execution attempts.
        /// </summary>
        private readonly int _attemptLimit = 1;

        /// <summary>
        /// The count of execution attempts.
        /// </summary>
        private int _attemptCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecoverableExecutionContext"/> class.
        /// </summary>
        public RecoverableExecutionContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecoverableExecutionContext"/> class.
        /// </summary>
        /// <param name="attemptLimit">The limit of execution attempts.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The attempt limit less than one.
        /// </exception>
        public RecoverableExecutionContext(int attemptLimit)
        {
            Contract.Requires<ArgumentOutOfRangeException>(attemptLimit >= 1);

            _attemptLimit = attemptLimit;
        }

        /// <summary>
        /// Gets a value indicating whether the opertaion can be executed.
        /// </summary>
        public override bool CanExecute
        {
            get { return base.CanExecute && _attemptCount < _attemptLimit; }
        }

        /// <summary>
        /// Tries to start operation execution.
        /// It is possible to retrying execution several times.
        /// Interface implementation is responsible for counting the attempts.
        /// </summary>
        public override void StartExecute()
        {
            base.StartExecute();

            _attemptCount++;
        }

        /// <summary>
        /// Aborts operation execution after error.
        /// It is impossible to restart execution after abort.
        /// </summary>
        public override void AbortExecute()
        {
            base.AbortExecute();

            _attemptCount = _attemptLimit;
        }
    }
}
