using System.Data.SqlClient;
using System.Data;
using UltimoDesafio.Model;

namespace UltimoDesafio.Repository
{
    public static class VentaHandler
    {
        public static List<Venta> GetVentas()
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "SELECT * FROM Venta;";

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = sqlCommand;

                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);

                    sqlCommand.Connection.Close();

                    foreach (DataRow row in table.Rows)
                    {
                        Venta venta = new Venta();

                        venta.Id = Convert.ToInt32(row["Id"]);
                        venta.Comentarios = row["Comentarios"].ToString();

                        ventas.Add(venta);
                    }
                }
            }

            foreach(var venta in ventas)
            {
                var productos = ProductoVendidoHandler.TraerProductosVendidosDeUnaVenta(venta.Id);
                venta.Productos = productos;
            }


            return ventas;
        }

        public static bool EliminarVenta(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                string queryDelete = "DELETE FROM Venta WHERE Id = @id";
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

        public static bool ModificarVenta(Venta venta)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                string queryInsert = "UPDATE Venta " +
                    "SET Comentarios = @comentariosParameter " +
                    "WHERE Id = @idParameter";

                SqlParameter comentariosParameter = new SqlParameter("comentariosParameter", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };
                SqlParameter idParameter = new SqlParameter("idParameter", System.Data.SqlDbType.BigInt) { Value = venta.Id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentariosParameter);
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

        public static int CrearVenta(Venta venta)
        {
            int idVenta = 0;
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                string queryInsert = "INSERT INTO Venta " +
                    "(Comentarios) VALUES " +
                    "(@comentariosParameter);" +
                    "SELECT SCOPE_IDENTITY();";

                SqlParameter comentariosParameter = new SqlParameter("comentariosParameter", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentariosParameter);

                    idVenta = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                sqlConnection.Close();
            }
            return idVenta;
        }
    }
}
