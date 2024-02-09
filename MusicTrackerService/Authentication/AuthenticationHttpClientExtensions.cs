using MusicTrackerService.Options;

namespace MusicTrackerService.Authentication
{
    public static class AuthenticationHttpClientExtensions
    {
        private static void AddTokenFetcherToClient(IHttpClientBuilder httpClientBuilder)
        {

            httpClientBuilder.AddHttpMessageHandler<TokenFetcherHttpClientHandler>();
        }

        private static void AddTokenAuthentication(this IServiceCollection services, TokenFetcherOptions options, IHttpClientBuilder httpClientBuilder)
        {
            services.Configure<TokenFetcherOptions>(o =>
            {
                o.ClientId = options.ClientId;
                o.ClientSecret = options.ClientSecret;  
                o.TokenEndpoint = options.TokenEndpoint;
            });
            services.AddTransient<TokenFetcherHttpClientHandler>();
            services.AddHttpClient<TokenFetcher>();
            AddTokenFetcherToClient(httpClientBuilder);
        }
    }
}
