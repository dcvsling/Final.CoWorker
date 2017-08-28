
namespace CoWorker.Rest.Conventions
{
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    public class DomainModelConvention : IApplicationModelConvention
	{
		private RouteValueConvention values;
		private RouteSelectorConvention selector;
		private ModelBindingConvention bindings;
		private ILogger<DomainModelConvention> logger;

		public DomainModelConvention(
			RouteValueConvention values,
			RouteSelectorConvention selector,
			ModelBindingConvention bindings,
			ILoggerFactory logger)
		{
			this.values = values;
			this.selector = selector;
			this.bindings = bindings;
			this.logger = logger.CreateLogger<DomainModelConvention>();
		}

		public void Apply(ApplicationModel application)
			=> application.Controllers.Each(x => Apply(x));

		public void Apply(ActionModel action)
		{
			values.Apply(action);
			bindings.Apply(action);
			selector.Apply(action);
			action.ApiExplorer.GroupName = action.Selectors.FirstOrDefault().AttributeRouteModel.Template.Replace("/", ".");
		}

		public void Apply(ControllerModel controller)
		{
			values.Apply(controller);
			selector.Apply(controller);
			controller.Actions.Each(x => Apply(x));
			bindings.Apply(controller);
			controller.ApiExplorer.GroupName = string.Join(".", new string[] { "model", "domain" }.Where(x => !string.IsNullOrEmpty(x)));//Selectors.FirstOrDefault().AttributeRouteModel.Template.Replace("/",".");
		}
	}
}
