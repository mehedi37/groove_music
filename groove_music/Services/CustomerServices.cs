using groove_music.Data;
using groove_music.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace groove_music.Services
{
    public class CustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerViewModel>> GetCustomersBySellerIdAsync(string sellerId)
        {
            // Fetch the necessary data without performing the aggregation
            var cartData = await _context.Cart
                .Where(c => c.IsPurchased && c.CartItems.Any(cd => cd.Albums.userId == sellerId))
                .Include(c => c.CartItems)
                .ThenInclude(cd => cd.Albums)
                .Include(c => c.ApplicationUser)
                .ToListAsync();

            // Perform the aggregation in memory
            var customers = cartData
                .GroupBy(c => c.UserId)
                .Select(g => new CustomerViewModel
                {
                    CustomerName = g.First().ApplicationUser.Name,
                    TotalSpent = g.Sum(c => c.CartItems.Sum(cd => cd.Price * cd.Quantity))
                })
                .ToList();

            return customers;
        }
    }
}
