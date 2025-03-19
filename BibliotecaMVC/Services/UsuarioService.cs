using System.ComponentModel.DataAnnotations;
using BibliotecaMVC.Data;
using BibliotecaMVC.DTOs;
using BibliotecaMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Services
{
    public class UsuarioService : IUsuarioService
    {
        //Inyecciones
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Métodos
        //Método para agregar un usuario
        public async Task AddAsync(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                Email = usuarioDTO.Email,
                Telefono = usuarioDTO.Telefono,
                Direccion = usuarioDTO.Direccion,
                Prestamos = usuarioDTO.Prestamos
            };
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        //Método para eliminar un usuario
        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios
                    .FindAsync(id);

            if (usuario == null)
            {
                throw new ApplicationException("El Usuario no existe.");
            }

            usuario.IsDeleted = true;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
        
        //Método para obtener todos los usuarios
        public async Task<List<UsuarioDTO>> GetAllAsync()
        {
            return await _context.Usuarios
                .Where(p => !p.IsDeleted)
                .Select(p => new UsuarioDTO
                {
                Id = p.Id,
                Nombre = p.Nombre,
                Email = p.Email,
                Telefono = p.Telefono,
                Direccion = p.Direccion,
                Prestamos = p.Prestamos
            })
            .ToListAsync();
        }

        //Método para obtener un usuario por su id
        public async Task<UsuarioDTO> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuarios
                .Include (x => x.Prestamos)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
            {
                throw new ApplicationException($"Usuario con ID {id} no encontrado.");
            }

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Prestamos = usuario.Prestamos
            };
        }

        //Método para actualizar un usuario
        public async Task UpdateAsync(UsuarioDTO usuarioDTO)
        {
            var usuario = await _context.Usuarios
                .FindAsync(usuarioDTO.Id);
            if (usuario == null)
            {
                throw new ApplicationException("El usuario no existe");
            }

            usuario.Nombre = usuarioDTO.Nombre;
            usuario.Email = usuarioDTO.Email;
            usuario.Telefono = usuarioDTO.Telefono;
            usuario.Direccion = usuarioDTO.Direccion;
            usuario.Prestamos = usuarioDTO.Prestamos;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
