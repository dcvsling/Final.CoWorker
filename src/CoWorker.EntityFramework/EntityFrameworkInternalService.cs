
namespace CoWorker.Builder
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore.Design.Internal;
    using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public abstract class EntityFrameworkInternalService :  IEntityFrameworkInternalService
    {
        private IServiceProvider _provider;
        private bool _isDispose;
        private readonly IOperationReportHandler _reportHandler;
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public abstract string Name { get; }
        protected EntityFrameworkInternalService(
            IOperationReportHandler reportHandler,
            IConfiguration config,
            IHostingEnvironment env)
        {
            this._reportHandler = reportHandler;
            this._config = config;
            this._env = env;
        }


        private IServiceProvider InitializeService()
        {
            var _services = new ServiceCollection()
                .AddDefaultService()
                .AddSingleton(_env)
                .AddSingleton(_config)
                .AddEntityFramework()
                .AddScaffolding(new OperationReporter(_reportHandler));
            Initialize(_services);
            _provider = _services.BuildServiceProvider();
            return _provider;
        }

        public abstract void Initialize(IServiceCollection services);

        public Object GetService(Type serviceType)
            => (_provider ?? InitializeService()).GetService(serviceType);

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDispose) return;
                
            if (disposing)
            {
                (_provider as IDisposable)?.Dispose();
            }
            _provider = null;
            _isDispose = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}