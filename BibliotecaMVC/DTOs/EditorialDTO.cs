using System.ComponentModel.DataAnnotations;
using BibliotecaMVC.Models;

namespace BibliotecaMVC.DTOs
{
    public class EditorialDTO : Registry
    {
        public int Id { get; set; } // Clave primaria

        [Display(Name = "Editorial")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } // Nombre de la editorial

    }
}
