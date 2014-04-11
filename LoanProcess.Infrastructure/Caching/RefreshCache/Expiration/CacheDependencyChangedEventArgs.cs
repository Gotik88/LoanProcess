using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanProcess.Infrastructure.Caching.RefreshCache.Expiration.NotificationExpiration;

namespace LoanProcess.Infrastructure.Caching.RefreshCache.Expiration
{
    public class CacheDependencyChangedEventArgs : EventArgs
    {
        public CacheDependencyChangedEventArgs(object data, CacheDependencyChangeTypes changeType)
        {
            Data = data;
            ChangeType = changeType;
        }

        public Object Data { get; set; }

        public CacheDependencyChangeTypes ChangeType { get; set; }
    }
}
