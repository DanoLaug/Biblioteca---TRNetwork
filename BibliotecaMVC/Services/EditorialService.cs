using BibliotecaMVC.Data;
using BibliotecaMVC.DTOs;
using BibliotecaMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Services
{
    public class EditorialService : IEditorialService
    {
        //Inyecciones
        private readonly ApplicationDbContext _context;

        public EditorialService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Métodos
        //Método para agregar 
        public async Task AddAsync(EditorialDTO editorialDTO)
        {
            var editorial = new Editorial
            {
                Nombre = editorialDTO.Nombre,
            };
            await _context.Editoriales.AddAsync(editorial);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var editorial = await _context.Editoriales
                    .FindAsync(id);

            if (editorial == null)
            {
                throw new ApplicationException("La Editorial no existe.");
            }

            //autor.IsDeleted = true;
            _context.Editoriales.Update(editorial);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EditorialDTO>> GetAllAsync()
        {
            return await _context.Editoriales
                .Select(p => new EditorialDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre
                })
            .ToListAsync();
        }

        public async Task<EditorialDTO> GetByIdAsync(int id)
        {
            var editorial = await _context.Editoriales
                .Include(x => x.Libros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (editorial == null)
            {
                throw new ApplicationException($"Editorial con ID {id} no encontrado.");
            }

            return new EditorialDTO
            {
                Id = editorial.Id,
                Nombre = editorial.Nombre,
            };
        }

        public async Task UpdateAsync(EditorialDTO editorialDTO)
        {
            var editorial = await _context.Editoriales
                .FindAsync(editorialDTO.Id);
            if (editorial == null)
            {
                throw new ApplicationException("La Editorial no existe");
            }

            editorial.Nombre = editorialDTO.Nombre;

            _context.Editoriales.Update(editorial);
            await _context.SaveChangesAsync();
        }
    }
}
