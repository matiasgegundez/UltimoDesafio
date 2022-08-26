using Microsoft.AspNetCore.Mvc;
using UltimoDesafio.Model;
using UltimoDesafio.Repository;

namespace UltimoDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AplicacionController : ControllerBase
    {
        public const string APP_Name = "Proyecto final C# de Matias";
        
        [HttpGet(Name = "GetApplicationName")]
        public string GetNombreDeApp()
        {
            return APP_Name;
        }
    }
}
