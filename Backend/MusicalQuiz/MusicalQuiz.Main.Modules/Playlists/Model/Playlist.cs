using System;
using System.Collections.Generic;
using System.Linq;
using MusicalQuiz.Main.Modules.Infrastructure.Components;
using MusicalQuiz.Main.Modules.Playlists.Exceptions;

namespace MusicalQuiz.Main.Modules.Playlists.Model;

public class Playlist
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public ValueComparableReadOnlyCollection<Track> Tracks { get; }

    public Playlist(Guid id, string name, string description, List<Track> tracks)
    {
        Id = id;

        if (string.IsNullOrEmpty(name))
        {
            throw new PlaylistModelException($"Field {nameof(Name)} is required.");
        }
        Name = name;

        Description = description;

        if (!tracks.Any())
        {
            throw new PlaylistModelException($"Field {nameof(Tracks)} is required.");
        }
        Tracks = tracks.ToReadonly();
    }
}