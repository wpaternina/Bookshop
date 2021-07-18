using DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface IAutor
    {
        Task<IEnumerable<AutorDTO>> ObtenerAutores();
        Task<AutorDTO> ObtenerAutorPorId(int AutorId);
        Task<int> RegistrarAutor(string NombreAutor, DateTime FechaNacimiento);
        Task<int> ModificarAutor(int AutorId, string NombreAutor, DateTime FechaNacimiento);
        Task<int> EliminarAutor(int AutorId);
    }
}
