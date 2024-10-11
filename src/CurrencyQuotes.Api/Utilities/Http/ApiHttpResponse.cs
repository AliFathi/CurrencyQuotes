using System.Net;

namespace CurrencyQuotes.Utilities.Http;

public record ApiHttpResponse(HttpStatusCode StatusCode, string? StatusMessage)
{
    public bool IsSuccessStatusCode { get; set; }

    public string? RawContent { get; set; }
}

public record ApiHttpResponse<TResponse>(HttpStatusCode StatusCode, string? StatusMessage)
    : ApiHttpResponse(StatusCode, StatusMessage)
{
    public TResponse? Content { get; set; }
}
