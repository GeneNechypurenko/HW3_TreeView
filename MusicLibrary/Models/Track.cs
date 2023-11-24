namespace MusicLibrary.Models
{
    public class Track
    {
        public string Name { get; set; }
        public string TrackInfo { get; set; }
        public override string ToString() => Name;
    }
}