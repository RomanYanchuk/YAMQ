namespace MusicalQuiz.Main.Modules.Playlists.Model.Repository;

public interface IPlaylistsReconstructionFactory
{
    Playlist Create(Storage.Playlist playlist);
}