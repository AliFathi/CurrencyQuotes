namespace CurrencyQuotes.Utilities.Http;

public record ApiHttpRequest(HttpMethod Method, string Url)
{
    public object? Body { get; set; }

    public Dictionary<string, string> Headers { get; set; } = [];

    public bool AddHeader(string name, string value) =>
        Headers.TryAdd(name, value);
}
