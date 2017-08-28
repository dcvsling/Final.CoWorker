//namespace CoWorker.Rest.Controllers
//{
//	using CoWorker.Abstractions.EntityParts;
//	using System.Threading.Tasks;
//	using CoWorker.Rest.Internal;
//    using System.Linq;
//    using Microsoft.AspNetCore.Mvc;

//    public class HistoryController<TDomain> where TDomain : class, IIdentity
//    {
//        private IRepositoryProvider repo;
//        private RestController ctrler;
//        public HistoryController(IRepositoryProvider repo,RestController ctrler)
//        {

//            this.ctrler = ctrler;
//			this.repo = repo;
//        }

//        public Task<IActionResult> Get()
//            => repo.Using<TDomain>(x => ctrler.Ok(x.Set<TDomain>().Select(_ => _)));

//        public Task<IActionResult> Get(string id)
//            => repo.Using<TDomain>(x => ctrler.Ok(x.Set<TDomain>().FindAsync(id)));
//    }
//}
