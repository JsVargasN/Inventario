using Inventario.Models;

namespace Inventario.Infrastructure.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<Categorias>> GetAllCategorias();
        Task<Categorias> GetCategoriaById(int id);
        Task AddCategoria(Categorias categoria);
        Task UpdateCategoria(Categorias categoria);
        Task DeleteCategoria(int id);
    }
}
