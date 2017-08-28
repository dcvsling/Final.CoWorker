using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using CoWorker.LightMvc.ApplicationParts;
using CoWorker.LightMvc.Internal;
using CoWorker.LightMvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Options;

namespace CoWorker.LightMvc
{
    public static class ServiceCollectionsExtensions
    {
        public static IMvcCoreBuilder AddApplicationPartManager(this IMvcCoreBuilder builder)
        {
            builder.Services.AddSingleton(p => builder.PartManager);
            builder.Services.Configure<ApplicationPartManager>((p, o) => o.FeatureProviders.Add(p.GetRequiredService<ControllerFeatureProvider>()));
            builder.Services.Configure<MvcOptions>((p,o) => o.Conventions.Add(p.GetRequiredService<IApplicationModelConvention>()));
            return builder;
        }

        public static IServiceCollection AddMvcLight(this IServiceCollection service)
            => service.AddMvcCore()
                .AddApiExplorer()
                .AddControllersAsServices()
                .AddApplicationPartManager()
                .Services
                .AddScoped(typeof(ApiController<>))
                .AddScoped(typeof(HistoryController<>))
                .AddScoped(typeof(ManagementController<>))
                .AddScoped(typeof(ResourcesController<>))
                .AddSingleton(typeof(IRepository<>), typeof(Repository<>))
                .AddSingleton<ControllerFeatureProvider>()
                .AddSingleton<IApplicationModelConvention,DefaultApplicationModelConvention>()
                ;

        public static IServiceCollection Configure<TOptions>(
            this IServiceCollection services,
            Action<IServiceProvider, TOptions> config)
            where TOptions : class
            => services.AddSingleton<IConfigureOptions<TOptions>>(p => new ConfigureOptions<TOptions>(o => config(p, o)));
    }
}
