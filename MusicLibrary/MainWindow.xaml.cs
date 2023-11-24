using MusicLibrary.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MusicLibrary
{
    public partial class MainWindow : Window
    {
        private string txtFilePath = "Txt/lstArtists.txt";
        public ObservableCollection<Artist> Artists { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Artists = new ObservableCollection<Artist>(LoadFromFile());
            artistListBox.ItemsSource = Artists;
        }
        private ObservableCollection<Artist> LoadFromFile()
        {
            ObservableCollection<Artist> artists = new ObservableCollection<Artist>();
            Artist currentArtist = null;
            Album currentAlbum = null;

            foreach (string line in File.ReadAllLines(txtFilePath))
            {
                if (line.StartsWith('.'))
                {
                    currentArtist = new Artist { Name = line.Trim('.') };
                    artists.Add(currentArtist);
                }
                else if (line.StartsWith('$'))
                {
                    currentAlbum = new Album { Name = line.Trim('$') };
                    currentArtist?.Albums.Add(currentAlbum);
                }
                else if (line.StartsWith('*'))
                {
                    currentAlbum.AlbumCover = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, line.Trim('*'));
                }
                else if (line.StartsWith('-'))
                {
                    Track currentTrack = new Track();
                    int index = line.IndexOf('=');
                    if (index != -1)
                    {
                        currentTrack.Name = line.Substring(1, index - 1).Trim();
                        currentTrack.TrackInfo = line.Substring(index + 1).Trim();
                    }
                    currentAlbum?.Tracks.Add(currentTrack);
                }
            }
            return artists;
        }
        private void artistTextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Artist selectedArtist = (sender as TextBlock)?.DataContext as Artist;

            if (selectedArtist != null)
            {
                albumListBox.ItemsSource = selectedArtist.Albums;
                trackListBox.ItemsSource = null;
                trackInfoListBox.ItemsSource = null;
            }
        }
        private void albumTextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Album selectedAlbum = (sender as TextBlock)?.DataContext as Album ?? (sender as Image)?.DataContext as Album;

            if (selectedAlbum != null)
            {
                trackListBox.ItemsSource = selectedAlbum.Tracks;
                trackInfoListBox.ItemsSource = null;
            }
        }
        private void trackTextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Track selectedTrack = (sender as TextBlock)?.DataContext as Track;

            if (selectedTrack != null) { trackInfoListBox.ItemsSource = new ObservableCollection<Track> { selectedTrack }; }
        }
    }
}