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
    public class ManejoRoles
    {
        public class RegistrarRoles : IRequest 
        {
            public string Nombre { get; set; }
        }

        public class ValidacionRoles : AbstractValidator<RegistrarRoles> 
        {
            public ValidacionRoles() 
            {
                RuleFor(n => n.Nombre).NotEmpty();
                RuleFor(n => n.Nombre).NotNull();
            }
        }

        public class ManejadorRoles : IRequestHandler<RegistrarRoles>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public ManejadorRoles(RoleManager<IdentityRole> rolManager) 
            {
                _roleManager = rolManager;
            }
            public async Task<Unit> Handle(RegistrarRoles request, CancellationToken cancellationToken)
            {
                var rol = await _roleManager.FindByNameAsync(request.Nombre);

                if (rol != null) 
                { 
                    throw new Error(HttpStatusCode.BadRequest, new { mensaje = "Este rol ya existe" });
                }

                var result = await _roleManager.CreateAsync(new IdentityRole(request.Nombre));

                if (result.Succeeded) 
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo guardar el rol");
            }
        }
    }
}
