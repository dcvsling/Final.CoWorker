namespace CoWorker.LightMvc.Controllers
{
    using Microsoft.AspNetCore.Http.Extensions;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq.Expressions;
    using CoWorker.LightMvc.Internal;

    public class ApiController<TEntity> where TEntity : class
    {
        private readonly ManagementController<TEntity> _ctrl;

        public ApiController(ManagementController<TEntity> ctrl)
        {
            this._ctrl = ctrl;
        }
        public Task<IActionResult> Get()
            => _ctrl.Get();
        public Task<IActionResult> Get(string id)
            => _ctrl.Get(id);
        public Task<IActionResult> Post(TEntity entity)
		    => _ctrl.Post(entity);

        public Task<IActionResult> Put(TEntity entity)
            => _ctrl.Put(entity);

        public Task<IActionResult> Delete(TEntity entity)
            => _ctrl.Delete(entity);
    }
}
