using Dapper;
using Dapper.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Logistica.Graficos;

namespace TSC_WEB.Models.Modelos.Logistica.Graficos
{
    public class GraficoModelo
    {

        public async Task<List<Periodos>> ListarPeriodos()
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("IP_OPCION", 1, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("IPV_PERIODO", null, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("IPV_GERENCIA", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_CODCC", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_FAMILIA", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Periodos>(sql: "USYSTEX.SPU_GET_LOGI_GRAFICOS",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderBy(x => x.COD_PERIODO).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<Gerencias>> ListarGerencias()
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("IP_OPCION", 2, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("IPV_PERIODO", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_GERENCIA", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_CODCC", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_FAMILIA", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Gerencias>(sql: "USYSTEX.SPU_GET_LOGI_GRAFICOS",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderByDescending(x => x.COD_GER).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Ceco>> ListarCeCo(string codGerencias)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("IP_OPCION", 3, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("IPV_PERIODO", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_GERENCIA", codGerencias, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_CODCC", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_FAMILIA", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Ceco>(sql: "USYSTEX.SPU_GET_LOGI_GRAFICOS",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderByDescending(x => x.CODCC).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<Familias>> ListarFamilias(string codPeriodos, string codGerencias, string codCeCo)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("IP_OPCION", 4, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("IPV_PERIODO", codPeriodos, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_GERENCIA", (codGerencias.Trim() == "" ? null : codGerencias), OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_CODCC", (codCeCo.Trim() == "" ? null : codCeCo), OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_FAMILIA", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Familias>(sql: "USYSTEX.SPU_GET_LOGI_GRAFICOS",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderByDescending(x => x.CODFAMILIA).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<Grafico1Lineas>> Grafico1Linea(string codPeriodos, string codGerencias, string codCeCo, string codFamilia)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("IP_OPCION", 5, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("IPV_PERIODO", codPeriodos, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_GERENCIA", codGerencias, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_CODCC", codCeCo, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_FAMILIA", codFamilia, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Grafico1Lineas>(sql: "USYSTEX.SPU_GET_LOGI_GRAFICOS",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderBy(x => x.CODP).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<Grafico2TortaPresupuesto>> Grafico2TortaPresupuesto(string codPeriodos, string codGerencias, string codCeCo)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("IP_OPCION", 6, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("IPV_PERIODO", codPeriodos, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_GERENCIA", codGerencias, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_CODCC", codCeCo, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_FAMILIA", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Grafico2TortaPresupuesto>(sql: "USYSTEX.SPU_GET_LOGI_GRAFICOS",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderBy(x => x.CODFAMILIA).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Grafico2TortaConsumido>> Grafico2TortaConsumido(string codPeriodos, string codGerencias, string codCeCo)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("IP_OPCION", 7, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("IPV_PERIODO", codPeriodos, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_GERENCIA", codGerencias, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_CODCC", codCeCo, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_FAMILIA", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Grafico2TortaConsumido>(sql: "USYSTEX.SPU_GET_LOGI_GRAFICOS",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderBy(x => x.CODFAMILIA).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<Grafico3Bar>> Grafico3Bar(string codPeriodos, string codFamilia)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("IP_OPCION", 8, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("IPV_PERIODO", codPeriodos, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_GERENCIA", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_CODCC", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("IPV_FAMILIA", codFamilia, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Grafico3Bar>(sql: "USYSTEX.SPU_GET_LOGI_GRAFICOS",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderBy(x => x.GERENCIA).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}