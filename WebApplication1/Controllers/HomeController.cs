using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Controllers.Filters.Infrastructure;

namespace WebApplication1.Controllers
{
	[Log]
	[ProfileResult]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}

	public class LogAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			Log("OnActionExecuted", filterContext.RouteData);
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Log("OnActionExecuting", filterContext.RouteData);
		}

		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			Log("OnResultExecuted", filterContext.RouteData);
		}

		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			Log("OnResultExecuting ", filterContext.RouteData);
		}

		private void Log(string methodName, RouteData routeData)
		{
			var controllerName = routeData.Values["controller"];
			var actionName = routeData.Values["action"];
			var message = String.Format("{0}- controller:{1} action:{2}", methodName,
				controllerName,
				actionName);
			Debug.WriteLine(message);
		}
	}

	namespace Filters.Infrastructure
	{
		public class ProfileResultAttribute : FilterAttribute, IResultFilter
		{
			private Stopwatch timer;
			public void OnResultExecuting(ResultExecutingContext
				filterContext)
			{
				timer = Stopwatch.StartNew();
			}
			public void OnResultExecuted(ResultExecutedContext filterContext)
			{
				timer.Stop();
				filterContext.HttpContext.Response.Write(
					string.Format("<div>Result  elapsed  time:  {0:F6}</ div > ",
	
				timer.Elapsed.TotalSeconds));
			}
		}
	}
}