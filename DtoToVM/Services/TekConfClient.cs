namespace DtoToVM.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;
	using AutoMapper;
	using Newtonsoft.Json;
	using DtoToVM.Dtos;
	using DtoToVM.Models;

	public class TekConfClient
	{
		public async Task<List<Conference>> GetConferences ()
		{
			IEnumerable<ConferenceDto> conferenceDtos = Enumerable.Empty<ConferenceDto>();
			IEnumerable<Conference> conferences = Enumerable.Empty<Conference> ();

			using (var httpClient = CreateClient ()) {
				var response = await httpClient.GetAsync ("conferences").ConfigureAwait(false);
				if (response.IsSuccessStatusCode) {
					var json = await response.Content.ReadAsStringAsync ().ConfigureAwait(false);
					if (!string.IsNullOrWhiteSpace (json)) {
						conferenceDtos = await Task.Run (() => 
							JsonConvert.DeserializeObject<IEnumerable<ConferenceDto>>(json)
						).ConfigureAwait(false);

						conferences = await Task.Run(() => 
							Mapper.Map<IEnumerable<Conference>> (conferenceDtos)
						).ConfigureAwait(false);
					}
				}
			}

			return conferences.ToList();
		}

		private const string ApiBaseAddress = "http://api.tekconf.com/v1/";
		private HttpClient CreateClient ()
		{
			var httpClient = new HttpClient 
			{ 
				BaseAddress = new Uri(ApiBaseAddress)
			};

			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return httpClient;
		}
	}
}