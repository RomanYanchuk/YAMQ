using System.Linq;
using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;

namespace MusicalQuiz.Main.Modules.Playlists.Model.Repository;

[DefaultTransientImplementation]
public class PlaylistStorageMapper : IPlaylistStorageMapper
{
    public Storage.Playlist Map(Playlist playlist)
    {
        return new Storage.Playlist
        {
            Id = playlist.Id,
            Name = playlist.Name,
            Description = playlist.Description,
            Tracks = playlist.Tracks.Select(Map).ToList()
        };
    }

    private static Storage.Track Map(Track track)
    {
        return new Storage.Track { Id = track.Id, Name = track.Name, Album = track.Album, Authors = track.Authors, ImageUrl = track.ImageUrl, PreviewUrl = track.PreviewUrl };
    }
}
