using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProcess.Infrastructure.Caching.RefreshCache.Expiration.NotificationExpiration
{
    public class SqlDbCacheListiner : CacheDependencyListenerBase
    {
        private readonly ICacheManager _cacheManager;

        public FileSystemCacheListiner(ICacheManager cacheManager, SqlNotificationRequest request)
        {
            _cacheManager = cacheManager;
            request. += new FileSystemEventHandler(OnFileSystemChanged);
        }

        private void OnFileSystemChanged(object sender, FileSystemEventArgs e)
        {
            // should get cache key from file 
            if (e.FullPath.Contains("CacheFolder") && e.Name.Contains("key"))
            {
                _cacheManager.Contains(e.FullPath);
                Notify(CacheDependencyChangeTypes.Changed);
            }
        }
    }
}
