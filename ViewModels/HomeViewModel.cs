using Inventario.Models;

namespace Inventario.ViewModels
{
    public class HomeViewModel
    {
        public int TotalProductos { get; set; }
        public int TotalCategorias { get; set; }
        public List<Productos> ProductosRecientes { get; set; }
    }
}
