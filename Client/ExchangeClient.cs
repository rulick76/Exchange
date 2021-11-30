using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Exchange.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            try
            {
               
                var actionurl = baseUrl;
                if (date != null)
                {
                    DateTime startingDate = DateTime.Parse(date);
                    actionurl += "historical/" + date + ".json";
                }
                else
                    actionurl = baseUrl + "latest.json";

                actionurl += "?app_id=" + apiKey;

                var result = "";
                using (var response = await _httpClient.GetAsync(actionurl).ConfigureAwait(false))
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                JObject data = JsonConvert.DeserializeObject<JObject>(result);

                return data.SelectToken("rates").ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task<string> GetStatistics(string date,int? interval)
        {
            try
            {
                if (date == null)
                {
                    throw new Exception("Must supply starting date");
                }
                if (interval < 0 || interval > 7)
                {
                    throw new Exception("Invalid! Interval must be between 1-7");
                }

                if (interval == null)
                {
                    interval = 0;
                }

                List<AssetsResponse> retResult = new List<AssetsResponse>();
                DateTime startingDate = DateTime.Parse(date);

                var result = "";
                
                for (int i = 0; i <= interval; i++)
                {
                    string strNewDate = startingDate.AddDays(i).ToString("yyyy-MM-dd");
                    result = await GetRates(strNewDate);

                    retResult.Add(new AssetsResponse(strNewDate, result));
                }

                return JsonConvert.SerializeObject(retResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ;
            }

        }
    }
}