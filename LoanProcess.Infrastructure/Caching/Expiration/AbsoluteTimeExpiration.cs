// ============================================================================
// <copyright file="AbsoluteTimeExpiration.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Expiration
{
    using System;

    [Serializable]
    public class AbsoluteTimeExpiration : ICacheExpiration
    {
        private DateTime _absoluteExpirationTime;

        public AbsoluteTimeExpiration(TimeSpan timeFromNow)
            : this(DateTime.Now + timeFromNow)
        {
        }

        public AbsoluteTimeExpiration(DateTime absoluteTime)
        {
            if (absoluteTime > DateTime.Now)
            {
                this._absoluteExpirationTime = absoluteTime;
            }
            else
            {
                throw new Exception();
            }
        }

        public bool HasExpired()
        {
            var nowDateTime = DateTime.Now;
            return nowDateTime.Ticks >= this._absoluteExpirationTime.Ticks;
        }
    }
}
