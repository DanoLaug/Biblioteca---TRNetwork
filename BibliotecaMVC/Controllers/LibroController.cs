using BibliotecaMVC.DTOs;
using BibliotecaMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BibliotecaMVC.Controllers
{
    public class LibroController : Controller
    {
        // Inyecciones de dependencias
        private readonly ILibroService _libroService;
        private readonly IAutorService _autorService;
        private readonly IEditorialService _editorialService;

        public LibroController(
            ILibroService libroService,
            IAutorService autorService,
            IEditorialService editorialService)
        {
            _libroService = libroService;
            _autorService = autorService;
            _editorialService = editorialService;
        }

        // Mostrar detalles
        public async Task<IActionResult> Details(int id)
        {
            var libro = await _libroService.GetByIdAsync(id);

            if (libro == null)
            {
                TempData["ErrorMessage"] = "El Libro no existe.";
                return RedirectToAction("Index");
            }

            return View(libro); // Muestra la vista de detalles
        }

        // Mostrar todo 
        public async Task<IActionResult> Index()
        {
            var libros = await _libroService.GetAllAsync();
            return View(libros);
        }

        // Acción para mostrar el formulario de agregar
        public async Task<IActionResult> Create()
        {
            // Cargar listas de autores y editoriales
            ViewBag.Autores = await _autorService.GetAllAsync();
            ViewBag.Editoriales = await _editorialService.GetAllAsync();
            return View();
        }

        // Acción para procesar el formulario de agregar 
        [HttpPost]
        public async Task<IActionResult> Create(LibroDTO libroDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _libroService.AddAsync(libroDTO);
                    TempData["SuccessMessage"] = "Libro creado exitosamente.";
                    return RedirectToAction("Index");
                }
                // Recargar listas de autores y editoriales en caso de error
                ViewBag.Autores = await _autorService.GetAllAsync();
                ViewBag.Editoriales = await _editorialService.GetAllAsync();
                return View(libroDTO);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al agregar el Libro: {e.Message}";
            }
            return View(libroDTO);
        }

        // Acción para mostrar el formulario de edición de un libro
        public async Task<IActionResult> Edit(int id)
        {
            var libro = await _libroService.GetByIdAsync(id);

            if (libro == null)
            {
                TempData["ErrorMessage"] = "El Libro no existe.";
                return RedirectToAction("Index");
            }

            // Cargar listas de autores y editoriales
            ViewBag.Autores = await _autorService.GetAllAsync();
            ViewBag.Editoriales = await _editorialService.GetAllAsync();
            return View(libro); // Muestra la vista de edición con los datos del libro
        }

        // Acción para procesar el formulario de edición de libro
        [HttpPost]
        public async Task<IActionResult> Edit(int id, LibroDTO libroDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _libroService.UpdateAsync(libroDTO);
                    TempData["SuccessMessage"] = "Libro actualizado exitosamente.";
                    return RedirectToAction("Index");
                }
                // Recargar listas de autores y editoriales en caso de error
                ViewBag.Autores = await _autorService.GetAllAsync();
                ViewBag.Editoriales = await _editorialService.GetAllAsync();
                return View(libroDTO);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al actualizar el Libro: {e.Message}";
            }
            return View(libroDTO);
        }

        // Acción para mostrar la confirmación de eliminación
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var libro = await _libroService.GetByIdAsync(id);

            if (libro == null)
            {
                TempData["ErrorMessage"] = "El Libro no existe.";
                return RedirectToAction("Index");
            }

            return View(libro); // Muestra la vista de confirmación de eliminación
        }

        // Acción para eliminar un libro
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _libroService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Libro eliminado exitosamente.";
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al eliminar el Libro: {e.Message}";
            }
            return RedirectToAction("Index");
        }
    }
}