using Api.DTO;
using Api.Interface;
using Dapper;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using Interface;

namespace Api.Repository
{
    public class PaginacionRepositorio : IPaginacion
    {
        private readonly IFabricaConexion _fabricaConexion;
        public PaginacionRepositorio(IFabricaConexion fabricaConexion) 
        {
            _fabricaConexion = fabricaConexion;
        }
        public async Task<PaginacionDTO> DevolverPaginacion(string sp, int NumeroPagina, int CantidadElementos, IDictionary<string, object> ParametrosFiltro, string OrdenamientoColumna)
        {
            PaginacionDTO paginacionDTO = new PaginacionDTO();
            List<IDictionary<string, object>> listaReporte = null;
            int TotalRegistro = 0;
            int TotalPaginas = 0;
            try
            {
                var con = _fabricaConexion.ConectarSQLServer();
                DynamicParameters parametros = new DynamicParameters();

                foreach (var param in ParametrosFiltro) 
                {
                    parametros.Add("@" + param.Key, param.Value);
                }

                // Parametros de entrada
                parametros.Add("@NumeroPagina", NumeroPagina);
                parametros.Add("@CantidadElementos", CantidadElementos);
                parametros.Add("@OrdenamientoColumna", OrdenamientoColumna);

                // Parametros de salida
                parametros.Add("@TotalRegistro", TotalRegistro, DbType.Int32, ParameterDirection.Output);
                parametros.Add("@TotalPaginas", TotalPaginas, DbType.Int32, ParameterDirection.Output);

                var result = await con.QueryAsync(sp, parametros, commandType: CommandType.StoredProcedure);
                listaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();
                paginacionDTO.ListaRegistro = listaReporte;
                paginacionDTO.NumeroPagina = parametros.Get<int>("@TotalPaginas");
                paginacionDTO.TotalRegistro = parametros.Get<int>("@TotalRegistro");

            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error en la paginación", ex);
            }
            finally 
            {
                _fabricaConexion.CerrarConexion();
            }

            return paginacionDTO;
        }
    }
}
