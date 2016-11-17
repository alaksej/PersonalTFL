using MyPersonalTfL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tfl.Api.Presentation.Entities;

namespace MyPersonalTfL.Services
{
	public class DbService
	{
		private StopPointContext context = new StopPointContext();

		public IEnumerable<string> GetAllStopPoints()
		{
			return (from o in context.FavoriteStopPoints
					where o.UserName == System.Web.HttpContext.Current.User.Identity.Name
					select o.NaptanId);
		}

		public FavoriteStopPoint GetStopPoint(string naptanId)
		{
			var userName = System.Web.HttpContext.Current.User.Identity.Name;
			return (from o in context.FavoriteStopPoints
					where o.NaptanId == naptanId &&
					o.UserName == userName
					select o).FirstOrDefault();
		}

		public bool ContainsStopPoint(string naptanId)
		{
			return (GetStopPoint(naptanId) != null);
		}

		public void AddStopPoint(string naptanId)
		{
			var user = System.Web.HttpContext.Current.User.Identity.Name;
			var newPoint = new FavoriteStopPoint()
			{
				Id = naptanId + user,
				NaptanId = naptanId,
				UserName = user
			};
			context.FavoriteStopPoints.Add(newPoint);
			context.SaveChanges();
		}

		public void RemoveStopPoint(string naptanId)
		{
			var stopPoint = GetStopPoint(naptanId);
			if (stopPoint != null)
			{
				context.FavoriteStopPoints.Remove(stopPoint);
				context.SaveChanges();
			}
		}
	}
}