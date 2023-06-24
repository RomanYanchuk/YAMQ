using MusicalQuiz.Main.Modules.Playlists.Model;
using System.Linq;
using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;

namespace MusicalQuiz.Main.Modules.Playlists.Api;

[DefaultTransientImplementation]
public class PlaylistsContractReconstructionFactory : IPlaylistsContractReconstructionFactory
{
    public PlaylistContract Create(Playlist playlist)
    {
        return new PlaylistContract
        {
            Id = playlist.Id,
            Name = playlist.Name,
            Description = playlist.Description,
            Tracks = playlist.Tracks.Select(Create).ToList()
        };
    }

    private static TrackContract Create(Track track)
    {
        return new TrackContract
        {
            Id = track.Id,
            Name = track.Name,
            Album = track.Album,
            Authors = track.Authors,
            ImageUrl = track.ImageUrl,
            PreviewUrl = track.PreviewUrl
        };
    }
}