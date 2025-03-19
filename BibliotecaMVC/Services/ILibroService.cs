using BibliotecaMVC.DTOs;

namespace BibliotecaMVC.Services
{
    public interface ILibroService
    {
        Task<List<LibroDTO>> GetAllAsync();
        Task<LibroDTO> GetByIdAsync(int id);
        Task AddAsync(LibroDTO libroDTO);
        Task UpdateAsync(LibroDTO libroDTO);
        Task DeleteAsync(int id);
    }
}
