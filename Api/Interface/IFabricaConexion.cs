using System.Data;

namespace Interface
{
    public interface IFabricaConexion
    {
        void CerrarConexion();
        IDbConnection ConectarSQLServer(); 
    }
}
