//Usuario funciona como Registro de usuarios, donde se almacenan los datos.
namespace BibliotecaMVC.Models
{
    public class Usuario
    {
        public int Id { get; set; } // Clave primaria
        public string Nombre { get; set; } // Nombre del usuario
        public string Email { get; set; } // Correo electrónico
        public string Telefono { get; set; } // Teléfono
        public string Direccion { get; set; } // Dirección

        // Relación con préstamos (un usuario puede tener muchos préstamos)
        public IEnumerable<Prestamo>? Prestamos { get; set; }
    }
}
