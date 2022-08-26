using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using UltimoDesafio.Controllers.DTOs;
using UltimoDesafio.Model;

namespace UltimoDesafio.Repository
{
    public static class ProductoVendidoHandler
    {
        public static List<Producto> TraerProductosVendidosDeUnUsuario(int idUsuario)
        {
            List<Producto> productosVendidos = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("select P.* from ProductoVendido PV " +
                    "INNER JOIN Producto P on P.Id = PV.IdProducto WHERE P.IdUsuario = @idUsuario", sqlConnection))
                {
                    sqlCommand.Connection.Open();
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();

                                productosVendidos.Add(producto);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return productosVendidos;
        }

        public static List<Producto> TraerProductosVendidosDeUnaVenta(int idVenta)
        {
            List<Producto> productosVendidos = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT P.* from ProductoVendido PV " +
                    "INNER JOIN Producto P ON " +
                    "P.Id = PV.IdProducto " +
                    "WHERE PV.IdVenta = @idVenta", sqlConnection))
                {
                    sqlCommand.Connection.Open();
                    sqlCommand.Parameters.AddWithValue("@IdVenta", idVenta);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();

                                productosVendidos.Add(producto);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return productosVendidos;
        }

        public static List<ProductoVendido> GetProductosVendidosDeUnaVenta(int idVenta)
        {
            List<ProductoVendido> resultados = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ProductoVendido WHERE IdVenta = @idVenta", sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@idVenta", idVenta);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();

                                productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                resultados.Add(productoVendido);
                            }
                        }
                    }
                }
            }

            return resultados;
        }

        public static List<ProductoVendido> GetProductosVendidos()
        {
            List<ProductoVendido> resultados = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ProductoVendido", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();

                                productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                resultados.Add(productoVendido);
                            }
                        }
                    }
                }
            }

            return resultados;
        }

        public static bool EliminarProductoVendido(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                string queryDelete = "DELETE FROM ProductoVendido WHERE Id = @id";
                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool EliminarProductosVendidosDeProductoId(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                string queryDelete = "DELETE FROM ProductoVendido WHERE IdProducto = @id";
                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool EliminarProductosVendidosPorUsuario(int idUsuario)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                string queryDelete = "DELETE pv from ProductoVendido pv " +
                    "INNER JOIN Producto p " +
                    "ON PV.IdProducto = P.Id " +
                    "WHERE p.IdUsuario = @id ;";

                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = idUsuario;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool EliminarProductosVendidosDeVentaId(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                string queryDelete = "DELETE FROM ProductoVendido WHERE IdVenta = @id";
                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool ModificarProductoVendido(ProductoVendido productoVendido)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                string queryInsert = "UPDATE ProductoVendido " +
                    "SET Stock = @stockParameter, " +
                    "IdProducto = @idProductoParameter, " +
                    "IdVenta = @idVentaParameter " +
                    "WHERE Id = @idParameter";

                SqlParameter stockParameter = new SqlParameter("stockParameter", System.Data.SqlDbType.Int) { Value = productoVendido.Stock };
                SqlParameter idProductoParameter = new SqlParameter("idProductoParameter", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                SqlParameter idVentaParameter = new SqlParameter("idVentaParameter", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdVenta };
                SqlParameter idParameter = new SqlParameter("idParameter", System.Data.SqlDbType.BigInt) { Value = productoVendido.Id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idProductoParameter);
                    sqlCommand.Parameters.Add(idVentaParameter);
                    sqlCommand.Parameters.Add(idParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool CrearProductoVendido(ProductoVendido productoVendido)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                string queryInsert = "INSERT INTO ProductoVendido " +
                    "(Stock, IdProducto, IdVenta) VALUES " +
                    "(@stockParameter, @idProductoParameter, @idVentaParameter )";

                SqlParameter stockParameter = new SqlParameter("stockParameter", System.Data.SqlDbType.Int) { Value = productoVendido.Stock };
                SqlParameter idProductoParameter = new SqlParameter("idProductoParameter", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                SqlParameter idVentaParameter = new SqlParameter("idVentaParameter", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdVenta };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idProductoParameter);
                    sqlCommand.Parameters.Add(idVentaParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }
    }
}
