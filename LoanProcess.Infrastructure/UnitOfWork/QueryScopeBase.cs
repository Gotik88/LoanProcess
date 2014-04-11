// ============================================================================
// <copyright file="QueryScopeBase.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.UnitOfWork
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Threading;

    /// <summary>
    /// The base implementation of the <see cref="IQueryScope"/> interface.
    /// </summary>
    public abstract class QueryScopeBase : IQueryScope
    {
        /// <summary>
        /// The lock object.
        /// </summary>
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryScopeBase"/> class.
        /// </summary>
        protected QueryScopeBase()
        {
            ExecutionContext = new RecoverableExecutionContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryScopeBase"/> class.
        /// </summary>
        /// <param name="executionContext">The execution context.</param>
        /// <exception cref="ArgumentNullException">
        /// The execution context is null.
        /// </exception>
        protected QueryScopeBase(IExecutionContext executionContext)
        {
            Contract.Requires<ArgumentNullException>(executionContext != null);

            ExecutionContext = executionContext;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="QueryScopeBase"/> class.
        /// </summary>
        ~QueryScopeBase()
        {
            Dispose(false);
        }

        #region Implementation of IQueryScope

        /// <summary>
        /// Gets the lock object for safe multithreading operations.
        /// </summary>
        public ReaderWriterLockSlim Lock
        {
            get { return _lock; }
        }

        /// <summary>
        /// Gets the execution context for this scope.
        /// </summary>
        public IExecutionContext ExecutionContext { get; private set; }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// Indicates whether it is a managed disposing or unmanaged.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
        }

        #endregion
    }
}
