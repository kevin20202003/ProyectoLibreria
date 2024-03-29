using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProyectoLibreria.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ProyectoLibreria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            string nombreUsuario = "";
            string fotoPerfil = "";
            string rol = "";

            if (claimsUser.Identity.IsAuthenticated)
            {
                nombreUsuario = claimsUser.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                fotoPerfil = claimsUser.Claims
                    .FirstOrDefault(c => c.Type == "FotoPerfil")?.Value;

                rol = claimsUser.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            }

            ViewData["nombre_usuario"] = nombreUsuario;
            ViewData["fotoPerfil"] = fotoPerfil;

            if (rol == "1")
            {
                // Redirigir a la pantalla de administrador
               
            }
            else if (rol == "2")
            {
                // Redirigir a la pantalla de cliente
                return RedirectToAction("Cliente", "Cliente");
            }

            // Otro caso, mantener la vista por defecto
            return View();
    }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "Login");
        }
    }
}
