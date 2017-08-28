using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoWorker.Rest.Internal
{
    using CoWorker.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        Task Remove(TEntity entity);
        Task Update(TEntity entity);
        Task<IEnumerable<T>> Query<T>(Action<IQueryable<TEntity>> query = null);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext<TEntity> _context;

        public Repository(DbContext<TEntity> context)
        {
            this._context = context;
        }
        public Task Add(TEntity entity) => _context.AddAsync(entity);
        async public Task<IEnumerable<T>> Query<T>(Action<IQueryable<TEntity>> query = null)
        {
            var set = _context.Set<TEntity>();
            query(set);

            var result = await set.ToListAsync();
            return result.Cast<T>();
        }
        public Task Remove(TEntity entity) => Task.Run(() => _context.Attach(entity));
        public Task Update(TEntity entity) => Task.Run(() => _context.Remove(entity));
    }
}
