namespace BibliotecaMVC.Models
{
    public class Prestamo : Usuario
    {
        public int Id { get; set; }
        public int LibroId { get; set; } // Clave foránea para el libro
        public Libro Libro { get; set; } // Relación con el libro
        public int UsuarioId { get; set; } // Clave foránea para el usuario
        public Usuario Usuario { get; set; } // Relación con el usuario
        public DateTime FechaPrestamo { get; set; } // Fecha de préstamo
    }
}
