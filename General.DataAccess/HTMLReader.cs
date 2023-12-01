﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace General.DataAccess
{
	public class HTMLReader : IHTMLReader
	{
		public HttpClient? _apiClient;

		private void InitializeClient()
		{
			string api = ConfigurationManager.AppSettings["api"];

			_apiClient = new HttpClient();
			_apiClient.BaseAddress = new Uri(api);
			_apiClient.DefaultRequestHeaders.Accept.Clear();
			_apiClient.DefaultRequestHeaders.Add("cookie", "session=" + ConfigurationManager.AppSettings["sessionID"]);
		}

		public string GetInputData(int day, int year)
		{
			if (_apiClient == null) InitializeClient();

			using (HttpResponseMessage response = _apiClient.GetAsync($"/{year}/day/{day}/input").Result)
			{
				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadAsStringAsync().Result;
					result= result.Trim();
					result= result.Replace("\n",Environment.NewLine);
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}
	}
}