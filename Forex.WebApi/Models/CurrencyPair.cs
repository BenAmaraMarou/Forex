namespace Forex.WebApi.Models
{
    public class CurrencyPair
    {
        public CurrencyPair(string from, string to)
        {
            From = from;
            To = to;
        }

        public string From { get; }

        public string To { get; }
    }
}