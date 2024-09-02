using groove_music.Models;

namespace groove_music.ViewModels
{
    public class AddMusicViewModel
    {
        public Musics Music { get; set; } = new Musics
        {
            MusicId = new int(),
            MusicName = string.Empty,
            MusicCover = string.Empty,
            AlbumId = new int()
        };
        public List<Albums>? Albums { get; set; }
    }
}
