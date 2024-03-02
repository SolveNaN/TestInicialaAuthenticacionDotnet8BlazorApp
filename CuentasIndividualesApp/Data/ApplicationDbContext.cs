using CuentasIndividualesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CuentasIndividualesApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Agrega un DbSet para cada entidad (por ejemplo, Product)
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioDB> UsuarioDBs { get; set; }
    }
}