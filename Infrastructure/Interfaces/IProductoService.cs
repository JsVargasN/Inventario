using Inventario.Models;
using System.Threading.Tasks;

namespace Inventario.Infrastructure.Interfaces
{
    public interface IProductoService
    {
        Task<List<Productos>> GetAllProductos();
        Task<Productos> GetProductoById(int id);
        Task AddProducto(Productos producto);
        Task UpdateProducto(Productos producto);
        Task DeleteProducto(int id);
    }
}
