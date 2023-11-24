using System.Collections.ObjectModel;

namespace MusicLibrary.Models
{
    public class Artist
    {
        public ObservableCollection<Album> Albums { get; set; }
        public string Name { get; set; }
        public Artist()
        {
            Albums = new ObservableCollection<Album>();
        }
        public override string ToString() => Name;
    }
}