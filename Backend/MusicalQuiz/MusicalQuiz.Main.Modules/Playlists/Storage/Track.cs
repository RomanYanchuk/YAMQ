using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicalQuiz.Main.Modules.Playlists.Storage;

public class Track
{
    [Key] public string Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Album { get; set; }
    [Required] public string Authors { get; set; }
    [Required] public string ImageUrl { get; set; }
    [Required] public string PreviewUrl { get; set; }
    [Required] public virtual List<Playlist> Playlists { get; set; }
    [Required] public DateTime DateCreated { get; set; }
}