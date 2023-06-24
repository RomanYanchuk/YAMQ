using System.Collections.Generic;

namespace MusicalQuiz.Main.Modules.Playlists.Api;

public class PostPlaylistContract
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> TracksIds { get; set; }
}