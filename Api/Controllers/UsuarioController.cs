using Api.Security.Autenticacion;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBaseApi
    {
        // http://localhost:5000/api/Usuario/registrar
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> RegistrarUsuario(ManejoUsuarios.RegistrarUsuario parametros)
        {
            return await Mediator.Send(parametros);
        }

        // http://localhost:5000/api/Usuario
        [HttpGet]
        public async Task<ActionResult<UsuarioData>> ObtenerUsuario()
        {
            return await Mediator.Send(new Sesion.SesionUsuario());
        }
    }
}
