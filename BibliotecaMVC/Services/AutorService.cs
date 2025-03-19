using System.ComponentModel.DataAnnotations;
using BibliotecaMVC.Data;
using BibliotecaMVC.DTOs;
using BibliotecaMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Services
{
    public class AutorService : IAutorService
    {
        //Inyecciones
        private readonly ApplicationDbContext _context;

        public AutorService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Métodos
        //Método para agregar un Autor
        public async Task AddAsync(AutorDTO autorDTO)
        {
            var autor = new Autor
            {
                Nombre = autorDTO.Nombre,
            };
            await _context.Autores.AddAsync(autor);
            await _context.SaveChangesAsync();
        }

        //Método para eliminar
        public async Task DeleteAsync(int id)
        {
            var autor = await _context.Autores
                    .FindAsync(id);

            if (autor == null)
            {
                throw new ApplicationException("El Autor no existe.");
            }

            autor.IsDeleted = true;
            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
        }

        //Método para obtener todos
        public async Task<List<AutorDTO>> GetAllAsync()
        {
            return await _context.Autores
                .Where(p => !p.IsDeleted)
                .Select(p => new AutorDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre
                })
            .ToListAsync();
        }

        //Método para obtener por su id
        public async Task<AutorDTO> GetByIdAsync(int id)
        {
            var autor = await _context.Autores
                .Include(x => x.Libros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                throw new ApplicationException($"Autor con ID {id} no encontrado.");
            }

            return new AutorDTO
            {
                Id = autor.Id,
                Nombre = autor.Nombre,
            };
        }

        //Método para actualizar 
        public async Task UpdateAsync(AutorDTO autorDTO)
        {
            var autor = await _context.Autores
                .FindAsync(autorDTO.Id);
            if (autor == null)
            {
                throw new ApplicationException("El Autor no existe");
            }

            autor.Nombre = autorDTO.Nombre;

            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
        }
    }
}
