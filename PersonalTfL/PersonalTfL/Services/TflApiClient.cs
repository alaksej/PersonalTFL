using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace MyPersonalTfL.Services
{
	public class TflApiClient
	{
		private const string BASE_ADDRESS = "https://api.tfl.gov.uk";
		private HttpClient client;
		private static Lazy<TflApiClient> instanceHolder = new Lazy<TflApiClient>(() => new TflApiClient());

		private TflApiClient()
		{
			start();
		}

		public static TflApiClient Instance
		{
			get { return instanceHolder.Value; }
		}

		public HttpClient HttpClient
		{
			get { return client; }
		}

		private void start()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri(BASE_ADDRESS);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

	}
}