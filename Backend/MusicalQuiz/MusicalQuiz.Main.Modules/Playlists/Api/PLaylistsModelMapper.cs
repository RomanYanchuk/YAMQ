using System;
using System.Linq;
using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;
using MusicalQuiz.Main.Modules.Playlists.Model;

namespace MusicalQuiz.Main.Modules.Playlists.Api;

[DefaultTransientImplementation]
public class PLaylistsModelMapper : IPlaylistsModelMapper
{
    public Playlist Map(PlaylistContract contract)
    {
        return new Playlist(Guid.NewGuid(), contract.Name, contract.Description, contract.Tracks.Select(Map).ToList());
    }

    public Track Map(TrackContract contract)
    {
        return new Track(contract.Id, contract.Name, contract.Album, contract.Authors, contract.ImageUrl,
            contract.PreviewUrl);
    }
}