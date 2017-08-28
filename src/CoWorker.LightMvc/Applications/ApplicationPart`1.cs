
namespace CoWorker.LightMvc.ApplicationParts
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using System.Reflection;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.Options;

    public class ControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly ModelOptions _options;

        public ControllerFeatureProvider(IOptions<ModelOptions> options)
        {
            this._options = options.Value;
        }
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var types = parts.Where(x => x is AssemblyPart).SelectMany(x => x.As<AssemblyPart>().Assembly.ExportedTypes);
            var models = types
                .Where(x => x.Namespace.StartsWith("EsportAsia", StringComparison.Ordinal)
                    && (_options.Models.Contains(x)
                    && x.IsClass
                    && !x.IsAbstract));

            types.Where(x => x.Name.Contains("Controller"))
                .SelectMany(x => models.Select(y => x.MakeGenericType(y)))
                .Each(x => feature.Controllers.Add(x.GetTypeInfo()));
        }
    }
}
