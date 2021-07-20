using Api.ErrorHandling;
using Api.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Security.Autenticacion
{
    public class IntegracionUsuarioRol 
    {
        public class RegistrarUsuarioRol : IRequest 
        { 
            public string Username { get; set; }
            public string RolNombre { get; set; }
        }

        public class ValidacionUsuarioRol : AbstractValidator<RegistrarUsuarioRol> 
        {
            public ValidacionUsuarioRol()
            {
                RuleFor(un => un.Username).NotEmpty();
                RuleFor(un => un.Username).NotNull();

                RuleFor(rn => rn.RolNombre).NotEmpty();
                RuleFor(rn => rn.RolNombre).NotNull();
            }
        }

        public class ManejadorUsuarioRol : IRequestHandler<RegistrarUsuarioRol>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public ManejadorUsuarioRol(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager) 
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(RegistrarUsuarioRol request, CancellationToken cancellationToken)
            {
                var rol = await _roleManager.FindByNameAsync(request.RolNombre);
                
                if (rol == null) 
                {
                    throw new Error(HttpStatusCode.NotFound, new { mensaje = "No se encontró el rol"});
                }

                var usuario = await _userManager.FindByNameAsync(request.Username);

                if (usuario == null) 
                {
                    throw new Error(HttpStatusCode.NotFound, new { mensaje = "No se encontró el usuario" });
                }

                var result = await _userManager.AddToRoleAsync(usuario, request.RolNombre);

                if (result.Succeeded) 
                {
                    return Unit.Value;
                }

                throw new Excepcion("no se puedo agregar el rol al usuario");

            }
        }
    }
}
