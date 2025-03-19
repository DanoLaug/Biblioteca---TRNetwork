using BibliotecaMVC.DTOs;

namespace BibliotecaMVC.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> GetAllAsync();
        Task<UsuarioDTO> GetByIdAsync(int id);
        Task AddAsync(UsuarioDTO usuarioDTO);
        Task UpdateAsync(UsuarioDTO usuarioDTO);
        Task DeleteAsync(int id);
    }
}
