using System.Linq;

namespace CoWorker.EntityFramework
{
    using CoWorker.EntityFramework.Abstractions;
    using Microsoft.EntityFrameworkCore;
    using CoWorker.EntityFramework.Internal;
    using CoWorker.DependencyInjection.Factory;

    public class DbContextOptionsBuilderConfigureOptions<TContext>
        : IDbContextOptionsBuilderConfigureOptions<TContext>
        where TContext : DbContext
    {
        private readonly IOptionsCache<DataSource> _sources;
        private readonly IOptionsCache<IDbContextConfigureOptions> _config;
        private readonly IModelBuilderFactory _modelbuilder;

        public DbContextOptionsBuilderConfigureOptions(
            IOptionsCache<DataSource> sources,
            IOptionsCache<IDbContextConfigureOptions> config,
            IModelBuilderFactory modelbuilder)
        {
            this._sources = sources;
            this._config = config;
            this._modelbuilder = modelbuilder;
        }
        
        public void Configure(DbContextOptionsBuilder<TContext> options)
        {
            var source = _sources.Get();
            _config.Get(source.Provider)?.Configure(source,options);
            
            var model = _modelbuilder.Create();
            ConfigureModel(model);
            options.UseModel(model.Model);
        }

        private void ConfigureModel(ModelBuilder model)
            => (typeof(TContext).IsGenericType
                    ? typeof(TContext).GenericTypeArguments
                    : typeof(TContext).GetProperties().Select(x => x.PropertyType).Where(x => typeof(DbSet<>).IsAssignableFrom(x)))
                .Each(x => model.Entity(x));
    }
}