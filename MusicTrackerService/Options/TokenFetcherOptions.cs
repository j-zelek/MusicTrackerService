namespace MusicTrackerService.Options
{
    public class TokenFetcherOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TokenEndpoint { get; set; }

        public string GetEndcodedClientCredentials()
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}");
            return Convert.ToBase64String(plainTextBytes);
        }
    }
