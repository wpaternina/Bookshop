using Api.Interface;
using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Security.Autenticacion
{
    public class Sesion
    {
        public class SesionUsuario : IRequest<UsuarioData> { }
        public class ManejadorSesion : IRequestHandler<SesionUsuario, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly IUsuarioSesion _usuarioSesion;
            public ManejadorSesion(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion)
            {
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _usuarioSesion = usuarioSesion;
            }
            public async Task<UsuarioData> Handle(SesionUsuario request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
                return new UsuarioData
                {
                    NombreCompleto = usuario.NombreCompleto,
                    Username = usuario.UserName,
                    Token = _jwtGenerador.CrearToken(usuario),
                    Email = usuario.Email,
                    Imagen = null
                };
            }
        }
    }
}
