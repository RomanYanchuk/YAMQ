using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicalQuiz.Main.Modules.Spotify.ApiService;
using MusicalQuiz.Main.Modules.Spotify.Contracts;

namespace MusicalQuiz.Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ISpotifyApiService _spotifyApiService;

        public TracksController(ISpotifyApiService spotifyApiService)
        {
            _spotifyApiService = spotifyApiService;
        }

        [HttpGet("search")]
        public async Task<List<TrackContract>> SearchTracks(string searchFilter)
        {
            return await _spotifyApiService.FindTracksByName(searchFilter);
        }
    }
}
