using System.Collections.Generic;

namespace Forex.WebApi.Services
{
    public class SupportedCurrencyDto
    {
        public string Message { get; set; }

        public IEnumerable<string> SupportedPairs { get; set; }
    }
}