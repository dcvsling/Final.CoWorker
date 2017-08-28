using CoWorker.Security.OAuth;

namespace CoWorker.Builder
{
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication;
	using CoWorker.Identity;
    using CoWorker.Security.Authentication;
    using Microsoft.AspNetCore.Authentication.OAuth;

    public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection services)
			=> services
				.AddDbContext<IdentityDbContext>()
                .AddSingleton<IConfigureOptions<IdentityOptions>, DefaultIdentityOptionsConfigureOptions>()
				.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<IdentityDbContext>()
				.AddDefaultTokenProviders()
				.Services;

        public static IServiceCollection AddSecurityService(this IServiceCollection services)
            => services.AddSingleton<IClaimsTransformation, AdditionalClaimTransformation>()
                .AddScoped<IAuthorizationHandler, AuthorizationHandler>()
                .AddClaimsProvider<DefaultClaimProvider>()
                .AddSingleton<IConfigureOptions<OAuthOptions>,OAuthOptionsConfigureOptions>();

        public static IServiceCollection AddClaimsProvider<TProvider>(this IServiceCollection services)
            where TProvider : class,IClaimProvider
            => services.AddSingleton<IClaimProvider, TProvider>();

    }
}
