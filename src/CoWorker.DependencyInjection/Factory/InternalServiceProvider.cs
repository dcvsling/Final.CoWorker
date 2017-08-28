
namespace CoWorker.DependencyInjection.Factory
{
    using Abstractions;
    using CoWorker.Builder;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public sealed class InternalServiceProvider<T> : IInternalServiceProvider<T>
    {
        private Action<IServiceCollection> _services;
        private Lazy<IServiceProvider> _provider;
        public InternalServiceProvider(IServiceCollection services)
            : this(services.LazyCopyTo())
        {
        }
        public InternalServiceProvider(Action<IServiceCollection> services)
        {
            _services = services;
            _provider = new Lazy<IServiceProvider>(
               () => services.Create(() => new ServiceCollection())
                   .BuildServiceProvider(), true);
        }

        private InternalServiceProvider(InternalServiceProvider<T> provider)
        {
            this._services = provider._services;
            this._provider = provider._provider;
        }

        public IServiceProvider Clone() => new InternalServiceProvider<T>(this);
        public void Dispose()
        {
            _provider = null;
            _services = null;
            GC.SuppressFinalize(this);
        }
        public Object GetService(Type serviceType)
            => _provider.Value.GetService(serviceType);
        public void AddTo(IServiceCollection services)
        {
            services.AddSingleton<IInternalServiceProvider<T>>(this);
            ReTarget(_services)(services);
        }

        private Action<IServiceCollection> ReTarget(Action<IServiceCollection> builder)
        {
            var newseq = new ServiceCollection();
            builder(newseq);
            var result = newseq.Select(x => SwitchConstructor(x)).ToList();
            return result.LazyCopyTo();
        }

        private object CreateService(IServiceProvider provider, Type type)
           => provider.GetRequiredService<IInternalServiceProvider<T>>().GetRequiredService(type);

        private ServiceDescriptor SwitchConstructor(ServiceDescriptor desc)
            => ServiceDescriptor
                .Describe(
                    desc.ServiceType,
                    p => CreateService(p,desc.ServiceType),
                    desc.Lifetime
                );   
    }
}