using Microsoft.EntityFrameworkCore;
using trabalho_rest.Model;

namespace trabalho_rest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Usuario> Usuarios { get; set; }
    }
}
