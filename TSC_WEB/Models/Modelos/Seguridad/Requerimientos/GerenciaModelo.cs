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
    public class GerenciaModelo
    {
        DBAccess conexion = new DBAccess();
        public List<GerenciaEntidad> ListarGerencias()
        {
            List<GerenciaEntidad> objLista = new List<GerenciaEntidad>();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GET_GERENCIAS", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("PCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objLista.Add(
                        new GerenciaEntidad
                        {
                            COD_GER = registros["COD_GER"].ToString(),
                            DESC_GER = registros["DESC_GER"].ToString()
                        }
                );
            }

            conexion.Desconectar();
            return objLista;
        }




        //LISTAMOS RESPOSNSABLES <<ACCESO MEDIANTE DAPPER>>
        public async Task<List<GerenciaEntidad>> ListarGerencias2()
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add(name: ":PCURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<GerenciaEntidad>(sql: "SYSTEXTILRPT.GET_GERENCIAS",
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