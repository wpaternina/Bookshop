using Api.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Interface
{
    public interface IPaginacion
    {
        Task<PaginacionDTO> DevolverPaginacion(string sp, int NumeroPagina, int CantidadElementos, IDictionary<string, object> ParametrosFiltro, string OrdenamientoColumna);
    }
}
