using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace WebApplication1.Helpers
{
	public static class URLHelper
	{
		public static string VersionedContent(this UrlHelper urlHelper, string contentPath)
		{
			var versionedPath = new StringBuilder(contentPath);
			versionedPath.AppendFormat("{0}v={1}",
				contentPath.IndexOf('?') >= 0 ? '&' : '?',
				getApplicationVersion());
			return urlHelper.Content(versionedPath.ToString());
		}

		private static string getApplicationVersion()
		{
			return Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		/////////////////////////////////////////////////////////////

		public static string DatedContent(this UrlHelper urlHelper, string contentPath)
		{
			var datedPath = new StringBuilder(contentPath);
			datedPath.AppendFormat("{0}m={1}",
				contentPath.IndexOf('?') >= 0 ? '&' : '?',
				getModifiedDate(contentPath));
			return urlHelper.Content(datedPath.ToString());
		}

		private static string getModifiedDate(string contentPath)
		{
			return System.IO.File.GetLastWriteTime(HostingEnvironment.MapPath(contentPath)).ToString("yyyyMMddhhmmss");
		}
	}
}