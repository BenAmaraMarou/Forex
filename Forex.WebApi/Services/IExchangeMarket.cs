using System.Collections.Generic;
using System.Threading.Tasks;
using Forex.WebApi.Models;

namespace Forex.WebApi.Services
{
    public interface IExchangeMarket
    {
        Task<IEnumerable<CurrencyPair>> GetSupportedCurrenciesAsync();
    }
}