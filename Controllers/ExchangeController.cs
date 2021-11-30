using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exchange.Client;
using Exchange.Helpers;
using Exchange.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Get(string date,string format)
        {
            try
            {
                string response = await _exchangeClient.GetRates(date);
                if (format == null)
                {
                    return Ok(response);
                }
                else if (format == "csv")
                {
                    string fileName = "GetRatesByDate.csv";
                    string formattedStr= Formatter.FormatCsv(response);
                    byte[] fileBytes = System.Text.Encoding.Unicode.GetBytes(formattedStr);
                    return File(fileBytes, "text/csv", fileName);

                }
                else
                {
                    throw new Exception("Invalid format");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [Route("statistics")]
        [HttpGet]
        public async Task<IActionResult> Get(string date,int? interval,string format)
        {
            try
            {
                var response = await _exchangeClient.GetStatistics(date, interval);

                if (format == null)
                {
                    return Ok(response);
                }
                else if (format == "csv")
                {
                    string fileName = "GetStatistics.csv";
                    string formattedStr = Formatter.FormatCsv(response);
                    byte[] fileBytes = System.Text.Encoding.Unicode.GetBytes(formattedStr);
                    return File(fileBytes, "text/csv", fileName);
                }
                else
                {
                    throw new Exception("Invalid format");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
