using groove_music.Areas.Identity.Data;
using groove_music.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace groove_music.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Artists> Artists { get; set; }
    public DbSet<Albums> Albums { get; set; }
    public DbSet<Cart> Cart { get; set; }
    public DbSet<CartItems> CartItems { get; set; }
    public DbSet<Musics> Musics { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
