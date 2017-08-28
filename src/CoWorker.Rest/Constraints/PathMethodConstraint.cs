//using Microsoft.AspNetCore.Routing.Template;

//namespace CoWorker.Rest.Internal
//{
//	using CoWorker.Abstractions;
//	using System.Collections.Immutable;
//	using Microsoft.AspNetCore.Mvc.Abstractions;
//	using Microsoft.AspNetCore.Mvc.ActionConstraints;
//	using Microsoft.AspNetCore.Mvc.Infrastructure;
//	using Microsoft.AspNetCore.Mvc.Internal;
//	using Microsoft.Extensions.DependencyInjection;
//	using System;
//	using System.Collections.Generic;
//	using System.Linq;
//	using Microsoft.AspNetCore.Http;
//	using Microsoft.AspNetCore.Routing;
//	using Microsoft.AspNetCore.Mvc.ApplicationModels;

//	public class PathMethodConstraint : IActionConstraint
//	{
//		private IEnumerable<string> methods;
//		public Int32 Order => 100;
//        private string template;
//		public PathMethodConstraint(string template,params string[] methods) :this(template,methods.AsEnumerable()) { }
//        public PathMethodConstraint(string template, IEnumerable<string> methods)
//        {
//            this.methods = methods;
//            this.template = template;
//        }
//		public Boolean Accept(ActionConstraintContext context)
//		{
//			var request = context.RouteContext.HttpContext.Request;
//			if (IsMatchHttpMethod(context) && request.IsHttps && IsMatchRoute(context)) return true;
//			var actions = FindByRouteData(context.RouteContext.HttpContext, context.CurrentCandidate.Action.RouteValues["controller"].Replace(".", "/"));
//            var matchresult = Match(template, context.RouteContext.HttpContext.Request.Path);

//            context.Candidates = actions.Select(y => CreateCandidate(y)).ToImmutableList();
//			context.CurrentCandidate = context.Candidates.FirstOrDefault();
//			return actions.Count() == 1;
//		}

//		private bool IsMatchHttpMethod(ActionConstraintContext context)
//			=> new HttpMethodActionConstraint(methods).Accept(context);

//		private bool IsMatchRoute(ActionConstraintContext context)
//			=> IsRouteValueMatch(
//				context.CurrentCandidate.Action.AttributeRouteInfo.Template
//				, context.RouteContext.HttpContext.Request.Path.Value);

//        public static RouteValueDictionary Match(string routeTemplate, string requestPath)
//        {
//            var template = TemplateParser.Parse(routeTemplate);
//            var matcher = new TemplateMatcher(template, GetDefaults(template));
//            var values = new RouteValueDictionary();
//            var moduleMatch = matcher.TryMatch(requestPath, values);
//            return values;
//        }

//        // This method extracts the default argument values from the template.
//        private static RouteValueDictionary GetDefaults(RouteTemplate parsedTemplate)
//            => parsedTemplate.Parameters.ExpectNull().Aggregate(
//                new RouteValueDictionary(), 
//                (seed, next) => 
//                {
//                    seed.Add(next.Name, next.DefaultValue);
//                    return seed;
//                });

//        private bool IsRouteValueMatch(string current, string template)
//			=> current.Split("/", StringSplitOptions.RemoveEmptyEntries).Zip(
//				template.Split("/", StringSplitOptions.RemoveEmptyEntries),
//				(x, y) => y.StartsWith("{",StringComparison.OrdinalIgnoreCase)
//					? (x == y)
//					: String.IsNullOrEmpty(y))
//				.Aggregate((x,y) => x && y);

//		private ActionSelectorCandidate CreateCandidate(ActionDescriptor descriptor)
//			=> new ActionSelectorCandidate(descriptor, descriptor.ActionConstraints.OfType<IActionConstraint>().ToImmutableList());

//		private IEnumerable<ActionDescriptor> FindByRouteData(HttpContext context,string template)
//			=> context.RequestServices
//				.GetService<IActionDescriptorCollectionProvider>()
//				.ActionDescriptors.Items
//				.Where(x => context.Request.Path
//				.StartsWithSegments($"/{template}")
//				&& x.RouteValues["verb"].Equal(context.Request.Method,StringComparison.OrdinalIgnoreCase));
//	}
//}
