using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;

using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace CoWorker.Models.HostingStartupBase
{
    using CoWorker.Builder;
    using CoWorker.Net;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    public class MEFHostingStartup : IHostingStartup
    {
        void IHostingStartup.Configure(IWebHostBuilder builder)
        {
            WebHostBuilderContext context = default;
            builder.ConfigureLogging((ctx,b) => {
                b.AddAzureWebAppDiagnostics();
                context = ctx;
                })
                .ConfigureServices(
                (ctx, srv) => srv.AddOptions()
                    .AddConfiguration(ctx.Configuration)
                    .AddElm()
                    .AddNetTools()
                    .AddSingleton<IStartupFilter, ElmStartupFilter>());
            new MEFProvider(context).CreateHost().GetExports<IHostingStartup>()
                .Each(x => x.Configure(builder));
        }
    }

}
