using System;

namespace MusicalQuiz.Main.Modules.Playlists.Exceptions;

public class PlaylistNotFoundException : PlaylistModelException
{
    public PlaylistNotFoundException(Guid id) : base(
        $"Playlist with id {id} is not found.")
    { }
}