using Microsoft.AspNetCore.Mvc;
using UltimoDesafio.Controllers.DTOs;
using UltimoDesafio.Model;
using UltimoDesafio.Repository;

namespace UltimoDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet("/GetProductoVendidoDeUsuario", Name = "GetProductoVendidoByUser")]
        public List<Producto> GetProductoVendidoPorUsuario(int userId)
        {
            try
            {
                return ProductoVendidoHandler.TraerProductosVendidosDeUnUsuario(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Producto>();
            }
        }

        [HttpGet(Name = "GetProductoVendido")]
        public List<ProductoVendido> GetProductoVendido()
        {
            try
            {
                return ProductoVendidoHandler.GetProductosVendidos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ProductoVendido>();
            }
        }

        [HttpDelete(Name = "DeleteProductoVendido")]
        public bool EliminarProductoVendido([FromBody] int id)
        {
            try
            {
                return ProductoVendidoHandler.EliminarProductoVendido(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut(Name = "UpdateProductoVendido")]
        public bool ModificarProductoVendido([FromBody] PutProductoVendido productoVendido)
        {
            {
                try
                {
                    return ProductoVendidoHandler.ModificarProductoVendido(new ProductoVendido
                    {
                        Id = productoVendido.Id,
                        Stock = productoVendido.Stock,
                        IdProducto = productoVendido.IdProducto,
                        IdVenta = productoVendido.IdVenta
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        [HttpPost(Name = "CreateProductoVendido")]
        public bool CrearProducto([FromBody] PostProductoVendido productoVendido)
        {
            try
            {
                return ProductoVendidoHandler.CrearProductoVendido(new ProductoVendido
                {
                    Stock = productoVendido.Stock,
                    IdProducto = productoVendido.IdProducto,
                    IdVenta = productoVendido.IdVenta
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
