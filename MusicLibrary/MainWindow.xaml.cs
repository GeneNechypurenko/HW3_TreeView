using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MusicLibrary.Models;

namespace MusicLibrary
{
    public partial class MainWindow : Window
    {
        private string txtFilePath = "Txt/lstArtists.txt";
        public List<Artist> Artists { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Artists = LoadFromFile();

            foreach (Artist artist in Artists)
            {
                Label artistLabel = new Label();
                artistLabel.Content = artist.Name;
                artistLabel.Tag = artist;
                artistLabel.FontWeight = FontWeights.Bold;
                artistLabel.HorizontalAlignment = HorizontalAlignment.Center;
                artistLabel.Margin = new Thickness(5);
                artistPanel.Children.Add(artistLabel);

                artistLabel.MouseDoubleClick += ArtistLabel_MouseDoubleClick;
            }
        }
        private void ArtistLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            albumPanel.Children.Clear();

            Label clickedLabel = (Label)sender;
            Artist selectedArtist = (Artist)clickedLabel.Tag;

            foreach (Album album in selectedArtist.Albums)
            {
                Label albumLabel = new Label();
                albumLabel.Content = album.Name;
                albumLabel.Tag = album;
                albumLabel.FontWeight = FontWeights.Bold;
                albumLabel.HorizontalAlignment = HorizontalAlignment.Center;
                albumLabel.Margin = new Thickness(5);
                albumPanel.Children.Add(albumLabel);

                albumLabel.MouseDoubleClick += AlbumLabel_MouseDoubleClick;

                if (!string.IsNullOrEmpty(album.AlbumCover) && File.Exists(album.AlbumCover))
                {
                    Image albumImage = new Image();
                    albumImage.Source = new BitmapImage(new Uri(album.AlbumCover, UriKind.Absolute));
                    albumImage.HorizontalAlignment = HorizontalAlignment.Center;
                    albumImage.Margin = new Thickness(0, -20, 0, 5);
                    albumImage.Width = 100;
                    albumImage.Height = 100;
                    albumPanel.Children.Add(albumImage);
                }
            }
        }
        private void AlbumLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label clickedLabel = (Label)sender;
            Album selectedAlbum = (Album)clickedLabel.Tag;

            foreach (Track track in selectedAlbum.Tracks)
            {
                Label trackLabel = new Label();
                trackLabel.Content = track.Name;
                trackLabel.Tag = track;
                trackLabel.FontWeight = FontWeights.Bold;
                trackLabel.HorizontalAlignment = HorizontalAlignment.Center;
                trackLabel.Margin = new Thickness(5);
                trackPanel.Children.Add(trackLabel);
            }

            trackPanel.InvalidateVisual();
        }

        private List<Artist> LoadFromFile()
        {
            List<Artist> artists = new List<Artist>();
            Artist currentArtist = null;

            foreach (string line in File.ReadAllLines(txtFilePath))
            {
                if (line.StartsWith('.'))
                {
                    currentArtist = new Artist { Name = line.Trim('.') };
                    artists.Add(currentArtist);
                }
                else
                {
                    Album currentAlbum = new Album();
                    if (line.StartsWith('$')) { currentAlbum.Name = line.Trim('$'); }
                    if (line.StartsWith('*')) { currentAlbum.AlbumCover = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, line.Trim('*')); }
                    if (line.StartsWith('-'))
                    {
                        Track currentTrack = new Track();
                        int index = line.IndexOf('=');
                        if (index != -1)
                        {
                            currentTrack.Name = line.Substring(1, index - 1).Trim();
                            currentTrack.TrackInfo = line.Substring(index + 1).Trim();
                        }
                        currentAlbum.Tracks.Add(currentTrack);
                    }
                    currentArtist?.Albums.Add(currentAlbum);
                }
            }
            return artists;
        }
    }
}
