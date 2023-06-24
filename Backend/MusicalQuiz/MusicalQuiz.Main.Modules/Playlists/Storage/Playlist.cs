using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicalQuiz.Main.Modules.Playlists.Storage;

public class Playlist
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Description { get; set; }
    [Required] public virtual List<Track> Tracks { get; set; }
    [Required] public DateTime DateCreated { get; set; }
}