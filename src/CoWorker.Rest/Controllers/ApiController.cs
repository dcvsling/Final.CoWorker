using CoWorker.Infrastructure.TypeAccessor;

namespace CoWorker.Rest.Controllers
{
    using Microsoft.AspNetCore.Http.Extensions;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Internal;
    using System;
    using CoWorker.Rest.Builder;
    using System.Linq.Expressions;

    public class ApiController<TEntity> where TEntity : class
    {
        private RestController ctrler;
        private IRepository<TEntity> repo;

        public ApiController(RestController ctrler,IRepository<TEntity> repo)
        {
            this.ctrler = ctrler;
            this.repo = repo;
        }
        async public Task<IActionResult> Get()
            => ctrler.Ok(await repo.Query<TEntity>());
        async public Task<IActionResult> Get(string id)
            => ctrler.Ok(await repo.Query<TEntity>(x => x.Where(id.EqualWithId<TEntity>())));
        async public Task<IActionResult> Post(TEntity domain)
		{
			await repo.Add(domain);
            return ctrler.Created(ctrler.HttpContext.Request.GetDisplayUrl(),ctrler.GetId(domain));
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

        private Expression<Func<TEntity,bool>> CreateFindByKeyQuery()
            => typeof(TEntity).ToParameter().MakeLambda<Func<TEntity, bool>>(
                exp => exp.GetPropertyOrField("Id")
                    .EqualWith(typeof(TEntity)
                        .GetProperty("Id")
                        .PropertyType
                        .ToVariable("id")));
    }
}
