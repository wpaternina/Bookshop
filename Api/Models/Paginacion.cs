using System.Collections.Generic;

namespace Api.Models
{
    public class Paginacion
    {
        public List<IDictionary<string, object>> ListaRegistro { get; set; }
        public int TotalRegistro { get; set; }
        public int NumeroPagina { get; set; }
    }
}
