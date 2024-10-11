namespace CurrencyQuotes.Api.Models
{
    public class SymbolViewModel
    {
        public required string Symbol { get; set; }
        public required string Title { get; set; }
        public bool Disabled { get; set; }
    }
}
