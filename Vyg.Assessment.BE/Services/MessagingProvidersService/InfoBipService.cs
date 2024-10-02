using System.Text;
using System.Text.Json;
using Vyg.Assessment.BE.Services.MessagingProvidersService.Contract;

namespace Vyg.Assessment.BE.Services.MessagingProvidersService
{
    public class InfoBipService : IMessagingProvidersService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _sender;

        public InfoBipService(Dictionary<string, string> configuration)
        {
            _apiKey = configuration["ApiKey"]!;
            _sender = configuration["SenderNumber"]!;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]!)
            };

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"App {_apiKey}");
        }


        public async Task SendSmsAsync(string toPhoneNumber, string messageBody)
        {
            var request = new
            {
                messages = new[]
                {
                    new
                    {
                        from = _sender,
                        destinations = new[]
                        {
                            new { to = toPhoneNumber }
                        },
                        text = messageBody
                    }
                }
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //var response = await _httpClient.PostAsync("/sms/2/text/advanced", content);

            await Task.CompletedTask;
        }
    }
}
