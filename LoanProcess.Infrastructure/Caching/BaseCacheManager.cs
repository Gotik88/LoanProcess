using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace LoanProcess.Infrastructure.Caching
{
    public abstract class BaseCacheManager : ICacheManager, IDisposable
    {
        private ICacheContainer container;
        /*private ExpirationPollTimer pollTimer;
        private readonly BackgroundScheduler backgroundScheduler;*/

        protected BaseCacheManager(ICacheContainer container)
        ////BackgroundScheduler backgroundScheduler, ExpirationPollTimer pollTimer)
        {

            this.container = container;
            /*this.pollTimer = pollTimer;
            this.backgroundScheduler = backgroundScheduler;*/


            ////pollTimer.StartPolling(backgroundScheduler.ExpirationTimeoutExpired);
        }

        public virtual long Count
        {
            get { return container.GetCount(); }
        }

        public virtual bool Contains(string key)
        {
            return container.Contains(key);
        }

        public virtual object this[string key]
        {
            get { return container.Pull<object>(key); }
        }

        public virtual void Add(string key, object value)
        {
            var policy = new CacheItemPolicy();
            ////policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            container.Push(new CacheItem(key, value));
        }

        public virtual  void Remove(string key)
        {
            container.Remove(key);
        }

        public virtual T GetData<T>(string key)
        {
            return container.Pull<T>(key);
        }

        public virtual void Clear()
        {
            container.Flush();
        }

        /// <summary>
        /// Not intended for public use. Only public due to requirements of IDisposable. If you call this method, your
        /// cache will be unusable.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);

            /*if (pollTimer != null)
            {
                pollTimer.StopPolling();
                pollTimer = null;
            }*/
            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }
    }
}
