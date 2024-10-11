using CurrencyQuotes.Utilities.ExchangeRates;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyQuotes.Api.Controllers;

[Route("api/currency")]
[ApiController]
public class CurrencyController : ControllerBase
{
    [Route("symbols")]
    public async Task<IActionResult> GetSymbols(
        [FromServices] ExchangeRatesApi api,
        CancellationToken ct
    )
    {
        var res = await api.GetSymbolsRawAsync(ct);
        return JsonContent(res);
    }

    [Route("quotes/{baseSymbol:alpha:length(3)}")]
    public async Task<IActionResult> GetSymbols(
        [FromServices] ExchangeRatesApi api,
        [FromRoute] string baseSymbol,
        [FromQuery] string? symbols,
        CancellationToken ct
    )
    {
        // دریافت همه نمادها برای بررسی ورودی کاربر
        var validSymbols = (await api.GetSymbolsAsync(ct))
            .Symbols!.Select(s => s.Key);

        if (!validSymbols.Contains(baseSymbol.ToUpper()))
            return NotFound($"symbol {baseSymbol} not supported.");

        var compareSymbols = string.IsNullOrWhiteSpace(symbols)
            ? []
            : symbols.Split(',');

        compareSymbols = compareSymbols.Where(s => s != string.Empty).ToArray();
        if (compareSymbols.Length > 5)
            return BadRequest($"5 symbol are supported.");

        foreach (var symbol in compareSymbols)
        {
            if (!validSymbols.Contains(symbol.ToUpper()))
                return BadRequest($"symbol {symbol} not supported.");
        }

        var res = await api.GetQuotesRawAsync(baseSymbol, compareSymbols, ct);
        return JsonContent(res);
    }

    protected ContentResult JsonContent(string content)
        => Content(content, "application/json", System.Text.Encoding.UTF8);
}
