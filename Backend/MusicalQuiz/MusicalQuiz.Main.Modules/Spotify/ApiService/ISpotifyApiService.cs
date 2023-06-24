using System.Collections.Generic;
using System.Threading.Tasks;
using MusicalQuiz.Main.Modules.Spotify.Contracts;

namespace MusicalQuiz.Main.Modules.Spotify.ApiService
{
    public interface ISpotifyApiService
    {
        Task<PlaylistContract> FullGetPopularPlayLists();
        Task<List<TrackContract>> FindTracksByName(string searchFilter);
        Task<List<TrackContract>> GetTracks(IEnumerable<string> tracksIds);
    }
}