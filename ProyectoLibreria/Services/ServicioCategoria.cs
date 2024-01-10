using Microsoft.EntityFrameworkCore;
using ProyectoLibreria.Models;
using ProyectoLibreria.Models.Entidades;

namespace ProyectoLibreria.Services
{
    public class ServicioCategoria : IServicioCategoria
    {
        private readonly LibreriaContext _context;

        public ServicioCategoria(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<Categoria> GetCategoria(string categoria)
        {
            Categoria categoriaE = await _context.Categorias.Where(c => c.categoria == categoria).FirstOrDefaultAsync();
            return categoriaE;
        }

        public async Task<Categoria> SaveCategorias(Categoria entidad)
        {
            _context.Categorias.Add(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}
