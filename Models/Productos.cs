﻿namespace Inventario.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
        public Categorias? Categorias { get; set; }
    }
}
