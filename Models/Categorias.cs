namespace Inventario.Models
{
    public class Categorias
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Productos> Productos { get; set; } = new List<Productos>();
    }
}
