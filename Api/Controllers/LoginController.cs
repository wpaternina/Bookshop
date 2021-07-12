using Api.Security.Autenticacion;
using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    // Autorización de controlador sin token
    [AllowAnonymous]
    public class LoginController : ControllerBaseApi
    {
        // http://localhost:5000/api/Login/login
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.LoginSistema parametros)
        {
            return await Mediator.Send(parametros);
        }
    }
}
