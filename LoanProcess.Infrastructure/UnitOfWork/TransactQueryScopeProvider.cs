// ============================================================================
// <copyright file="TransactQueryScopeProvider.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.UnitOfWork
{
    using System;
    using System.Configuration;
    using System.Diagnostics.Contracts;
    using System.Transactions;
    using System.Xml.Linq;

    /// <summary>
    /// The query scope provider for the <see cref="TransactQueryScope"/> class.
    /// </summary>
    public class TransactQueryScopeProvider : IQueryScopeProvider, IExecutionContextProvider
    {
        /// <summary>
        /// The limit of execution attempts.
        /// </summary>
        private int _attemptLimit = 1;

        /// <summary>
        /// Gets or sets the transaction scope option,
        /// which to be applied for created query scope
        /// </summary>
        public TransactionScopeOption TransactionScopeOption { get; set; }

        /// <summary>
        /// Gets or sets the transaction isolation level,
        /// which to be applied for created query scope
        /// </summary>
        public IsolationLevel IsolationLevel { get; set; }

        /// <summary>
        /// Gets or sets the transaction timeout,
        /// which to be applied for created query scope
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Gets or sets the limit of execution attempts.
        /// </summary>
        public int AttemptLimit
        {
            get
            {
                return _attemptLimit;
            }

            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 1);

                _attemptLimit = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the instance of the <see cref="IQueryScope"/> interface
        /// should be recreated every time when an exception occured during query processing.
        /// </summary>
        public bool RecreateScope { get; set; }

        /// <summary>
        /// Creates an instance of scope provider from configuration file.
        /// </summary>
        /// <param name="applyOnChanges">
        /// The value indicating whether the process is data changes or data loading.
        /// </param>
        /// <returns>The provider instance or null.</returns>
        public static TransactQueryScopeProvider Create(bool applyOnChanges = true)
        {
            var content = GetConfigElement();
            if (content != null)
            {
                return CreateProvider(content, applyOnChanges);
            }

            return new TransactQueryScopeProvider();
        }

        /// <summary>
        /// Creates an operation scope for a batch of operations.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <returns>The operation scope isntance.</returns>
        public IQueryScope CreateScope(IExecutionContext context)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel,
                Timeout = TimeSpan.FromMilliseconds(Timeout)
            };

            var scope = new TransactionScope(TransactionScopeOption, transactionOptions);

            return new TransactQueryScope(scope, context);
        }

        /// <summary>
        /// Creates the execution context.
        /// </summary>
        /// <returns>The execution context instance.</returns>
        public IExecutionContext CreateContext()
        {
            return new RecoverableExecutionContext(_attemptLimit);
        }

        /// <summary>
        /// Tries to extract query model configuration from app.config file.
        /// </summary>
        /// <returns>The XML element</returns>
        private static XElement GetConfigElement()
        {
            var section = ConfigurationManager.GetSection("queryModel");
            if (section != null)
            {
                var type = section.GetType();
                var propertyInfo = type.GetProperty("Content");
                if (propertyInfo != null)
                {
                    return propertyInfo.GetValue(section, null) as XElement;
                }
            }

            return null;
        }

        /// <summary>
        /// Creates an instance of the <see cref="TransactQueryScopeProvider"/> type.
        /// </summary>
        /// <param name="content">The XML element containing transaction settings.</param>
        /// <param name="applyOnChanges">
        /// The value indicating whether the process is data changes or data loading.
        /// </param>
        /// <returns>The created provider.</returns>
        private static TransactQueryScopeProvider CreateProvider(XElement content, bool applyOnChanges)
        {
            var schemaElement = content.Element("querySchema");
            if (schemaElement != null)
            {
                content = schemaElement;
            }

            var globalElement = content.Element("global");
            if (globalElement != null)
            {
                content = globalElement;
            }

            var scopeElement = content.Element("queryScope");
            if (scopeElement != null)
            {
                content = scopeElement;
            }

            foreach (var element in content.Elements("transactScope"))
            {
                var apply = false;
                var applyOnLoadAttr = element.Attribute("applyOnLoad");
                var applyOnChangesAttr = element.Attribute("applyOnChanges");
                if ((applyOnChanges &&
                     applyOnChangesAttr != null &&
                     bool.TryParse(applyOnChangesAttr.Value, out apply) &&
                     apply) ||
                    (!applyOnChanges &&
                     applyOnLoadAttr != null &&
                     bool.TryParse(applyOnLoadAttr.Value, out apply) &&
                     apply))
                {
                    return
                        (TransactQueryScopeProvider)
                        TransactQueryScopeProviderXmlConfigurator.GetScopeProvider(element);
                }
            }

            return (TransactQueryScopeProvider)TransactQueryScopeProviderXmlConfigurator.GetScopeProvider(content);
        }
    }
}
