using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ECars.Back.Models;

namespace ECars.Back.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Subscricao> Subscricoes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}