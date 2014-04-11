using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProcess.Infrastructure.Caching.CacheGone
{
    public abstract class TimeBasedExpiration : ICacheExpiration
    {
        public abstract bool HasExpired();
    }
}
