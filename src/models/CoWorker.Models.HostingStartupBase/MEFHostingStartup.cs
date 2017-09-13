

namespace CoWorker.Models.HostingStartupBase
{
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using CoWorker.Builder;
    using Microsoft.Extensions.DependencyInjection;
    public class MEFHostingStartup : IHostingStartup
    {
        void IHostingStartup.Configure(IWebHostBuilder builder)
        {
            WebHostBuilderContext context = default;
            builder
                .ConfigureLogging((ctx, b) =>
            {
                b.AddAzureWebAppDiagnostics();
                context = ctx;
            })
                .ConfigureServices(
                (ctx, srv) => srv.AddOptions()
                    .AddConfiguration(ctx.Configuration)
                    .AddElm()
                    .AddNetTools()
                    .AddAntiforgeryMiddleware()
                    .AddSingleton<IStartupFilter, ElmStartupFilter>());
            
            new MEFProvider(context)
                .CreateHost()
                .GetExports<IHostingStartup>()
                .Each(x => x.Configure(builder));
        }
    }

}
