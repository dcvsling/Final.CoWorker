
using CoWorker.Rest.Internal;
using CoWorker.Rest.Features;
using Microsoft.Extensions.Options;

namespace CoWorker.Rest.Conventions
{
    public class RestConventionOptions
    {
        public RestConventionOptions(
            HttpVerbRouteFactory httpVerbFactory,
            IOptions<ParameterModelFeature> parameterModelFeature,
            IOptions<ControllerModelFeature> controllerModelFeature)
        {
            this.HttpVerbFactory = httpVerbFactory;
            this.ParameterModelFeature = parameterModelFeature.Value;
            this.ControllerModelFeature = controllerModelFeature.Value;
        }

        public HttpVerbRouteFactory HttpVerbFactory { get; }
        public ParameterModelFeature ParameterModelFeature { get; }
        public ControllerModelFeature ControllerModelFeature { get; }
    }
}