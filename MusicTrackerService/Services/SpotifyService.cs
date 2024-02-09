using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTrackerService.Services
{
    public interface ISpotifyService
    {

    }
    public class SpotifyService : ISpotifyService
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public SpotifyService(ILogger<ISpotifyService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }
    }
}
