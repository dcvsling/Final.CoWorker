using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using CoWorker.Builder;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using CoWorker.DependencyInjection.Factory;
using Microsoft.EntityFrameworkCore.Design;

namespace CoWorker.EntityFramework
{
    using Microsoft.EntityFrameworkCore;

    public class ModelDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        private readonly IOptionsCache<IDbContextPool> _pool;
        private readonly IEntityFrameworkInternalService _internalService;
        private readonly IDbContextOptionsBuilderConfigureOptions<TContext> _config;

        public ModelDbContextFactory(
            IEntityFrameworkInternalService internalService
            )
        {
            this._internalService = internalService;
            this._pool = internalService.GetService<IOptionsCache<IDbContextPool>>();
            this._config = _internalService.GetService<IDbContextOptionsBuilderConfigureOptions<TContext>>();
        }
        public TContext CreateDbContext(System.String[] args)
        {
            var ctxType = typeof(TContext);
            var name = ctxType.IsGenericType
                ? ctxType.GetGenericArguments().First().FullName
                : ctxType.FullName;
            return _pool.GetOrAdd(name,() => Create()).Rent() as  TContext;
        }

        private IDbContextPool Create()
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            _config.Configure(builder);
            return new DbContextPool<TContext>(builder.Options);
        }
    }

    public class ModelDbContextFactoryDecorator<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private readonly ModelDbContextFactory<TContext> _factory;

        public ModelDbContextFactoryDecorator(ModelDbContextFactory<TContext> factory)
        {
            this._factory = factory;
        }

        public TContext CreateDbContext(System.String[] args)
        {
            var context = _factory.CreateDbContext(args);
            Task.Run(async () => await CheckAndMigrate(context));
            return context;
        }

        async private Task CheckAndMigrate(TContext context)
        {
            var pendings = await context.Database.GetPendingMigrationsAsync();
            if (pendings.Any())
                await context.Database.MigrateAsync();
        }

    }
}