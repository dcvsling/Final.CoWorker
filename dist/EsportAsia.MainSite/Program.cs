﻿using Microsoft.AspNetCore.ApplicationInsights.HostingStartup;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using CoWorker.Models.HostingStartupBase;
using Microsoft.Extensions.PlatformAbstractions;
using System.Threading.Tasks;

[assembly: HostingStartup(typeof(ApplicationInsightsHostingStartup))]
[assembly: HostingStartup(typeof(BootstrappingHostingStartup))]

namespace EsportAsia.MainSite
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                .UseSetting(WebHostDefaults.ApplicationKey, PlatformServices.Default.Application.ApplicationName)
                .Build();

    }
}
