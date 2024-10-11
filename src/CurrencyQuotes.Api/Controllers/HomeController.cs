using CurrencyQuotes.Api.Models;
using CurrencyQuotes.Utilities.ExchangeRates;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CurrencyQuotes.Api.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(
            [FromServices] ExchangeRatesApi api,
            CancellationToken ct
        )
        {
            var symbols = await api.GetSymbolsAsync(ct);
            var model = symbols.Symbols!.Select(s => new SymbolViewModel
            {
                Symbol = s.Key,
                Title = s.Value,
                //Disabled = s.Key != "EUR",
            });

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
