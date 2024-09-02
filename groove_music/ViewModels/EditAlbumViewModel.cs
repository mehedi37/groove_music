using groove_music.Models;

namespace groove_music.ViewModels
{
    public class EditAlbumViewModel
    {
        public Albums Album { get; set; }
        public List<Artists>? Artists { get; set; }
        public int ArtistId { get; set; }
        public List<Musics>? Musics { get; set; }
    }
}