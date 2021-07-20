using System.Collections.Generic;

namespace Api.DTO
{
    public class PaginacionDTO
    {
        public List<IDictionary<string, object>> ListaRegistro { get; set; }
        public int TotalRegistro { get; set; }
        public int NumeroPagina { get; set; }
    }
}
