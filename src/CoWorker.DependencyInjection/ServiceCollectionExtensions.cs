using Microsoft.AspNetCore.Hosting;
using System.Linq;
using CoWorker.DependencyInjection.Decorator;
using CoWorker.DependencyInjection.Abstractions;

namespace CoWorker.Builder
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using CoWorker.DependencyInjection.Configuration;
    using CoWorker.DependencyInjection.Factory;
    using CoWorker.Infrastructure.Cache;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProvider<T>(
            this IServiceCollection services,
            Action<IServiceCollection> builder)
        {
            new InternalServiceProvider<T>(builder).AddTo(services);
            return services;
        }

        public static IServiceCollection AddProvider<T>(this IServiceCollection services)
        {
            var result = new ServiceCollection();
            new InternalServiceProvider<T>(services).AddTo(result);
            return result;
        }

        public static IServiceCollection AddDefaultService(this IServiceCollection services)
        {
            services.AddOptions();

            //services.TryAddSingleton(typeof(IOptionsFactory<>),typeof(DefaultFactory<>));
            services.TryAddSingleton(typeof(IObjectFactory<>), typeof(ObjectFactory<>));

            return services
                .AddDefaultStructure()
                .AddConfiguration();
        }

        public static IServiceCollection AddConfiguration(
            this IServiceCollection services,
            Action<IConfigurationBuilder> builder = default)
        {
            services.TryAddSingleton(typeof(IObjectCreator<>), typeof(DefaultFactoryObjectCreator<>));
            services.TryAddSingleton(typeof(IObjectConfigure<>), typeof(ConfigureOptionsObjectConfigure<>));
            services.TryAddSingleton(typeof(IObjectExtensions<>), typeof(DecoratorFactoryExtensions<>));
            services.TryAddSingleton(typeof(IConfigureOptions<>), typeof(ConfigurationConfigureOptions<>));
            services.TryAddSingleton(typeof(IObjectFactory<>), typeof(ObjectFactory<>));
            services.TryAddSingleton(typeof(IOptionsCache<>), typeof(OptionsFactoryCache<>));

            if(services.Any(x => x.ServiceType == typeof(WebHostBuilderContext)))
            {
                services.TryAddSingleton(p => p.GetRequiredService<WebHostBuilderContext>().Configuration);
            }
            else
            {
                services.TryAddSingleton<IConfigurationBuilder, ConfigurationBuilder>();
                services.TryAddSingleton<IConfiguration, Configuration>();
            }
            return services.AddConfigurationConfigureOptions(builder);
        }

        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config)
            => services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration),config));


        private static IServiceCollection AddConfigurationConfigureOptions(
            this IServiceCollection services,
            Action<IConfigurationBuilder> config)
        {
            if (config == null) return services;
            services.AddSingleton<IConfigurationBuilderConfigureOptions>(
                   p => new ConfigurationBuilderConfigureOptions(config));
            return services;
        }
        public static void Configure<T>(this IObjectConfigure<T> config, T t)
            where T : class
            => config.Configure(string.Empty, t);
    }
}