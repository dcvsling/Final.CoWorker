using Microsoft.Extensions.DependencyInjection;

using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace CoWorker.Models.HostingStartupBase
{
    using CoWorker.Builder;
    using CoWorker.Net;
    using Microsoft.Extensions.DependencyInjection;
    public class MEFHostingStartup : IHostingStartup
    {
        void IHostingStartup.Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                (ctx, srv) => srv.AddOptions()
                    .AddConfiguration(ctx.Configuration)
                    .AddElm()
                    .AddNetTools()
                    .AddSingleton<IStartupFilter, ElmStartupFilter>());
            new MEFProvider().CreateHost().GetExports<IHostingStartup>()
                .Each(x => x.Configure(builder));
        }
    }

}
