using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace groove_music.Models
{
    public class CartItems
    {
        [Key]
        public required int CartItemId { get; set; }

        [ForeignKey("Albums")]
        public required int AlbumId { get; set; } // Changed from string to int

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Albums? Albums { get; set; }

        [ForeignKey("Cart")]
        public required int CartId { get; set; }

        [DeleteBehavior(DeleteBehavior.Cascade)]
        public Cart? Cart { get; set; }

        [Precision(18, 2)]
        public required decimal Price { get; set; }

        public required int Quantity { get; set; }
    }
}
