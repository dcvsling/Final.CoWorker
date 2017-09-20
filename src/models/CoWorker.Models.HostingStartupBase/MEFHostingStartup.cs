using Microsoft.Extensions.Logging;

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
            builder
                .ConfigureAppConfiguration(
                    (ctx,b) => b.AddAzureKeyVault(
                        $"https://esport-key.vault.azure.net/",
                        ctx.Configuration.GetSection("keyvault:clientid").Get<string>(),
                        ctx.Configuration.GetSection("keyvault:clientsecret").Get<string>()))
                .ConfigureLogging((ctx,b) => {
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
