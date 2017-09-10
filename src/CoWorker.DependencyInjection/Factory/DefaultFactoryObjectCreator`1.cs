using System.Linq;
using System.Net;
using CoWorker.Infrastructure.TypeStore;
namespace CoWorker.DependencyInjection.Factory
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;

    public class DefaultFactoryObjectCreator<T> : IObjectCreator<T>
        where T : class
    {
        private readonly IServiceProvider _provider;
        private readonly ITypeStore _store;

        public DefaultFactoryObjectCreator(
            IServiceProvider provider,
            ITypeStore store)
        {
            this._provider = provider;
            this._store = store;
        }
        public T Create(String name = Helper.EmptyString)
            => ActivatorUtilities.GetServiceOrCreateInstance(_provider, FindClassType(typeof(T))) as T;

        public Type FindClassType(Type type)
            => type.IsInterface
                ? _store.Find(t => t.GetInterfaces().Contains(type))
                : type;
    }
}