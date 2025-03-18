namespace BibliotecaMVC.Models
{
    public class Libro : Usuario
    {
        public int Id { get; set; } // Clave primaria
        public string Titulo { get; set; } // Título del libro
        public int AutorId { get; set; } // Clave foránea para el autor
        public Autor Autor { get; set; } // Relación con el autor
        public int EditorialId { get; set; } // Clave foránea para la editorial
        public Editorial Editorial { get; set; } // Relación con la editorial
        public string ImagenPortada { get; set; } // URL de la imagen de portada
    }
}
