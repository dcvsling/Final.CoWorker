using CoWorker.DependencyInjection.Factory;
using Microsoft.AspNetCore.Mvc;

namespace CoWorker.Rest.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepositoryProvider<TEntity> where TEntity : class
    {
        IQueryRepositoryProvider<TEntity> CreateQuery();
        ICommandRepositoryProvider<TEntity> CreateCommand();
    }

    public interface IRepositoryProvider<TDelegate,TTask> where TTask : Task
    {
        Func<TDelegate,TTask> Using { get; }
    }

    public interface IQueryRepositoryProvider<TEntity>
        : IRepositoryProvider<Func<IRepository<TEntity>,Task<IEnumerable<TEntity>>>,Task<IActionResult>>
        where TEntity : class
    {
    }

    public interface ICommandRepositoryProvider<TEntity>
        : IRepositoryProvider<Func<IRepository<TEntity>, Func<Task<IActionResult>>>, Task<IActionResult>>
        where TEntity : class
    {
    }

    public class QueryRepositoryProvider<TEntity> : IQueryRepositoryProvider<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _provider;

        public QueryRepositoryProvider(IRepository<TEntity> provider)
        {
            this._provider = provider;
        }

        public Func<Func<IRepository<TEntity>, Task<IEnumerable<TEntity>>>, Task<IActionResult>> Using
            => invoker => Task.Run<IActionResult>(() => new OkObjectResult(invoker(_provider)));
    }

    public class CommandRepositoryProvider<TEntity> : ICommandRepositoryProvider<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _provider;

        public CommandRepositoryProvider(IRepository<TEntity> provider)
        {
            this._provider = provider;
            this._provider = provider;
        }
        public Func<Func<IRepository<TEntity>,Func<Task<IActionResult>>>, Task<IActionResult>> Using
            => invoker => Task.Run(() => invoker(_provider)());
    }

    public class RepositoryProvider<TEntity> : IRepositoryProvider<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repo;

        public RepositoryProvider(IRepository<TEntity> repo)
        {
            this._repo = repo;
        }
        public ICommandRepositoryProvider<TEntity> CreateCommand() => new CommandRepositoryProvider<TEntity>(_repo);
        public IQueryRepositoryProvider<TEntity> CreateQuery() => new QueryRepositoryProvider<TEntity>(_repo);
    }
}
