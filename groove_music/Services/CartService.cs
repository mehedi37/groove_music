using groove_music.Areas.Identity.Data;
using groove_music.Data;
using groove_music.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace groove_music.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<CartItems>> GetCartDetailsAsync(ApplicationUser user)
        {
            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(cd => cd.Albums)
                .FirstOrDefaultAsync(c => c.UserId == user.Id && !c.IsPurchased);

            if (cart == null || cart.CartItems == null)
            {
                return new List<CartItems>();
            }

            return cart.CartItems.ToList();
        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(cd => cd.Albums)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsPurchased);

            if (cart == null || cart.CartItems == null)
            {
                return null;
            }
            return cart;
        }

        public async Task AddToCartAsync(string userId, int albumId, int quantity)
        {
            var cart = await GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    CartId = new int(),
                    UserId = userId,
                    IsPurchased = false
                };
                _context.Cart.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.AlbumId == albumId);

            if (cartItem == null)
            {
                var album = await _context.Albums.FindAsync(albumId);
                if (album == null)
                {
                    throw new Exception("Album not found");
                }

                cartItem = new CartItems
                {
                    CartItemId = new int(),
                    CartId = cart.CartId,
                    AlbumId = albumId,
                    Quantity = quantity,
                    Price = album.AlbumPrice
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
                _context.CartItems.Update(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.CartItems.Update(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();

                var cart = await _context.Cart
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.CartId == cartItem.CartId);

                if (cart != null && !cart.CartItems.Any())
                {
                    _context.Cart.Remove(cart);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<int> GetCartDetailsCountAsync(string userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            return cart?.CartItems.Count ?? 0;
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                _context.Cart.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task PurchaseCartAsync(string userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                cart.IsPurchased = true;
                _context.Cart.Update(cart);
                await _context.SaveChangesAsync();
            }
        }
    }
}
