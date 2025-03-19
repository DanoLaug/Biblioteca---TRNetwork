using BibliotecaMVC.Models;

namespace BibliotecaMVC.DTOs
{
    public class PrestamoDTO
    {
        public int Id { get; set; }
        public int LibroId { get; set; } // Clave foránea para el libro
        public string? Libro { get; set; } // Relación con el libro
        public int UsuarioId { get; set; } // Clave foránea para el usuario
        public string? Usuario { get; set; } // Relación con el usuario
        public DateTime FechaPrestamo { get; set; } // Fecha de préstamo
    }
}
