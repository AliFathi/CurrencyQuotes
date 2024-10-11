using System.Text.Json.Serialization;

namespace CurrencyQuotes.Utilities.ExchangeRates.Models;

public class OutputBase
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("error")]
    public Error? Error { get; set; }
}
