namespace CoWorker.DependencyInjection.Factory
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class DefaultFactoryObjectCreator<T> : IObjectCreator<T>
        where T : class
    {
        private readonly IServiceProvider _provider;

        public DefaultFactoryObjectCreator(
            IServiceProvider provider)
        {
            this._provider = provider;
        }
        public T Create(String name = null)
            => ActivatorUtilities.GetServiceOrCreateInstance<T>(_provider);

        //=> typeof(T).GetConstructors().Any(x => !x.GetParameters().Any())
        //    ? _factory.Get(typeof(T), name) as T
        //    : ActivatorUtilities.GetServiceOrCreateInstance<T>(_provider);
    }
}