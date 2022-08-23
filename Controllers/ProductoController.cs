using Microsoft.AspNetCore.Mvc;
using UltimoDesafio.Controllers.DTOs;
using UltimoDesafio.Model;
using UltimoDesafio.Repository;

namespace UltimoDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {

        [HttpGet(Name = "GetProductos")]
        public List<Producto> GetProductos()
        {
            try
            {
                return ProductoHandler.GetProducto();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Producto>();
            }
        }

        [HttpGet("GetProductosProIdUsuario", Name = "GetProductosByIdUsuario")]
        public List<Producto> GetProductosPorIdUsuario(int idUsuario)
        {
            try
            {
                return ProductoHandler.TraerProductosPorIdUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Producto>();
            }
        }

        [HttpGet("GetProductoPorId", Name = "GetProductoById")]
        public Producto GetProductosPorId(int id)
        {
            try
            {
                return ProductoHandler.GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Producto() { Descripciones = string.Empty };
            }
        }

        [HttpDelete(Name = "DeleteProducto")]
        public bool EliminarProducto([FromBody] int id)
        {
            try
            {
                return ProductoHandler.EliminarProducto(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut(Name = "UpdateProducto")]
        public bool ModificarProducto([FromBody] PutProducto producto)
        {
            try
            {
                return ProductoHandler.ModificarUsuario(new Producto()
                {
                    Id = producto.Id,
                    Descripciones = producto.Descripciones,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    Costo = producto.Costo,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPost(Name = "CreateProductos")]
        public bool CrearProducto([FromBody] PostProducto producto)
        {
            try
            {
                return ProductoHandler.CrearProducto(new Producto()
                {
                    Descripciones = producto.Descripciones,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    Costo = producto.Costo,
                    IdUsuario = producto.IdUsuario 
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
