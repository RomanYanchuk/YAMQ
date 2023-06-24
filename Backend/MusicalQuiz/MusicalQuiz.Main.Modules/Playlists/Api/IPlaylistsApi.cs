using System;
using System.Threading.Tasks;

namespace MusicalQuiz.Main.Modules.Playlists.Api;

public interface IPlaylistsApi
{
    Task<Guid> Create(PlaylistContract contract);
    Task<PlaylistContract> Get(Guid id);
}