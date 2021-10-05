using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Finanzas;
using TSC_WEB.Models.Entidades.Finanzas.RendicionGastos;

namespace TSC_WEB.Models.Modelos.Finanzas.RendicionGastos
{
    public class RendicionModelValidacion
    {


        public StatusResponse ValidarEstadoRendicion(SPS_ParametroValid parametro)
        {
            using (var conexion = DBAccess.ObtenerConexionSQL())
            {
                var sp_parametros = new DynamicParameters();

                sp_parametros.Add("@opcion", parametro.opcion, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idSolicitud", parametro.idSolicitud, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@codigo", parametro.codigo, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@secuencia", parametro.secuencia, DbType.Int32, ParameterDirection.Input);
                sp_parametros.Add("@idRendCompte", parametro.idRendCompte, DbType.Int32, ParameterDirection.Input);

                sp_parametros.Add("@nserie", parametro.nserie, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@ndocof", parametro.ndocof, DbType.String, ParameterDirection.Input);

                sp_parametros.Add("@rucDNI", parametro.rucDNI, DbType.String, ParameterDirection.Input);
                sp_parametros.Add("@idTipoComp", parametro.idTipoComp, DbType.Int32, ParameterDirection.Input);

                return conexion.QuerySingle<StatusResponse>("dbo.uspSetRendicionValidacion",
                                                            sp_parametros,
                                                            commandType: CommandType.StoredProcedure);
            }
        }








    }
}