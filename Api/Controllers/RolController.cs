using Api.Security.Autenticacion;
using Controllers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBaseApi
    {
        // http://localhost:5000/Api/Rol/crear
        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> CrearRol(ManejoRoles.RegistrarRoles parametros) 
        {
            return await Mediator.Send(parametros);
        }

        // http://localhost:5000/Api/Rol/eliminar
        [HttpDelete("eliminar")]
        public async Task<ActionResult<Unit>> Eliminar(BorrarRoles.EliminarRoles parametros) 
        {
            return await Mediator.Send(parametros);
        }

        // http://localhost:5000/Api/Rol/lista
        [HttpGet("lista")]
        public async Task<ActionResult<List<IdentityRole>>> Lista()
        {
            return await Mediator.Send(new ConsultaRoles.ListaRoles());
        }

        // http://localhost:5000/Api/Rol/agregarRolUsuario
        [HttpPost("agregarRolUsuario")]
        public async Task<ActionResult<Unit>> AgregarRolUsuario(IntegracionUsuarioRol.RegistrarUsuarioRol parametros) 
        {
            return await Mediator.Send(parametros);
        }

    }
}
