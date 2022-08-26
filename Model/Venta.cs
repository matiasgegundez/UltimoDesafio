namespace UltimoDesafio.Model
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }
        public List<Producto> Productos { get; set; }
    }
}
