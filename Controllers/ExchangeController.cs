using System.Collections.Generic;
using System.Threading.Tasks;
using Exchange.Client;
using Exchange.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Exchange.Controllers
{
    [ApiController]
    [Route("exchange")]
    public class ExchangeController : ControllerBase
    {

        private readonly ILogger<ExchangeController> _logger;
        private readonly IExchangeClient _exchangeClient;
        public ExchangeController(ILogger<ExchangeController> logger,IExchangeClient exchangeClient)
        {
            _logger = logger;
            _exchangeClient=exchangeClient;
        }
        [Route("rates")]
        [HttpGet]
        public async Task<IActionResult> Get(string date)
        {
            List<Currency> currencies = new List<Currency>();
            string response= await _exchangeClient.GetRates(date);
            //foreach (var cur in response)
            //{
            //    currencies.Add(new Currency() { Symbol= cur.symbol,Name=cur.name, Price= cur.price});
            //}
            return Ok(response);
        }
    }
}
