using Api.Models;

namespace Api.Interface
{
    public interface IJwtGenerador
    {
        string CrearToken(Usuario usuario);
    }
}
