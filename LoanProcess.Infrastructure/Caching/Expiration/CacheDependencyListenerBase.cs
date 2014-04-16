// ============================================================================
// <copyright file="CacheDependencyListenerBase.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Expiration
{
    public abstract class CacheDependencyListenerBase : ICacheExpiration
    {
        private bool _hasExpired;

        public bool HasExpired()
        {
            return _hasExpired;
        }

        protected void Notify(CacheDependencyChangeTypes changeType)
        {
            if (changeType == CacheDependencyChangeTypes.Changed)
            {
                _hasExpired = true;
            }
        }
    }
}
