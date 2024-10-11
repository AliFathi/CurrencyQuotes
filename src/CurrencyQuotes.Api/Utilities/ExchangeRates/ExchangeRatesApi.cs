using CurrencyQuotes.Utilities.ExchangeRates.Models;
using CurrencyQuotes.Utilities.Http;

namespace CurrencyQuotes.Utilities.ExchangeRates;

public class ExchangeRatesApi(
    ApiHttpClient apiClient,
    ExchangeRatesOptions options
)
{
    public async Task<SymbolsOutput> GetSymbolsAsync(CancellationToken ct = default)
    {
        var res = await apiClient.GetAsync<SymbolsOutput>(
            SymbolsUrl(),
            ct
        );

        return res.IsSuccessStatusCode
            ? res.Content!
            : throw new Exception();
    }

    public async Task<string> GetSymbolsRawAsync(CancellationToken ct = default)
    {
        var res = await apiClient.GetAsync(
            SymbolsUrl(),
            ct
        );

        return res.IsSuccessStatusCode
            ? res.RawContent!
            : throw new Exception();
    }

    private string SymbolsUrl() =>
        $"https://api.exchangeratesapi.io/v1/symbols?access_key={options.ApiKey}";
}

public class ExchangeRatesOptions
{
    public required string ApiKey { get; set; }
}
