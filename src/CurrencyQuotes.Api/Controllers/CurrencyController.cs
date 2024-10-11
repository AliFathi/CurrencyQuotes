using CurrencyQuotes.Utilities.ExchangeRates;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyQuotes.Api.Controllers;

[Route("api/currencry")]
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

    protected ContentResult JsonContent(string content)
        => Content(content, "application/json", System.Text.Encoding.UTF8);
}
