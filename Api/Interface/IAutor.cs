using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface
{
    public interface IAutor
    {
        Task<IEnumerable<AutorDTO>> ObtenerAutores();
        Task<AutorDTO> ObtenerAutorPorId(int AutorId);
        Task<int> RegistrarAutor(AutorDTO autor);
        Task<int> ModificarAutor(AutorDTO autor);
        Task<int> EliminarAutor(int AutorId);
    }
}
