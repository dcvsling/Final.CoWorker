using System.Collections;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
namespace CoWorker.LightMvc.Controllers
{
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HistoryController<TEntity> where TEntity : class
    {
        private readonly ILogger<HistoryController<TEntity>> _logger;
        private readonly ResourcesController<TEntity> _ctrl;
        private readonly IHttpContextAccessor _accessor;
        private HttpContext _context => _accessor.HttpContext;
        public HistoryController(
            ILogger<HistoryController<TEntity>> logger,
            ResourcesController<TEntity> ctrl,
            IHttpContextAccessor accessor)
        {
            this._logger = logger;
            this._ctrl = ctrl;
            this._accessor = accessor;
        }
    }
}
