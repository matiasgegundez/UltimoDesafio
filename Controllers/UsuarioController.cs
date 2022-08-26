using Microsoft.AspNetCore.Mvc;
using UltimoDesafio.Controllers.DTOs;
using UltimoDesafio.Model;
using UltimoDesafio.Repository;

namespace UltimoDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            try
            {
                return UsuarioHandler.GetUsuarios();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Usuario>();
            }
        }

        [HttpGet("/GetUsuarioPorNombre", Name ="GetUsuarioByName")]
        public Usuario GetUsuarioPorNombre(string nombre)
        {
            try
            {
                return UsuarioHandler.TraerUsuarioPorNombre(nombre);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Usuario();
            }
        }

        [HttpGet("/GetUsuarioPorNombreYContraseña", Name = "IniciarSesion")]
        public Usuario GetUsuarioPorNombreYContraseña(string nombre, string contraseña)
        {
            try
            {
                return UsuarioHandler.BuscarUsuarioPorUsuarioYContraseña(nombre, contraseña);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Usuario() { Nombre = String.Empty, NombreUsuario = string.Empty, Apellido = string.Empty, Contraseña = String.Empty, Mail = string.Empty };
            }
        }

        [HttpDelete(Name = "DeleteUsuarios")]
        public bool EliminarUsuario([FromBody] int id)
        {
            try
            {
                return UsuarioHandler.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        [HttpPut(Name = "UpdateUsuario")]
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            {
                try
                {
                    return UsuarioHandler.ModificarUsuario(new Usuario
                    {
                        Id = usuario.Id,
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        Contraseña = usuario.Contraseña,
                        Mail = usuario.Mail,
                        NombreUsuario = usuario.NombreUsuario 
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        [HttpPost(Name = "CreateUsuario")]
        public bool CrearUsuario([FromBody] PostUsuario usuario)
        {
            try
            {
                return UsuarioHandler.CrearUsuario(new Usuario() 
                { 
                    Apellido = usuario.Apellido, 
                    Contraseña = usuario.Contraseña, 
                    Nombre = usuario.Nombre, 
                    NombreUsuario = usuario.NombreUsuario, 
                    Mail = usuario.Mail 
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