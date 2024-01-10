using Microsoft.EntityFrameworkCore;
using ProyectoLibreria.Models;
using ProyectoLibreria.Models.Entidades;

namespace ProyectoLibreria.Services
{
    public class ServicioAutor : IServicioAutor
    {
        private readonly LibreriaContext _context;

        public ServicioAutor(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<Autor> GetAutor(string nombre_autor, string apellido_autor)
        {
            Autor Autor = await _context.Autores.Where(u => u.nombre == nombre_autor && u.apellido == apellido_autor).FirstOrDefaultAsync();

            return Autor;
        }

        public async Task<Autor> GetAutor(string nombre_autor)
        {
            Autor Autor = await _context.Autores.Where(u => u.nombre == nombre_autor).FirstOrDefaultAsync();

            return Autor;
        }

        public async Task<Autor> SaveAutor(Autor Autor)
        {
            _context.Autores.Add(Autor);
            await _context.SaveChangesAsync();
            return Autor;
        }
    }
}
