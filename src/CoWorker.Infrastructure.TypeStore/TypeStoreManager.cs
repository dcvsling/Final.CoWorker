using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Collections.Concurrent;

namespace CoWorker.Infrastructure.TypeStore
{
    public class TypeStoreManager : ITypeStore
    {
        private readonly IDictionary<Func<Type, bool>, Type> _typeCache;
        private readonly IEnumerable<AssemblyName> _names;
        private readonly IOptionsCache<Assembly> _assemblyCache;

        public TypeStoreManager()
        {
           
            _names = DependencyContext.Default.GetDefaultAssemblyNames();
            _assemblyCache = new OptionsCache<Assembly>();
        }
        public String Name => nameof(TypeStoreManager);

        public IEnumerable<Type> List
            => _names.Select(x => _assemblyCache.GetOrAdd(x.Name, x.Load))
                .SelectMany(x => x.ExportedTypes);

        public Type Find(Func<Type, bool> predicate)
            => _typeCache.TryGetValue(predicate, out var result)
                ? result ?? FindAndCache(predicate)
                : throw new IndexOutOfRangeException();

        private Type FindAndCache(Func<Type, bool> predicate)
        {
            var result = List.FirstOrDefault(predicate);
            _typeCache.Add(predicate, result);
            return result;
        }
    }
}