using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using pabio.Models;
using System.Text;

namespace pabio.Services
{
    public class ChatGptService
    {
        // TODO Move to Azure
        private readonly string apiUrl = "";
        private readonly string apiKey;

        readonly ILogger _logger;
        public ChatGptService(IConfiguration configuration, ILoggerFactory factory)
        {
            apiUrl = configuration["ChatGptApiUrl"]!;
            apiKey = configuration["ChatGptApiKey"]!;
            _logger = factory.CreateLogger<EventService>();
        }

        public async Task<string> GetResponse(string prompt)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = prompt }
                },
                    //max_tokens = 150,
                    temperature = 0.7
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API call failed with status code: {response.StatusCode}, Message: {await response.Content.ReadAsStringAsync()}");
                }

                var responseString = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(responseString);

                return responseObject.choices[0].message.content;
            }
        }
    }
}
