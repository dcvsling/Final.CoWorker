using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using CoWorker.Models.HostingStartupBase;

[assembly: HostingStartup(typeof(MEFHostingStartup))]

namespace EsportAsia.MainSite
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
            => HostStartup.Initialize(WebHost.CreateDefaultBuilder(args)).Build();

    }
}
