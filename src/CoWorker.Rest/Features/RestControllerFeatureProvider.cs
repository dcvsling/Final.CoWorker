
namespace CoWorker.Rest.ApplicationParts
{
	using Microsoft.AspNetCore.Mvc.ApplicationParts;
	using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.Configuration;
    using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
    

    public class RestControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public Type BaseType { get; }
		private IConfiguration config;
		public RestControllerFeatureProvider(IConfiguration config)
		{
            this.config = config;
		}

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var range = config.GetSection("domain")
                   .GetChildren()
                   .Select(y => y.Get<string>())
                   .Append(this.GetType().Namespace.Split('.').First());

           var types = parts.Cast<AssemblyPart>()
				.SelectMany(x => x.Types)
                .Where(x => range.Any(y => x.FullName.StartsWith(y)))
				.Distinct();
			var domains = types.Where(DomainFilter);
			var models = types.Where(ModelFilter);
			models.Each(
				x => {
					if (x.IsGenericType) AddGenericControllerFeature(x, domains, feature);
					else AddCommonControllerFeature(x, feature);
				});
        }
        
		private void AddGenericControllerFeature(
			Type model,
			IEnumerable<Type> domains,
			ControllerFeature feature)
			=> domains
				.Select(x => model.GetGenericTypeDefinition().MakeGenericType(x).GetTypeInfo())
				.Except(feature.Controllers)
				.Each(x => feature.Controllers.Add(x));

		private void AddCommonControllerFeature(
            Type models,
			ControllerFeature feature)
			=> (feature.Controllers.Contains(models) 
				? feature.Controllers.Add 
				: Helper.Empty<TypeInfo>())(models.GetTypeInfo());

        private bool DomainFilter(Type type)
            => (BaseType.IsAssignableFrom(type) || BaseType.IsAssignableFrom(type));

		private bool ModelFilter(Type type)
			=> type.ToFormatString().EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
				&& type.GetTypeInfo().IsGenericType;
	}
}
