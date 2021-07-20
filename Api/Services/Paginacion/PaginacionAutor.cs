using Api.DTO;
using Api.Interface;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services.Paginacion
{
    public class PaginacionAutor
    {
        public class PaginacionListaAutor : IRequest<PaginacionDTO> 
        { 
            public string NombreAutor { get; set; }
            public int NumeroPagina { get; set; }
            public int CantidadElementos { get; set; }
        }

        public class ManejadorPaginacion : IRequestHandler<PaginacionListaAutor, PaginacionDTO>
        {
            private readonly IPaginacion _paginacion;
            public ManejadorPaginacion(IPaginacion paginacion) 
            {
                _paginacion = paginacion;
            }
            public async Task<PaginacionDTO> Handle(PaginacionListaAutor request, CancellationToken cancellationToken)
            {
                var sp = "sp_ObtenerPaginacionAutor";
                var ordenamiento = "NombreAutor";
                var parametros = new Dictionary<string, object>();
                parametros.Add("NombreAutor", request.NombreAutor);
                return await _paginacion.DevolverPaginacion(sp, request.NumeroPagina, request.CantidadElementos, parametros, ordenamiento);
            }
        }
    }
}
