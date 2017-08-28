using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace CoWorker.Rest.Features
{

	public class BindingSourceTemplateProvider : IBindingSourceTemplateProvider
	{
		public BindingSourceTemplateProvider(
			string name,
			string template,
			BindingSource source
			)
		{
			this.BindingSource = source;
			this.Template = template;
			this.Name = name;
			this.Order = 0;
		}

		public BindingSource BindingSource { get; }

		public String Template { get; }

		public Int32? Order { get; }

		public String Name { get; }
	}
}
