namespace CoWorker.DependencyInjection.Factory
{
    using CoWorker.DependencyInjection.Abstractions;
    using CoWorker.Infrastructure.Cache;
    using System;

    public class OptionsFactoryCache<T> : IOptionsCache<T> where T : class
    {
        private readonly IObjectFactory<T> _factory;
        ICache<T> _cache;
        public OptionsFactoryCache(IObjectFactory<T> factory)
        {
            this._factory = factory;
            this._cache = new DictionaryCache<T>();
        }

        public void Clear() => _cache.Clear();
        public T Get(String name = default)
            => this.GetOrAdd(name);

        public T GetOrAdd(String name = default, Func<T> createOptions = default)
            => this._cache.GetOrAdd(
                name ?? string.Empty,
                createOptions ?? ObjectCreator(name));

        public Boolean TryAdd(String name, T options)
            => this._cache.TryAdd(
                name ?? string.Empty, 
                options ?? ObjectCreator(name)());

        public Boolean TryRemove(String name)
            => this._cache.TryRemove(name ?? String.Empty);

        private Func<T> ObjectCreator(string name = default)
            => () => _factory.Create(name ?? string.Empty);
    }
}