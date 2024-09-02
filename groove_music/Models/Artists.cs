using System.ComponentModel.DataAnnotations;

namespace groove_music.Models
{
    public class Artists
    {
        [Key]
        public required int ArtistId { get; set; }
        public required string ArtistName { get; set; }
    }
}
