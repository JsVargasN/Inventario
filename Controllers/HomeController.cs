using System.Diagnostics;
using Inventario.Infrastructure.Interfaces;
using Inventario.Models;
using Inventario.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;

        public HomeController(IProductoService productoService, ICategoriaService categoriaService)
        {
            _productoService = productoService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.GetAllProductos();
            var categorias = await _categoriaService.GetAllCategorias();

            var viewModel = new HomeViewModel
            {
                TotalProductos = productos.Count,
                TotalCategorias = categorias.Count,
                ProductosRecientes = productos.OrderByDescending(p => p.Id).Take(5).ToList()
            };

            return View(viewModel);
        }
    }
}
