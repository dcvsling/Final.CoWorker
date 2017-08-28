using CoWorker.DependencyInjection.Decorator;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Design;

namespace CoWorker.Builder
{
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using Microsoft.EntityFrameworkCore.ValueGeneration;
    using CoWorker.EntityFramework.Conventions;
    using CoWorker.EntityFramework.Abstractions;
    using CoWorker.EntityFramework.EntityParts.Conventions;
    using CoWorker.EntityFramework.EntityParts.ValueGenerators;
    using CoWorker.EntityFramework;

    public static class ServiceCollectionExtensions
    {
        
        internal static IServiceCollection AddEntityFramework(this IServiceCollection services)
        {
            services.TryAddSingleton(
                typeof(IDbContextOptionsBuilderConfigureOptions<>),
                typeof(DbContextOptionsBuilderConfigureOptions<>));
            services.TryAddSingleton<IModelBuilderFactory, ModelBuilderFactory>();
            services.TryAddSingleton<IConventionSetFactory, ConventionSetFactory>();
            services.AddDefaultModelBuilderConventions();
            services.AddValueGeneratorProvider();

            return services;
        }

        public static IServiceCollection AddEntityFrameworkService(this IServiceCollection services)
        {
            services.AddLogging();
            services.TryAddSingleton<IEntityFrameworkInternalService, SqlServerEntityFrameworkInternalService>();
            //services.TryAddSingleton<IEntityFrameworkInternalService, SqliteEntityFrameworkInternalService>();
            services.TryAddSingleton(typeof(ModelDbContextFactory<>));
            services.TryAddSingleton(typeof(IDesignTimeDbContextFactory<>), typeof(ModelDbContextFactoryDecorator<>));
            services.TryAddSingleton<IOperationReportHandler, DefaultLoggingOperationReportHandler>();
            
            return services;
        }


        public static IServiceCollection AddConventionSet<TConvetion>(this IServiceCollection services)
            where TConvetion : class, IConfigureOptions<ConventionSet>
        {
            services.TryAddSingleton<IConfigureOptions<ConventionSet>, TConvetion>();
            return services;
        }

        public static IServiceCollection AddDefaultModelBuilderConventions(this IServiceCollection services)
        {
            services.TryAddSingleton<ICoreConventionSetBuilder, CoreConventionSetBuilder>();
            services.TryAddSingleton<CoreConventionSetBuilderDependencies>();
            return services.AddConventionSet<AutoKeyConventions>()
                .AddConventionSet<StringMaxLengthConventions>()
                .AddConventionSet<FormatTableNameConvention>()
                .AddConventionSet<NeverNullConvention>()
                .AddConventionSet<ForceRemoveAllCascadeOnDeleteConvention>()
                .AddConventionSet<AutoLogDatetimeConventions>()
                .AddConventionSet<AutoRowLogConvention>();
        }
        public static IServiceCollection AddSharedUnit<TUnit>(this IServiceCollection services)
            => services.AddConventionSet<SharedUnitConvention<TUnit>>();

        public static IServiceCollection AddValueGeneratorProvider(this IServiceCollection services)
            => services.AddSingleton<ValueGeneratorFactory, ValueGenFactory>()
                .AddDefaultValueGeneratorProviders();
        
        public static IServiceCollection AddValueGeneratorProvider<TProvider>(
            this IServiceCollection services)
            where TProvider : class, IValueGeneratorProvider
            => services.AddSingleton<IValueGeneratorProvider, TProvider>();

        public static IServiceCollection AddDefaultValueGeneratorProviders(this IServiceCollection services)
            => services.AddValueGeneratorProvider<CreateDateValueGenerator>()
                .AddValueGeneratorProvider<ModifyDateValueGenerator>()
                .AddValueGeneratorProvider<CreatorClaimsValueGenerator>()
                .AddValueGeneratorProvider<ModifierClaimsValueGenerator>();
    }
}