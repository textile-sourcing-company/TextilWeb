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
    public class EstadosReqModelo
    {
        DBAccess conexion = new DBAccess();
        //LISTAMOS RESPOSNSABLES <<ACCESO MEDIANTE DAPPER>>
        public async Task<List<EstadosReqEntidad>> ListarEstadosReq()
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add(name: ":PCURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<EstadosReqEntidad>(sql: "SYSTEXTILRPT.GET_ESTADOS_REQ",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);
                    return resultado.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}