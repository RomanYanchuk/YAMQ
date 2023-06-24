using System.Collections.Generic;

namespace MusicalQuiz.Main.Modules.Spotify.Contracts
{
    public class PlaylistContract
    {
        public string Name { get; set; }
        public IEnumerable<TrackContract> Tracks { get; set; }
    }
}