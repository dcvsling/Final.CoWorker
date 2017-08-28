namespace CoWorker.Security.Authentication
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authentication;
    using System;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.Extensions.Options;

    public class AuthorizationHandler : IAuthorizationHandler
    {
        private const string LastSchema = "LastSchema";
        private IAuthenticationSchemeProvider schemas;
        private readonly CookieAuthenticationOptions _options;
        private readonly ICookieManager _manager;
        private readonly IHostingEnvironment _environment;
        private readonly IOptions<CookieOptions> _cookieOptions;
        private readonly IHttpContextAccessor _accessor;

        public AuthorizationHandler(
            IHostingEnvironment environment,
            IAuthenticationSchemeProvider schemas,
            IHttpContextAccessor accessor)
        {
            this.schemas = schemas;
            _environment = environment;
            this._accessor = accessor;
        }

        public Task<IEnumerable<AuthenticationScheme>> ListScheme
            => Task.Run(schemas.GetAllSchemesAsync)
                .ContinueWith(t => t.Result.Select(x =>
                    new AuthenticationScheme()
                    {
                        Url = $"{_accessor.HttpContext.Request.Path.ToString()}/{x.DisplayName}",
                        Image = $"{_accessor.HttpContext.Request.Path.ToString()}/{x.DisplayName}".Replace("auth", "rsc"),
                        DisplayName = x.DisplayName,
                        Scheme = x.Name
                    }));


        async public Task ChallengeAsync(string scheme = null)
        {
            var context = _accessor.HttpContext;
            if(string.IsNullOrEmpty(scheme))
            {
                await context.ChallengeAsync();
                return;
            }
            var schemes = await schemas.GetAllSchemesAsync();
            var currentScheme = schemes.FirstOrDefault(x => x.Name.Equals(scheme, StringComparison.OrdinalIgnoreCase))
                ?.Name
                ?? (await schemas.GetDefaultChallengeSchemeAsync()).Name;
            var properties =new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                    RedirectUri = context.Request.Query[_options.ReturnUrlParameter].ToString()
                };

            await context.ChallengeAsync(currentScheme,properties);
        }

        public Task<Dictionary<string, string>> User
            => Task.FromResult(_accessor.HttpContext.User.Identities.SelectMany(x => x.Claims)
                .ToDictionary(x => x.Type, x => x.Value));

        async public Task Login()
        {
            var httpcontext = _accessor.HttpContext;
            await _accessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, httpcontext.User);
        }

        public Task Logout()
            => _accessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

}
