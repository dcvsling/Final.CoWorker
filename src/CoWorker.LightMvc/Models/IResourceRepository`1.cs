
namespace CoWorker.LightMvc.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IResourceRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<IResource>> List { get; }
        Task<IResource> Get(Guid id = default);
        Task Set(IResource resource);
        Task Restore(Guid id);
        Task DropLast();
    }
}
