namespace CurrencyQuotes.Utilities.Http;

public class ApiHttpClient(IHttpClientFactory clientFactory)
{
    public Task<ApiHttpResponse> GetAsync(
        string url,
        CancellationToken ct = default
    )
    {
        return SendAsync(new ApiHttpRequest(HttpMethod.Get, url), ct);
    }

    public Task<ApiHttpResponse<TResponse>> GetAsync<TResponse>(
        string url,
        CancellationToken ct = default
    )
    {
        return SendAsync<TResponse>(new ApiHttpRequest(HttpMethod.Get, url), ct);
    }

    public Task<ApiHttpResponse> PostAsync(
        string url,
        object? body = null,
        CancellationToken ct = default
    )
    {
        return SendAsync(new ApiHttpRequest(HttpMethod.Post, url) { Body = body }, ct);
    }

    public Task<ApiHttpResponse<TResponse>> PostAsync<TResponse>(
        string url,
        object? body = null,
        CancellationToken ct = default
    )
    {
        return SendAsync<TResponse>(new ApiHttpRequest(HttpMethod.Post, url) { Body = body }, ct);
    }

    public async Task<ApiHttpResponse> SendAsync(
        ApiHttpRequest req,
        CancellationToken ct = default
    )
    {
        var resMsg = await InternalSendAsync(req, ct);
        return await ReadResponseAsync(resMsg, ct);
    }

    public async Task<ApiHttpResponse<TResponse>> SendAsync<TResponse>(
        ApiHttpRequest req,
        CancellationToken ct = default
    )
    {
        var resMsg = await InternalSendAsync(req, ct);
        return await ReadResponseAsync<TResponse>(resMsg, ct);
    }

    #region Request

    protected HttpRequestMessage CreateRequest(ApiHttpRequest req)
    {
        var reqMsg = new HttpRequestMessage(req.Method, req.Url);
        foreach (var head in req.Headers)
            reqMsg.Headers.Add(head.Key, head.Value);

        if (req.Body != null)
            reqMsg.Content = JsonContent(req.Body);

        return reqMsg;
    }

    protected HttpClient GetClient(string? name = null)
        => name == null
        ? clientFactory.CreateClient()
        : clientFactory.CreateClient(name);

    protected Task<HttpResponseMessage> InternalSendAsync(
        ApiHttpRequest req,
        CancellationToken ct = default
    )
    {
        var reqMsg = CreateRequest(req);
        var client = GetClient();
        return client.SendAsync(reqMsg, ct);
    }

    #endregion

    #region Response

    protected async Task<ApiHttpResponse> ReadResponseAsync(
        HttpResponseMessage resMsg,
        CancellationToken ct = default
    )
    {
        return new ApiHttpResponse(resMsg.StatusCode, resMsg.ReasonPhrase)
        {
            IsSuccessStatusCode = resMsg.IsSuccessStatusCode,
            RawContent = await resMsg.Content.ReadAsStringAsync(ct)
        };
    }

    protected async Task<ApiHttpResponse<TResponse>> ReadResponseAsync<TResponse>(
        HttpResponseMessage resMsg,
        CancellationToken ct = default
    )
    {
        var res = new ApiHttpResponse<TResponse>(resMsg.StatusCode, resMsg.ReasonPhrase)
        {
            IsSuccessStatusCode = resMsg.IsSuccessStatusCode,
            RawContent = await resMsg.Content.ReadAsStringAsync(ct)
        };

        if (res.RawContent != null)
            res.Content = ParseJson<TResponse>(res.RawContent);

        return res;
    }

    #endregion

    #region Json

    protected StringContent JsonContent(object body)
        => new(ToJson(body), System.Text.Encoding.UTF8, "application/json");

    protected string ToJson(object body)
        => System.Text.Json.JsonSerializer.Serialize(body);

    protected TResponse? ParseJson<TResponse>(string content)
        => System.Text.Json.JsonSerializer.Deserialize<TResponse>(content);

    #endregion
}
