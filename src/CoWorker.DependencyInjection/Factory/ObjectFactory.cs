using System.Collections.Generic;
namespace CoWorker.DependencyInjection.Factory
{
    using System.Linq;
    using CoWorker.DependencyInjection.Abstractions;
    using System;
    using CoWorker.Infrastructure.Cache;

    public class ObjectFactory<T> : IObjectFactory<T> where T : class
    {
        private readonly IObjectCreator<T> _creator;
        private readonly IEnumerable<IObjectConfigure<T>> _configs;
        private readonly IEnumerable<IObjectExtensions<T>> _exts;
        private readonly ICache<T> _cache;

        public ObjectFactory(
            IObjectCreator<T> creatpr,
            IEnumerable<IObjectConfigure<T>> configs,
            IEnumerable<IObjectExtensions<T>> exts,
            ICache<T> cache)
        {
            this._creator = creatpr;
            this._configs = configs;
            this._exts = exts;
            this._cache = cache;
        }

        public T Create(string name)
            => _cache.GetOrAdd(name ?? string.Empty,() => New(name));

        private T New(string name)
        {
            var result = _configs.Aggregate(
                _creator.Create(name),
                (seed,next) => next.Configure(name,seed));

            var extensions = _exts.Aggregate<IObjectExtensions<T>,Action<T>>(
                x => result = x,
                (seed,next) => t => next.Invoke(name,t,seed));

            extensions(result);
            return result;
        }
    }
}