using Dapper;
using Dapper.Oracle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Seguridad.Requerimientos;

namespace TSC_WEB.Models.Modelos.Seguridad.Requerimientos
{
    public class RequerimientosModelo
    {
        //INSTANCIA CONEXION
        DBAccess conexion = new DBAccess();


        public List<RequerimientosEntidad> ListarReporteRequerimientos(string tipo_fecha, string fechaInicio, string fechaFin,
                                                                        string gerencia,
                                                                    string solicitante, string num_req, string responsable, string area, string estado)
        {

            try
            {

                List<RequerimientosEntidad> RequerimientoLista = new List<RequerimientosEntidad>();
                OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GET_REQUERIMIENTOS_PROG", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("P_TIPO_FECHA", tipo_fecha));
                comando.Parameters.Add(new OracleParameter("P_FECHA_INI", fechaInicio));
                comando.Parameters.Add(new OracleParameter("P_FECHA_FIN", fechaFin));
                comando.Parameters.Add(new OracleParameter("P_GERENCIA", gerencia));
                comando.Parameters.Add(new OracleParameter("P_SOLICITANTE", solicitante));
                comando.Parameters.Add(new OracleParameter("P_NUM_REQ", num_req));
                comando.Parameters.Add(new OracleParameter("P_RESPONSABLES", responsable));
                comando.Parameters.Add(new OracleParameter("P_AREAS", area));
                comando.Parameters.Add(new OracleParameter("P_ESTADOS", estado));
                comando.Parameters.Add(new OracleParameter("PCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                OracleDataReader registros = comando.ExecuteReader();
                if (registros.HasRows)
                {
                    while (registros.Read())
                    {
                        RequerimientosEntidad objRequerimientoE = new RequerimientosEntidad();
                        objRequerimientoE.IDREQUERIMIENTO = registros["IDREQUERIMIENTO"].ToString();
                        objRequerimientoE.REQUERIMIENTO = registros["REQUERIMIENTO"].ToString();
                        objRequerimientoE.FECHA_REQ = registros["FECHA_REQ"].ToString();
                        objRequerimientoE.SOLICITANTE = registros["SOLICITANTE"].ToString();
                        objRequerimientoE.NOMBRE_AREA = registros["NOMBRE_AREA"].ToString();
                        objRequerimientoE.DESC_GER = registros["DESC_GER"].ToString();
                        objRequerimientoE.RESPONSABLE = registros["RESPONSABLE"].ToString();
                        objRequerimientoE.FECHA_INI_REQ = registros["FECHA_INI_REQ"].ToString();
                        objRequerimientoE.FECHA_FIN_REQ = registros["FECHA_FIN_REQ"].ToString();
                        objRequerimientoE.FECHA_INICIO_REAL = registros["FECHA_INICIO_REAL"].ToString();
                        objRequerimientoE.OBSERVACIONES = registros["OBSERVACIONES"].ToString();
                        objRequerimientoE.ESTADO_REQ = registros["ESTADO_REQ"].ToString();
                        RequerimientoLista.Add(objRequerimientoE);
                    }
                }

                conexion.Desconectar();
                return RequerimientoLista;
            }
            catch (Exception  ex)
            {

                throw;
            }

          
        }




        public async Task<List<RequerimientosEntidad>> ListarReporteRequerimientos2(string tipo_fecha, string fechaInicio, string fechaFin, string gerencia,
                                                            string solicitante, string num_req, string responsable, string area, string estado)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_TIPO_FECHA", tipo_fecha, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_FECHA_INI", fechaInicio, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_FECHA_FIN", fechaFin, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_GERENCIA", gerencia, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_SOLICITANTE", solicitante, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_NUM_REQ", num_req, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_RESPONSABLES", responsable, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_AREAS", area, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_ESTADOS", estado, OracleMappingType.Varchar2, ParameterDirection.Input);

                    parametros.Add(name: ":PCURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<RequerimientosEntidad>(sql: "SYSTEXTILRPT.GET_REQUERIMIENTOS_PROG",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}