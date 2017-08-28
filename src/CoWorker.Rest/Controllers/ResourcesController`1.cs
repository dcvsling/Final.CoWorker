
//namespace CoWorker.Rest.Controllers
//{
//    using CoWorker.Rest.Internal;
//	using Microsoft.AspNetCore.Mvc;
//	using System;
//	using System.IO;
//	using System.Threading.Tasks;

//	public class ResourcesController<TDomain> where TDomain : class
//    {
//		private IRepositoryProvider repo;
//		private RestController ctrler;
//		public ResourcesController(IRepositoryProvider repo, RestController ctrler)
//		{
//			this.repo = repo;
//			this.ctrler = ctrler;
//		}
//        public Task<IActionResult> Get(Guid id)
//        {
//            return repo.Using<Resource>(x =>
//            {
//                var rsc = x.Set<Resource>().Find(id);
//                var path  = Path.Combine(rsc.Path, rsc.Id.ToString());
//                var name = rsc.FileName + rsc.Extensions;
//                return ctrler.File(path, rsc.ContentType, name);
//            });
//		}
//	}
//}