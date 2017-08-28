

//namespace CoWorker.Rest.Internal
//{
//    using Microsoft.Extensions.DependencyInjection;
//    using Microsoft.AspNetCore.Mvc;
//    using System;
//    using System.Threading.Tasks;

//    public class RepositoryProvider<TEntity> : IRepositoryProvider<TEntity>
//        where TEntity : class
//    {
//        private readonly IRepository<TEntity> _provider;

//        public RepositoryProvider(IRepository<TEntity> provider)
//        {
//            this._provider = provider;
//        }

//        public Task<IActionResult> Using<TModel>(Func<IRepository<TModel>, IActionResult> action)
//            where TModel : class
//            => Task.Run(() => action(_provider.GetRequiredService<IRepository<TModel>>()));
//    }
//}
