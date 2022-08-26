using UltimoDesafio.Controllers.DTOs;
using UltimoDesafio.Model;
using System.Data.SqlClient;
using System.Data;

namespace UltimoDesafio.Repository
{
    public static class UsuarioHandler
    {
        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> resultados = new List<Usuario>();

            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();

                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();

                                resultados.Add(usuario);
                            }
                        }
                    }
                }
            }

            return resultados;
        }


        public static bool EliminarUsuario(int id)
        {
            bool resultado = false;

            ProductoVendidoHandler.EliminarProductosVendidosPorUsuario(id);
            ProductoHandler.EliminarProductoPorUsuario(id);

            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                string queryDelete = "DELETE FROM Usuario WHERE Id = @id; ";

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

        public static bool ModificarUsuario(Usuario usuario)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                string queryInsert = "UPDATE Usuario " +
                    "SET Nombre = @nombreParameter, " +
                    "Apellido = @apellidoParameter, " +
                    "NombreUsuario = @nombreUsuarioParameter, " +
                    "Contraseña = @contraseñaParameter, " +
                    "Mail = @mailParameter " +
                    "WHERE Id = @idParameter;";

                SqlParameter nombreParameter = new SqlParameter("nombreParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("apellidoParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuarioParameter", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParameter = new SqlParameter("contraseñaParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mailParameter = new SqlParameter("mailParameter", System.Data.SqlDbType.VarChar) { Value = usuario.Mail };

                SqlParameter idParameter = new SqlParameter("idParameter", System.Data.SqlDbType.BigInt) { Value = usuario.Id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    sqlCommand.Parameters.Add(mailParameter);
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


        public static bool CrearUsuario(Usuario postUsuario)
        {
            bool resultado = false;
            var userIsValid = ValidarCamposObligatorios(postUsuario);

            if(userIsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
                {
                    string queryInsert = "INSERT INTO Usuario " +
                        "(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES " +
                        "(@nombreParameter, @apellidoParameter, @nombreUsuarioParameter, @contraseñaParameter, @mailParameter )";

                    SqlParameter nombreParameter = new SqlParameter("nombreParameter", System.Data.SqlDbType.VarChar) { Value = postUsuario.Nombre };
                    SqlParameter apellidoParameter = new SqlParameter("apellidoParameter", System.Data.SqlDbType.VarChar) { Value = postUsuario.Apellido };
                    SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuarioParameter", System.Data.SqlDbType.VarChar) { Value = postUsuario.NombreUsuario };
                    SqlParameter contraseñaParameter = new SqlParameter("contraseñaParameter", System.Data.SqlDbType.VarChar) { Value = postUsuario.Contraseña };
                    SqlParameter mailParameter = new SqlParameter("mailParameter", System.Data.SqlDbType.VarChar) { Value = postUsuario.Mail };

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(nombreParameter);
                        sqlCommand.Parameters.Add(apellidoParameter);
                        sqlCommand.Parameters.Add(nombreUsuarioParameter);
                        sqlCommand.Parameters.Add(contraseñaParameter);
                        sqlCommand.Parameters.Add(mailParameter);

                        int numberOfRows = sqlCommand.ExecuteNonQuery();

                        if (numberOfRows > 0)
                        {
                            resultado = true;
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return resultado;
        }

        private static bool ValidarCamposObligatorios(Usuario postUsuario)
        {
            var nombreExistente = TraerUsuarioPorNombre(postUsuario.NombreUsuario);
            if (nombreExistente != null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(postUsuario.Contraseña) 
                || string.IsNullOrEmpty(postUsuario.Mail) 
                || string.IsNullOrEmpty(postUsuario.NombreUsuario) 
                || string.IsNullOrEmpty(postUsuario.Nombre) 
                || string.IsNullOrEmpty(postUsuario.Apellido))
            {
                return false;
            }

            return true;
        }

        public static Usuario TraerUsuarioPorNombre(string nombreUsuario)
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "select * from Usuario where NombreUsuario = @nombreUsuario;";

                    sqlCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = sqlCommand;
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table); //Se ejecuta el Select
                    sqlCommand.Connection.Close();
                    foreach (DataRow row in table.Rows)
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(row["Id"]);
                        usuario.NombreUsuario = row["NombreUsuario"]?.ToString();
                        usuario.Nombre = row["Nombre"]?.ToString();
                        usuario.Apellido = row["Apellido"]?.ToString();
                        usuario.Contraseña = row["Contraseña"]?.ToString();
                        usuario.Mail = row["Mail"]?.ToString();

                        usuarios.Add(usuario);
                    }
                }
            }
            return usuarios?.FirstOrDefault();
        }

        public static Usuario BuscarUsuarioPorUsuarioYContraseña(string nombreUsuario, string contraseña)
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(DbHandler.GetConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "select * from Usuario where NombreUsuario = @nombreUsuario AND Contraseña = @contraseña;";

                    sqlCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    sqlCommand.Parameters.AddWithValue("@contraseña", contraseña);


                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = sqlCommand;
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table); //Se ejecuta el Select
                    sqlCommand.Connection.Close();
                    foreach (DataRow row in table.Rows)
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(row["Id"]);
                        usuario.NombreUsuario = row["NombreUsuario"]?.ToString();
                        usuario.Nombre = row["Nombre"]?.ToString();
                        usuario.Apellido = row["Apellido"]?.ToString();
                        usuario.Contraseña = row["Contraseña"]?.ToString();
                        usuario.Mail = row["Mail"]?.ToString();

                        usuarios.Add(usuario);
                    }
                }
            }
            var result = usuarios?.FirstOrDefault();

            if (result == null)
                return new Usuario() { Nombre = String.Empty, NombreUsuario = string.Empty, Apellido = string.Empty, Contraseña = String.Empty, Mail = string.Empty };
            else
                return result;
        }
    }
}
