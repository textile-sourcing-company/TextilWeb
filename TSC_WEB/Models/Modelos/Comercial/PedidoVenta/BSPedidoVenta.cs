using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Util.Comercial;

namespace TSC_WEB.Models.Modelos.Comercial.PedidoVenta
{
    public class BSPedidoVenta
    {
        DBAccess conexion = new DBAccess();

        public bool Actipedido(string PPEDIDOS, string PUSUARIO)
        {
            EBBaseMulti objMulti = new EBBaseMulti();
            OracleCommand comando = new OracleCommand("USYSTEX.PACK_COME_PEDIDOS.SPSET_COME_ACTIVARPEDIDO", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("PPEDIDOS", PPEDIDOS));
                comando.Parameters.Add(new OracleParameter("PUSUARIO", PUSUARIO));
                comando.Parameters.Add(new OracleParameter("POUT", OracleDbType.Int16)).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                var rpt = Convert.ToInt16(comando.Parameters["POUT"].Value.ToString());
                objMulti.errorbool = rpt == 0 ? true : false;
            }
            catch
            {
                objMulti.errorbool = false;
            }
            finally
            {
                conexion.Desconectar();
            }
            return objMulti.errorbool;
        }

        public bool Anularpedido(string PPEDIDOS, string PUSUARIO)
        {
            EBBaseMulti objMulti = new EBBaseMulti();
            OracleCommand comando = new OracleCommand("USYSTEX.PACK_COME_PEDIDOS.SPSET_COME_ANULARPEDIDO", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("PPEDIDOS", PPEDIDOS));
                comando.Parameters.Add(new OracleParameter("PUSUARIO", PUSUARIO));
                comando.Parameters.Add(new OracleParameter("POUT", OracleDbType.Int16)).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                var rpt = Convert.ToInt16(comando.Parameters["POUT"].Value.ToString());
                objMulti.errorbool = rpt == 0 ? true : false;
            }
            catch
            {
                objMulti.errorbool = false;
            }
            finally
            {
                conexion.Desconectar();
            }
            return objMulti.errorbool;
        }
    }
}