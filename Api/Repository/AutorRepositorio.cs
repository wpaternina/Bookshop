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


        public async Task<int> EliminarAutor(int AutorId)
        {
            var sp = @"sp_EliminarAutores";
            try
            {
                var con = _fabricaConexion.ConectarSQLServer();
                var result = await con.ExecuteAsync(sp,
                                                new
                                                {
                                                    AutorId = AutorId
                                                }, commandType: CommandType.StoredProcedure);

                _fabricaConexion.CerrarConexion();
                return result;
            } 
            catch (Exception ex) 
            {
                throw new Exception("No se pudo eliminar el autor ", ex);
            }
        }

        public async Task<int> ModificarAutor(int AutorId, string NombreAutor, DateTime FechaNacimiento)
        {
            var sp = @"sp_EditarAutores";
            try
            {
                var con = _fabricaConexion.ConectarSQLServer();
                var result =  await con.ExecuteAsync(sp, 
                                              new { 
                                                AutorId = AutorId,
                                                NombreAutor = NombreAutor,
                                                FechaNacimiento = FechaNacimiento
                                              }, commandType: CommandType.StoredProcedure);

                _fabricaConexion.CerrarConexion();
                return result;
            }
            catch (Exception ex) 
            {
                throw new Exception("No se pudo modificar el autor ", ex);
            }
        }

        public async Task<IEnumerable<AutorDTO>> ObtenerAutores()
        {
            IEnumerable<AutorDTO> Atores = null;
            var sp = @"sp_ObtenerAutores";
            try
            {
                var con = _fabricaConexion.ConectarSQLServer();
                Atores = await con.QueryAsync<AutorDTO>(sp, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar la lista de autores ", ex);
            }
            finally 
            {
                _fabricaConexion.CerrarConexion();
            }

            return Atores;
        }

        public async Task<AutorDTO> ObtenerAutorPorId(int AutorId)
        {
            AutorDTO Atores = null;
            var sp = @"sp_ObtenerAutoresById";
            try
            {
                var con = _fabricaConexion.ConectarSQLServer();
                Atores = await con.QueryFirstAsync<AutorDTO>(sp, new { AutorId = AutorId }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar la información del autor ", ex);
            }
            finally 
            {
                _fabricaConexion.CerrarConexion();
            }

            return Atores;
        }

        public async Task<int> RegistrarAutor(string NombreAutor, DateTime FechaNacimiento)
        {
            var sp = @"sp_GuardarAutores";
            try
            {
                var con = _fabricaConexion.ConectarSQLServer();
                var result = await con.ExecuteAsync(sp, 
                                          new {NombreAutor = NombreAutor, 
                                               FechaNacimiento = FechaNacimiento}, 
                                          commandType: CommandType.StoredProcedure);

                _fabricaConexion.CerrarConexion();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo guardar el autor ", ex);
            }
        }
    }
}
