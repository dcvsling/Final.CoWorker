
namespace CoWorker.Builder
{
    using System;
    using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
    using CoWorker.Security.OAuth.Twitch;
    using Microsoft.AspNetCore.Authentication.Facebook;
    using CoWorker.DependencyInjection.Configuration;
    using Microsoft.AspNetCore.Authentication.Google;
    using CoWorker.Security.OAuth;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication;
    using CoWorker.Security.Authentication;
    using Microsoft.AspNetCore.Authentication.OAuth;
    using Microsoft.AspNetCore.Authentication.Cookies;

    public static class ServiceCollectionExtensions
	{
        public static AuthenticationBuilder AddCookieAuth(this IServiceCollection services)
            => services.AddSingleton<IPostConfigureOptions<GoogleOptions>, ConfigurationConfigureOptions<GoogleOptions>>()
                .AddSingleton<IPostConfigureOptions<FacebookOptions>, ConfigurationConfigureOptions<FacebookOptions>>()
                .AddSingleton<IPostConfigureOptions<TwitchOptions>, ConfigurationConfigureOptions<TwitchOptions>>()
                .AddSingleton<IPostConfigureOptions<MicrosoftAccountOptions>, ConfigurationConfigureOptions<MicrosoftAccountOptions>>()
                .AddSingleton<IPostConfigureOptions<CookieAuthenticationOptions>, ConfigurationConfigureOptions<CookieAuthenticationOptions>>()
                .AddClaimBaseAuthorization()
                .AddAuthentication(o =>
                    {
                        o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    })
                    .AddCookie(o =>
                    {
                        o.LoginPath = "/auth/login";
                        o.LogoutPath = "/auth/logout";
                        o.AccessDeniedPath = "/";
                        o.ClaimsIssuer = "https://esoprtasia.tv";
                        o.ExpireTimeSpan = new TimeSpan(1, 0, 0);
                    });


        public static IServiceCollection AddClaimBaseAuthorization(this IServiceCollection services)
            => services.AddSingleton<IClaimsTransformation, AdditionalClaimTransformation>()
                .AddClaimsProvider<DefaultClaimProvider>()
                .AddSingleton<IConfigureOptions<OAuthOptions>,OAuthOptionsConfigureOptions>();

        public static IServiceCollection AddClaimsProvider<TProvider>(this IServiceCollection services)
            where TProvider : class,IClaimProvider
            => services.AddSingleton<IClaimProvider, TProvider>();
    }
}
