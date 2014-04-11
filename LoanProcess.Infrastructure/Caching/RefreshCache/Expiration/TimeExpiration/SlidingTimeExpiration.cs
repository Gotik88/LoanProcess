// ============================================================================
// <copyright file="AbsoluteTimeExpiration.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.CacheGone
{
    using System;

    [Serializable]
    public class SlidingTimeExpiration : TimeBasedExpiration
    {
        private readonly DateTime _timeLastUsed;
        private readonly TimeSpan _itemSlidingExpiration;

        public SlidingTimeExpiration(DateTime timeLastUsed, TimeSpan slidingExpiration)
        {
            if (slidingExpiration.TotalSeconds < 1)
            {
                throw new Exception();
            }

            this._timeLastUsed = timeLastUsed;
            this._itemSlidingExpiration = slidingExpiration;
        }

        public override bool HasExpired()
        {
            var expired = CheckSlidingExpiration(DateTime.Now, this._timeLastUsed, this._itemSlidingExpiration);
            return expired;
        }

        private static bool CheckSlidingExpiration(DateTime nowDateTime, DateTime lastUsed, TimeSpan slidingExpiration)
        {
            var expirationTicks = lastUsed.Ticks + slidingExpiration.Ticks;
            return nowDateTime.Ticks >= expirationTicks;
        }
    }
}
