using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace CoWorker.Net.Proxy
{

    public static class ProxyExtensions
    {
        public static IServiceCollection AddProxy(this IServiceCollection services)
            => services.AddTransient<IHttpClient, DefaultHttpClient>()
                .AddSingleton<ProxyHandler>()
                .AddTransient<ProxyClient>()
                .AddTransient<ProxyMiddleware>();

        public static IApplicationBuilder UsePrefixRouteProxy(this IApplicationBuilder app)
            => app.Use(next => app.ApplicationServices.GetRequiredService<ProxyMiddleware>().Middleware(next));
    }

}