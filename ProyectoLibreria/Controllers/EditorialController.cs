using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoLibreria.Models;
using ProyectoLibreria.Models.Entidades;

namespace ProyectoLibreria.Controllers
{
    public class EditorialController : Controller
    {
        private readonly LibreriaContext _context;

        public EditorialController(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ListadoEditorial()
        {
            return View(await _context.Editoriales.ToListAsync());
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Editorial editorial)
        {

            if (ModelState.IsValid)
            {
                _context.Add(editorial);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Editorial creado exitosamente";
                return RedirectToAction("ListadoEditorial");

            }
            else
            {
                ModelState.AddModelError(String.Empty, "Ha ocurrido Un error");
            }



            return View();
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || _context.Editoriales == null)
            {
                return NotFound();
            }

            var editorial = await _context.Editoriales.FindAsync(id);
            if (editorial == null)
            {
                return NotFound();
            }
            return View(editorial);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Editorial editorial)
        {
            if (id != editorial.id_editorial)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editorial);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Editorial actualizado " +
                        "exitosamente!!!";
                    return RedirectToAction("ListadoEditorial");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(ex.Message, "Ocurrio un error " +
                        "al actualizar");
                }
            }
            return View(editorial);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Editoriales == null)
            {
                return NotFound();
            }

            var editorial = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.id_editorial == id);

            if (editorial == null)
            {
                return NotFound();
            }

            try
            {
                _context.Editoriales.Remove(editorial);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Editorial eliminado exitosamente!!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");
            }

            return RedirectToAction(nameof(ListadoEditorial));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
