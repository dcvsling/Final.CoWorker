using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Design;

namespace CoWorker.Builder
{
    using Microsoft.Extensions.DependencyInjection;
    using CoWorker.EntityFramework.Abstractions;
    using CoWorker.EntityFramework;
    using System;
    using Microsoft.Extensions.Configuration;

    public sealed class SqliteEntityFrameworkInternalService : EntityFrameworkInternalService
    {
        public override System.String Name => "Sqlite";
        public SqliteEntityFrameworkInternalService(
             IOperationReportHandler reportHandler,
             IConfiguration config,
             IHostingEnvironment env) : base(reportHandler, config, env)
        {
        }

        public override void Initialize(IServiceCollection services)
            => services.AddEntityFrameworkSqlite()
                .AddSingleton<IDbContextConfigureOptions, SqliteDbContextConfigureOptions>();
    }
}