using System.Text.Json.Serialization;

namespace CurrencyQuotes.Utilities.ExchangeRates.Models;

public class SymbolsOutput : OutputBase
{
    [JsonPropertyName("symbols")]
    public Dictionary<string, string>? Symbols { get; set; }
}
