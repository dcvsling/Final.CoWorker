using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;


namespace CoWorker.Net.Antiforgery
{
    using System;
    using CoWorker.Abstractions.Helper;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Linq;
    using System.Threading.Tasks;

    public class AntiforgeryMiddleware
	{
        private string[] IgnoreValidMethod = new string[] { "GET", "HEAD", "TRACE", "OPTIONS" };
        private string[] EntryEndPoint = new string[] { "/", "/index.html" };
        private readonly IAntiforgery _antiforgery;
        private readonly ILogger<AntiforgeryMiddleware> _logger;
        private readonly AntiforgeryOptions _options;
        private readonly IHostingEnvironment _env;

        public AntiforgeryMiddleware(
            IHostingEnvironment env,
            IAntiforgery antiforgery,
            ILogger<AntiforgeryMiddleware> logger,
            IOptions<AntiforgeryOptions> options)
        {
            _antiforgery = antiforgery;
            _logger = logger;
            _options = options.Value;
            _env = env;
        }

        async public Task Invoke(HttpContext context)
        {
            string path = context.Request.Path.Value;

            if (ShouldValidate(context))
            {
                try
                {
                    await _antiforgery.ValidateRequestAsync(context);
                }
                catch (AntiforgeryValidationException exception)
                {
                    _logger.LogWarning(exception.Message, exception);
                    _logger.LogInformation(context.ToJson());
                    context.Response.StatusCode = 400;
                }
            }
            else
            {
                var tokens = _antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append(_options.Cookie.Name, tokens.RequestToken, _options.Cookie.Build(context));
            }
        }
        public RequestDelegate Middleware(RequestDelegate next)
            => async ctx =>
            {
                await Invoke(ctx);
                await (ctx.Response.StatusCode == 400 ? Task.CompletedTask : next(ctx));
            };

        private bool ShouldValidate(HttpContext context)
            => !IgnoreValidMethod.Contains(
                    context.Request.Method,
                    EqualityComparer<string>.Create(
                        obj => obj.GetHashCode(),
                        (x,y) => x.ToLower().Equals(y.ToLower(), StringComparison.OrdinalIgnoreCase)))
                || !_env.IsDevelopment();
    }
}