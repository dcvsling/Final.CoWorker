
//namespace CoWorker.Rest.Internal
//{
//	using System;
//	using System.Collections.Generic;
//	using Microsoft.AspNetCore.Mvc.ActionConstraints;
//	using System.Linq;
//	public class WebSocketActionConstraint : IActionConstraint
//	{
//		private IEnumerable<string> acceptTemplates;
//		public WebSocketActionConstraint(params string[] acceptTemplates) : this(acceptTemplates.AsEnumerable())
//		{ }

//		public WebSocketActionConstraint(IEnumerable<string> acceptTemplates)
//			=> this.acceptTemplates = acceptTemplates;
//		public Int32 Order => 0;

//		public Boolean Accept(ActionConstraintContext context)
//			=> acceptTemplates.Any(x => context.CurrentCandidate.Action.AttributeRouteInfo.Template.Contains(x))
//				&& context.RouteContext.HttpContext.WebSockets.IsWebSocketRequest;
//	}
//}
