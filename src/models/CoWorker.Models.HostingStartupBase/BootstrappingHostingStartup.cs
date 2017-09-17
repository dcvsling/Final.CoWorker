using Microsoft.AspNetCore.Builder;


namespace CoWorker.Models.HostingStartupBase
{
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using CoWorker.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

    public class BootstrappingHostingStartup : IHostingStartup
    {
        private IWebHostBuilder _builder;

        void IHostingStartup.Configure(IWebHostBuilder builder)
        {
            new HostingStartupProvider()
                .HostingStartups
                .Each(x => x.Configure(builder));

            builder.CaptureStartupErrors(true)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureLogging(ConfigureLogging)
                .ConfigureServices(ConfigureService)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot("wwwroot")
                //.RunIf(
                //    () => builder.GetSetting(WebHostDefaults.EnvironmentKey) == "Development",
                //    srv => srv.AddKestrelHttps())
                .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                .UseStartup<Startup>();

            
        }

        public void ConfigureAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            context.Configuration = builder
                .AddEnvironmentVariables()
                .AddInMemoryCollection(context.Configuration.AsEnumerable())
                .Build();
        }

        public void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder builder)
            => builder.SetMinimumLevel(LogLevel.Trace)
                .AddConfiguration(context.Configuration)
                .AddAzureWebAppDiagnostics();

        public void ConfigureService(WebHostBuilderContext context, IServiceCollection services)
        {
            services.AddOptions()
                    .AddConfiguration(context.Configuration)
                    .AddElm()
                    .AddCors()
                    .AddHttpsRedirect()
                    .AddAntiforgeryMiddleware()
                    .AddSingleton<IStartupFilter, StartupFilterBase>();

            if (context.HostingEnvironment.IsDevelopment())
                services.AddKestrelHttps();

            Helper.InitDefaultJsonSetting();
        }
    }

}
