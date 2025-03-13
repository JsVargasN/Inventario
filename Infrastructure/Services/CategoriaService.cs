using System;
using Inventario.Data;
using Inventario.Infrastructure.Interfaces;
using Inventario.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Infrastructure.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ApplicationDbContext _context;

        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categorias>> GetAllCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categorias> GetCategoriaById(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task AddCategoria(Categorias categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoria(Categorias categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }
    }
}
