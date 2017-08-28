using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;
namespace CoWorker.LightMvc.Controllers
{
	using System.Threading.Tasks;
	using CoWorker.LightMvc.Internal;
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http.Extensions;
    using System.Linq.Expressions;
    using System;
    using System.IO;

    public class ManagementController<TEntity> where TEntity : class
	{
		private HistoryController<TEntity> ctrler;
		private IRepository<TEntity> repo;
        private readonly IHttpContextAccessor _accessor;

        public ManagementController(
            HistoryController<TEntity> ctrler,
            IRepository<TEntity> repo,
            IHttpContextAccessor accessor)
		{
			this.ctrler = ctrler;
			this.repo = repo;
            this._accessor = accessor;
        }

        async public Task<IActionResult> Get()
              => new OkObjectResult(await repo.Query<TEntity>());
        async public Task<IActionResult> Get(string id)
            => new OkObjectResult(await repo.Query<TEntity>(x => x.Where(id.EqualWithId<TEntity>())));
        async public Task<IActionResult> Post(TEntity entity)
        {
            await repo.Add(entity);
            return new CreatedResult(_accessor.HttpContext.Request.GetDisplayUrl(), entity);
        }

        async public Task<IActionResult> Put(TEntity entity)
        {
            await repo.Update(entity);
            return new AcceptedResult(_accessor.HttpContext.Request.Path, entity);
        }

        async public Task<IActionResult> Delete(TEntity entity)
        {
            await repo.Remove(entity);
            return new AcceptedResult(_accessor.HttpContext.Request.Path, entity);
        }


    }
}
