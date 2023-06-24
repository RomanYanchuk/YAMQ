using MusicalQuiz.Main.Modules.Playlists.Model;

namespace MusicalQuiz.Main.Modules.Playlists.Api;

public interface IPlaylistsModelMapper
{
    Playlist Map(PlaylistContract contract);
}