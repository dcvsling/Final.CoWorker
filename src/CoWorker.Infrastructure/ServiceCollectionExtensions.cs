using System.Dynamic;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorker.Builder
{
    using CoWorker.Infrastructure.TypeAccessor;
    using CoWorker.Infrastructure.Cache;
    using CoWorker.Infrastructure.TypeStore;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using CoWorker.Infrastructure.DefaultFactory;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultStructure(this IServiceCollection services)
        {
            services.TryAddSingleton<IObjectFactory, ObjectFactory>();
            services.TryAddSingleton<ITypeStore, TypeStoreManager>();
            services.TryAddSingleton(typeof(ICache<>), typeof(DictionaryCache<>));
            services.TryAddSingleton<PropertyAccessorStore>();
            services.TryAddSingleton(typeof(ITypeMapper<>), typeof(TypeMapper<>));
            services.TryAddSingleton<QueryFactory>();
            return services;
        }

        public static T Create<T>(this IObjectFactory factory) where T : class
            => factory.Create(typeof(T)).As<T>();
    }
}
