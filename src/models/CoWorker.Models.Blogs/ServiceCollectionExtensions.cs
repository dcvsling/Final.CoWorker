namespace CoWorker.Models.Blogs
{
    using Microsoft.Extensions.Options;
    using CoWorker.Models.Blogs.Migrations;
    using CoWorker.Models.Blog;
    using Microsoft.Extensions.DependencyInjection;
    using CoWorker.Builder;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlog(this IServiceCollection services)
            => services
                .AddSingleton<BlogSeed>()
                .AddDefaultValueGeneratorProviders()
                .AddSingleton<IConfigureOptions<PostRelated>,PostRelatedConfigureOptions>()
                .AddScoped<IBlogFactory, BlogFactory>()
                .AddScoped<BlogController>();

        //public static IServiceCollection AddBlog<TContext>(this IServiceCollection services,
        //    Action<IServiceProvider, DbContextOptionsBuilder<TContext>> config)
        //    where TContext : BlogDbContext
        //    => services.AddBlog<TContext>()
        //        .AddSingleton<IConfigureOptions<DbContextOptionsBuilder<TContext>>>(
        //            p => new ConfigureOptions<DbContextOptionsBuilder<TContext>>(o => config(p, o)));
    }
}
