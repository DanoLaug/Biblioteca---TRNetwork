using BibliotecaMVC.Data;
using BibliotecaMVC.DTOs;
using BibliotecaMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Services
{
    public class LibroService : ILibroService
    {
        //Inyecciones
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public LibroService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //Métodos
        //Método para agregar
        public async Task AddAsync(LibroDTO libroDTO)
        {
            var imagenPortada = await UploadImage(libroDTO.File);
            var libro = new Libro
            {
                Titulo = libroDTO.Titulo,
                AutorId = libroDTO.AutorId,
                EditorialId = libroDTO.EditorialId,
                ImagenPortada = imagenPortada

            };
            await _context.Libros.AddAsync(libro);
            await _context.SaveChangesAsync();
        }

        private async Task<string> UploadImage(IFormFile file)
        {
            // Guardar la imagen en el servidor y generar la ruta
            var fileName = Path.GetFileNameWithoutExtension(file.FileName)
                            + Guid.NewGuid().ToString()
                            + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/{fileName}";
        }

        //Método para eliminar
        public async Task DeleteAsync(int id)
        {
            var libro = await _context.Libros
                    .FindAsync(id);

            if (libro == null)
            {
                throw new ApplicationException("El Libro no existe.");
            }

            //libro.IsDeleted = true;
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
        }

        //Método para obtener todos
        public async Task<List<LibroDTO>> GetAllAsync()
        {
            return await _context.Libros
                //.Where(p => !p.IsDeleted)
                .Select(p => new LibroDTO
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    AutorId = p.AutorId,
                    AutorNombre = p.Autor.Nombre,
                    EditorialId = p.EditorialId,
                    ImagenPortada = p.ImagenPortada
                })
            .ToListAsync();
        }

        //Método para obtener por su id
        public async Task<LibroDTO> GetByIdAsync(int id)
        {
            var libro = await _context.Libros
                .Include(x => x.Autor)
                .Include(x => x.Editorial)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (libro == null)
            {
                throw new ApplicationException($"Libro con ID {id} no encontrado.");
            }

            return new LibroDTO
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                AutorId = libro.AutorId,
                AutorNombre = libro.Autor.Nombre,
                EditorialId = libro.EditorialId,
                EditorialNombre = libro.Editorial.Nombre,
                ImagenPortada = libro.ImagenPortada
            };
        }

        //Método para actualizar 
        public async Task UpdateAsync(LibroDTO libroDTO)
        {
            var libro = await _context.Libros
               .FindAsync(libroDTO.Id);
            if (libro == null)
            {
                throw new ApplicationException("El Libro no existe");
            }

            libro.Titulo = libroDTO.Titulo;
            libro.AutorId = libroDTO.AutorId;
            libro.EditorialId = libroDTO.EditorialId;
            libro.ImagenPortada = libroDTO.ImagenPortada;

            _context.Libros.Update(libro);
            await _context.SaveChangesAsync();
        }
    }
}
