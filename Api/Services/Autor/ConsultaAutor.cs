using DTO;
using Interface;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Autor
{
    public class ConsultaAutor
    {
        public class ListaAutor : IRequest<List<AutorDTO>> { }
        public class ManejadorAutor : IRequestHandler<ListaAutor, List<AutorDTO>>
        {
            private readonly IAutor _autor;
            public ManejadorAutor(IAutor autor) 
            {
                _autor = autor;
            }

            public async Task<List<AutorDTO>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var resultado = await _autor.ObtenerAutores();
                return resultado.ToList();

            }
        }
    }
}
