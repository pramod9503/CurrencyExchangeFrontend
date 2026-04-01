using System.Text.Json;

namespace CurrencyExchangeCustomer.Infrastructure
{
    public static class CustomerClient
    {
        //const string BaseUrl = "https://127.0.0.1:7000/api/";
        const string BaseUrl = "https://localhost:7000/api/";
        public static HttpClient HttpClient { get; } = new HttpClient() 
        {
            BaseAddress = new Uri(BaseUrl),
            Timeout = TimeSpan.FromSeconds(120)
        };

        public static readonly JsonSerializerOptions JsonSerializerOptions = new() 
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static string SignalRUrl = "https://localhost:7000/";
    }
}
