using System.Collections.ObjectModel;

namespace MusicLibrary.Models
{
    public class Album
    {
        public ObservableCollection<Track> Tracks { get; set; }
        public string Name { get; set; }
        public string AlbumCover { get; set; }

        public Album()
        {
            Tracks = new ObservableCollection<Track>();
        }

        public override string ToString() => Name;
    }
}