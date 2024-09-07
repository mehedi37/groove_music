using groove_music.Areas.Identity.Data;
using groove_music.Data;
using groove_music.Services;
using groove_music.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace groove_music.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CustomerService _customerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AlbumsServices _albumsServices;

        public SellerController(ApplicationDbContext context,
            CustomerService customerService,
            UserManager<ApplicationUser> userManager,
            AlbumsServices albumsServices
            )
        {
            _context = context;
            _userManager = userManager;
            _customerService = customerService;
            _albumsServices = albumsServices;
        }

        public async Task<IActionResult> SellMusic()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }
            var viewModel = new SellerViewModel
            {
                Artists = _context.Artists.ToList(),
                Musics = _context.Musics.ToList(),
                Albums = _context.Albums.ToList(),
                ItemsForSale = await _albumsServices.GetItemsForSaleByUserIdAsync(userId),
                Customers = await _customerService.GetCustomersBySellerIdAsync(userId)
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddArtist(AddArtistViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Artists.Any(a => a.ArtistName == model.Artist.ArtistName.Trim()))
                {
                    ModelState.AddModelError("Artist.ArtistName", "Artist already exists.");
                    return PartialView("_AddArtistPartial", model);
                }
                else if (string.IsNullOrEmpty(model.Artist.ArtistName))
                {
                    ModelState.AddModelError("Artist.ArtistName", "Artist name cannot be empty.");
                    return PartialView("_AddArtistPartial", model);
                }
                model.Artist.ArtistName = model.Artist.ArtistName.Trim();
                _context.Artists.Add(model.Artist);
                _context.SaveChanges();
                return RedirectToAction("SellMusic");
            }
            return PartialView("_AddArtistPartial", model);
        }

        [HttpPost]
        public IActionResult AddAlbum(AddAlbumViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                if (_context.Albums.Any(a => a.AlbumName == model.Album.AlbumName.Trim()))
                {
                    ModelState.AddModelError("Album.AlbumName", "Album already exists.");
                    return View("_AddAlbumPartial", model);
                }
                model.Album.userId = userId;
                _context.Albums.Add(model.Album);
                _context.SaveChanges();
                return RedirectToAction("SellMusic");
            }
            model.Artists = _context.Artists.ToList();
            return View("_AddAlbumPartial", model);
        }


        [HttpPost]
        public IActionResult AddMusic(AddMusicViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                _context.Musics.Add(model.Music);
                _context.SaveChanges();
                return RedirectToAction("SellMusic");
            }
            model.Albums = _context.Albums.ToList();
            return View("_AddMusicPartial", model);
        }


        public IActionResult EditAlbum(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var album = _context.Albums
                .Include(a => a.Musics)
                .FirstOrDefault(a => a.AlbumId == id && a.userId == userId);

            if (album == null)
            {
                return NotFound();
            }

            var viewModel = new EditAlbumViewModel
            {
                Album = album,
                Artists = _context.Artists.ToList(),
                ArtistId = album.ArtistId,
                Musics = album.Musics.ToList()
            };

            return View(viewModel);
        }



        [HttpPost]
        public IActionResult EditAlbum(EditAlbumViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                var album = _context.Albums.Find(model.Album.AlbumId);
                if (album == null || album.userId != userId)
                {
                    model.Artists = _context.Artists.ToList();
                    model.Musics = _context.Musics.Where(m => m.AlbumId == model.Album.AlbumId).ToList();
                    return View(model);
                }

                album.AlbumName = model.Album.AlbumName;
                album.AlbumYear = model.Album.AlbumYear;
                album.AlbumGenre = model.Album.AlbumGenre;
                album.AlbumCover = model.Album.AlbumCover;
                album.Stock = model.Album.Stock;
                album.AlbumPrice = model.Album.AlbumPrice;
                album.ArtistId = model.ArtistId;

                _context.Albums.Update(album);
                _context.SaveChanges();
                return RedirectToAction("SellMusic");
            }
            model.Artists = _context.Artists.ToList();
            model.Musics = _context.Musics.Where(m => m.AlbumId == model.Album.AlbumId).ToList(); // Ensure Musics is initialized
            return View(model);
        }




        public IActionResult DeleteAlbum(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var album = _context.Albums.Find(id);
            if (album == null || album.userId != userId)
            {
                return NotFound();
            }
            _context.Albums.Remove(album);
            _context.SaveChanges();
            return RedirectToAction("SellMusic");
        }

        [HttpPost]
        public IActionResult DeleteMusic(int musicId, int albumId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var music = _context.Musics.Include(m => m.Albums).FirstOrDefault(m => m.MusicId == musicId);
            if (music == null || music.Albums.userId != userId)
            {
                return NotFound();
            }
            _context.Musics.Remove(music);
            _context.SaveChanges();
            return RedirectToAction("EditAlbum", new { id = albumId });
        }

        public async Task<IActionResult> AlbumDetails(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var album = await _context.Albums
                .Include(a => a.Musics)
                .Include(a => a.Artists)
                .Where(a => a.userId != userId)
                .FirstOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            var otherAlbums = await _context.Albums
                .Where(a => a.ArtistId == album.ArtistId && a.AlbumId != id)
                .ToListAsync();

            var viewModel = new ProductDetailsViewModel
            {
                Albums = album,
                Musics = album.Musics.ToList(),
                OtherAlbums = otherAlbums
            };

            return View(viewModel);
        }


    }
}
