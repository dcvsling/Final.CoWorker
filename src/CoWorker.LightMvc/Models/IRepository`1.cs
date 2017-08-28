using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoWorker.LightMvc.Internal
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
}
