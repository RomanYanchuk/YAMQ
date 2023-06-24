using System.Linq;
using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;

namespace MusicalQuiz.Main.Modules.Playlists.Model.Repository;

[DefaultTransientImplementation]
public class PlaylistsReconstructionFactory : IPlaylistsReconstructionFactory
{
    public Playlist Create(Storage.Playlist playlist)
    {
        return new Playlist(playlist.Id, playlist.Name, playlist.Description, playlist.Tracks.Select(Create).ToList());
    }

    private static Track Create(Storage.Track track)
    {
        return new Track(track.Id, track.Name, track.Album, track.Authors, track.ImageUrl, track.PreviewUrl);
    }
}