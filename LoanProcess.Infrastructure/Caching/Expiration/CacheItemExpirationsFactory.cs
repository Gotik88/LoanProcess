// ============================================================================
// <copyright file="CacheItemExpirationsFactory.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Expiration
{
    using System;
    using System.Collections.Generic;

    public static class CacheItemExpirationsFactory
    {
        private static int slidingExpirationInMinutes;
        private static int absoluteExpirationInMinutes;
        private static DateTime lastTimeAccessed;

        static CacheItemExpirationsFactory()
        {
            slidingExpirationInMinutes = 1;
            absoluteExpirationInMinutes = 1;
        }

        public static ICacheExpiration[] GetCacheExpirations(CacheItem item)
        {
            lastTimeAccessed = item.LastAccessedTime;
            var expirations = new List<ICacheExpiration>();

            switch (item.GetCacheItemType())
            {
                case CacheItemType.Object:
                    expirations.AddRange(GetTimeExpirations(true, true));

                    break;

                case CacheItemType.Page:
                    expirations.AddRange(GetTimeExpirations(true, true));

                    break;

                case CacheItemType.Collection:
                    expirations.AddRange(GetTimeExpirations(true, true));

                    break;
            }

            return expirations.ToArray();
        }

        private static IEnumerable<ICacheExpiration> GetTimeExpirations(bool hasAbsoluteExpiration = false, bool hasSlidingExpiration = false)
        {
            var timeExpirations = new List<ICacheExpiration>();

            if (hasAbsoluteExpiration)
            {
                timeExpirations.Add(new AbsoluteTimeExpiration(new TimeSpan(0, 0, ConvertExpirationTimeToSeconds(absoluteExpirationInMinutes))));
            }

            if (hasSlidingExpiration)
            {
                timeExpirations.Add(new SlidingTimeExpiration(lastTimeAccessed, new TimeSpan(0, 0, ConvertExpirationTimeToSeconds(slidingExpirationInMinutes))));
            }

            return timeExpirations;
        }

        private static int ConvertExpirationTimeToSeconds(int expirationInMinutes)
        {
            return expirationInMinutes * 60;
        }
    }
}
