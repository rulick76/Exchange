
using Exchange.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Client
{
    public interface IExchangeClient
    {
        Task<string> GetRates(string date);
        Task<string> GetStatistics(string date, int? interval);
    }
}