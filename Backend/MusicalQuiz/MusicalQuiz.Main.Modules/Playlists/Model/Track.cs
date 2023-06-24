using MusicalQuiz.Main.Modules.Playlists.Exceptions;

namespace MusicalQuiz.Main.Modules.Playlists.Model;

public class Track
{
    public string Id { get; }
    public string Name { get; }
    public string Album { get; }
    public string Authors { get; }
    public string ImageUrl { get; }
    public string PreviewUrl { get; }

    public Track(string id, string name, string album, string authors, string imageUrl, string previewUrl)
    {
        ValidateFieldIfEmpty(id, nameof(Id));
        ValidateFieldIfEmpty(name, nameof(Name));
        ValidateFieldIfEmpty(album, nameof(Album));
        ValidateFieldIfEmpty(authors, nameof(Authors));
        ValidateFieldIfEmpty(imageUrl, nameof(ImageUrl));
        ValidateFieldIfEmpty(previewUrl, nameof(PreviewUrl));
        Id = id;
        Name = name;
        Album = album;
        Authors = authors;
        ImageUrl = imageUrl;
        PreviewUrl = previewUrl;
    }


    private static void ValidateFieldIfEmpty(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new PlaylistModelException($"Field {name} is required.");
        }
    }
}