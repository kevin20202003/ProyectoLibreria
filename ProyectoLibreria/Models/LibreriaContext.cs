using Microsoft.EntityFrameworkCore;
using ProyectoLibreria.Models.Entidades;

namespace ProyectoLibreria.Models
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext()
        {
        }
        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options)
        {

        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<DetalleVenta> Detalles { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categoria>().HasIndex(c => c.categoria).IsUnique();
        }
    }
}
