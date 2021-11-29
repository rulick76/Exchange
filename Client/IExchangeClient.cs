
using System.Threading.Tasks;

namespace Exchange.Client
{
    public interface IExchangeClient
    {
         Task<string> GetRates(string date);
    }
}