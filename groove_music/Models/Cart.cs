using groove_music.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace groove_music.Models
{
    public class Cart
    {
        [Key]
        public required int CartId { get; set; }
        [ForeignKey("ApplicationUser")]
        public required string UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public bool IsPurchased { get; set; } = false;
        public ICollection<CartItems> CartItems { get; set; } = new List<CartItems>();
    }
}
