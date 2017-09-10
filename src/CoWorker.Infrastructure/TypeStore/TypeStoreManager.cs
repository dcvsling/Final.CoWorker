using System.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Collections.Concurrent;
using CoWorker.Infrastructure.Cache;

namespace CoWorker.Infrastructure.TypeStore
{
    public class TypeStoreManager : ITypeStore
    {
        private readonly IDictionary<Func<Type, bool>, Type> _typeCache;
        private readonly IEnumerable<AssemblyName> _names;
        private readonly ICache<Assembly> _cache;

        public TypeStoreManager(ICache<Assembly> cache)
        {
            _names = DependencyContext.Default.GetDefaultAssemblyNames();
            _cache = cache;
            _typeCache = new Dictionary<Func<Type, bool>, Type>();
        }

        public IEnumerable<Type> List
            => _names.Select(x => _cache.GetOrAdd(x.Name, x.Load))
                .SelectMany(x => x.ExportedTypes);

        public Type Find(Func<Type, bool> predicate)
            => _typeCache.TryGetValue(predicate, out var result)
                ? result ?? FindAndCache(predicate)
                : FindAndCache(predicate);

        private Type FindAndCache(Func<Type, bool> predicate)
        {
            var result = List.FirstOrDefault(predicate);
            _typeCache.Add(predicate, result);
            return result;
        }
    }
}