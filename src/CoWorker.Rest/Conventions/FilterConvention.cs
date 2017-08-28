//using Microsoft.AspNetCore.Authentication.Cookies;
//using CoWorker.Rest.Internal;

//namespace CoWorker.Rest.Conventions
//{
//    using Microsoft.AspNetCore.Mvc.ApplicationModels;
//    using Microsoft.Extensions.Options;
//    using Microsoft.AspNetCore.Mvc.Filters;
//    using Microsoft.AspNetCore.Mvc.Authorization;
//    using Microsoft.AspNetCore.Mvc;
//    using Microsoft.AspNetCore.Mvc.Internal;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using CoWorker.Rest.Internal;
//    using Microsoft.AspNetCore.Authorization;

//    public class FilterConvention : IActionModelConvention
//	{
//        private readonly IEnumerable<IActionModelFilterConvention> _filters;

//        public FilterConvention(IEnumerable<IActionModelFilterConvention> filters)
//        {
//            this._filters = filters;
//        }
//		public String Name => nameof(FilterConvention);
//		public void Apply(ActionModel action)
//		{
//            action.Filters.Clear();
//#warning auth should be open
//            //if (action.Filters.Any()) return;
//            //_filters.Each(x => x.Apply(action));
//        }
//	}

//    public class ClaimsAuthorizeFilterActionModelConvention : IActionModelFilterConvention
//    {
//        public String Name => nameof(AuthorizeFilter);

//        private readonly IRepositoryProvider _provider;

//        public ClaimsAuthorizeFilterActionModelConvention(
//            IRepositoryProvider provider
//            )
//        {
//            _provider = provider;
//        }
//        public void Apply(ActionModel action)
//        {
//            var template = action.GetTemplateName();
//            var matchclaims = _provider.Using<Service>(
//                x => x.Set<Service>()
//                    .Where(y => y.Path == template)
//                    .Each(y => action.Filters.Add(
//                        new AuthorizeFilter(
//                            y.Claims.Aggregate(
//                                new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme),
//                                (seed, next) => {
//                                    seed.RequireClaim(next.DisplayName);
//                                    return seed;
//                                }).Build()
//                            )
//                        )
//                    )
//                );
//        }
//    }



//    public interface IActionModelFilterConvention : IActionModelConvention { }

//    public class AllowAnonmousFilterActionModelConvention : IActionModelFilterConvention
//    {
//        public String Name => nameof(AllowAnonymousFilter);

//        public void Apply(ActionModel builder)
//        {
//            builder.Filters.Add(new AllowAnonymousFilter());
//            builder.Filters.Where(x => typeof(IAuthorizationFilter).IsAssignableFrom(x.GetType()))
//                .Each(x => builder.Filters.Remove(x));
//        }
//    }

//    public class ResponseCacheFilterConvention : IActionModelFilterConvention
//    {
//        public String Name => nameof(ResponseCacheFilter);

//        private readonly ResponseCacheRuleFeature _feature;

//        public ResponseCacheFilterConvention(ResponseCacheRuleFeature feature)
//        {
//            _feature = feature;
//        }
//        public void Apply(ActionModel action)
//        {
//            var profile =_feature.Rules.Where(x => x.Key(action))
//                .GroupBy(x => x.Value)
//                .Aggregate((seed, next) => seed.Count() > next.Count() ? seed : next).Key;
//                ;
//            action.Filters.Add(new ResponseCacheFilter(profile));
//        }
//    }

//    public class ResponseCacheRuleFeature
//    {
//        public IDictionary<Func<ActionModel, bool>,CacheProfile> Rules { get; } = new Dictionary<Func<ActionModel, bool>, CacheProfile>();
//    }

//    public class ResponseCacheRuleFeatureConfigureOptions : IConfigureOptions<ResponseCacheRuleFeature>
//    {
//        private readonly Func<ActionModel,bool> _predicate;
//        private readonly Action<CacheProfile> _config;

//        public ResponseCacheRuleFeatureConfigureOptions(Action<CacheProfile> config,Func<ActionModel,bool> predicate)
//        {
//            _predicate = predicate;
//            _config = config;
//        }
//        public void Configure(ResponseCacheRuleFeature options)
//        {
//            var profile = new CacheProfile();
//            _config(profile);
//            options.Rules.Add(_predicate, profile);
//        }
//    }

//    public class RequireHttpsAttributeFilterConvention : IActionModelFilterConvention
//    {
//        public String Name => nameof(RequireHttpsAttribute);

//        public void Apply(ActionModel action)
//            => action.Filters.Add(new RequireHttpsAttribute());
//    }
//}
