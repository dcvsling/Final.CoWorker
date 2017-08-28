using Microsoft.Extensions.DependencyInjection;
namespace CoWorker.Rest.Internal
{
	using Microsoft.AspNetCore.Mvc.ModelBinding;
	using Microsoft.AspNetCore.Mvc.Routing;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class RestMvcContext
	{
		public static RestMvcContext New => new RestMvcContext();
		private RestMvcContext() { }
		private RestMvcContext(RestMvcContext other)
		{
			this.Merge(other);
		}

		protected internal RestMvcContext Merge(RestMvcContext other)
		{
			other.HttpMethods.Each(x => this.HttpMethods.Add(x));
			other.Routes.Each(x => this.Routes.Add(x));
			other.Parameters.Each(x => this.Parameters.Add(x));
			this.Order = Math.Min(this.Order.Value, other.Order.Value);
			return this;
		}
		public RestMvcContext Clone() => new RestMvcContext(this);
		public IList<string> HttpMethods { get; } = new List<string>();
		public IList<IRouteTemplateProvider> Routes { get; } = new List<IRouteTemplateProvider>();
		public IDictionary<string,BindingSource> Parameters { get; } = new Dictionary<string, BindingSource>();
		public virtual String Template => this.Routes.Select(x => x.Template).Aggregate((x,y) => x + y);
		public virtual Int32? Order { get; internal set; } = 0;
		public virtual String Name => this.Template.Replace("/","_");
	}
}