using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProyectoLibreria.Models;
using ProyectoLibreria.Models.Entidades;
using ProyectoLibreria.Services;
using System.Security.Claims;

namespace ProyectoLibreria.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServicioUsuario _servicioUsuario;
        private readonly IServicioImagen _servicioImagen;
        private readonly LibreriaContext _context;

        public LoginController(IServicioUsuario servicioUsuario, IServicioImagen servicioImagen, LibreriaContext context)
        {
            _servicioUsuario = servicioUsuario;
            _servicioImagen = servicioImagen;
            _context = context;
        }

        public async Task<IActionResult> Registro(Usuario usuario, IFormFile Imagen)
        {
            try
            {
                if (Imagen == null || Imagen.Length == 0)
                {
                    // Manejar el error de imagen nula o vacía
                    ViewData["Mensaje"] = "La imagen es obligatoria para el registro.";
                    return View();
                }

                if (string.IsNullOrEmpty(Imagen.FileName))
                {
                    // Manejar el error de nombre de archivo nulo o vacío
                    ViewData["Mensaje"] = "El nombre de la imagen no es válido.";
                    return View();
                }

                // Verifica que _servicioImagen y _servicioUsuario no sean null antes de usarlos
                if (_servicioImagen != null && _servicioUsuario != null)
                {
                    using (Stream image = Imagen.OpenReadStream())
                    {
                        string urlImagen = await _servicioImagen.SubirImagen(image, Imagen.FileName);

                        usuario.contrasena = Utilitario.EncriptarClave(usuario.contrasena);
                        usuario.URLFotoPerfil = urlImagen;

                        Usuario usuarioCreado = await _servicioUsuario.SaveUsuario(usuario);

                        if (usuarioCreado != null && usuarioCreado.id_usuario > 0)
                        {
                            return RedirectToAction("IniciarSesion", "Login");
                        }
                        else
                        {
                            // Manejar el error de creación de usuario
                            ViewData["Mensaje"] = "No se pudo crear el usuario.";
                            return View();
                        }
                    }
                }
                else
                {
                    // Manejar el error de servicios nulos
                    ViewData["Mensaje"] = "Error interno en los servicios.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                ViewData["Mensaje"] = "Error interno: " + ex.Message;
                return View();
            }
        }



        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            Usuario usuarioEncontrado = await _servicioUsuario.GetUsuario(correo, Utilitario.EncriptarClave(clave));

            if (usuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuarioEncontrado.nombre_usuario),
                new Claim("FotoPerfil", usuarioEncontrado.URLFotoPerfil),
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
