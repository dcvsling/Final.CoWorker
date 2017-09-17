using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Linq;
using Microsoft.Extensions.DependencyModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace CoWorker.Models.HostingStartupBase
{
    public class Startup
    {
        public Startup(IHostingEnvironment env) { }
        public void Configure(IApplicationBuilder app) {
            var type = app.ApplicationServices.GetService<IServiceCollection>().FirstOrDefault(x => x.ServiceType.Name.Contains("Swagger")).ServiceType;
            var service = app.ApplicationServices.GetService(type);
        }
        public void ConfigureServices(IServiceCollection services) { services.AddSingleton(services); }
    }
}
