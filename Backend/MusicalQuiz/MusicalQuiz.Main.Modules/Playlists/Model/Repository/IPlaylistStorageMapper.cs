namespace MusicalQuiz.Main.Modules.Playlists.Model.Repository;

public interface IPlaylistStorageMapper
{
    Storage.Playlist Map(Playlist playlist);
}