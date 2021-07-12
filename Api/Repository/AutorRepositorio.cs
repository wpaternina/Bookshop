using Dapper;
using DTO;
using Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Repository
{
    public class AutorRepositorio : IAutor
    {
        private readonly IFabricaConexion _fabricaConexion;

        public AutorRepositorio(IFabricaConexion fabricaConexion) 
        {
            _fabricaConexion = fabricaConexion;
        }


        public Task<int> EliminarAutor(int AutorId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ModificarAutor(AutorDTO autor)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<AutorDTO>> ObtenerAutores()
        {
            IEnumerable<AutorDTO> Atores = null;
            var sp = "sp_ObtenerAutores";
            try
            {
                var con = _fabricaConexion.ConectarSQLServer();
                Atores = await con.QueryAsync<AutorDTO>(sp, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la transacción con la base de datos", ex);
            }
            finally 
            {
                _fabricaConexion.CerrarConexion();
            }

            return Atores;
        }

        public Task<AutorDTO> ObtenerAutorPorId(int AutorId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> RegistrarAutor(AutorDTO autor)
        {
            throw new System.NotImplementedException();
        }
    }
}
