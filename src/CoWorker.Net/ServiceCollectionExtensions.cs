using Microsoft.AspNetCore.Hosting;
using CoWorker.Builder;
namespace CoWorker.Net
{
    using CoWorker.Net.FileUpload;
    using CoWorker.Net.Antiforgery;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System.Net;
    using Microsoft.AspNetCore.Server.Kestrel;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.AspNetCore.Rewrite;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNetTools(this IServiceCollection services)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            services.AddAntiforgeryMiddleware()
                  .TryAddTransient<FileUploadHandler>();
            services.Configure<RewriteOptions>(o => o.AddRedirectToHttpsPermanent());
            return services;
        }

        public static IServiceCollection AddKestrelHttps(this IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(o => o.Listen(new IPEndPoint(
                IPAddress.Loopback, 443),
                l => l.UseHttps("esportasia.pfx", "1")));
            return services;
        }
    }

    //public static class Kestrel
    //{
    //    public static ConfigureDelegate<IServiceCollection> Https => ServiceCollectionExtensions.AddKestrelHttps;
    //}
}