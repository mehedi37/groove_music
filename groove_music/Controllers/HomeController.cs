using groove_music.Data;
using groove_music.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace groove_music.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var albums = await _context.Albums
                .Include(a => a.Artists)
                .Include(a => a.Musics)
                .Where(a => a.userId != userId)
                .ToListAsync();

            return View(albums);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            var albums = from a in _context.Albums
                         .Include(a => a.Artists)
                         .Include(a => a.Musics)
                         select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(a => a.AlbumName.Contains(searchString) ||
                                           a.Artists.ArtistName.Contains(searchString) ||
                                           a.Musics.Any(m => m.MusicName.Contains(searchString)));
            }

            return PartialView("_MusicsPartial", await albums.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
