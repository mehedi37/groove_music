using groove_music.Models;

namespace groove_music.ViewModels
{
    public class AddArtistViewModel
    {
        public Artists Artist { get; set; } = new Artists
        {
            ArtistId = new int(),
            ArtistName = string.Empty
        };
    }
}
