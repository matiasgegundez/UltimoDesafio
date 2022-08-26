using UltimoDesafio.Model;

namespace UltimoDesafio.Controllers.DTOs
{
    public class PostVenta
    {
        public string Comentarios { get; set; }
        public List<PostProductoVendido> Productos {get;set;}
    }
}
