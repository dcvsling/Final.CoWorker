using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Design;

namespace CoWorker.Builder
{
    using Microsoft.Extensions.DependencyInjection;
    using CoWorker.EntityFramework.Abstractions;
    using CoWorker.EntityFramework;

    public sealed class SqlServerEntityFrameworkInternalService : EntityFrameworkInternalService
    {
        public SqlServerEntityFrameworkInternalService(
            IOperationReportHandler reportHandler,
            Microsoft.Extensions.Configuration.IConfiguration config,
            IHostingEnvironment env) : base(reportHandler, config, env)
        {
        }

        public override System.String Name => "SqlServer";

        public override void Initialize(IServiceCollection services)
            => services.AddEntityFrameworkSqlServer()
                .AddSingleton<IDbContextConfigureOptions, SqlServerDbContextConfigureOptions>();
    }
}