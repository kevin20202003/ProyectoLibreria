﻿using Microsoft.EntityFrameworkCore;
using ProyectoLibreria.Models;
using ProyectoLibreria.Models.Entidades;

namespace ProyectoLibreria.Services
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly LibreriaContext _context;

        public ServicioUsuario(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave, int rol)
        {
            Usuario usuario = await _context.Usuarios.Where(u => u.correo == correo && u.contrasena == clave && u.RolId == rol).FirstOrDefaultAsync();

            return usuario;
        }

        public async Task<Usuario> GetUsuario(string nombreUsuario)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.nombre_usuario == nombreUsuario);
        }

        public async Task<Usuario> SaveUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
