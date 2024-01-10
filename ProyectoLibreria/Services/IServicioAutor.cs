using ProyectoLibreria.Models.Entidades;

namespace ProyectoLibreria.Services
{
    public interface IServicioAutor
    {
        Task<Autor> GetAutor(string nombre_autor, string apellido_autor);
        Task<Autor> SaveAutor(Autor Autor);
        Task<Autor> GetAutor(string nombre_autor);
    }
}
