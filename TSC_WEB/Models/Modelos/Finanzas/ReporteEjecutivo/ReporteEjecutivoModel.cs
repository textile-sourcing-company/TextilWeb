using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Finanzas.ReporteEjecutivo;

namespace TSC_WEB.Models.Modelos.Finanzas.ReporteEjecutivo
{
    public class ReporteEjecutivoModel
    {
        public async Task<IEnumerable<REE_TipoPeriodo>> Get_TipoPeriodo()
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

                    var resultado = await conexion.QueryAsync<REE_TipoPeriodo>("uspGetReporteEjecutivo",
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

        public async Task<IEnumerable<REE_Periodo>> Get_Periodos(int tipoPeriodo)
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 2, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", tipoPeriodo, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", 0, DbType.Int32, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<REE_Periodo>("uspGetReporteEjecutivo",
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

        public async Task<IEnumerable<REE_FlujoEfectivoLista2>> ListarFlujoEfectivo2(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 3, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", TipoPeriodo, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", IdPeriodoIni, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", IdPeriodoFin, DbType.Int32, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<REE_FlujoEfectivoLista2>("uspGetReporteEjecutivo",
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

        public async Task<IEnumerable<REE_FlujoEfectivoLista3>> ListarFlujoEfectivo3(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 4, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", TipoPeriodo, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", IdPeriodoIni, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", IdPeriodoFin, DbType.Int32, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<REE_FlujoEfectivoLista3>("uspGetReporteEjecutivo",
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

    }
}