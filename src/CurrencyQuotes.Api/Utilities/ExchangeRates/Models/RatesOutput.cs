using System.Text.Json.Serialization;

namespace CurrencyQuotes.Utilities.ExchangeRates.Models;

public class RatesOutput : OutputBase
{
    [JsonPropertyName("historical")]
    public bool Historical { get; set; }

    [JsonPropertyName("date")]
    public required string Date { get; set; }

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("base")]
    public required string Base { get; set; }

    [JsonPropertyName("rates")]
    public Dictionary<string, string>? Rates { get; set; }
}
