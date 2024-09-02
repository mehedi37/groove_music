using groove_music.Models;

namespace groove_music.ViewModels
{
    public class AddAlbumViewModel
    {
        public Albums Album { get; set; } = new Albums
        {
            AlbumId = 0,
            AlbumName = string.Empty,
            AlbumYear = string.Empty,
            AlbumGenre = string.Empty,
            ArtistId = 0,
            AlbumCover = string.Empty,
            Stock = 0,
            AlbumPrice = 0.0m,
            userId = string.Empty
        };
        public List<Artists>? Artists { get; set; }
    }
}
