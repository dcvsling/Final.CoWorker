
namespace CoWorker.Rest.Internal
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.Configuration;

    public class RestController : ControllerBase
	{
        private IActionContextAccessor accessor;
        public ActionContext ActionContext => accessor.ActionContext;
		public IHostingEnvironment Environment { get; }
		public FileExtensionContentTypeProvider ContentTypes { get; }
		public IUrlHelper UrlHelper { get; }
        public IConfiguration Configuration { get; }
        public RestController(
            IActionContextAccessor accessor,
            IHostingEnvironment env,
            IConfiguration config,
			FileExtensionContentTypeProvider contentType,
			IUrlHelperFactory urlfactory)
        {
            this.accessor = accessor;
			this.Environment = env;
			this.ContentTypes = contentType;
			this.UrlHelper = urlfactory.GetUrlHelper(ActionContext);
            this.Configuration = config;
            this.ControllerContext = new ControllerContext(ActionContext);
        }
	}
}
