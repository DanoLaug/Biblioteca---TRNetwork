using BibliotecaMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        //Dar de alta nuestros modelos, sin darlos de alta no se crean las tablas en la base de datos
        //Para este trabajo usaremos Libro, Autor, Editorial, Prestamo, Usuario
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }            
    }
}
