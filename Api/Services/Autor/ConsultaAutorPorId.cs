using Api.ErrorHandling;
using DTO;
using Interface;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services.Autor
{
    public class ConsultaAutorPorId
    {
        public class ListaAutorPorId : IRequest<AutorDTO>
        {
            public int AutorId;
        }

        public class ManejadorAutor : IRequestHandler<ListaAutorPorId, AutorDTO>
        {
            private IAutor _autor;
            public ManejadorAutor(IAutor autor) 
            {
                _autor = autor;
            }

            public async Task<AutorDTO> Handle(ListaAutorPorId request, CancellationToken cancellationToken)
            {
                var result = await _autor.ObtenerAutorPorId(request.AutorId);

                if (result == null) 
                {
                    throw new Error(HttpStatusCode.NotFound, new { mensaje = "No se encontró el autor"});
                }

                return result;
            }
        }
    }
}
