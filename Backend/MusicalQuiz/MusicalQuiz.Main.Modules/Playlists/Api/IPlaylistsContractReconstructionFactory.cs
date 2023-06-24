using MusicalQuiz.Main.Modules.Playlists.Model;

namespace MusicalQuiz.Main.Modules.Playlists.Api;

public interface IPlaylistsContractReconstructionFactory
{
    PlaylistContract Create(Playlist playlist);
}