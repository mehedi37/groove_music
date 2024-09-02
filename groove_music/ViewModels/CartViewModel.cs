
using groove_music.Models;

namespace groove_music.ViewModels
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public List<CartItems> CartDetails { get; set; } = new List<CartItems>();
        public decimal TotalPrice { get; set; }
    }
}
