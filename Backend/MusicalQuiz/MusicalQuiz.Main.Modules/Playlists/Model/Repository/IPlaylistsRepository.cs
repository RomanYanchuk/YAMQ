using System;
using System.Threading.Tasks;

namespace MusicalQuiz.Main.Modules.Playlists.Model.Repository;

public interface IPlaylistsRepository
{
    Task Create(Playlist playlist);
    Task<Playlist> Get(Guid id);
}