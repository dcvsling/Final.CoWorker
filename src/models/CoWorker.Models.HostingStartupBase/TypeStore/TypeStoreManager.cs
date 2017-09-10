using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Collections;

namespace CoWorker.Models.HostingStartupBase
{
    public class TypeSelector : ITypeSelector
    {
        private readonly IDictionary<AssemblyName, Lazy<Assembly>> _assemblies;

        public TypeSelector()
        {
            _assemblies = DependencyContext.Default.GetDefaultAssemblyNames().Aggregate(
                new Dictionary<AssemblyName, Lazy<Assembly>>(),
                (seed, next) => seed.Add(next, new Lazy<Assembly>(next.Load)));
        }

        public IEnumerator<Type> GetEnumerator() => _assemblies.Values.SelectMany(x => x.Value.ExportedTypes).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}