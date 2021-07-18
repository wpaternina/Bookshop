using FluentValidation;
using Interface;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services.Autor
{
    public class EditarAutor
    {
        public class ModificarAutor : IRequest
        {
            public int AutorId { get; set; }
            public string NombreAutor { get; set; }
            public DateTime FechaNacimiento { get; set; }
        }

        public class ValidacionAutor : AbstractValidator<ModificarAutor> 
        {
            public ValidacionAutor() 
            {
                RuleFor(na => na.NombreAutor).NotEmpty();
                RuleFor(na => na.NombreAutor).NotNull();

                RuleFor(fn => fn.FechaNacimiento).NotEmpty();
                RuleFor(fn => fn.FechaNacimiento).NotNull();
            }
        }

        public class ManejadorAutor : IRequestHandler<ModificarAutor>
        {
            private readonly IAutor _autor;
            public ManejadorAutor(IAutor autor) 
            {
                _autor = autor;
            }
            public async Task<Unit> Handle(ModificarAutor request, CancellationToken cancellationToken)
            {
                var result = await _autor.ModificarAutor(request.AutorId, request.NombreAutor, request.FechaNacimiento);
                if (result > 0) 
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo actualizar los datos del Autor");
            }
        }
    }
}
