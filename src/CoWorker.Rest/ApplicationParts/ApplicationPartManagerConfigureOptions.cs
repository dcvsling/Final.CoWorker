
namespace CoWorker.Rest.ApplicationParts
{
    using CoWorker.Infrastructure.TypeStore;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public class ApplicationPartManagerConfigureOptions : IConfigureOptions<ApplicationPartManager>
	{
		public string Name => nameof(ApplicationPartManagerConfigureOptions);
        private readonly IConfiguration _config;
        private readonly RestControllerFeatureProvider _ctrls;
        private readonly ITypeStore _store;

        public ApplicationPartManagerConfigureOptions(
            IConfiguration config, 
            RestControllerFeatureProvider ctrls,
            ITypeStore store)
        {
            _config = config;
            _ctrls = ctrls;
            this._store = store;
        }
        public virtual void Configure(ApplicationPartManager options)
		    => options.ApplicationParts.Add(new ApplicationPart<ITypeStore>(_store));
	}
}
