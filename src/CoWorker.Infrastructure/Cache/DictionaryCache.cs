using Microsoft.Extensions.Options;
using System;

namespace CoWorker.Infrastructure.Cache
{
    public class DictionaryCache<TOptions> : ICache<TOptions> where TOptions : class
    {
        private readonly IOptionsMonitorCache<TOptions> _cache;
        public DictionaryCache()
        {
            _cache = new OptionsCache<TOptions>();
        }

        public void Clear() => _cache.Clear();
        
        public virtual TOptions GetOrAdd(string name = default, Func<TOptions> createOptions = default)
            => _cache.GetOrAdd(
                    name ?? string.Empty, 
                    createOptions ?? Helper.Default<TOptions>());
        public TOptions Get(String name) => this.GetOrAdd(name);
        public Boolean TryAdd(String name, TOptions options) => this._cache.TryAdd(name, options);
        public Boolean TryRemove(String name) => this._cache.TryRemove(name);
    }
}
