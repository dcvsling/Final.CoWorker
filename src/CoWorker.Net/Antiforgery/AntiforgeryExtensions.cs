using Microsoft.AspNetCore.Builder;
using CoWorker.Net.Proxy;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Antiforgery;
namespace CoWorker.Net.Antiforgery
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class AntiforgeryExtensions
    {
        public static IServiceCollection AddAntiforgeryMiddleware(this IServiceCollection services, Action<AntiforgeryOptions> config = null)
            => services.AddSingleton<IConfigureOptions<AntiforgeryOptions>,DefaultAntiforgeryConfigureOptions>()
                .AddSingleton<AntiforgeryMiddleware>()
                .AddAntiforgery(config ?? Helper.Empty<AntiforgeryOptions>());

        public static IApplicationBuilder UseAntiforgeryMiddleware(this IApplicationBuilder app)
            => app.Use(app.ApplicationServices.GetRequiredService<AntiforgeryMiddleware>().Middleware);
    }
}