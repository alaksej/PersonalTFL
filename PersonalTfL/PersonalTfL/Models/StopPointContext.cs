using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MyPersonalTfL.Models
{
	public class StopPointContext : DbContext
	{
		private ILog log = LogManager.GetLogger("StopPointController");

		public StopPointContext() : base ("StopPointContext")
		{
			log.Debug(Database.Connection.ConnectionString);
			log.Debug(Database.Connection.Database);
		}

		public virtual DbSet<FavoriteStopPoint> FavoriteStopPoints { get; set; }
	}
}