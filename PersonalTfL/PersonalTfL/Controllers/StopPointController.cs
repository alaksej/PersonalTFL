using MyPersonalTfL.Services;
using System.Web.Mvc;
using Tfl.Api.Presentation.Entities;
using System.Collections.Generic;
using log4net;
using System;

namespace PersonalTfL.Controllers
{
	public class StopPointController : Controller
    {
		private DbService db = new DbService();
		private ApiService api = new ApiService();
		private ILog log = LogManager.GetLogger("StopPointController");

		// GET: StopPoint/Search/{query} -> /StopPoint/Search/{query}
		public ActionResult Search(string query)
        {
			if (query == "")
				return RedirectToAction("Index", "Home");
			var searchResults = api.SearchStopPoint(query);
			return View(searchResults);
        }

		// GET: StopPoint/Details/{id} -> /StopPoint/{id}
		public ActionResult Details(string id)
		{
			var stop = api.GetStopPoint(id);
			ViewBag.IsInFavorites = db.ContainsStopPoint(id);
			return View(stop);
		}

		// GET: StopPoint/Manage
		public ActionResult Manage()
		{
			log.Debug("Favorites enetered");
			IEnumerable<string> favoritesIds = null;
			try
			{
				favoritesIds = db.GetAllStopPoints();
			}
			catch (Exception ex)
			{
				log.Debug(ex.Message);
				log.Debug(ex.StackTrace);
			}
			var stopPoints = api.GetStopPoints(favoritesIds);
			return View(stopPoints);
		}

		// GET: StopPoint/AddToFavorites/{id}
		public ActionResult AddToFavorites(string id, string returnUrl)
		{
			if (!db.ContainsStopPoint(id))
				db.AddStopPoint(id);
			return Redirect(returnUrl);
		}

		// GET: StopPoint/RemoveFromFavorites/{id}
		public ActionResult RemoveFromFavorites(string id, string returnUrl)
		{
			if (db.ContainsStopPoint(id))
				db.RemoveStopPoint(id);
			return Redirect(returnUrl);
		}
	}
}