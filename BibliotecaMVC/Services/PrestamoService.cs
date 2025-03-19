using BibliotecaMVC.Data;
using BibliotecaMVC.DTOs;
using BibliotecaMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Services
{
    public class PrestamoService : IPrestamoService
    {
        //Inyecciones
        private readonly ApplicationDbContext _context;

        public PrestamoService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Métodos
        //Método para agregar
        public async Task AddAsync(PrestamoDTO prestamoDTO)
        {
            var prestamo = new Prestamo
            {
                LibroId = prestamoDTO.LibroId,
                UsuarioId = prestamoDTO.UsuarioId,
                FechaPrestamo = prestamoDTO.FechaPrestamo
            };
            await _context.Prestamos.AddAsync(prestamo);
            await _context.SaveChangesAsync();
        }

        //Método para eliminar
        public async Task DeleteAsync(int id)
        {
            var prestamo = await _context.Prestamos
                    .FindAsync(id);

            if (prestamo == null)
            {
                throw new ApplicationException("El Prestamo no existe.");
            }

            //prestamo.IsDeleted = true;
            _context.Prestamos.Update(prestamo);
            await _context.SaveChangesAsync();
        }

        //Método para obtener todos
        public async Task<List<PrestamoDTO>> GetAllAsync()
        {
            return await _context.Prestamos
                //.Where(p => !p.IsDeleted)
                .Select(p => new PrestamoDTO
                {
                    Id = p.Id,
                    LibroId = p.LibroId,
                    UsuarioId = p.UsuarioId,
                    FechaPrestamo = p.FechaPrestamo
                })
            .ToListAsync();
        }

        //Método para obtener por su id
        public async Task<PrestamoDTO> GetByIdAsync(int id)
        {
            var prestamo = await _context.Prestamos
                .Include(x => x.Libro)
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (prestamo == null)
            {
                throw new ApplicationException($"El Prestamo con ID {id} no encontrado.");
            }

            return new PrestamoDTO
            {
                Id = prestamo.Id,
                LibroId = prestamo.LibroId,
                UsuarioId = prestamo.UsuarioId,
                FechaPrestamo = prestamo.FechaPrestamo
            };
        }

        //Método para actualizar 
        public async Task UpdateAsync(PrestamoDTO prestamoDTO)
        {
            var prestamo = await _context.Prestamos
               .FindAsync(prestamoDTO.Id);
            if (prestamo == null)
            {
                throw new ApplicationException("El Prestamo no existe");
            }

            prestamo.LibroId = prestamoDTO.LibroId;
            prestamo.UsuarioId = prestamoDTO.UsuarioId;
            prestamo.FechaPrestamo = prestamoDTO.FechaPrestamo;

            _context.Prestamos.Update(prestamo);
            await _context.SaveChangesAsync();
        }
    }
}
