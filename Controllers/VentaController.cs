using Microsoft.AspNetCore.Mvc;
using UltimoDesafio.Controllers.DTOs;
using UltimoDesafio.Model;
using UltimoDesafio.Repository;

namespace UltimoDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet(Name = "GetVentas")]
        public List<Venta> GetVentas()
        {
            try
            {
                return VentaHandler.GetVentas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Venta>();
            }
        }

        [HttpDelete(Name = "DeleteVenta")]
        public bool EliminarVenta([FromBody] int id)
        {
            try
            {
                var productosVendidos = ProductoVendidoHandler.GetProductosVendidosDeUnaVenta(id);
                ProductoVendidoHandler.EliminarProductosVendidosDeVentaId(id);
                if (productosVendidos.Count > 0)
                {
                    foreach (var producto in productosVendidos)
                    {
                        ProductoHandler.ActualizarProductoPorEliminacionDeVenta(producto.Stock, producto.Id);
                    }
                }

                return VentaHandler.EliminarVenta(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut(Name = "UpdateVenta")]
        public bool ModificarVenta([FromBody] PutVenta venta)
        {
            try
            {
                return VentaHandler.ModificarVenta(new Venta()
                {
                    Id = venta.Id,
                    Comentarios = venta.Comentarios
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpPost(Name = "CreateVenta")]
        public int CrearVenta([FromBody] PostVenta venta)
        {
            try
            {
                int idVenta = VentaHandler.CrearVenta(new Venta()
                {
                    Comentarios = venta.Comentarios
                });

                foreach (var producto in venta.Productos)
                {
                    ProductoVendidoHandler.CrearProductoVendido(new ProductoVendido()
                    {
                        IdProducto = producto.IdProducto,
                        IdVenta = idVenta,
                        Stock = producto.Stock
                    });

                    ProductoHandler.ActualizarProductoPorVenta(producto.Stock, producto.IdProducto);
                }

                return idVenta;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
