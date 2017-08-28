using CoWorker.Net.Antiforgery;
using CoWorker.Net.Proxy;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Antiforgery;
namespace CoWorker.Net
{
    using CoWorker.Net.FileUpload;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNetTools(this IServiceCollection services)
            => services.AddAntiforgeryMiddleware()
                .AddTransient<FileUploadHandler>();

    }
}