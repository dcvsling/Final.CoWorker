
namespace CoWorker.Rest.Conventions
{
	using CoWorker.Abstractions;
	using CoWorker.Rest.Internal;
	using CoWorker.Rest.Features;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;
	using Microsoft.AspNetCore.Mvc.Internal;
	using Microsoft.AspNetCore.Mvc.Routing;
	using System;
	using System.Collections.Generic;
	using System.Linq;
    using Microsoft.EntityFrameworkCore.Scaffolding.Internal;

    public class RouteSelectorConvention : IControllerModelConvention,IActionModelConvention
	{
		public String Name => nameof(RouteSelectorConvention);
		private IEnumerable<string> methods => HttpMethodGroup.All.GetNames();
		private HttpVerbRouteFactory factory;
		public RouteSelectorConvention(RestConventionOptions options)
		{
			this.factory = options.HttpVerbFactory;
		}

		public void Apply(ControllerModel controller)
		{
			if (HasAttributeRoute(controller.Selectors)) return;
			var context = controller.Properties.GetContext();
			context.Routes.Add(new RouteAttribute(controller.ControllerName.Replace("_", "/")));
			var template = context;
			controller.Selectors
				.Each(x => {
					x.AttributeRouteModel = new AttributeRouteModel()
					{
						Name = template.Name.Replace("/", "."),
						Template = template.Template
					};
				});
		}

		public void Apply(ActionModel action)
		{
			if (HasAttributeRoute(action.Selectors)) return;
			var context = action.Properties.GetContext();
			var commandTemplate = action.RouteValues.ContainsKey("command") ? action.RouteValues["command"]: string.Empty;
            context.AddHttpMethodTemplateProvider(factory.Create(commandTemplate,(HttpMethod)Enum.Parse(typeof(HttpMethod),action.RouteValues["verb"],true)));
			action.Selectors.Each(x =>
			{
				x.AttributeRouteModel = new AttributeRouteModel()
				{
					Name = action.GetTemplateName(),
					Template = context.Template
				};
				x.ActionConstraints.Add(new HttpMethodActionConstraint(context.HttpMethods));
			});
		}

		private bool HasAttributeRoute(IList<SelectorModel> selectors)
			=> selectors.Any(selector => selector.AttributeRouteModel != null);
	}
}
