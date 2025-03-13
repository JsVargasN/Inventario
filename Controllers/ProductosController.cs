using Inventario.Infrastructure.Interfaces;
using Inventario.Models;
using Inventario.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;

        public ProductosController(IProductoService productoService, ICategoriaService categoriaService)
        {
            _productoService = productoService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.GetAllProductos();
            var viewModels = productos.Select(p => new ProductosViewModel
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId,
                CategoriaNombre = p.Categorias.Nombre
            }).ToList();
            return View(viewModels);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = await _categoriaService.GetAllCategorias();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductosViewModel model)
        {
            Console.WriteLine($"Datos recibidos - Nombre: {model.Nombre}, Precio: {model.Precio}, Stock: {model.Stock}, CategoriaId: {model.CategoriaId}, CategoriaNombre: {model.CategoriaNombre}");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error de validación: {error.ErrorMessage}");
                }
                ViewBag.Categorias = await _categoriaService.GetAllCategorias();
                return View(model);
            }

            try
            {
                var producto = new Productos
                {
                    Nombre = model.Nombre,
                    Precio = model.Precio,
                    Stock = model.Stock,
                    CategoriaId = model.CategoriaId
                };
                await _productoService.AddProducto(producto);
                Console.WriteLine("Producto guardado exitosamente.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al guardar producto: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                ModelState.AddModelError("", $"Ocurrió un error al guardar el producto: {ex.Message}");
                ViewBag.Categorias = await _categoriaService.GetAllCategorias();
                return View(model);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _productoService.GetProductoById(id);
            if (producto == null) return NotFound();
            var viewModel = new ProductosViewModel
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = producto.Categorias.Nombre
            };
            ViewBag.Categorias = await _categoriaService.GetAllCategorias();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductosViewModel model)
        {
            if (ModelState.IsValid)
            {
                var producto = new Productos
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Precio = model.Precio,
                    Stock = model.Stock,
                    CategoriaId = model.CategoriaId
                };
                await _productoService.UpdateProducto(producto);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = await _categoriaService.GetAllCategorias();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _productoService.GetProductoById(id);
            if (producto == null) return NotFound();
            var viewModel = new ProductosViewModel
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = producto.Categorias.Nombre
            };
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productoService.DeleteProducto(id);
            return RedirectToAction(nameof(Index));
        }
    // MiProyecto.Web/Controllers/ProductosController.cs
public async Task<IActionResult> Details(int id)
        {
            var producto = await _productoService.GetProductoById(id);
            if (producto == null) return NotFound();
            var viewModel = new ProductosViewModel
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = producto.Categorias.Nombre
            };
            return View(viewModel);
        }
    }
}
