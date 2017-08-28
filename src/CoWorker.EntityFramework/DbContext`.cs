namespace CoWorker.EntityFramework
{
    using Microsoft.EntityFrameworkCore;

    public class DbContext<TModel> : DbContext where TModel : class
    {
        public DbContext(DbContextOptions options) : base(options) { }
    }

    public class DecorateDbContext<TModel> : DbContext<TModel> where TModel : class
    {
        public DecorateDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}