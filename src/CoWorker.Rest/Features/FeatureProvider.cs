
//namespace CoWorker.Rest.ApplicationParts
//{
//	using Microsoft.AspNetCore.Mvc.ApplicationParts;
//	using System.Reflection;
//	using System.Collections.Generic;
//	using System.Linq;

//    public class FeatureProvider<T>
//    {
//		public void PopulateFeature(IEnumerable<ApplicationPart> parts)
//		{
//            parts.Where(x => x is AssemblyPart y)
//                .Cast<AssemblyPart>()
//                .SelectMany(x => x.Types)
//                .Where(x => x.IsAssignablePOCOFrom<T>())
//                .Each(x => feature.Features.Add(x));
//		}
//    }
//}
