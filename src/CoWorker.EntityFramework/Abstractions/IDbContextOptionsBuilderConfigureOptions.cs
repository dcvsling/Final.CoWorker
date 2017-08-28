
namespace CoWorker.EntityFramework
{
    using Microsoft.Extensions.Options;
    using Microsoft.EntityFrameworkCore;
    public interface IDbContextOptionsBuilderConfigureOptions<TContext>
        : IConfigureOptions<DbContextOptionsBuilder<TContext>>
        where TContext : DbContext
    {
    }
}