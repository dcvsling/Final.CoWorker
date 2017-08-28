
namespace CoWorker.Rest.Conventions
{
	using CoWorker.Abstractions;
	using CoWorker.Rest.Features;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public class RouteValueConvention : IActionModelConvention
	{
		public String Name => nameof(RouteValueConvention);
		public ControllerModelFeature ModelFeature { get; }
		public RouteValueConvention(RestConventionOptions options)
        {
			this.ModelFeature = options.ControllerModelFeature;
		}

		public void Apply(ControllerModel controller)
		{
			controller.RouteValues.Clear();
			controller.RouteValues.Add("model", ModelFeature.Features.FirstOrDefault(x => x.Value == controller.ControllerType.GetGenericDefinitionIfGeneric()).Key);
			controller.RouteValues.Add("domain", controller.ControllerType.AsType().IsGenericType ? controller.ControllerType.GenericTypeArguments.Select(x => x.Name).ToJoin(",") : string.Empty);
			controller.ControllerName = $"{ controller.RouteValues["model"] }{ (controller.ControllerType.AsType().IsGenericType ? $"_{controller.RouteValues["domain"]}" : "")}";
			ClearEmptyRouteValue(controller.RouteValues);
		}

		public void Apply(ActionModel action)
		{
			action.RouteValues.Clear();
			action.RouteValues.Add("command", IsCommandAction(action) ? action.ActionName : string.Empty);
			action.RouteValues.Add("verb", IsCommandAction(action) ? "Get" : action.ActionName);
			action.Parameters.Each(x => action.RouteValues.Add(x.ParameterName, null));
			action.ActionName = $"{action.ActionName}({action.Parameters.Select(x => x.ParameterName).ToJoin(",")})";
			ClearEmptyRouteValue(action.RouteValues);
		}

		public void ClearEmptyRouteValue(IDictionary<string,string> values)
			=> values.Where(x => string.IsNullOrEmpty(x.Value)).Each(x => values.Remove(x.Key));

		public bool IsCommandAction(ActionModel action)
			=> !HttpMethodGroup.All.GetNames().Contains(string.IsNullOrEmpty(action.ActionName) ? action.RouteValues["command"] : action.ActionName);
	}
}
