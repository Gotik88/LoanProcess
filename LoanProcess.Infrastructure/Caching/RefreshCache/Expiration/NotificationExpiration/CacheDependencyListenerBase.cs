
namespace LoanProcess.Infrastructure.Caching.RefreshCache.Expiration
{
    using LoanProcess.Infrastructure.Caching.CacheGone;
    using LoanProcess.Infrastructure.Caching.RefreshCache.Expiration.NotificationExpiration;

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
