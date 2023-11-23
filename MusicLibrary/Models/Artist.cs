using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Models
{
    public class Artist
    {
        public List<Album> Albums { get; set; }
        public string Name { get; set; }
        public Artist()
        { 
            Albums = new List<Album>();
        }
        public override string ToString() => Name;
    }
}
