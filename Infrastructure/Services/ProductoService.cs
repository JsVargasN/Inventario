using System;
using Inventario.Data;
using Inventario.Infrastructure.Interfaces;
using Inventario.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Infrastructure.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ApplicationDbContext _context;

        public ProductoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Productos>> GetAllProductos()
        {
            return await _context.Productos.Include(p => p.Categorias).ToListAsync();
        }

        public async Task<Productos> GetProductoById(int id)
        {
            return await _context.Productos.Include(p => p.Categorias)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProducto(Productos producto)
        {
            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Producto guardado con ID: {producto.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar en la base de datos: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        public async Task UpdateProducto(Productos producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
