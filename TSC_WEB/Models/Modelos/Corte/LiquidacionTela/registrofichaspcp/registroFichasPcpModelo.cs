using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela.registrofichaspcp
{
    public class registroFichasPcpModelo
    {
        DBAccess conexion = new DBAccess();

        // GET SET FICHAS PROGRAMADAS DE PCP
        public DataTable GSFichasProgramadasPCP(int? ficha, string fecha,string usuario,string opcion,out string error)
        {
            DataTable dt = new DataTable();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_GETFICHASPROPCP", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            try
            {
                comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
                comando.Parameters.Add(new OracleParameter("i_fecha", fecha));
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
                comando.Parameters.Add(new OracleParameter("i_opcion", opcion));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                dt.Load(comando.ExecuteReader());
                error = string.Empty;

            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                //response = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return dt;


        }
    }
}