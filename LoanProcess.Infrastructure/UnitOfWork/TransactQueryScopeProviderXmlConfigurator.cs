// ============================================================================
// <copyright file="TransactQueryScopeProviderXmlConfigurator.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.UnitOfWork
{
    using System;
    using System.Transactions;
    using System.Xml.Linq;

    /// <summary>
    /// The utility class for configuring the <see cref="TransactQueryScopeProvider"/> from XML element.
    /// </summary>
    internal static class TransactQueryScopeProviderXmlConfigurator
    {
        /// <summary>
        /// Gets the sope provider from XML element.
        /// </summary>
        /// <param name="element">
        /// The XML element containing provider configuration.
        /// </param>
        /// <returns>The scope provider instance.</returns>
        public static IQueryScopeProvider GetScopeProvider(XElement element)
        {
            var provider = new TransactQueryScopeProvider();
            FillScopeOption(element, provider);
            FillIsolationLevel(element, provider);
            FillTimeout(element, provider);
            FillAttemptLimit(element, provider);
            FillRecreateScope(element, provider);

            return provider;
        }

        /// <summary>
        /// Fills the transaction scope option from XML element.
        /// </summary>
        /// <param name="element">
        /// The XML element containing provider configuration.
        /// </param>
        /// <param name="provider">The scope provider instance.</param>
        private static void FillScopeOption(XElement element, TransactQueryScopeProvider provider)
        {
            var optionAttribute = element.Attribute("scopeOption");
            if (optionAttribute != null)
            {
                switch (optionAttribute.Value.ToLower())
                {
                    case "required":
                        provider.TransactionScopeOption = TransactionScopeOption.Required;
                        break;
                    case "requiresnew":
                        provider.TransactionScopeOption = TransactionScopeOption.RequiresNew;
                        break;
                    case "suppress":
                        provider.TransactionScopeOption = TransactionScopeOption.Suppress;
                        break;
                    default:
                        throw new ArgumentException(string.Format(
                            "Can't recognise scope option: '{0}'. Check transaction scope settings in the query configuration file.",
                            optionAttribute.Value));
                }
            }
        }

        /// <summary>
        /// Fills the transaction isolation level from XML element.
        /// </summary>
        /// <param name="element">
        /// The XML element containing provider configuration.
        /// </param>
        /// <param name="provider">The scope provider instance.</param>
        private static void FillIsolationLevel(XElement element, TransactQueryScopeProvider provider)
        {
            var isolationLevelAttribute = element.Attribute("isolationLevel");
            if (isolationLevelAttribute != null)
            {
                switch (isolationLevelAttribute.Value.ToLower())
                {
                    case "chaos":
                        provider.IsolationLevel = IsolationLevel.Chaos;
                        break;
                    case "readcommitted":
                        provider.IsolationLevel = IsolationLevel.ReadCommitted;
                        break;
                    case "readuncommitted":
                        provider.IsolationLevel = IsolationLevel.ReadUncommitted;
                        break;
                    case "repetableread":
                        provider.IsolationLevel = IsolationLevel.RepeatableRead;
                        break;
                    case "serializable":
                        provider.IsolationLevel = IsolationLevel.Serializable;
                        break;
                    case "snapshot":
                        provider.IsolationLevel = IsolationLevel.Snapshot;
                        break;
                    default:
                        throw new ArgumentException(string.Format(
                            "Can't recognise isolation level: '{0}'. Check transaction scope settings in the query configuration file.",
                            isolationLevelAttribute.Value));
                }
            }
        }

        /// <summary>
        /// Fills the transaction timeout from XML element.
        /// </summary>
        /// <param name="element">
        /// The XML element containing provider configuration.
        /// </param>
        /// <param name="provider">The scope provider instance.</param>
        private static void FillTimeout(XElement element, TransactQueryScopeProvider provider)
        {
            var timeoutAttribute = element.Attribute("timeout");
            int timeout;
            if (timeoutAttribute != null &&
                int.TryParse(timeoutAttribute.Value, out timeout))
            {
                provider.Timeout = timeout;
            }
        }

        /// <summary>
        /// Gets the attempt limit attribute from XML element.
        /// </summary>
        /// <param name="element">
        /// The XML element containing provider configuration.
        /// </param>
        /// <param name="provider">The scope provider instance.</param>
        private static void FillAttemptLimit(XElement element, TransactQueryScopeProvider provider)
        {
            var attemptLimit = element.Attribute("attemptLimit");
            var limit = 1;
            if (attemptLimit != null)
            {
                int.TryParse(attemptLimit.Value, out limit);
            }

            provider.AttemptLimit = Math.Max(1, limit);
        }

        /// <summary>
        /// Gets the recreate scope attribute from XML element.
        /// </summary>
        /// <param name="element">
        /// The XML element containing provider configuration.
        /// </param>
        /// <param name="provider">The scope provider instance.</param>
        private static void FillRecreateScope(XElement element, TransactQueryScopeProvider provider)
        {
            var recreateScopeAttribute = element.Attribute("recreateScope");
            var recreateScope = false;
            if (recreateScopeAttribute != null)
            {
                bool.TryParse(recreateScopeAttribute.Value, out recreateScope);
            }

            provider.RecreateScope = recreateScope;
        }
    }
}
