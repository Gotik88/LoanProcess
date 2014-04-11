// ============================================================================
// <copyright file="TransactQueryScope.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.UnitOfWork
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Transactions;

    /// <summary>
    /// The implementation of the <see cref="IQueryScope"/> interaface,
    /// based on the <see cref="TransactionScope"/> functionality.
    /// </summary>
    public class TransactQueryScope : QueryScopeBase
    {
        /// <summary>
        /// The transaction scope.
        /// </summary>
        private readonly TransactionScope _scope;

        /// <summary>
        /// The disposed flag.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactQueryScope"/> class.
        /// </summary>
        /// <param name="scope">The transaction scope.</param>
        /// <exception cref="ArgumentNullException">The scope is null</exception>
        public TransactQueryScope(TransactionScope scope)
        {
            Contract.Requires<ArgumentNullException>(scope != null);

            _scope = scope;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactQueryScope"/> class.
        /// </summary>
        /// <param name="scope">The transaction scope.</param>
        /// <param name="attemptLimit">The limit of execution attempts.</param>
        /// <exception cref="ArgumentNullException">The scope is null</exception>
        public TransactQueryScope(TransactionScope scope, int attemptLimit)
            : base(new RecoverableExecutionContext(attemptLimit))
        {
            Contract.Requires<ArgumentNullException>(scope != null);

            _scope = scope;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactQueryScope"/> class.
        /// </summary>
        /// <param name="scope">The transaction scope.</param>
        /// <param name="executionContext">The execution context.</param>
        public TransactQueryScope(TransactionScope scope, IExecutionContext executionContext)
            : base(executionContext)
        {
            Contract.Requires<ArgumentNullException>(scope != null);

            _scope = scope;
        }

        /// <summary>
        /// Creates an instance of scope from configuration file.
        /// </summary>
        /// <param name="applyOnChanges">
        /// The value indicating whether the process is data changes or data loading.
        /// </param>
        /// <returns>The scope instance.</returns>
        public static TransactQueryScope Create(bool applyOnChanges)
        {
            var provider = TransactQueryScopeProvider.Create(applyOnChanges);
            var context = provider.CreateContext();

            return (TransactQueryScope)provider.CreateScope(context);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// Indicates whether it is a managed disposing or unmanaged.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (ExecutionContext.Status == ExecutionStatus.Completed)
                {
                    _scope.Complete();
                }

                _scope.Dispose();
            }

            _disposed = true;
        }
    }
}
