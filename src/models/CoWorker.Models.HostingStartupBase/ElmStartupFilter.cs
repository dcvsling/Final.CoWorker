using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using CoWorker.Builder;

namespace CoWorker.Models.HostingStartupBase
{

    public class ElmStartupFilter : IStartupFilter
    {
        Action<IApplicationBuilder> IStartupFilter.Configure(Action<IApplicationBuilder> next)
            => app => Configure(app,next);

        public void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next)
        {
            var logger = app.ApplicationServices.GetService<ILogger<ElmStartupFilter>>();
            logger.LogInformation($"begin {nameof(ElmStartupFilter)} application builder ");
            Configure(app,
                logger,
                app.ApplicationServices.GetService<IHostingEnvironment>(),
                next);
            logger.LogInformation($"end {nameof(ElmStartupFilter)} application builder ");
            app.UseStatusCodePagesWithRedirects("{0}");
        }

        public void Configure(
            IApplicationBuilder app,
            ILogger<ElmStartupFilter> logger,
            IHostingEnvironment env,
            Action<IApplicationBuilder> next)
        {
            app.UseElmCapture();
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandler = ErrorHandler })
                    .UseElmPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            app.UseAntiforgeryMiddleware();
            app.UseRewriter();
            next(app);
            app.UseStatusCodePages();
        }

        private Task ErrorHandler(HttpContext context)
        {
            var feature = context.Features.Get<IExceptionHandlerPathFeature>();
            var msg = JsonConvert.SerializeObject(
                new
                {
                    context.Response.StatusCode,
                    feature.Path,
                    feature.Error
                }, new JsonSerializerSettings().Initialize());
            return context.Response.WriteAsync(msg);
        }
    }
}
