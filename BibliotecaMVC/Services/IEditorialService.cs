using System.ComponentModel.DataAnnotations;
using BibliotecaMVC.DTOs;

namespace BibliotecaMVC.Services
{
    public interface IEditorialService
    {
        Task<List<EditorialDTO>> GetAllAsync();
        Task<EditorialDTO> GetByIdAsync(int id);
        Task AddAsync(EditorialDTO editorialDTO);
        Task UpdateAsync(EditorialDTO editorialDTO);
        Task DeleteAsync(int id);
    }
}
