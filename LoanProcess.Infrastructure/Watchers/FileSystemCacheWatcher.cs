using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanProcess.Infrastructure.Caching.CacheGone;
using LoanProcess.Infrastructure.Caching.RefreshCache.Expiration;
using LoanProcess.Infrastructure.Caching.RefreshCache.Expiration.NotificationExpiration;

namespace LoanProcess.Infrastructure.Watchers
{
    public class FileSystemCacheWatcher : CacheDependencyListenerBase, IWather
    {
        private FileSystemWatcher _watcher;

        public void Start()
        {
            _watcher = new FileSystemWatcher();
            _watcher.Changed += new FileSystemEventHandler(OnFileSystemChanged);
        }

        public void Stop()
        {
            if (_watcher != null)
            {
                _watcher.Changed += new FileSystemEventHandler(OnFileSystemChanged);
            }
        }

        private void OnFileSystemChanged(object sender, FileSystemEventArgs e)
        {
            // should get cache key from file 
            if (e.FullPath.Contains("CacheFolder") && e.Name.Contains("key"))
            {
                Notify(CacheDependencyChangeTypes.Changed);
            }
        }
    }
}
