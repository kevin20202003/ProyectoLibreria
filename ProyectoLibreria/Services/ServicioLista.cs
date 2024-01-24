using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoLibreria.Models;

namespace ProyectoLibreria.Services
{
    public class ServicioLista : IServicioLista
    {
        private readonly LibreriaContext _context;

        public ServicioLista(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetListaAutores()
        {
            List<SelectListItem> list = await _context.Autores.Select(x => new SelectListItem
            {
                Text = x.nombre,
                Value = $"{x.id_autor}"
            })
                 .OrderBy(x => x.Text)
                 .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un autor...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetListaCategorias()
        {
            List<SelectListItem> list = await _context.Categorias.Select(x => new SelectListItem
            {
                Text = x.categoria,
                Value = $"{x.idcategoria}"
            })
              .OrderBy(x => x.Text)
              .ToListAsync();
            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una categoría...]",
                Value = "0"
            });
            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetListaEditoriales()
        {
            List<SelectListItem> list = await _context.Editoriales.Select(x => new SelectListItem
            {
                Text = x.nombre,
                Value = $"{x.id_editorial}"
            })
              .OrderBy(x => x.Text)
              .ToListAsync();
            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una editorial...]",
                Value = "0"
            });
            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetListaRoles()
        {
            List<SelectListItem> list = await _context.Roles.Select(x => new SelectListItem
            {
                Text = x.nombre_rol,
                Value = $"{x.id_rol}"
            })
              .OrderBy(x => x.Text)
              .ToListAsync();
            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un rol...]",
                Value = "0"
            });
            return list;
        }
    }
}
