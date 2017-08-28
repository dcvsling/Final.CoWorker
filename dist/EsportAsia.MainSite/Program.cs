using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.AspNetCore.Server.Kestrel.Https.Internal;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using CoWorker.Net;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CoWorker.Builder;
using CoWorker.LightMvc;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using System.IO;
using Microsoft.AspNetCore;
using CoWorker.Net.Antiforgery;
using Microsoft.Extensions.PlatformAbstractions;

namespace EsportAsia.MainSite
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args,host => host.Run());
        }

        public static void BuildWebHost(string[] args,Action<IWebHost> action)
        {
            using (var host = HostStartup.Initialize(WebHost.CreateDefaultBuilder(args)).Build())
            {
                action(host);
            }
        }
    }
}
