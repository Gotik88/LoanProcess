using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoanProcess.Infrastructure.Caching.RefreshCache.Expiration.NotificationExpiration
{
    public class FileSystemNotification
    {
        private bool hasExpired;

        public FileSystemNotification(string filePath)
        {
            var cacheFileWatcher = new FileSystemWatcher(filePath);
            cacheFileWatcher.Created += new FileSystemEventHandler(OnCacheFileChanged);
            cacheFileWatcher.Changed += new FileSystemEventHandler(OnCacheFileChanged);
            cacheFileWatcher.Deleted += new FileSystemEventHandler(OnCacheFileChanged);
            cacheFileWatcher.Renamed += new RenamedEventHandler(OnCacheFileChanged);
            cacheFileWatcher.EnableRaisingEvents = true;
        }

        private static void OnCacheFileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {

            }
        }
    }
}
