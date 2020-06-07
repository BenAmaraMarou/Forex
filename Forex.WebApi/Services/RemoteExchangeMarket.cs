using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using Forex.WebApi.Models;
using Microsoft.Extensions.Configuration;

namespace Forex.WebApi.Services
{
    public class RemoteExchangeMarket : IExchangeMarket
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public RemoteExchangeMarket(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IEnumerable<CurrencyPair>> GetSupportedCurrenciesAsync()
        {
            return await GetSupportedCurrency()
                .ContinueWith(task => CreatePairs(task.Result.SupportedPairs));
        }

        private static IEnumerable<CurrencyPair> CreatePairs(IEnumerable<string> supportedPairs)
        {
            return supportedPairs.Select(dto => new CurrencyPair(dto.Substring(0, 3), dto.Substring(3)));
        }

        private async Task<SupportedCurrencyDto> GetSupportedCurrency()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync(_configuration[Constants.ForexUrlName]);
            response.EnsureSuccessStatusCode();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<SupportedCurrencyDto>(stream, options);
        }
    }
}