using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using MusicTrackerService.Options;
using MusicTrackerService.Authentication.Models;
using System.Text.Json;

namespace MusicTrackerService.Authentication
{
    public class TokenFetcher
    {
        private HttpClient _httpClient;
        private readonly TokenFetcherOptions _options;

        public static string GRANT_TYPE = "client_credentials";

        public TokenFetcher(HttpClient httpClient, IOptions<TokenFetcherOptions> tokenFetcherOptions) {
            _options = tokenFetcherOptions.Value ?? throw new ArgumentNullException(nameof(tokenFetcherOptions));
            _httpClient = httpClient;
        }

        public async Task<AccessTokenResponse> GetAccessTokenAsync()
        {
            var body = new Dictionary<string, string>
            {
                {"grant_type", GRANT_TYPE}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, _options.TokenEndpoint);
            request.Content = new FormUrlEncodedContent(body);
            request.Headers.Authorization = new AuthenticationHeaderValue($"Basic {_options.GetEndcodedClientCredentials()}");
            using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to fetch token"); // TODO: do not throw generic exception

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var token = JsonSerializer.Deserialize<AccessTokenResponse>(stream);
            if (token != null) throw new Exception("Failed to deserialize token");
            return token!;
        }
    }
}
