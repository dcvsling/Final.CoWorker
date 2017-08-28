using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
namespace CoWorker.Builder
{
    using CoWorker.Rest.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestMvc(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDefaultRest();
            return services;
        }

        public static IApplicationBuilder UseRestMvc(this IApplicationBuilder app)
            => app.UseMvc();
	}
    public static class RestDefalt
    {
    }
}
