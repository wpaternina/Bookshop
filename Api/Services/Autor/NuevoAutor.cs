using DTO;
using FluentValidation;
using Interface;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services.Autor
{
    public class NuevoAutor
    {
        public class RegistrarAutor : IRequest 
        { 
            public string NombreAutor { get; set; }
            public DateTime FechaNacimiento { get; set; }
        }

        public class ValidacionAutor : AbstractValidator<RegistrarAutor>
        {
            public ValidacionAutor() 
            {
                RuleFor(na => na.NombreAutor).NotEmpty();
                RuleFor(na => na.NombreAutor).NotNull();

                RuleFor(fn => fn.FechaNacimiento).NotEmpty();
                RuleFor(fn => fn.FechaNacimiento).NotNull();
            }
        }

        public class ManejadorAutor : IRequestHandler<RegistrarAutor>
        {
            private readonly IAutor _autor;
            public ManejadorAutor(IAutor autor) 
            {
                _autor = autor;
            }
            public async Task<Unit> Handle(RegistrarAutor request, CancellationToken cancellationToken)
            {
                var autor = new AutorDTO();
                var result = await _autor.RegistrarAutor(request.NombreAutor, request.FechaNacimiento);

                if (result > 0) 
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el Autor");

            }
        }
    }
}
