using System.Linq;
using Microsoft.Extensions.Options;
namespace CoWorker.Builder
{
    using System;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.DependencyInjection;
    using CoWorker.Rest.Internal;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using CoWorker.Rest.Conventions;
    using CoWorker.Rest.Features;
    using CoWorker.Rest.ApplicationParts;
    using CoWorker.Rest.Controllers;
    using CoWorker.Rest.Builder;

    public static class RestConventionModelExtensions
    {
        public static IMvcCoreBuilder AddConventionModel(this IMvcCoreBuilder services)
        {
            services.Services.AddSingleton(p =>
                  {
                      p.GetServices<IConfigureOptions<ApplicationPartManager>>()
                          .Each(x => x.Configure(services.PartManager));
                      return services.PartManager;
                  });
            return services;
        }

        public static IServiceCollection AddConventionModel(this IServiceCollection services)
            => services
                  .AddSingleton<ResponseCacheRuleFeature>()
                  .AddSingleton<ParameterModelFeature>()
                  .AddSingleton<ControllerModelFeature>()
                  .AddSingleton<IApplicationModelConvention, DomainModelConvention>()
                  .AddSingleton<RouteValueConvention>()
                  .AddSingleton<RouteSelectorConvention>()
                  .AddSingleton<FilterConvention>()
                  .AddSingleton<ModelBindingConvention>()
                  .AddSingleton<RestConventionOptions>()
                  .AddSingleton<RestControllerFeatureProvider>()
                  .AddSingleton<HttpVerbRouteFactory>()
                  .AddFeature<ControllerFeature>()
                  .AddSingleton<IConfigureOptions<ApplicationPartManager>, ApplicationPartManagerConfigureOptions>()
                  .AddNamingMapping()
                  .AddMvcCore()
                  .AddConventionModel()
                  .Services;

        public static IServiceCollection AddRepository(this IServiceCollection services, Type type)
            => services
                .AddSingleton(typeof(IRepository<>), type)
                .AddSingleton(typeof(IRepositoryProvider<>), typeof(RepositoryProvider<>));

        #region for Naming Mapping
        public static IServiceCollection AddNamingMapping(this IServiceCollection services)
            => services
                .AddModelBindingNamings()
                .AddControllerModelNamings();

        public static IServiceCollection AddModelBindingNamings(this IServiceCollection services)
            => services.AddSingleton<ParameterModelFeature>()
                .AddNaming("path", "{*path}", BindingSource.Path)
                .AddNaming("id", "{id}", BindingSource.Path)
                .AddNaming("key", "{key}", BindingSource.Path)
                .AddNaming("domain", string.Empty, BindingSource.ModelBinding)
                .AddNaming("schema", "{schema}", BindingSource.Path)
                .AddNaming("returnUrl", string.Empty, BindingSource.ModelBinding);
        #endregion

        #region for controller
        public static IServiceCollection AddControllerModelNamings(this IServiceCollection services)
            => services
                .AddModel("api", typeof(ApiController<>))
                .AddModel("hst", typeof(HistoryController<>))
                .AddModel("mnt", typeof(ManagementController<>))
                .AddModel("rsc", typeof(ResourcesController<>))
                .AddModel<MigrationController>("mgt")
                .AddModel<AuthorizationController>("auth")
                ;
        #endregion

        #region Cache
        public static IServiceCollection AddCacheProfile(
            this IServiceCollection services,
            Action<CacheProfile> config,
            Func<ActionModel, bool> predicate)
            => services.AddSingleton(p => new ResponseCacheRuleFeatureConfigureOptions(config, predicate));
        #endregion
    }
}
