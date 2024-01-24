using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoLibreria.Models;
using ProyectoLibreria.Models.Entidades;

namespace ProyectoLibreria.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly LibreriaContext _context;

        public CategoriasController(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ListadoCategoria()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Categoria entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(entidad);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Categoria creada exitosamente";
                    return RedirectToAction("ListadoCategoria");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Los datos de la categoría no son válidos");
                }
            }
            catch (Exception ex)
            {
                // Captura la excepción y realiza algún registro o manejo de errores
                ModelState.AddModelError(string.Empty, $"Ha ocurrido un error: {ex.Message}");
            }

            return View();
        }


        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var entidad = await _context.Categorias.FindAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            return View(entidad);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Categoria entidad)
        {
            if (id != entidad.idcategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entidad);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Categoria actualizada " +
                        "exitosamente!!!";
                    return RedirectToAction("ListadoCategoria");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(ex.Message, "Ocurrio un error " +
                        "al actualizar");
                }
            }
            return View(entidad);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var entidad = await _context.Categorias
                .FirstOrDefaultAsync(m => m.idcategoria == id);

            if (entidad == null)
            {
                return NotFound();
            }

            try
            {
                _context.Categorias.Remove(entidad);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Categoria eliminada exitosamente!!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");
            }

            return RedirectToAction(nameof(ListadoCategoria));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
