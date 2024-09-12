using groove_music.Data;
using groove_music.Services;
using groove_music.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace groove_music.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;
        private readonly AlbumsServices _productService;

        public CartController(ApplicationDbContext context, CartService cartService, AlbumsServices productService)
        {
            _context = context;
            _cartService = cartService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null || !cart.CartItems.Any())
            {
                ViewBag.Message = "Your cart is empty.";
                return View(new CartViewModel());
            }

            var cartViewModel = new CartViewModel
            {
                CartId = cart.CartId,
                CartDetails = cart.CartItems.ToList(),
                TotalPrice = cart.CartItems.Sum(cd => cd.Price * cd.Quantity)
            };

            return View(cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int albumId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _context.Albums.FindAsync(albumId);

            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("Index");
            }

            // Retrieve the current cart items for the user
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            var cartItem = cart?.CartItems.FirstOrDefault(ci => ci.AlbumId == albumId);

            // Calculate the total quantity if the item is already in the cart
            var totalQuantity = (cartItem?.Quantity ?? 0) + quantity;

            // Ensure the total quantity does not exceed stock
            if (totalQuantity > product.Stock)
            {
                quantity = product.Stock - (cartItem?.Quantity ?? 0);
                TempData["ErrorMessage"] = "Insufficient stock. Quantity adjusted to available stock.";
            }

            if (quantity <= 0)
            {
                TempData["ErrorMessage"] = "Insufficient stock to add more of this product.";
                return RedirectToAction("Index");
            }

            await _cartService.AddToCartAsync(userId, albumId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(int cartDetailsId, int quantity)
        {
            if (quantity < 1)
            {
                quantity = 1;
            }

            var cartDetail = await _context.CartItems.FindAsync(cartDetailsId);
            if (cartDetail == null)
            {
                return NotFound("Cart item not found.");
            }

            var product = await _context.Albums.FindAsync(cartDetail.AlbumId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            if (quantity > product.Stock)
            {
                quantity = product.Stock;
                TempData["ErrorMessage"] = "Insufficient stock. Quantity adjusted to available stock.";
            }

            await _cartService.UpdateCartAsync(cartDetailsId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartDetailsId)
        {
            await _cartService.RemoveFromCartAsync(cartDetailsId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.ClearCartAsync(userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Purchase()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null || !cart.CartItems.Any())
            {
                return BadRequest("Your cart is empty.");
            }

            foreach (var cartDetail in cart.CartItems)
            {
                var product = await _context.Albums.FindAsync(cartDetail.AlbumId);
                if (product == null || product.Stock < cartDetail.Quantity)
                {
                    return BadRequest("Insufficient stock for product: " + product?.AlbumName);
                }

                product.Stock -= cartDetail.Quantity;
                await _productService.UpdateItemAsync(product);
            }

            await _cartService.PurchaseCartAsync(userId);
            return RedirectToAction("Index");
        }
    }
}