namespace JbhifiTechTest.Server.Services
{
    public interface IOpenWeatherMapService
    {
        Task<string> GetWeatherDetailsAsync(string city, string country);
    }
}
