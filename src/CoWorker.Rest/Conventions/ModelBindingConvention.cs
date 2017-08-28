
namespace CoWorker.Rest.Conventions
{
	using CoWorker.Abstractions;
	using CoWorker.Rest.Internal;
	using CoWorker.Rest.Features;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;
	using Microsoft.AspNetCore.Mvc.ModelBinding;
	using System;
	using System.Linq;
	public class ModelBindingConvention : IActionModelConvention
	{
		public String Name => nameof(ModelBindingConvention);
		private ParameterModelFeature feature;
		public ModelBindingConvention(RestConventionOptions options)
			=> this.feature = options.ParameterModelFeature;
		public void Apply(ControllerModel controller)
			=> controller.ControllerProperties.Each(x => Apply(x));

		private void Apply(PropertyModel property)
		{
			if (!feature.Features.ContainsKey(property.PropertyName)) return;
			var context = feature.Features[property.PropertyName];
			property.Properties.Add(nameof(RestMvcContext), context);
			property.BindingInfo = new BindingInfo()
			{
				BinderModelName = property.PropertyName,
				BindingSource = context.BindingSource
			};
		}

		public void Apply(ActionModel builder)
		{
			builder.Parameters.Each(x => Apply(x));
		}

		private void Apply(ParameterModel param)
		{
			param.BindingInfo = new BindingInfo();
			param.BindingInfo.BinderModelName = param.ParameterName;
			param.BindingInfo.BindingSource = GetBindingSource(param);
		}
		private BindingSource GetSources(string name,params BindingSource[] sources)
			=> CompositeBindingSource.Create(sources,name);

		private BindingSource GetBindingSource(ParameterModel model)
		{
            if (!feature.Features.ContainsKey(model.ParameterName)) return BindingSource.ModelBinding;
			var source = feature.Features[model.ParameterName];
			model.Action.Properties.GetContext().AddBindingSourceTemplateProvider(source);
			return source.BindingSource;
		}
	}
}
