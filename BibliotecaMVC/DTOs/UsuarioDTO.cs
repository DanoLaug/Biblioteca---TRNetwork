using System.ComponentModel.DataAnnotations;
using BibliotecaMVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BibliotecaMVC.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; } // Clave primaria

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } // Nombre del usuario

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El Email es obligatorio")]
        public string Email { get; set; } // Correo electrónico

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "El Telefono es obligatorio")]
        public string Telefono { get; set; } // Teléfono

        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "La Direccion es obligatoria")]
        public string Direccion { get; set; } // Dirección
        
        // Relación con préstamos (un usuario puede tener muchos préstamos)
        public IEnumerable<Prestamo>? Prestamos { get; set; }

    }
}
