namespace CoWorker.EntityFramework
{
    using CoWorker.EntityFramework.Abstractions;
    using CoWorker.EntityFramework.Internal;
    using Microsoft.EntityFrameworkCore;
    using System;
    public class SqliteDbContextConfigureOptions : IDbContextConfigureOptions
    {
        public void Configure(DataSource source, DbContextOptionsBuilder builder)
            => builder.UseSqlite(source.ConnStr);
    }
}
