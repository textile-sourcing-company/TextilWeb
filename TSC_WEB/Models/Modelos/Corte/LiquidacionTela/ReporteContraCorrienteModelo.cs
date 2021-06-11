using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using System.Data;

namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela
{
    public class ReporteContraCorrienteModelo
    {
        DBAccess conexion = new DBAccess();

        public DataTable getCabecera(int operacion,int ficha, int combo, int version,string tipotela) {

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_LIQUIDACION_RC_HEAD", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("i_operacion", operacion));
            comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
            comando.Parameters.Add(new OracleParameter("i_combo", combo));
            comando.Parameters.Add(new OracleParameter("i_version", version));
            comando.Parameters.Add(new OracleParameter("i_tipotela", tipotela));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            DataTable dt = new DataTable();
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();
            return dt;


        }
    }
}