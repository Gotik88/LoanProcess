// ============================================================================
// <copyright file="FileSystemCacheWatcher.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Expiration
{
    using System.IO;

    public class FileSystemCacheWatcher : CacheDependencyListenerBase, IWather
    {
        private string _fullFileName;
        private FileSystemWatcher _watcher;

        public FileSystemCacheWatcher(string fullFileName)
        {
            _fullFileName = fullFileName;
            Start();
        }

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
