using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using TextilSoftware.Entidad.Logistica.SeguimientoXPrograma;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Logistica;

namespace TSC_WEB.Models.Modelos.Logistica
{
    public class SegAvioProgModelo
    {
        //INSTANCIA CONEXION
        DBAccess conexion = new DBAccess();
        public List<EBSegOCMes> ListarSeguiProgAvio(
                                                                    string P_OC,
                                                                    string P_PROVEEDOR,
                                                                    string P_COMPRADOR,
                                                                    string P_PROGRAMA,
                                                                    string P_FAMILIA,
                                                                    string P_NIVEL,
                                                                    string P_GRUPO,
                                                                    string P_SUBGRUPO,
                                                                    string P_ITEM,
                                                                    string P_MES_INI,
                                                                    string P_MES_FIN,
                                                                    string P_ANIO_INI,
                                                                    string P_ANIO_FIN,
                                                                    string P_CCO
                                                                )
        {

            List<EBSegOCMes> Lista = new List<EBSegOCMes>();      //Creamos una lista
            EBSegOCMes objMov = new EBSegOCMes();
            //OracleCommand comando = new OracleCommand("USYSTEX.SPU_GET_LOGI_SIG_X_MES_V2", conexion.Acceder());
            OracleCommand comando = new OracleCommand("USYSTEX.GENESYS_UPD_OC_X_MES_V2", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {

                string _mesini = P_MES_INI == "" || P_MES_INI == null ? "" : P_MES_INI.ToString().Substring(5, 2);// P_MES_INI.ToString().Substring(4,2) ;
                string _mesfin = P_MES_FIN == "" || P_MES_FIN == null ? "" : P_MES_FIN.ToString().Substring(5, 2);
                string _anoini = P_ANIO_INI == "" || P_ANIO_INI == null ? "" : P_ANIO_INI.ToString().Substring(0, 4);
                string _anofin = P_ANIO_FIN == "" || P_ANIO_FIN == null ? "" : P_ANIO_FIN.ToString().Substring(0, 4);
                comando.Parameters.Add(new OracleParameter("P_OC", P_OC));
                comando.Parameters.Add(new OracleParameter("P_PROVEEDOR", P_PROVEEDOR));
                comando.Parameters.Add(new OracleParameter("P_COMPRADOR", P_COMPRADOR));
                comando.Parameters.Add(new OracleParameter("P_PROGRAMA", P_PROGRAMA));
                comando.Parameters.Add(new OracleParameter("P_FAMILIA", P_FAMILIA));

                comando.Parameters.Add(new OracleParameter("P_NIVEL", P_NIVEL));
                comando.Parameters.Add(new OracleParameter("P_GRUPO", P_GRUPO));
                comando.Parameters.Add(new OracleParameter("P_SUBGRUPO", P_SUBGRUPO));
                comando.Parameters.Add(new OracleParameter("P_ITEM", P_ITEM));
                comando.Parameters.Add(new OracleParameter("P_MES_INI", _mesini));
                comando.Parameters.Add(new OracleParameter("P_MES_FIN", _mesfin));
                comando.Parameters.Add(new OracleParameter("P_ANIO_INI", _anoini));
                comando.Parameters.Add(new OracleParameter("P_ANIO_FIN", _anofin));
                comando.Parameters.Add(new OracleParameter("P_CCO", P_CCO));

                comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                //DATA READER 
                OracleDataReader registros = comando.ExecuteReader();
                while (registros.Read())
                {
                    objMov = new EBSegOCMes();
                    objMov.CLIENTE = registros["CLIENTE"].ToString();
                    objMov.CNT_OC = Convert.ToDecimal(registros["CNT_OC"].ToString());
                    objMov.COD_PROD = registros["COD_PROD"].ToString();
                    objMov.COMPRADOR = registros["COMPRADOR"].ToString();
                    objMov.DESCRIPCIONPAGO = registros["DESCRIPCIONPAGO"].ToString();
                    objMov.DSC_PROD = registros["DSC_PROD"].ToString();
                    objMov.FAMILIA = registros["FAMILIA"].ToString();
                    objMov.FECHA_COMP_OC = Convert.ToDateTime(registros["FECHA_COMP_OC"].ToString()).ToString("dd/MM/yyyy");
                    objMov.FECHA_EMI_OC = Convert.ToDateTime(registros["FECHA_EMI_OC"].ToString()).ToString("dd/MM/yyyy");
                    objMov.MES = registros["MES"].ToString();
                    objMov.OC = Convert.ToInt32(registros["OC"].ToString());
                    objMov.PREC_UNIT = Convert.ToDecimal(registros["PREC_UNIT"].ToString());
                    objMov.PROGRAMA = registros["PROGRAMA"].ToString();
                    objMov.PROVEEDOR = registros["PROVEEDOR"].ToString();
                    objMov.TOTAL = Convert.ToDecimal(registros["TOTAL"].ToString());
                    objMov.COD_CENTROCOSTO = Convert.ToInt32(registros["COD_CENTROCOSTO"].ToString());
                    objMov.DESC_CENTROCOSTO = registros["DESC_CENTROCOSTO"].ToString();
                    Lista.Add(objMov);
                }
                //.OrderBy(o=>o.OrderDate).ToList()
                return Lista.OrderBy(o => o.MES).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<SegAvioProgEntidad> Listar_SegAvioProg(string PPROG, string PPEDIDO, string PPO)
        {
            List<SegAvioProgEntidad> Listobjsegavioprg = new List<SegAvioProgEntidad>();
            SegAvioProgEntidad objsegavioprg_Ent = new SegAvioProgEntidad();
            OracleCommand comando = new OracleCommand("USYSTEX.GET_LOGI_SEGUIMIENTOPROGRAMA01", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("P_PROGRAMA", PPROG.ToUpper()));
                comando.Parameters.Add(new OracleParameter("P_PEDIDO", PPEDIDO));
                comando.Parameters.Add(new OracleParameter("P_PO", PPO));
                comando.Parameters.Add(new OracleParameter("P_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                //DATA READER 
                OracleDataReader registros = comando.ExecuteReader();

                if (registros.HasRows)
                {

                    while (registros.Read())
                    {
                        
                        objsegavioprg_Ent = new SegAvioProgEntidad();
                        objsegavioprg_Ent.Cliente = registros["CLIENTE"].ToString();
                        objsegavioprg_Ent.Fecha_creacion_pedido = registros["FECHA_CREACIÓN_PEDIDO"].ToString();
                        objsegavioprg_Ent.Fecha_Exfactory = registros["FECHA_EXFACTORY"].ToString();
                        objsegavioprg_Ent.Programa = registros["PROGRAMA"].ToString();
                        objsegavioprg_Ent.Estilo_cliente = registros["ESTILO_CLIENTE"].ToString();
                        objsegavioprg_Ent.Pedido = registros["PEDIDO"].ToString();
                        objsegavioprg_Ent.Fecha_emision_req = registros["FECHA_EMISIÓN_REQUERIMIENTO"].ToString();
                        objsegavioprg_Ent.Requerimiento = registros["REQUERIMIENTO"].ToString();
                        objsegavioprg_Ent.Po = registros["PO"].ToString();
                        objsegavioprg_Ent.Fecha_asig_avio = (string.IsNullOrEmpty(Convert.ToDateTime(registros["FECHA_ASIGNACIÓN_AVÍO"].ToString()).ToString("dd/MM/yyyy")) ? null : (Convert.ToDateTime(registros["FECHA_ASIGNACIÓN_AVÍO"].ToString()).ToString("dd/MM/yyyy")).ToString());

                        objsegavioprg_Ent.Codigo_avio = registros["CÓDIGO_DE_AVÍO"].ToString();
                        objsegavioprg_Ent.Descripcion_avio = registros["DESCRIPCIÓN_AVÍO"].ToString();
                        objsegavioprg_Ent.Orden_compra = registros["ORDEN_DE_COMPRA"].ToString();
                        objsegavioprg_Ent.Fecha_Emision_Oc = (string.IsNullOrEmpty(Convert.ToDateTime(registros["FECHA_EMISIÓN_OC"].ToString()).ToString("dd/MM/yyyy")) ? null : (Convert.ToDateTime(registros["FECHA_EMISIÓN_OC"].ToString()).ToString("dd/MM/yyyy")).ToString());
                        objsegavioprg_Ent.Fecha_compromiso_Oc = (string.IsNullOrEmpty(Convert.ToDateTime(registros["FECHA_COMPROMISO_OC"].ToString()).ToString("dd/MM/yyyy")) ? null : (Convert.ToDateTime(registros["FECHA_COMPROMISO_OC"].ToString()).ToString("dd/MM/yyyy")).ToString());

                        objsegavioprg_Ent.Cantidad_req_programa = registros["CANTIDAD_REQUERIDA_PROGRAMA"].ToString();
                        objsegavioprg_Ent.Cantidad_requerimiento = registros["CANTIDAD_REQUERIDA"].ToString();
                        objsegavioprg_Ent.Cantidad_Oc = registros["CANTIDAD_OC"].ToString();
                        objsegavioprg_Ent.Cantidad_Recibida = registros["CANTIDAD_RECIBIDA"].ToString();
                        objsegavioprg_Ent.Fecha_Ingreso_alm = string.IsNullOrEmpty(registros["FECHA_INGRESO_ALMACÉN"].ToString()) ? null : (Convert.ToDateTime(registros["FECHA_INGRESO_ALMACÉN"].ToString()).ToString("dd/MM/yyyy")).ToString();
                        objsegavioprg_Ent.Periodo_booking = registros["PERIDO_BOOKING"].ToString();
                        objsegavioprg_Ent.Orden_Planeamiento = registros["ORDEN_PLANEMIENTO"].ToString();
                        Listobjsegavioprg.Add(objsegavioprg_Ent);

                    }

                }

            }
            catch
            {
            }
            finally
            {
                conexion.Desconectar();
            }
            return Listobjsegavioprg;
        }
        public List<EBListarCentroCosto> ListarCentroCosto()
        {
            var Conexion = new DBAccess();
            List<EBListarCentroCosto> ListarCCO = new List<EBListarCentroCosto>();
            EBListarCentroCosto objListarCCosto = new EBListarCentroCosto();
            OracleCommand comando = new OracleCommand("USYSTEX.SP_GENESYS_GETCCO", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("P_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                //DATA READER 
                OracleDataReader registros = comando.ExecuteReader();
                while (registros.Read())
                {
                    objListarCCosto = new EBListarCentroCosto();
                    objListarCCosto.centrocosto = registros["DES_CCO"].ToString();
                    objListarCCosto.codcentrocosto = Convert.ToInt32(registros["CODCCO"].ToString());
                    ListarCCO.Add(objListarCCosto);
                }

            }
            catch
            {
            }
            finally
            {
                conexion.Desconectar();
            }
            return ListarCCO;
        }
    }
}