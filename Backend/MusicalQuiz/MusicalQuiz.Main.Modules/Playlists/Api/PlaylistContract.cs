using System;
using System.Collections.Generic;

namespace MusicalQuiz.Main.Modules.Playlists.Api;

public class PlaylistContract
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<TrackContract> Tracks { get; set; }
}