using CoWorker.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoWorker.Builder;
using CoWorker.LightMvc;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using CoWorker.Net.Antiforgery;
using Microsoft.AspNetCore;
using System;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Reflection;

namespace EsportAsia.MainSite
{
    public class HostStartup : IHostingStartup
    {
        public static IWebHostBuilder Initialize(IWebHostBuilder builder)
        {
            (new HostStartup() as IHostingStartup).Configure(builder);
            return builder;
        }

        public void ConfigureAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
            => context.Configuration = builder
                .SetBasePath(new FileInfo(Assembly.GetEntryAssembly().Location).Directory.FullName)
                .Build();

        public void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder builder)
            => builder.SetMinimumLevel(LogLevel.Trace)
                .AddConsole()
                .AddDebug()
                .AddConfiguration(context.Configuration)
                .AddAzureWebAppDiagnostics();

        public void ConfigureService(WebHostBuilderContext context, IServiceCollection services)
            => services
                .AddSingleton<IHostingStartup,HostStartup>()
                .AddDefaultService()
                .AddElm()
                .AddSecurityService()
                    .AddAuth(
                        OAuth.Google,
                        OAuth.Facebook,
                        OAuth.Twitch,
                        OAuth.Cookie)
                .AddIdentityService()
                .AddEntityFrameworkService()
                .AddNetTools()
                .AddMvcLight()
                .AddSwaggerGen(o => o.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" }))
                .Configure<RewriteOptions>(o => o.AddRedirectToHttpsPermanent())
                .Configure<KestrelServerOptions>(o => o.Listen(new IPEndPoint(
                    IPAddress.Loopback, 443),
                    l => l.UseHttps(Path.Combine(@"E:\\Workspace\\CoWorker\", "esportasia.pfx"), "1")));

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .UseDatabaseErrorPage()
                    .UseElmPage();
            }
            else
            {
                app.UseExceptionHandler("/");
            }
            app.UseAntiforgeryMiddleware();
            app.UseRewriter();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        void IHostingStartup.Configure(IWebHostBuilder builder)
        {
            builder.CaptureStartupErrors(true)
                .UseSetting(WebHostDefaults.ApplicationKey, PlatformServices.Default.Application.ApplicationName)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureLogging(ConfigureLogging)
                .ConfigureServices(ConfigureService)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                .UseApplicationInsights()
                .UseStartup<HostStartup>();
        }
    }
}
