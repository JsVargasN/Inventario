using Inventario.Infrastructure.Interfaces;
using Inventario.Models;
using Inventario.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IProductoService _productoService;

        public CategoriasController(ICategoriaService categoriaService, IProductoService productoService)
        {
            _categoriaService = categoriaService;
            _productoService = productoService;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.GetAllCategorias();
            var viewModels = categorias.Select(c => new CategoriasViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre
            }).ToList();
            return View(viewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var categoria = new Categorias
                {
                    Nombre = model.Nombre
                };
                await _categoriaService.AddCategoria(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaService.GetCategoriaById(id);
            if (categoria == null) return NotFound();
            var viewModel = new CategoriasViewModel
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoriasViewModel model)
        {
            if (ModelState.IsValid)
            {
                var categoria = new Categorias
                {
                    Id = model.Id,
                    Nombre = model.Nombre
                };
                await _categoriaService.UpdateCategoria(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaService.GetCategoriaById(id);
            if (categoria == null) return NotFound();
            var viewModel = new CategoriasViewModel
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productos = await _productoService.GetAllProductos();
            if (productos.Any(p => p.CategoriaId == id))
            {
                TempData["ErrorMessage"] = "No se puede eliminar la categoría porque tiene productos asociados.";
                return RedirectToAction(nameof(Index));
            }
            await _categoriaService.DeleteCategoria(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
