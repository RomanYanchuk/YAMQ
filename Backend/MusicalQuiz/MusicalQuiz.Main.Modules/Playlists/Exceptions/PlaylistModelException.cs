using System;

namespace MusicalQuiz.Main.Modules.Playlists.Exceptions
{
    public class PlaylistModelException : Exception
    {
        public PlaylistModelException() { }

        public PlaylistModelException(string message) : base(message)
        { }

        public PlaylistModelException(string message, Exception innerException) : base(message,
            innerException)
        { }
    }
}