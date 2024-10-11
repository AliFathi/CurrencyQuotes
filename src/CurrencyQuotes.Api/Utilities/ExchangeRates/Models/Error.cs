using System.Text.Json.Serialization;

namespace CurrencyQuotes.Utilities.ExchangeRates.Models;

public class Error
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("info")]
    public string? Info { get; set; }
}
