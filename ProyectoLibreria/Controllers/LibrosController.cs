using Microsoft.AspNetCore.Mvc;
using ProyectoLibreria.Models;
using ProyectoLibreria.Services;
using ProyectoLibreria.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ProyectoLibreria.Controllers
{
    public class LibrosController : Controller
    {
        private readonly IServicioUsuario _servicioUsuario;
        private readonly IServicioImagen _servicioImagen;
        private readonly LibreriaContext _context;
        private readonly IServicioLista _servicioLista;

        public LibrosController(IServicioUsuario servicioUsuario, IServicioImagen servicioImagen, LibreriaContext context, IServicioLista servicioLista)
        {
            _servicioUsuario = servicioUsuario;
            _servicioImagen = servicioImagen;
            _context = context;
            _servicioLista = servicioLista;
        }

        public async Task<IActionResult> Lista()
        {
            return View(await _context.Libros
                 .Include(l => l.Categoria)
                 .Include(l => l.Autor)
                 .Include(l => l.Editorial)
                 .ToListAsync());

        }

        public async Task<IActionResult> Crear()
        {
            Libro libro = new Libro()
            {
                Categorias = await _servicioLista.GetListaCategorias(),
                Autores = await _servicioLista.GetListaAutores(),
                Editoriales = await _servicioLista.GetListaEditoriales()
            };

            return View(libro);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Libro libro, IFormFile Imagen)
        {
            if (Imagen != null && !ModelState.IsValid)
            {
                Stream image = Imagen.OpenReadStream();

                // Generar un sufijo único (por ejemplo, la fecha actual en milisegundos)
                string uniqueSuffix = DateTime.Now.Ticks.ToString();

                // Obtener la extensión de la imagen original
                string extension = Path.GetExtension(Imagen.FileName);

                // Crear un nuevo nombre único para la imagen
                string uniqueFileName = $"{Imagen.Name}_{uniqueSuffix}{extension}";

                // Subir la imagen con el nuevo nombre único
                string urlImagen = await _servicioImagen.SubirImagen(image, uniqueFileName);

                libro.URLImagen = urlImagen;

                _context.Add(libro);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Libro creado exitosamente";
                return RedirectToAction("Lista");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error");
            }

            libro.Categorias = await _servicioLista.GetListaCategorias();
            libro.Autores = await _servicioLista.GetListaAutores();
            libro.Editoriales = await _servicioLista.GetListaEditoriales();
            return View(libro);
        }


        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            libro.Autores = await _servicioLista.GetListaAutores();
            libro.Categorias = await _servicioLista.GetListaCategorias();
            libro.Editoriales = await _servicioLista.GetListaEditoriales();

            return View(libro);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Libro libro, IFormFile Imagen)
        {
            if (Imagen != null && ModelState.IsValid)
            {
                try
                {
                    var libroexistente = await _context.Libros.FindAsync(libro.IdLibro);

                    if (libroexistente == null)
                    {
                        return NotFound();
                    }

                    // Generar un sufijo único (por ejemplo, la fecha actual en milisegundos)
                    string uniqueSuffix = DateTime.Now.Ticks.ToString();

                    // Obtener la extensión de la imagen original
                    string extension = Path.GetExtension(Imagen.FileName);

                    // Crear un nuevo nombre único para la imagen
                    string uniqueFileName = $"{Imagen.Name}_{uniqueSuffix}{extension}";

                    // Subir la nueva imagen con el nuevo nombre único
                    Stream image = Imagen.OpenReadStream();
                    string urlImagen = await _servicioImagen.SubirImagen(image, uniqueFileName);
                    libroexistente.URLImagen = urlImagen;

                    // Actualizar los demás campos del libro
                    libroexistente.Titulo = libro.Titulo;
                    libroexistente.Autor = await _context.Autores.FindAsync(libro.AutorId);
                    libroexistente.Categoria = await _context.Categorias.FindAsync(libro.CategoriaId);
                    libroexistente.Editorial = await _context.Editoriales.FindAsync(libro.EditorialId);
                    libroexistente.Precio = libro.Precio;
                    libroexistente.year = libro.year;
                    libroexistente.FechaRegistro = libro.FechaRegistro;

                    _context.Update(libroexistente);
                    await _context.SaveChangesAsync();

                    TempData["AlertMessage"] = "Libro actualizado exitosamente!!!";
                    return RedirectToAction("Lista");
                }
                catch (Exception ex)
                {
                    TempData["AlertMessage"] = ex.Message;
                    return RedirectToAction("Lista");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error");
            }

            return View(libro);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.IdLibro == id);

            if (libro == null)
            {
                return NotFound();
            }

            try
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Libro eliminado exitosamente!!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");
            }

            return RedirectToAction(nameof(Lista));
        }

        public async Task<IActionResult> Compra()
        {
            try
            {
                var libros = await _context.Libros
                    .Include(l => l.Categoria)
                    .Include(l => l.Autor)
                    .Include(l => l.Editorial)
                    .ToListAsync();

                return View(libros);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(new List<Libro>()); // Puedes proporcionar una lista vacía en caso de error
            }
        }

    }
}
