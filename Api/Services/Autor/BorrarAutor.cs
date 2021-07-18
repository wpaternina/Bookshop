using FluentValidation;
using Interface;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services.Autor
{
    public class BorrarAutor
    {
        public class EliminarAutor : IRequest 
        {
            public int AutorId;
        }

        public class ValidacionAutor : AbstractValidator<EliminarAutor> 
        {
            public ValidacionAutor() 
            {
                RuleFor(ai => ai.AutorId).NotEmpty();
                RuleFor(ai => ai.AutorId).NotNull();
            }
        }

        public class ManejadorAutor : IRequestHandler<EliminarAutor>
        {
            private readonly IAutor _autor;
            public ManejadorAutor(IAutor autor) 
            {
                _autor = autor;
            }
            public async Task<Unit> Handle(EliminarAutor request, CancellationToken cancellationToken)
            {
                var result = await _autor.EliminarAutor(request.AutorId);

                if (result > 0) 
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo eliminar el Autor");
            }
        }
    }
}
