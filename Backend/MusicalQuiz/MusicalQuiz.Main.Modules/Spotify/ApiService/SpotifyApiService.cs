using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;
using MusicalQuiz.Main.Modules.Spotify.Contracts;
using SpotifyAPI.Web;

namespace MusicalQuiz.Main.Modules.Spotify.ApiService
{
    [DefaultTransientImplementation]
    public class SpotifyApiService : ISpotifyApiService
    {
        private readonly ISpotifyClient _spotifyClient;

        public SpotifyApiService(ISpotifyClient spotifyClient)
        {
            _spotifyClient = spotifyClient;
        }

        public async Task<PlaylistContract> FullGetPopularPlayLists()
        {
            var featuredPlaylists =
                await _spotifyClient.Browse.GetFeaturedPlaylists(new FeaturedPlaylistsRequest { Country = "UA" });
            if (featuredPlaylists.Playlists.Items == null)
            {
                // TODO: exception handling
                throw new Exception();
            }

            var playlist = await _spotifyClient.Playlists.Get(featuredPlaylists.Playlists.Items[0].Id);
            if (playlist.Tracks?.Items == null)
            {
                // TODO: exception handling
                throw new Exception();
            }

            playlist.Tracks.Items =
                playlist.Tracks.Items.Where(p => !string.IsNullOrWhiteSpace(((FullTrack)p.Track).PreviewUrl)).ToList();

            return MapPlaylistContract(playlist);
        }

        public async Task<List<TrackContract>> FindTracksByName(string searchFilter)
        {
            var result = await _spotifyClient.Search.Item(new SearchRequest(SearchRequest.Types.Track, searchFilter));
            var tracks = result.Tracks.Items.Where(p => !string.IsNullOrWhiteSpace(p.PreviewUrl))
                .ToList();
            return tracks.Select(MapTrackContract).ToList();
        }

        public async Task<List<TrackContract>> GetTracks(IEnumerable<string> tracksIds)
        {
            try
            {
                var result = await _spotifyClient.Tracks.GetSeveral(new TracksRequest(tracksIds.ToList()));
                return result.Tracks.Select(MapTrackContract).ToList();
            }
            catch
            {
                // TODO: exception handling
                throw new Exception();
            }
        }

        private static PlaylistContract MapPlaylistContract(FullPlaylist playlist)
        {
            var playlistTracks = playlist.Tracks.Items;
            var mappedTracks = playlistTracks.Select(pt => (FullTrack)pt.Track)
                .Select(MapTrackContract)
                .ToList();
            return new PlaylistContract
            {
                Name = playlist.Name,
                Tracks = mappedTracks
            };
        }

        private static TrackContract MapTrackContract(FullTrack track) =>
            new()
            {
                Id = track.Id,
                Name = track.Name,
                Authors = string.Join(", ",
                    track.Artists.Select(p => p.Name)
                        .ToArray()),
                Album = track.Album.Name,
                ImageUrl = track.Album.Images[2]
                    .Url,
                PreviewUrl = track.PreviewUrl
            };
    }
}