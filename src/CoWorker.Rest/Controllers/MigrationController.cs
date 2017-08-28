
//namespace CoWorker.Rest.Controllers
//{
//    using System.Linq;
//    using System.Threading.Tasks;
//    using CoWorker.Rest.Internal;
//    using CoWorker.Rest.ApplicationParts;
//    using Microsoft.AspNetCore.Mvc;
//    using CoWorker.EntityFramework.Migrations;

//    public class MigrationController
//    {
//        private RestController ctrler;
//		private DomainFeature feature;
//		private IMigrationModelsBuilderFactory factory;
//		public MigrationController(
//			DomainFeature feature,
//			RestController ctrler,
//            IMigrationModelsBuilderFactory factory)
//        {
//            this.ctrler = ctrler;
//			this.feature = feature;
//			this.factory = factory;

//		}
//		public Task<IActionResult> Get()
//			=> Task.FromResult<IActionResult>(ctrler.Ok(feature.Features
//				.Aggregate(factory.Create(), (seed, next) => seed.AddModel(next))
//				.GetMigrationScriptAsync()));


//		public Task<IActionResult> Post()
//            => Task.FromResult<IActionResult>(ctrler.Created("/",feature.Features
//				.Aggregate(factory.Create(), (seed, next) => seed.AddModel(next))
//				.MigrateAsync()));
//    }
//}
