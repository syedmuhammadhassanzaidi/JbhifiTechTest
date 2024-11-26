using System.Net.Http;

namespace JbhifiTechTest.Server.Services
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string url {  get; set; }
        private string ApiKey { get; set; }

        public OpenWeatherMapService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            url = configuration.GetValue<string>("OpenWeatherMap:Url");
            url = configuration.GetValue<string>("OpenWeatherMap:ApiKey");
        }

        public async Task<string> GetWeatherDetailsAsync(string city, string country)
        {
            return string.Empty;
        }

    }
}
