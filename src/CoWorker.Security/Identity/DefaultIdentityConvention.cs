using Microsoft.AspNetCore.Http;

namespace CoWorker.Identity
{
	using System;
	using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;

    public class DefaultIdentityOptionsConfigureOptions : IConfigureOptions<IdentityOptions>
    {
        private readonly IOptions<CookieOptions> _options;


        public DefaultIdentityOptionsConfigureOptions(IOptions<CookieOptions> options)
        {
            this._options = options;
        }

        protected virtual void Apply(PasswordOptions password)
		{
			password.RequiredLength = 8;
			password.RequireDigit = true;
			password.RequireNonAlphanumeric = true;
		}
        protected virtual void Apply(LockoutOptions lockout)
		{
			lockout.MaxFailedAccessAttempts = 5;
			lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 0, 15);
		}
        protected virtual void Apply(UserOptions user)
		{
			user.RequireUniqueEmail = true;
		}
        protected virtual void Apply(ClaimsIdentityOptions claimsIdentity)
		{
            var prefix = _options.Value.Domain + "/";
			claimsIdentity.RoleClaimType = prefix + "role";
			claimsIdentity.UserIdClaimType = prefix + "uid";
			claimsIdentity.SecurityStampClaimType = prefix + "ssc";
			claimsIdentity.UserNameClaimType = prefix + "username";
		}
        protected virtual void Apply(SignInOptions signIn)
		{
			signIn.RequireConfirmedEmail = true;
		}
        protected virtual void Apply(TokenOptions token) { }
		public virtual void Configure(IdentityOptions options)
		{
			Apply(options.ClaimsIdentity);
			Apply(options.Tokens);
			Apply(options.Lockout);
			Apply(options.Password);
			Apply(options.SignIn);
			Apply(options.User);
		}
    }
}
