using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Finanzas.RegistrarProyectado;

namespace TSC_WEB.Models.Modelos.Finanzas.RegistrarProyectado
{
    public class RegistrarProyModel
    {
        public async Task<IEnumerable<ERP_Ubicacion>> GetUbicaciones()
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 1, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipoAct", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@CodEFx", "", DbType.String, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<ERP_Ubicacion>("uspGetProyectado",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<ERP_TipoPeriodo>> GetTiposPeriodos()
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 2, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipoAct", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@CodEFx", "", DbType.String, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<ERP_TipoPeriodo>("uspGetProyectado",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<ERP_Periodos>> GetPeriodos(int tipoPeriodo)
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 3, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", tipoPeriodo, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipoAct", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@CodEFx", "", DbType.String, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<ERP_Periodos>("uspGetProyectado",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<ERP_EF>> GetEF(int IdTipoAct)
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 4, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipoAct", IdTipoAct, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@CodEFx", "", DbType.String, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<ERP_EF>("uspGetProyectado",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<ERP_Concepto>> GetConceptos(string CodEFx)
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 5, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipoAct", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@CodEFx", CodEFx, DbType.String, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<ERP_Concepto>("uspGetProyectado",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ERP_ListarRegistrados>> GetListarRegistrados()
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 6, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipoAct", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@CodEFx", "", DbType.String, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<ERP_ListarRegistrados>("uspGetProyectado",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ERP_RespuestaOp InsertarProyectado(int idRSx, int idTipoPeriodo, int idPeriodo, decimal monto, string usuario)
        {
            ERP_RespuestaOp respuesta = new ERP_RespuestaOp();
            try
            {
                using (SqlConnection conexion = DBAccess.ObtenerConexionSQL())
                {
                    using (SqlCommand comando = conexion.CreateCommand())
                    {
                        conexion.Open();
                        comando.CommandText = @"uspSetProyectado";
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.Parameters.Add("@IdRSx", SqlDbType.Int).Value = idRSx;
                        comando.Parameters.Add("@idTipoPeriodo", SqlDbType.Int).Value = idTipoPeriodo;
                        comando.Parameters.Add("@idPeriodo", SqlDbType.Int).Value = idPeriodo;
                        comando.Parameters.Add("@monto", SqlDbType.Decimal).Value = monto;
                        comando.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;

                        SqlDataReader reader = comando.ExecuteReader(CommandBehavior.SingleResult);

                        if (reader.HasRows)
                        {
                            respuesta = new ERP_RespuestaOp();


                            if (reader.Read())
                            {
                                respuesta.IdEstado = Convert.ToInt32(reader["IdEstado"]);
                                respuesta.DescEstado = Convert.ToString(reader["DescEstado"]);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return respuesta;
        }





    }
}