using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Exchange.Client
{
    public class ExchangeClient : IExchangeClient
    {
        string baseUrl = "https://openexchangerates.org/api/";
        string apiKey = "1ece7e2217b34f6f921bc423d7177449";
        private readonly ILogger<ExchangeClient> _logger;
        private readonly HttpClient _httpClient;
        public ExchangeClient(ILogger<ExchangeClient> logger,IHttpClientFactory httpClientFactory)
        {
            _logger=logger;
            _httpClient=httpClientFactory.CreateClient(GetType().FullName);

        }
        public async Task<string> GetRates(string date)
        {
            var actionurl = baseUrl;
            if (date != null)
                actionurl += "historical/" + date + ".json";
            else
                actionurl = baseUrl + "latest.json";

            actionurl += "?app_id=" + apiKey;

            var result ="";
            using(var response = await _httpClient.GetAsync(actionurl).ConfigureAwait(false))
            {
                 result=await response.Content.ReadAsStringAsync();
            }
            //string data = JsonConvert.SerializeObject(result);

            return result;
        }
    }
}