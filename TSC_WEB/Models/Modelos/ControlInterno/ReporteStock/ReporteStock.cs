using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.MovimientoPorPeriodo;

namespace TSC_WEB.Models.Modelos.ControlInterno.ReporteStock
{
    public class ReporteStock
    {
        DBAccess conexion = new DBAccess();

      

        //REPORTE DE STOCK
        public List<EBMovimientoPorPeriodo> ConsultaReporteStock(string codalmacen)
        {
            List<EBMovimientoPorPeriodo> lista = new List<EBMovimientoPorPeriodo>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("USYSTEX.SPU_REPORTE_STOCK", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("I_DEPOSITO", codalmacen));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader(CommandBehavior.SingleResult);

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    lista.Add(new EBMovimientoPorPeriodo
                    {
                    CH_LINEA = registros["CH_LINEA"].ToString(),
                    CH_SUB_LINEA = registros["CH_SUB_LINEA"].ToString(),
                    I_STOCK = (string.IsNullOrEmpty(registros["I_STOCK"].ToString()) ? 0 : Convert.ToInt32(int.Parse(registros["I_STOCK"].ToString()))),
                    I_PRECIO = (string.IsNullOrEmpty(registros["I_PRECIO"].ToString()) ? 0 : Convert.ToDecimal(decimal.Parse(registros["I_PRECIO"].ToString()))),
                    CH_GENERO = registros["CH_GENERO"].ToString(),
                    CH_TALLA = registros["CH_TALLA"].ToString(),
                    CH_COLOR = registros["CH_COLOR"].ToString(),
                    CH_DESCRPICION = registros["CH_DESCRI_ALMACEN"].ToString(),
                    CH_CH_COD_PRODUCTO = registros["CH_COD_PRODUCTO"].ToString(),
                    });

                }
            }
            conexion.Desconectar();
            return lista;
        }




    }
}