using System.Text.Json.Serialization;

namespace CurrencyQuotes.Utilities.ExchangeRates.Models;

public class Error
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
