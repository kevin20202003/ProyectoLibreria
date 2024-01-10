using ProyectoLibreria.Models.Entidades;

namespace ProyectoLibreria.Services
{
    public interface IServicioCategoria
    {
        public Task<Categoria> GetCategoria(string categoria);
        public Task<Categoria> SaveCategorias(Categoria entidad);
    }
}
