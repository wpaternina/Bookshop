using Api.ErrorHandling;
using Api.Interface;
using Api.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Security.Autenticacion
{
    public class Login
    {
        public class LoginSistema : IRequest<UsuarioData> 
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class ValidacionLogin : AbstractValidator<LoginSistema> 
        {
            public ValidacionLogin()
            {
                RuleFor(e => e.Email).NotEmpty();
                RuleFor(e => e.Email).EmailAddress();

                RuleFor(p => p.Password).NotEmpty();
            }
        }

        public class ManejadorLogin : IRequestHandler<LoginSistema, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IJwtGenerador _jwtGenerador;
            public ManejadorLogin(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerador = jwtGenerador;

            }
            public async Task<UsuarioData> Handle(LoginSistema request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);

                if (usuario == null)
                {
                    throw new Error(HttpStatusCode.Unauthorized);
                }

                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                if (resultado.Succeeded)
                {
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario),
                        Username = usuario.UserName,
                        Email = usuario.Email,
                        Imagen = null
                    };
                }

                throw new Error(HttpStatusCode.Unauthorized);
            }
        }
    }
}
