using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace groove_music.Models
{
    public class Musics
    {
        [Key]
        public required int MusicId { get; set; }
        public required string MusicName { get; set; }
        public required string MusicCover { get; set; }
        [ForeignKey("Albums")]
        public required int AlbumId { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public Albums? Albums { get; set; }
    }
}
