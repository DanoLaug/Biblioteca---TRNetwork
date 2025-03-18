namespace BibliotecaMVC.Models
{
    public class Autor : Usuario
    {
        public int Id { get; set; } // Clave primaria
        public string Nombre { get; set; } // Nombre del autor
        // Relación con libros (un autor puede tener muchos libros)
        public IEnumerable<Libro>? Libros { get; set; }
    }
}
