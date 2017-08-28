using Microsoft.AspNetCore.Authentication;
namespace CoWorker.Rest.Controllers
{
	using System.Threading.Tasks;
	using System.Linq;
	using System.IO;
	using CoWorker.Rest.Internal;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using Microsoft.Net.Http.Headers;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Http;
    using CoWorker.Security.Authentication;

    public class AuthorizationController
    {
        private const string LastSchema = "LastSchema";
        private RestController ctrler;
		private IAuthenticationSchemeProvider schemas;
        private readonly IAuthorizationHandler _handler;

        public AuthorizationController(
			RestController ctrler,
			IAuthorizationHandler handler)
        {
            this.ctrler = ctrler;
            this._handler = handler;
        }

        public Task<IEnumerable<AuthenticationScheme>> Get()
            => _handler.ListScheme;
        async public Task<AuthenticationScheme> Get(string scheme)
        {
            var schemes = await _handler.ListScheme;
            return schemes.FirstOrDefault(x => x.Scheme == scheme);
        }
        public Task<IActionResult> Post()
            => _handler.ChallengeAsync();

		async public Task<IActionResult> Post(string schema)
			=> ctrler.Challenge(
                new AuthenticationProperties() {
                    AllowRefresh = true,
                    IsPersistent = true,
                    RedirectUri = ctrler.HttpContext.Request.Headers[HeaderNames.Referer].ToString() ?? "/"},
                schemas.GetAllSchemesAsync()
                    .WaitForResult()
                    .FirstOrDefault(x => x.Name.Equals(schema, StringComparison.OrdinalIgnoreCase))
                    ?.Name
                    ?? (await schemas.GetDefaultChallengeSchemeAsync()).Name);
		public Task<Dictionary<string,string>> Me()
		    => Task.FromResult(ctrler.ActionContext.HttpContext.User.Identities.SelectMany(x => x.Claims)
                .ToDictionary(x => x.Type,x => x.Value));
        async public Task<IActionResult> Login()
        {
            var httpcontext = ctrler.ActionContext.HttpContext;
            await ctrler.ActionContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,httpcontext.User);
            httpcontext.Request.Query.TryGetValue(_options.ReturnUrlParameter, out var url);
            return ctrler.LocalRedirect(url);
        }
        async public Task<IActionResult> Logout()
        {
            var httpcontext = ctrler.ActionContext.HttpContext;
            await ctrler.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties() { });
            httpcontext.Request.Query.TryGetValue(_options.ReturnUrlParameter, out var url);
            return ctrler.LocalRedirect("/");
        }
    }
}
