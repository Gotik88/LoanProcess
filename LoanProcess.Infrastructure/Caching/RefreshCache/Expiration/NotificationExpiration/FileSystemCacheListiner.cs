

namespace LoanProcess.Infrastructure.Caching.RefreshCache.Expiration.NotificationExpiration
{
    using System.IO;

    public class FileSystemCacheListiner : CacheDependencyListenerBase
    {
        private readonly ICacheManager _cacheManager;

        public FileSystemCacheListiner(ICacheManager cacheManager, FileSystemWatcher watcher)
        {
            _cacheManager = cacheManager;
            watcher.Changed += new FileSystemEventHandler(OnFileSystemChanged);
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
