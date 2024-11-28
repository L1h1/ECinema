using Blazored.LocalStorage;
using ECinema;
using Microsoft.JSInterop;

public class AuthorizedHttpClientHandler : DelegatingHandler
{
    private readonly TokenState _state;


    public AuthorizedHttpClientHandler(TokenState state)
    {
        _state = state;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _state.Token;

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
