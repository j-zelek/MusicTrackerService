using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Caching.Memory;
using MusicTrackerService.Models;

namespace MusicTrackerService.Services
{
    internal abstract class RequestService
    {
        protected readonly ILogger _logger;
        protected HttpClient _httpClient;
        private readonly string _clientName;
        private IMemoryCache _memoryCache;

        protected RequestService(ILogger logger, IHttpClientFactory httpClientFactory, string clientName, IMemoryCache memoryCache)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient(clientName);
            _memoryCache = memoryCache;
            _clientName = clientName;
        }

        private string CreateCacheKey(string? suffix = null) => $"{_clientName}{suffix ?? string.Empty}";
    }
}
