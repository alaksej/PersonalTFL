using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PersonalTfL
{
	public class LoggerConfig
	{
		public static void Configure()
		{
			string webConfigFile = Path.Combine(
				HostingEnvironment.ApplicationPhysicalPath, "web.config");
			FileInfo config = new FileInfo(webConfigFile);
			log4net.Config.XmlConfigurator.Configure(config);
			var log = log4net.LogManager.GetLogger(typeof(LoggerConfig));
			log.Info("Application started");
		}
	}
}