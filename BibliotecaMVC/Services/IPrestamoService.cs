using BibliotecaMVC.DTOs;

namespace BibliotecaMVC.Services
{
    public interface IPrestamoService
    {
        Task<List<PrestamoDTO>> GetAllAsync();
        Task<PrestamoDTO> GetByIdAsync(int id);
        Task AddAsync(PrestamoDTO prestamoDTO);
        Task UpdateAsync(PrestamoDTO prestamoDTO);
        Task DeleteAsync(int id);
    }
}
