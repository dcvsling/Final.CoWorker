namespace CoWorker.EntityFramework
{
    using Microsoft.EntityFrameworkCore;
    using CoWorker.EntityFramework.Internal;
    using CoWorker.EntityFramework.Abstractions;

    public class SqlServerDbContextConfigureOptions : IDbContextConfigureOptions
    {
        public void Configure(DataSource source, DbContextOptionsBuilder builder)
            => builder.UseSqlServer(source.ConnStr);
    }
}
