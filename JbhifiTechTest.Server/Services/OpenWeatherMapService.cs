using JbhifiTechTest.Server.Models;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;

namespace JbhifiTechTest.Server.Services
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string url {  get; set; }
        private string apiKey { get; set; }

        public OpenWeatherMapService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            url = configuration.GetValue<string>("OpenWeatherMap:Url");
            apiKey = configuration.GetValue<string>("OpenWeatherMap:ApiKey");
        }

        public async Task<string> GetWeatherDetailsAsync(string city, string country)
        {
            var httpClient = _httpClientFactory.CreateClient("OpenWeatherMap");
            var response = await httpClient.GetAsync(string.Format(url, city, country, apiKey));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var parsedResult = JObject.Parse(result)["weather"];
                var weatherWithDesc = parsedResult.ToObject<List<Weather>>().Where(x => x.Description != null);
                var description = weatherWithDesc.Select(x => x.Description).FirstOrDefault();

                if (!string.IsNullOrEmpty(description))
                {
                    return $"The weather forecase for {city}, {country} is {description}";
                }
            }
            
            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException($"City ({city},{country}) not found");
            }

            throw new HttpRequestException("Error occurred while trying to retrieve weather information");
        }

    }
}
