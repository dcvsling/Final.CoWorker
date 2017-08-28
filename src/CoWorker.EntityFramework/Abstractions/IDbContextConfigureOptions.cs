namespace CoWorker.EntityFramework.Abstractions
{
    using CoWorker.EntityFramework.Internal;
    using Microsoft.EntityFrameworkCore;

    public interface IDbContextConfigureOptions
    {
        void Configure(DataSource source, DbContextOptionsBuilder builder);
    }
}
