using System;

namespace Models
{
    public class Libro
    {
        public int LibroId { get; set; }
        public int CategoriaId { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public string Idioma { get; set; }
        public int Paginas { get; set; }
        public string InformacionEditorial { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioAlquiler { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal? PrecioPromocion { get; set; }
        public byte?[] Portada { get; set; }
    }
}
