
namespace CoWorker.Rest.Internal
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc.Routing;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public static class ActionContextHelper
    {
		public static TService GetService<TService>(this ActionContext context)
			where TService : class
			=> context.GetService(typeof(TService)) as TService;

		public static IEnumerable<TService> GetServices<TService>(this ActionContext context)
			where TService : class
			=> context.GetService(typeof(IEnumerable<>).MakeGenericType(typeof(TService))) as IEnumerable<TService>;


		public static object GetService<TContext>(this TContext context,Type type)
			where TContext : ActionContext
			=> context.HttpContext.RequestServices.GetService(type);
        
		public static HttpRequest GetRequest(this ActionContext context)
			=> context.HttpContext.Request;

        public static string GetTemplateName(this ActionModel action)
            => $"{action.Controller.ControllerName}.{action.ActionName}";

    }
}
