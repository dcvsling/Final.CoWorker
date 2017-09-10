using CoWorker.LightMvc.Swagger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CoWorker.Models.Swagger
{
    public class SwaggerHostingStartup : IHostingStartup
    {
        void IHostingStartup.Configure(IWebHostBuilder builder)
            => builder.ConfigureServices(srv => srv
                .AddMvcCore()
                    .AddApiExplorer()
                    .AddControllersAsServices()
                    .AddJsonFormatters(o => o.Initialize())
                    .AddJsonOptions(o => o.SerializerSettings.Initialize())
                    .AddMvcOptions(o => {
                        o.AllowEmptyInputInBodyModelBinding = true;
                        o.RequireHttpsPermanent = true;
                        o.SslPort = 443;
                    })
                    .Services
                .AddSwagger()
                .AddSingleton<IStartupFilter,SwaggerStartupFilter>());
    }

}
