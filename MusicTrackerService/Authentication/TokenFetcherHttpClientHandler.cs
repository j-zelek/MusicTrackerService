using System.Net.Http.Headers;

namespace MusicTrackerService.Authentication
{
    internal class TokenFetcherHttpClientHandler : DelegatingHandler
    {
        private readonly TokenFetcher _tokenFetcher;

        public TokenFetcherHttpClientHandler(TokenFetcher tokenFetcher)
        {
            _tokenFetcher = tokenFetcher;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await AddTokenCredentialAuthorizationHeader(request).ConfigureAwait(false);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private async Task AddTokenCredentialAuthorizationHeader(HttpRequestMessage request)
        {
            var token = await _tokenFetcher.GetAccessTokenAsync().ConfigureAwait(false);
            if (token != null) request.Headers.Authorization = new AuthenticationHeaderValue($"{token.TokenType} {token.AccessToken}");
        }
    }
}
