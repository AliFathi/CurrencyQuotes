using CurrencyQuotes.Utilities.ExchangeRates.Models;
using CurrencyQuotes.Utilities.Http;
using System.Net;

namespace CurrencyQuotes.Utilities.ExchangeRates;

public class ExchangeRatesApi(
    ApiHttpClient apiClient,
    ExchangeRatesOptions options
)
{
    private const string _BaseUrl = "https://api.exchangeratesapi.io/v1";
    private const string _BaseSymbol = "EUR";

    /// <summary>
    /// گرفتن همه نمادها به صورت کلاس برای استفاده در برنامه
    /// </summary>
    public async Task<SymbolsOutput> GetSymbolsAsync(CancellationToken ct = default)
    {
        var res = await apiClient.GetAsync<SymbolsOutput>(
            SymbolsUrl(),
            ct
        );

        return res.IsSuccessStatusCode
            ? res.Content!
            : throw new ExchangeRatesException("symbols", res.StatusCode, res.StatusMessage);
    }

    /// <summary>
    /// گرفتن همه نمادها به صورت جیسون برای فرستادن به کلاینت
    /// </summary>
    public async Task<string> GetSymbolsRawAsync(CancellationToken ct = default)
    {
        var res = await apiClient.GetAsync(
            SymbolsUrl(),
            ct
        );

        return res.IsSuccessStatusCode
            ? res.RawContent!
            : throw new ExchangeRatesException("symbols", res.StatusCode, res.StatusMessage);
    }

    public async Task<RatesOutput> GetQuotesAsync(
        string? baseSymbol,
        IEnumerable<string>? compareSymbols = null,
        CancellationToken ct = default
    )
    {
        var res = await apiClient.GetAsync<RatesOutput>(
            QuotesUrl(baseSymbol, compareSymbols),
            ct
        );

        return res.IsSuccessStatusCode
            ? res.Content!
            : throw new ExchangeRatesException("quotes", res.StatusCode, res.StatusMessage);
    }

    public async Task<string> GetQuotesRawAsync(
        string? baseSymbol,
        IEnumerable<string>? compareSymbols = null,
        CancellationToken ct = default
    )
    {
        var res = await apiClient.GetAsync(
            QuotesUrl(baseSymbol, compareSymbols),
            ct
        );

        return res.IsSuccessStatusCode
            ? res.RawContent!
            : throw new ExchangeRatesException("quotes", res.StatusCode, res.StatusMessage);
    }

    #region Urls

    private string SymbolsUrl()
        => $"{_BaseUrl}/symbols?access_key={options.ApiKey}";

    private string QuotesUrl(string? baseSymbol, IEnumerable<string>? compareSymbols)
    {
        var url = $"{_BaseUrl}/latest?access_key={options.ApiKey}";

        if (baseSymbol != null)
            url += $"&base={baseSymbol}";

        if (compareSymbols != null)
            url += $"&symbols={string.Join(',', compareSymbols)}";

        return url;
    }

    #endregion
}

public class ExchangeRatesOptions
{
    public required string ApiKey { get; set; }
}

public class ExchangeRatesException(
    string name,
    HttpStatusCode statusCode,
    string? reasonPhrase
) : Exception($"failed to call {name} api of ExchangeRates with code {(int)statusCode}: {reasonPhrase ?? statusCode.ToString()}");
