
namespace CoWorker.Rest.Internal
{
	using CoWorker.Abstractions;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;
	using Microsoft.AspNetCore.Mvc.Routing;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class HttpVerbRouteFactory
    {
		private IDictionary<string, Func<string, IRouteTemplateProvider>> providers;
		public HttpVerbRouteFactory() => CreateSource();

		public IRouteTemplateProvider Create(IEnumerable<string> templates,string method = Helper.EmptyString)
			=> this.Create(templates.Aggregate(AttributeRouteModel.CombineTemplates), method);
		public IRouteTemplateProvider Create(string template,string method = Helper.EmptyString)
			=>  method == Helper.EmptyString
				? new RouteAttribute(template)
				: CreateTemplate(template,providers[method]);

		private IRouteTemplateProvider CreateTemplate(string template,Func<string, IRouteTemplateProvider> factory)
			=> factory(template) as HttpMethodAttribute;

		private void CreateSource()
		{
			var basetype = typeof(HttpMethodAttribute);
			providers = basetype.Assembly.ExportedTypes
				.Where(x => basetype.IsAssignableFrom(x) && x != basetype)
				.ToDictionary<Type, string, Func<string, IRouteTemplateProvider>>(
					x => x.Name.Remove("Attribute").Remove("Http"),
					x => str => Activator.CreateInstance(x, str) as IRouteTemplateProvider);
		}
    }
}
