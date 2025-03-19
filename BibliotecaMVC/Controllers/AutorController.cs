using BibliotecaMVC.DTOs;
using BibliotecaMVC.Models;
using BibliotecaMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Controllers
{
    public class AutorController : Controller
    {
        //Inyecciones de dependencias
        private readonly IAutorService _autorService;

        public AutorController(
            IAutorService autorService)
        {
            _autorService = autorService;
        }

        //Mostrar detalles
        public async Task<IActionResult> Details(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);

            if (autor == null)
            {
                TempData["ErrorMessage"] = "El Autor no existe.";
                return RedirectToAction("Index");
            }

            return View(autor); // Muestra la vista de detalles
        }

        //Mostrar todo 
        public async Task<IActionResult> Index()
        {
            var autores = await _autorService.GetAllAsync();
            return View(autores);
        }

        // Acción para mostrar el formulario de agregar
        public IActionResult Create()
        {
            return View();
        }

        // Acción para procesar el formulario de agregar 
        [HttpPost]
        public async Task<IActionResult> Create(AutorDTO autorDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _autorService.AddAsync(autorDTO);
                    TempData["SuccessMessage"] = "Autor creado exitosamente.";
                    return RedirectToAction("Index");
                }
                return View(autorDTO);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al agregar al Autor.";
            }
            return View(autorDTO);
        }

        // Acción para mostrar el formulario de edición de un producto
        public async Task<IActionResult> Edit(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);

            if (autor == null)
            {
                TempData["ErrorMessage"] = "El Autor no existe.";
                return RedirectToAction("Index");
            }

            return View(autor); // Muestra la vista de edición con los datos del autor
        }
        
        // Acción para procesar el formulario de edición de producto
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AutorDTO autorDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _autorService.UpdateAsync(autorDTO);
                    TempData["SuccessMessage"] = "Autor actualizado exitosamente.";
                    return RedirectToAction("Index");
                }
                return View(autorDTO);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al actualizar al Autor: {e.Message}";
            }
            return View(autorDTO);
        }

        // Acción para mostrar la confirmación de eliminación
        public async Task<IActionResult> Delete(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);

            if (autor == null)
            {
                TempData["ErrorMessage"] = "El Autor no existe.";
                return RedirectToAction("Index");
            }

            return View(autor); // Muestra la vista de confirmación de eliminación
        }
        
        // Acción para eliminar un producto
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _autorService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Autor eliminado exitosamente.";
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"Hubo un error al eliminar al Autor: {e.Message}";
            }
            return RedirectToAction("Index");
        }

    }
}
