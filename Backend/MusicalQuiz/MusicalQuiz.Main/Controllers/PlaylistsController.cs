using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MusicalQuiz.Main.Modules.Playlists.Api;
using MusicalQuiz.Main.Modules.Spotify.ApiService;
using PlaylistContract = MusicalQuiz.Main.Modules.Spotify.Contracts.PlaylistContract;

namespace MusicalQuiz.Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly ISpotifyApiService _spotifyApiService;
        private readonly IPlaylistsApi _playlistsApi;

        public PlaylistsController(ISpotifyApiService spotifyApiService, IPlaylistsApi playlistsApi)
        {
            _spotifyApiService = spotifyApiService;
            _playlistsApi = playlistsApi;
        }

        [HttpGet]
        public async Task<PlaylistContract> GetMostPopularPlaylist()
        {
            return await _spotifyApiService.FullGetPopularPlayLists();
        }

        [HttpGet("{id:guid}")]
        public async Task<Modules.Playlists.Api.PlaylistContract> GetPlaylist(Guid id)
        {
            return await _playlistsApi.Get(id);
        }

        [HttpPost]
        public async Task<Guid> CreatePlaylist(PostPlaylistContract contract)
        {
            var tracks = (await _spotifyApiService.GetTracks(contract.TracksIds)).Select(t => new TrackContract
            {
                Id = t.Id,
                Album = t.Album,
                Authors = t.Authors,
                ImageUrl = t.ImageUrl,
                Name = t.Name,
                PreviewUrl = t.PreviewUrl
            }).ToList();
            return await _playlistsApi.Create(new Modules.Playlists.Api.PlaylistContract
            {
                Name = contract.Name,
                Description = contract.Description,
                Tracks = tracks
            });
        }
    }
}