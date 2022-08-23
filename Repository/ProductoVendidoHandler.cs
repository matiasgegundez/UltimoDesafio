using System.Data.SqlClient;
using UltimoDesafio.Controllers.DTOs;
using UltimoDesafio.Model;

namespace UltimoDesafio.Repository
{
    public static class ProductoVendidoHandler
    {
        public const string ConnectionString = "Server=localhost;Database=SistemaGestion;Trusted_Connection=True;";

        public static List<ProductoVendido> GetProductosVendidos()
        {
            List<ProductoVendido> resultados = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
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
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
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

        public static bool ModificarProductoVendido(ProductoVendido productoVendido)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
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
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
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
