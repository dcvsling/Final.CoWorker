using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoWorker.Infrastructure.DefaultFactory
{
    public class ObjectFactory : IObjectFactory
    {
        private readonly IServiceProvider _provider;

        public ObjectFactory(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public Object Create(Type type)
            => IsNewableClass(type)
                ? ActivatorUtilities.GetServiceOrCreateInstance(_provider, type)
                : type == typeof(string)
                    ? String.Empty
                    : Activator.CreateInstance(type);

        private bool IsNewableClass(Type type)
            => type.IsClass && !type.IsAbstract;
    }
}
