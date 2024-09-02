using groove_music.Models;

namespace groove_music.ViewModels
{
    public class SellerViewModel
    {
        public List<Albums> ItemsForSale { get; set; } = new List<Albums>();
        public List<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();
        public List<Artists> Artists { get; internal set; } = new List<Artists>();
        public List<Musics> Musics { get; internal set; } = new List<Musics>();
        public List<Albums> Albums { get; internal set; } = new List<Albums>();
    }

    public class CustomerViewModel
    {
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalSpent { get; set; }
    }
}
