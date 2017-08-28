
namespace CoWorker.Rest.Builder
{
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Configuration;

    public class MvcOptionsConfigureOptions : IConfigureOptions<MvcOptions>
	{
		private IEnumerable<IApplicationModelConvention> conventions;
        private readonly IConfiguration _config;
        private const string CONFIG_ROOT = "mvc";
        private string GetPath(string path) => CONFIG_ROOT + ":" + path;
        public MvcOptionsConfigureOptions(
			IEnumerable<IApplicationModelConvention> conventions,
            IConfiguration config
			)
		{
			this.conventions = conventions;
            _config = config;
        }

		public virtual void Configure(MvcOptions options)
		{
			conventions.Each(x => options.Conventions.Add(x));
            options.SslPort = _config.GetSection(GetPath(nameof(MvcOptions.SslPort))).Get<int>();
            options.RequireHttpsPermanent = _config.GetSection(GetPath(nameof(MvcOptions.RequireHttpsPermanent))).Get<bool>();
        }
	}
}
