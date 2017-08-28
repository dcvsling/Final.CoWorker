using System.Runtime.CompilerServices;
using CoWorker.Abstractions;
using CoWorker.Builder;
using CoWorker.Rest.Features;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CoWorker.Rest.Builder
{

    public static class FeaturesExtensions
    {
        public static IServiceCollection AddModel(this IServiceCollection services, string name, Type type)
            => services.Configure<ControllerModelFeature>(o => o.Features.TryAdd(name, type))
                .AddScoped(type);
        public static IServiceCollection AddModel<TType>(this IServiceCollection services, string name)
            => services.AddModel(name, typeof(TType));

        public static IServiceCollection AddNaming(
            this IServiceCollection services,
            string name,
            string pattern,
            BindingSource source)
            => services.AddNaming(name, new BindingSourceTemplateProvider(name, pattern, source));

        public static TFeature Populate<TFeature, TFeatureContainer>(this ApplicationPartManager app)
            where TFeatureContainer : class
            where TFeature : class, TFeatureContainer, new()
        {
            var feature = new TFeature();
            app.PopulateFeature<TFeatureContainer>(feature);
            return feature;
        }

        public static IServiceCollection AddFeature<TFeature, TProvider>(this IServiceCollection services)
            where TFeature : class, new()
            where TProvider : class, IApplicationFeatureProvider
        {
            services.AddSingleton<TProvider>()
                .AddSingleton(p =>
            {
                var app = p.GetService<ApplicationPartManager>();
                app.FeatureProviders.Add(p.GetService<TProvider>());
                var feature = new TFeature();
                app.PopulateFeature(feature);
                return feature;
            });
            return services;
        }

        public static IServiceCollection AddFeature<TFeature>(this IServiceCollection services)
               where TFeature : class, new()
        {
            services.AddSingleton(p =>
            {
                var app = p.GetService<ApplicationPartManager>();
                var feature = new TFeature();
                app.PopulateFeature(feature);
                return feature;
            });
            return services;
        }


        public static IServiceCollection AddNaming(
            this IServiceCollection services,
            string name,
            IBindingSourceTemplateProvider value)
            => services.Configure<ParameterModelFeature>(o => o.Features.TryAdd(name, value));
    }


}
