using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using CoWorker.LightMvc.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CoWorker.Models.Swagger
{

    public class SwaggerStartupFilter : IStartupFilter
    {
        Action<IApplicationBuilder> IStartupFilter.Configure(Action<IApplicationBuilder> next)
           => app => Configure(app, next);

        public void Configure(IApplicationBuilder app, Action<IApplicationBuilder> next)
        {
            var logger = app.ApplicationServices.GetService<ILogger<SwaggerStartupFilter>>();
            logger.LogInformation($"begin {nameof(SwaggerStartupFilter)} application builder ");
            Configure(app,
                logger,
                app.ApplicationServices.GetService<IHostingEnvironment>(),
                next);
            logger.LogInformation($"end {nameof(SwaggerStartupFilter)} application builder ");
        }

        public void Configure(
            IApplicationBuilder app,
            ILogger<SwaggerStartupFilter> logger,
            IHostingEnvironment env,
            Action<IApplicationBuilder> next)
        {
            next(app);
            app.UseFileServer(new FileServerOptions() {
                EnableDefaultFiles = true,
                RequestPath = string.Empty,
                EnableDirectoryBrowsing = false,
                FileProvider = env.ContentRootFileProvider});
            app.UseMvc();
            app.Use(req => async ctx =>
            {
                if (env.IsDevelopment())
                {
                    if(ctx.User.Claims.Any() && !ctx.User.Identity.IsAuthenticated)
                    {
                        await ctx.SignInAsync(ctx.User);
                        logger.LogInformation($"signiin {ctx.User.FindFirst(ClaimTypes.Email).Value} for swagger");
                    }
                    else
                    {
                        await ctx.ChallengeAsync("Google");
                        logger.LogInformation("challenge for swagger");
                        return;
                    }
                }
                logger.LogInformation($"{ctx.User.FindFirst(ClaimTypes.Email).Value} enter swagger ui");
                await req(ctx);
            });

            app.UseSwaggerWithUI();
        }
    }
}
