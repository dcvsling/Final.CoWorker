using CoWorker.DependencyInjection.Factory;

namespace CoWorker.Rest.Controllers
{
	using System.Threading.Tasks;
	using CoWorker.Rest.Internal;
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http.Extensions;
    using System.Linq;

    public class ManagementController<TEntity> where TEntity : class
	{
		private RestController ctrler;
		private IRepository<TEntity> repo;
		public ManagementController(RestController ctrler, IRepository<TEntity> repo)
		{
			this.ctrler = ctrler;
			this.repo = repo;
		}

        async public Task<IActionResult> Get()
            => ctrler.Ok(await repo.Query<TEntity>());
        async public Task<IActionResult> Get(string id)
            => ctrler.Ok(await repo.Query<TEntity>(x => x.Where(id.EqualWithId())));
        async public Task<IActionResult> Post(TEntity domain)
        {
            await repo.Add(domain);
            return ctrler.Created(ctrler.HttpContext.Request.GetDisplayUrl(), ctrler.GetId(domain));
        }
        async public Task<IActionResult> Put(TEntity domain)
        {
            await repo.Update(domain);
            return ctrler.Accepted();
        }
        async public Task<IActionResult> Delete(TEntity domain)
        {
            await repo.Remove(domain);
            return ctrler.Accepted();
        }
    }
}
