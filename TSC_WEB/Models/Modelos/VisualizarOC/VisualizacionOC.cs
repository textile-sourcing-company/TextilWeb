using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Logistica.VisualizacionOC;

namespace TSC_WEB.Models.Modelos.Logistica.VisualizacionOC
{

    public class VisualizacionOC
    {
        
        DBAccess conexion = new DBAccess();
        public List<VisualizacionOcDet> ListarReporteOC_1(int I_OrdenCompra,decimal? PorceTela)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            List<VisualizacionOcDet> objListar1 = new List<VisualizacionOcDet>();
            decimal presupuesto_total = 0;
            decimal consumido_total = 0;

            OracleCommand comando = new OracleCommand("SPU_VISUALIZACION_OC", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OC", I_OrdenCompra));
            comando.Parameters.Add(new OracleParameter("I_PORCETELA", PorceTela));
            comando.Parameters.Add(new OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar1.Add(
                            new VisualizacionOcDet
                            {
                          
                                I_Item = Convert.ToDecimal(registros["I_Item"].ToString()),
                                CH_MEDIDA = registros["CH_MEDIDA"].ToString(),
                                CH_CODIGO_TSC = registros["CH_CODIGO_TSC"].ToString(),
                                CH_DESCRIPCION = registros["CH_DESCRIPCION"].ToString(),
                                CH_CODIGO_PROVEEDOR = registros["CH_CODIGO_PROVEEDOR"].ToString(),
                                
                                I_OC_REFERENCIA_SEQ = registros["I_OC_REFERENCIA_SEQ"].ToString(),
                                I_CANT  = Convert.ToDecimal(registros["I_CANT"].ToString()),
                                I_MAXIMO = Convert.ToDecimal(registros["I_MAXIMO"].ToString()),
                                I_P_UNIT = Convert.ToDecimal(registros["I_P_UNIT"].ToString()),
                                I_SUB_TOTAL = Convert.ToDecimal(registros["I_SUB_TOTAL"].ToString()),
                           


                            }
                        );


            }
            conexion.Desconectar();
            return objListar1;
        }
        ////CABEZERA
        public VisualizacionOcCab ListarReporteOC_Cabezera(int I_OrdenCompra,string Usuario)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            VisualizacionOcCab objListar1 = new VisualizacionOcCab();
            decimal presupuesto_total = 0;
            decimal consumido_total = 0;

            OracleCommand comando = new OracleCommand("SPU_VISUALIZACION_OC_CAB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OC", I_OrdenCompra));
            comando.Parameters.Add(new OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {

                objListar1 = new VisualizacionOcCab
                {

                    I_ORDEM_COMPRA = Convert.ToInt32(registros["I_ORDEM_COMPRA"].ToString()),
                    CH_PROVEEDOR = registros["CH_PROVEEDOR"].ToString(),
                    CH_RUC = registros["CH_RUC"].ToString(),
                    CH_DIRECCION = registros["CH_DIRECCION"].ToString(),
                    CH_CONTACTO = registros["CH_CONTACTO"].ToString(),
                    CH_MONEDA = registros["CH_MONEDA"].ToString(),
                    CH_CONDICION_PAGO  = registros["CH_CONDICION_PAGO"].ToString(),
                    DT_FECHA_EMISION    = (registros["DT_FECHA_EMISION"].ToString()),
                    DT_FECHA_ENTREGA_PREV = (registros["DT_FECHA_ENTREGA_PREV"].ToString()),
                    CH_SUB_TOTAL = registros["CH_SUB_TOTAL"].ToString(),
                    CH_IGV = registros["CH_IGV"].ToString(),
                    CH_TOTAL = registros["CH_TOTAL"].ToString(),
                    I_OC_REFERENCIA = registros["I_OC_REFERENCIA"].ToString(),
                    CH_FIRMANTE_01_USU = registros["CH_FIRMANTE_01_USU"].ToString(),
                    CH_FIRMANTE_02_USU = registros["CH_FIRMANTE_02_USU"].ToString(),
                    CH_FIRMANTE_03_USU = registros["CH_FIRMANTE_03_USU"].ToString(),
                    CH_MONTO_LETRAS = registros["CH_MONTO_LETRAS"].ToString(),
                    CH_REQUERIMIENTOS = registros["CH_REQUERIMIENTOS"].ToString(),
                    CH_CC_COSTO = registros["CH_CC_COSTO"].ToString(),
                    CH_OSERVACION = registros["CH_OSERVACION"].ToString(),
                    CH_RUTA_BORRADOR = @"/desarrolloproducto/getfile/?ruta=" + registros["CH_RUTA_BORRADOR"].ToString(),
                    CH_FIRMANTE_01 = @"/desarrolloproducto/getfile/?ruta=" + registros["CH_FIRMANTE_01"].ToString(),
                    CH_FIRMANTE_02 = @"/desarrolloproducto/getfile/?ruta=" + registros["CH_FIRMANTE_02"].ToString(),
                    CH_FIRMANTE_03 = @"/desarrolloproducto/getfile/?ruta=" + registros["CH_FIRMANTE_03"].ToString(),
                    CH_USUARIO_SECCION = Usuario,
                  

                };
                


            }
            conexion.Desconectar();
            return objListar1;
        }
    }
}