using FluentValidation;
using System;

namespace DTO
{
    public class AutorDTO
    {
        public int AutorId { get; set; }
        public string NombreAutor { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
