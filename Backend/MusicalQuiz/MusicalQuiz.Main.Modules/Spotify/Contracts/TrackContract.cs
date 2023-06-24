namespace MusicalQuiz.Main.Modules.Spotify.Contracts
{
    public class TrackContract
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Album { get; set; }
        public string Authors { get; set; }
        public string ImageUrl { get; set; }
        public string PreviewUrl { get; set; }
    }
}