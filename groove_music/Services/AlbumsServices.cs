using groove_music.Data;
using groove_music.Models;
using Microsoft.EntityFrameworkCore;

namespace groove_music.Services
{
    public class AlbumsServices
    {
        private readonly ApplicationDbContext _context;

        public AlbumsServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Albums>> GetItemsForSaleByUserIdAsync(string userId)
        {
            return await _context.Albums.Where(p => p.userId == userId).ToListAsync();
        }

        public async Task<Albums?> GetItemByIdAsync(int id)
        {
            return await _context.Albums.FindAsync(id);
        }

        public async Task AddItemAsync(Albums product)
        {
            _context.Albums.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Albums product)
        {
            _context.Albums.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var product = await _context.Albums.FindAsync(id);
            if (product != null)
            {
                _context.Albums.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
