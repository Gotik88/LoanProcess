

namespace LoanProcess.Infrastructure.Caching.CacheGone
{
    using LoanProcess.Infrastructure.Caching.RefreshCache;

    public interface ICacheExpiration : ICacheRefresher
    {
        /// <summary>
        ///	Specifies if item has expired or not.
        /// </summary>
        /// <returns>Returns true if the item has expired, otherwise false.</returns>
        bool HasExpired();
    }
}
