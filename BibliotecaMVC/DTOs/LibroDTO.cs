using System.ComponentModel.DataAnnotations;
using BibliotecaMVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BibliotecaMVC.DTOs
{
    public class LibroDTO : Registry
    {
        public int Id { get; set; } // Clave primaria

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Titulo { get; set; } // Título del libro
        public int AutorId { get; set; } // Clave foránea para el autor
        public string? Autor { get; set; } // Relación con el autor
        public int EditorialId { get; set; } // Clave foránea para la editorial
        public string? Editorial { get; set; } // Relación con la editorial
        public string? ImagenPortada { get; set; } //Ruta de la imagen obtenida de la base de datos
        public IFormFile File { get; set; } //Archivo de imagen cargada en la vista Create
    }
}
