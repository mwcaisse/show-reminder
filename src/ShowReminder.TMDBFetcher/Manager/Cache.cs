using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace ShowReminder.TMDBFetcher.Manager
{
    public class Cache<TK, TV>
    {

        private readonly IDictionary<TK, CacheItem<TV>> _cache;

        public Cache()
        {
            _cache = new ConcurrentDictionary<TK, CacheItem<TV>>();
        }

        /// <summary>
        /// Purges the expired records from the cache
        /// </summary>
        public void Purge()
        {
            var toRemove = _cache.Where(pair => pair.Value.Expired).Select(pair => pair.Key);
            foreach(var key in toRemove)
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// Fetches a value from the cache. If a value with the key is not found, returns null.
        /// 
        /// If the item with the given key has expired, null is returned and item is removed from the cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TV Get(TK key)
        {
            if (Contains(key))
            {
                return _cache[key].Item;
            }
            return default(TV);
        }

        /// <summary>
        /// Determines if a value for the given key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(TK key)
        {
            if (_cache.ContainsKey(key))
            {
                var item = _cache[key];
                if (!item.Expired)
                {
                    return true;
                }
                _cache.Remove(key);
            }
            return false;
        }
        
        /// <summary>
        /// Adds the given value with the specified key to the cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TK key, TV value)
        {
            CacheItem<TV> item = new CacheItem<TV>(value);

            if (Contains(key))
            {
                //if the key already exists in the cache, update the existing record
                _cache[key] = item;
            }
            else
            {
                _cache.Add(key, item);
            }
        }


        protected class CacheItem<T>
        {

            private static readonly TimeSpan DefaultExpireAfter = new TimeSpan(23, 0, 0);

            public T Item { get; private set; }

            private readonly DateTime _added;

            private readonly TimeSpan _expireAfter;

            public bool Expired => _added + _expireAfter < DateTime.Now;

            public CacheItem(T item) : this(item, DefaultExpireAfter)
            {
                
            }

            public CacheItem(T item, TimeSpan expireAfter)
            {
                Item = item;
                _expireAfter = expireAfter;
                _added = DateTime.Now;
            }



        }
    }

   
}
