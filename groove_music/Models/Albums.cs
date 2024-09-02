using groove_music.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace groove_music.Models
{
    public class Albums
    {
        [Key]
        public required int AlbumId { get; set; }
        [ForeignKey("ApplicationUser")]
        public required string userId { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public ApplicationUser? ApplicationUser { get; set; }

        public required string AlbumName { get; set; }
        public required string AlbumYear { get; set; }
        public required string AlbumGenre { get; set; }
        [ForeignKey("Artists")]
        public required int ArtistId { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public Artists? Artists { get; set; }
        public required string AlbumCover { get; set; }
        public required int Stock { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public required decimal AlbumPrice { get; set; }
        public ICollection<Musics> Musics { get; set; } = new List<Musics>();
    }
}
