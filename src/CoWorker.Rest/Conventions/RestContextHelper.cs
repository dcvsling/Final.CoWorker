using Microsoft.Extensions.DependencyInjection;
namespace CoWorker.Rest.Internal
{
    using CoWorker.Rest.Features;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
	using Microsoft.AspNetCore.Mvc.Routing;
	using System;
	using System.Collections.Generic;
	using System.Linq;

    public static class RestContextHelper
    {
        public static RestMvcContext GetContext(this IDictionary<object, object> properties)
        {
            var key = nameof(RestMvcContext);
            if (!properties.ContainsKey(key)) properties.Add(key, RestMvcContext.New);
            return properties[key] as RestMvcContext;
        }

        public static void AddBindingSourceTemplateProvider(this RestMvcContext context, IBindingSourceTemplateProvider provider)
        {
            context.Routes.Add(provider);
            context.Parameters.Add(provider.Name, provider.BindingSource);
        }
        public static void AddHttpMethodTemplateProvider(this RestMvcContext context, IRouteTemplateProvider provider)
        {
            context.Routes.Insert(0, provider);
            (provider as IActionHttpMethodProvider)?.HttpMethods.Each(x => context.HttpMethods.Add(x));
        }
    }
}