using System;
using System.Linq.Expressions;

namespace CoWorker.Rest.Builder
{
    using CoWorker.Builder;
    using CoWorker.Rest.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class RestMvcExtensions
    {
        public static IServiceCollection AddRestMvcCore(this IServiceCollection services)
            => services
                .AddMvcCore()
                .AddControllersAsServices()
                .AddAuthorization()
                //.AddApiExplorer()
                .AddFormatterMappings()
                .AddJsonFormatters()
            .Services;

		public static IServiceCollection AddDefaultRest(this IServiceCollection services)
			=> services
				.AddAntiforgery()
				.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
				.AddScoped<RestController>()
				.AddSingleton<FileExtensionContentTypeProvider>()
				.AddSingleton<IConfigureOptions<MvcOptions>, MvcOptionsConfigureOptions>()
                .AddConventionModel()
                .AddRestMvcCore();

        internal static Expression<Func<TEntity, bool>> EqualWithId<TEntity>(this Object val)
            => typeof(TEntity).ToParameter().MakeLambda<Func<TEntity, bool>>(
                exp => exp.AsTypeTo(typeof(TEntity))
                    .GetPropertyOrField("Id")
                    .EqualWith(val.ToConstant()));
	}
}
