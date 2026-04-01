using System.Text.Json;

namespace CurrencyExchangeAdministrator.Infrastructure
{
    public static class AdministratorClient
    {        
        const string BaseUrl = "https://localhost:7001/api/";

        public static HttpClient HttpClient { get; } = new HttpClient() 
        {
            BaseAddress = new Uri(BaseUrl),
            Timeout = TimeSpan.FromSeconds(120)
        };

        public static readonly JsonSerializerOptions JsonSerializerOptions = new() 
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static string SignalRUrl = "https://localhost:7001/";
    }
}
