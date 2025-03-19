using System.ComponentModel.DataAnnotations;
using BibliotecaMVC.Models;

namespace BibliotecaMVC.DTOs
{
    public class AutorDTO : Registry
    {
        public int Id { get; set; } // Clave primaria

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; } // Nombre del autor

    }
}
