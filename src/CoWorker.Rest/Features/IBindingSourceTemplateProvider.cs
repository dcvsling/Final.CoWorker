using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace CoWorker.Rest.Features
{
    public interface IBindingSourceTemplateProvider : IRouteTemplateProvider
	{
		BindingSource BindingSource { get; }
	}
}
