using BibliotecaMVC.DTOs;

namespace BibliotecaMVC.Services
{
    public interface IAutorService
    {
        Task<List<AutorDTO>> GetAllAsync();
        Task<AutorDTO> GetByIdAsync(int id);
        Task AddAsync(AutorDTO autorDTO);
        Task UpdateAsync(AutorDTO autorDTO);
        Task DeleteAsync(int id);
    }
}
