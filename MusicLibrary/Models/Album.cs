using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace MusicLibrary.Models
{
    public class Album
    {
        public List<Track> Tracks { get; set; }
        public string Name { get; set; }
        public string AlbumCover { get; set; }

        public Album()
        {
            Tracks = new List<Track>();
        }

        public override string ToString() => Name;
    }
}
