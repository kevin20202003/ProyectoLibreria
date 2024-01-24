using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProyectoLibreria.Models;
using ProyectoLibreria.Models.Entidades;
using ProyectoLibreria.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProyectoLibreria.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServicioUsuario _servicioUsuario;
        private readonly IServicioImagen _servicioImagen;
        private readonly LibreriaContext _context;
        private readonly IServicioLista _servicioLista;

        public LoginController(IServicioUsuario servicioUsuario, IServicioImagen servicioImagen, LibreriaContext context, IServicioLista servicioLista)
        {
            _servicioUsuario = servicioUsuario;
            _servicioImagen = servicioImagen;
            _context = context;
            _servicioLista = servicioLista;
        }

        public async Task<IActionResult> Lista()
        {
            return View(await _context.Usuarios
                 .Include(l => l.Rol)
                 .ToListAsync());

        }

        public async Task<IActionResult> Registro()
        {
            Usuario usuario = new Usuario()
            {
                Roles = await _servicioLista.GetListaRoles()
            };

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Registro(Usuario usuario, IFormFile Imagen)
        {
            if (Imagen == null || Imagen.Length == 0)
            {
                // La imagen está vacía, muestra un mensaje de error
                ViewData["Mensaje"] = "Por favor coloque la imagen.";
                return View(usuario);
            }

            Stream image = Imagen.OpenReadStream();
            string urlImagen = await _servicioImagen.SubirImagen(image, Imagen.FileName);

            usuario.contrasena = Utilitario.EncriptarClave(usuario.contrasena);
            usuario.URLFotoPerfil = urlImagen;

            Usuario usuarioCreado = await _servicioUsuario.SaveUsuario(usuario);

            usuario.Roles = await _servicioLista.GetListaRoles();

            if (usuarioCreado.id_usuario > 0)
            {
                return RedirectToAction("IniciarSesion", "Login");
            }

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View(usuario);
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Usuario usuario, string correo, string clave, int rol)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(clave) || rol == 0)
            {
                ViewData["Mensaje"] = "Por favor, complete todos los campos.";
                return View();
            }

            // Obtener la lista de roles y asignarla al modelo
            usuario.Roles = await _servicioLista.GetListaRoles();

            // Obtener el usuario según los parámetros
            Usuario usuarioEncontrado = await _servicioUsuario.GetUsuario(correo, Utilitario.EncriptarClave(clave), rol);

            if (usuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View(usuario);
            }

            List<Claim> claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name, usuarioEncontrado.nombre_usuario),
        new Claim("FotoPerfil", usuarioEncontrado.URLFotoPerfil),
        new Claim(ClaimTypes.Role, usuarioEncontrado.RolId.ToString()),
    };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            return RedirectToAction("Index", "Home");
        }
    }
}