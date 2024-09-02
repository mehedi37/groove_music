using groove_music.Models;

namespace groove_music.ViewModels
{
    public class ProductDetailsViewModel
    {
        public required Albums Albums { get; set; }
        public required List<Musics> Musics { get; set; } = new List<Musics>();
        public required List<Albums> OtherAlbums { get; set; }
    }
}
