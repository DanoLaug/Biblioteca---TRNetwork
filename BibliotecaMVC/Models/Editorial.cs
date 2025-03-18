namespace BibliotecaMVC.Models
{
    public class Editorial : Usuario
    {
        public int Id { get; set; } // Clave primaria
        public string Nombre { get; set; } // Nombre de la editorial
        // Relación con libros (una editorial puede tener muchos libros)
        public IEnumerable<Libro>? Libros { get; set; }
    }
}
