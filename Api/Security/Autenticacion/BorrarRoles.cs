using Api.ErrorHandling;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Security.Autenticacion
{
    public class BorrarRoles
    {
        public class EliminarRoles : IRequest 
        {
            public string Nombre { get; set; }
        }

        public class ValidacionRoles : AbstractValidator<EliminarRoles> 
        {
            public ValidacionRoles()
            {
                RuleFor(n => n.Nombre).NotEmpty();
                RuleFor(n => n.Nombre).NotNull();
            }
        }

        public class ManejadorRoles : IRequestHandler<EliminarRoles>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public ManejadorRoles(RoleManager<IdentityRole> roleManager) 
            {
                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(EliminarRoles request, CancellationToken cancellationToken)
            {
                var rol = await _roleManager.FindByNameAsync(request.Nombre);

                if (rol == null) 
                {
                    throw new Error(HttpStatusCode.BadRequest, new { mensaje = "No existe el rol" });
                }

                var result = await _roleManager.DeleteAsync(rol);

                if (result.Succeeded) 
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo eliminar error");
            }
        }
    }
}
