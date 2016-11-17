using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Tfl.Api.Presentation.Entities;

namespace MyPersonalTfL.Services
{
	public class ApiService
	{
		private const string STOPPOINT_SEARCH = "/StopPoint/Search/";
		private const int MAX_STOPPOINTS_PER_REQUEST = 15;
		private const string STOPPOINT = "/StopPoint/";

		private HttpClient client = TflApiClient.Instance.HttpClient;


		public StopPoint GetStopPoint(string id)
		{
			return PerformApiQuery<StopPoint>(STOPPOINT + id);
		}

		public IEnumerable<StopPoint> GetStopPoints(IEnumerable<string> ids)
		{
			int i = 0, j = 0;
			var formattedIds = "";
			var num = ids.Count();
			var list = new List<StopPoint>();

			if (num == 1)
			{
				list.Add(GetStopPoint(ids.First()));
				return list;
			}
			foreach (var id in ids)
			{
				j++;
				if (i < MAX_STOPPOINTS_PER_REQUEST)
				{
					i++;
					if (i == MAX_STOPPOINTS_PER_REQUEST || j == num)
					{
						formattedIds += id;
					}
					else
					{
						formattedIds += id + ",";
					}
				}
				if (i == MAX_STOPPOINTS_PER_REQUEST || j == num)
				{
					list.AddRange(PerformApiQuery<IEnumerable<StopPoint>>(STOPPOINT + formattedIds));
					i = 0;
					formattedIds = "";
				}
			}
			return list;
		}


		public SearchResponse SearchStopPoint(string query)
		{
			return PerformApiQuery<SearchResponse>(STOPPOINT_SEARCH + query);
		}

		private T PerformApiQuery<T>(string query) where T : class
		{
			T result = null;
			HttpResponseMessage response = client.GetAsync(query).Result;
			if (response.IsSuccessStatusCode)
			{
				result = response.Content.ReadAsAsync<T>().Result;
			}
			return result;
		}

	}
}