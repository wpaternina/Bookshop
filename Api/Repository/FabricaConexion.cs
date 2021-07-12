using Interface;
using Microsoft.Extensions.Options;
using Persistences;
using System.Data;
using System.Data.SqlClient;

namespace Repository
{
    public class FabricaConexion : IFabricaConexion
    {
        private IDbConnection _dbConnection;
        private readonly IOptions<ConexionSQLServer> _conexionSQLServer;

        public FabricaConexion(IOptions<ConexionSQLServer> conexionSQLServer) 
        {
            _conexionSQLServer = conexionSQLServer;
        }

        public void CerrarConexion()
        {
            if (_dbConnection != null && _dbConnection.State == ConnectionState.Open) 
            {
                _dbConnection.Close();
            }
        }

        public IDbConnection ConectarSQLServer()
        {
            if (_dbConnection == null) 
            {
                _dbConnection = new SqlConnection(_conexionSQLServer.Value.DefaultConnection);
            }

            if (_dbConnection.State != ConnectionState.Open) 
            {
                _dbConnection.Open();
            }

            return _dbConnection;
        }
    }
}
