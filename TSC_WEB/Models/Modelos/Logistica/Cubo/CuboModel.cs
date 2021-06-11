
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Logistica.Cubo;

namespace TSC_WEB.Models.Modelos.Logistica.Cubo
{
    public class CuboModel
    {
        DBAccess conexion = new DBAccess();
        public List<EBCuboInfoActualizado> ListarCuboInformacion(DateTime? FechaIni
                                                                , DateTime? FechaFin
                                                                , string Proveedor
                                                                , string Comprador
                                                                , string Almacen
                                                                , string OC
                                                                )
        {
            List<EBCuboInfoActualizado> Lista = new List<EBCuboInfoActualizado>(); // Creamos una lista
            EBCuboInfoActualizado ObjCuboInformacion = new EBCuboInfoActualizado(); // Creamos un objeto para capturar nuestras entidades
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GETSYS_CUBOGUIAS_OC", conexion.Acceder());
            //OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GENESYS_GETSYS_CUBOGUIAS_OC", conexion.Acceder());
            //OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GENESYS_GETSYS_CUBOGUIAS_OC_1", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("P_FEC_OC_INICIO", (DateTime?)FechaIni));
                comando.Parameters.Add(new OracleParameter("P_FEC_OC_FIN", (DateTime?)FechaFin));
                comando.Parameters.Add(new OracleParameter("P_PROVEEDOR", (Proveedor == string.Empty) ? "%" : Proveedor));
                comando.Parameters.Add(new OracleParameter("P_COMPRADOR", (Comprador == string.Empty) ? "%" : Comprador));
                comando.Parameters.Add(new OracleParameter("P_ALMACEN", (Almacen == string.Empty) ? "%" : Almacen));
                comando.Parameters.Add(new OracleParameter("P_NUM_OC", (OC != string.Empty) ? OC : string.Empty));
                comando.Parameters.Add(new OracleParameter("PCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                //DATA READER 
                OracleDataReader RegistrosCuboInformacion = comando.ExecuteReader();
                while (RegistrosCuboInformacion.Read())
                {
                    ObjCuboInformacion = new EBCuboInfoActualizado();
                    ObjCuboInformacion.num_requisicion = RegistrosCuboInformacion["NUM_REQUISICION"].ToString();
                    ObjCuboInformacion.cant_req = RegistrosCuboInformacion["CANTIDAD_REQUERIDA"] != DBNull.Value ? Convert.ToDecimal(RegistrosCuboInformacion["CANTIDAD_REQUERIDA"]) : (decimal?)null;//decimal.Parse(RegistrosCuboInformacion["CANTIDAD_REQUERIDA"].ToString());
                    ObjCuboInformacion.pedido_compra = RegistrosCuboInformacion["PEDIDO_DE_COMPRA"].ToString();
                    ObjCuboInformacion.Fch_Reg_OC = (string.IsNullOrEmpty(RegistrosCuboInformacion["FECHA_REGISTRO_OC"].ToString()) ? "" : (Convert.ToDateTime(RegistrosCuboInformacion["FECHA_REGISTRO_OC"].ToString()).ToString("dd/MM/yyyy")));
                    ObjCuboInformacion.Fch_Ent_Prevista_OC = (string.IsNullOrEmpty(RegistrosCuboInformacion["FEC_ENTREGA_PREVIS_OC"].ToString()) ? "" : (Convert.ToDateTime(RegistrosCuboInformacion["FEC_ENTREGA_PREVIS_OC"].ToString()).ToString("dd/MM/yyyy"))); //RegistrosCuboInformacion["FEC_ENTREGA_PREVIS_OC"].ToString();
                    ObjCuboInformacion.Cod_Proveedor = RegistrosCuboInformacion["CODIGO_PROVEEDOR"].ToString();
                    ObjCuboInformacion.Des_Proveedor = RegistrosCuboInformacion["DESCRIPCION_PROVEEDOR"].ToString();
                    ObjCuboInformacion.Cod_Local_Entrega = RegistrosCuboInformacion["CODIGO_LOCAL_ENTREGA"].ToString();
                    ObjCuboInformacion.Des_Local_Entrega = (string.IsNullOrEmpty((RegistrosCuboInformacion["DESC_LOCAL_ENTREGA"].ToString()).ToString()) ? null : (RegistrosCuboInformacion["DESC_LOCAL_ENTREGA"].ToString()).ToString());//(DateTime.Parse(RegistrosMov["data_movimento"].ToString())).ToString();
                    ObjCuboInformacion.Cod_Local_Cobranza = RegistrosCuboInformacion["CODIGO_LOCAL_COBRANZA"].ToString();
                    ObjCuboInformacion.Des_Local_Cobranza = RegistrosCuboInformacion["CODIGO_LOCAL_COBRANZA"].ToString();
                    ObjCuboInformacion.Cod_Condicion_pago = RegistrosCuboInformacion["COD_CONDICION_PAGO"].ToString();
                    ObjCuboInformacion.Des_Condicion_pago = RegistrosCuboInformacion["DESC_CONDICION_PAGO"].ToString();
                    ObjCuboInformacion.Cod_Comprador = RegistrosCuboInformacion["CODIGO_COMPRADOR"].ToString();
                    ObjCuboInformacion.Des_Comprador = RegistrosCuboInformacion["DESCRIPCION_COMPRADOR"].ToString();
                    ObjCuboInformacion.Contacto = RegistrosCuboInformacion["CONTACTO"].ToString();
                    ObjCuboInformacion.Cod_Moneda = RegistrosCuboInformacion["CODIGO_MONEDA"].ToString();
                    ObjCuboInformacion.Des_Moneda = RegistrosCuboInformacion["DESC_MONEDA"].ToString();
                    ObjCuboInformacion.Observacion_OC = RegistrosCuboInformacion["OBSERVACION_OC"].ToString();
                    ObjCuboInformacion.Secuencia_OC = RegistrosCuboInformacion["SECUENCIA_OC"].ToString();
                    ObjCuboInformacion.Cod_producto2 = RegistrosCuboInformacion["CODIGO_PRODUCTO2"].ToString();
                    ObjCuboInformacion.Des_producto = RegistrosCuboInformacion["DESCRIPCION_PRODUCTO"].ToString();
                    ObjCuboInformacion.Unidad_Consumo = RegistrosCuboInformacion["UNIDAD_CONSUMO"].ToString();
                    ObjCuboInformacion.Unidad_Compra = RegistrosCuboInformacion["UNIDAD_COMPRA"].ToString();
                    ObjCuboInformacion.Cant_Soli_UnidadConsumo = RegistrosCuboInformacion["CANT_SOLICI_UNIDCONSUMO"] != DBNull.Value ? Convert.ToDecimal(RegistrosCuboInformacion["CANT_SOLICI_UNIDCONSUMO"]) : (decimal?)null; //decimal.Parse(RegistrosCuboInformacion["CANT_SOLICI_UNIDCONSUMO"].ToString());
                    ObjCuboInformacion.Cant_Soli_UnidadCompra = RegistrosCuboInformacion["CANT_SOLICI_UNIDCOMPRA"] != DBNull.Value ? Convert.ToDecimal(RegistrosCuboInformacion["CANT_SOLICI_UNIDCOMPRA"]) : (decimal?)null; //decimal.Parse(RegistrosCuboInformacion["CANT_SOLICI_UNIDCOMPRA"].ToString());
                    ObjCuboInformacion.Valor_Unita_UnidadConsumo = RegistrosCuboInformacion["VALOR_UNITA_UNIDCONSUMO"] != DBNull.Value ? Convert.ToDecimal(RegistrosCuboInformacion["VALOR_UNITA_UNIDCONSUMO"]) : (decimal?)null; //decimal.Parse(RegistrosCuboInformacion["VALOR_UNITA_UNIDCONSUMO"].ToString());
                    ObjCuboInformacion.Valor_Total_UnidadConsumo = RegistrosCuboInformacion["VALOR_TOTAL_UNIDCONSUMO"] != DBNull.Value ? Convert.ToDecimal(RegistrosCuboInformacion["VALOR_TOTAL_UNIDCONSUMO"]) : (decimal?)null; //decimal.Parse(RegistrosCuboInformacion["VALOR_TOTAL_UNIDCONSUMO"].ToString());
                    ObjCuboInformacion.Porc_Descuento = RegistrosCuboInformacion["PORC_DESCUENTO"] != DBNull.Value ? Convert.ToDecimal(RegistrosCuboInformacion["PORC_DESCUENTO"]) : (decimal?)null; //decimal.Parse(RegistrosCuboInformacion["PORC_DESCUENTO"].ToString());
                    ObjCuboInformacion.Porc_Impuesto = RegistrosCuboInformacion["PORC_IMPUESTO"] != DBNull.Value ? Convert.ToDecimal(RegistrosCuboInformacion["PORC_IMPUESTO"]) : (decimal?)null; //decimal.Parse(RegistrosCuboInformacion["PORC_IMPUESTO"].ToString());
                    ObjCuboInformacion.Cod_Almacen = RegistrosCuboInformacion["CODIGO_ALMACEN"].ToString();
                    ObjCuboInformacion.Des_Almacen = (string.IsNullOrEmpty(RegistrosCuboInformacion["DES_ALMACEN"].ToString()) ? "" : (RegistrosCuboInformacion["DES_ALMACEN"].ToString()).ToString());//RegistrosMov["Corte_Destino"].ToString();
                    ObjCuboInformacion.Cod_CCO = (string.IsNullOrEmpty(RegistrosCuboInformacion["CODIGO_CCOSTO"].ToString()) ? "" : (RegistrosCuboInformacion["CODIGO_CCOSTO"].ToString()).ToString());//RegistrosMov["AppChn_Destino"].ToString();
                    ObjCuboInformacion.Des_CCO = (string.IsNullOrEmpty(RegistrosCuboInformacion["DESC_CCOSTO"].ToString()) ? "" : (RegistrosCuboInformacion["DESC_CCOSTO"].ToString()).ToString());//RegistrosMov["DespachoDestino"].ToString();
                    ObjCuboInformacion.Orden_Planeamiento = (string.IsNullOrEmpty(RegistrosCuboInformacion["ORDEN_PLANEMAIENTO"].ToString()) ? "" : (RegistrosCuboInformacion["ORDEN_PLANEMAIENTO"].ToString()).ToString());//RegistrosMov["TipoServicio"].ToString();
                    ObjCuboInformacion.Grupo_Planeamiento = (string.IsNullOrEmpty(RegistrosCuboInformacion["GRUPO_PLANEAMIENTO"].ToString()) ? "" : (RegistrosCuboInformacion["GRUPO_PLANEAMIENTO"].ToString()).ToString());
                    ObjCuboInformacion.Fch_Prevista_Entrega_OC = (string.IsNullOrEmpty(RegistrosCuboInformacion["FEC_PREVIS_ENTREGA_OC"].ToString()) ? "" : (Convert.ToDateTime(RegistrosCuboInformacion["FEC_PREVIS_ENTREGA_OC"].ToString()).ToString("dd/MM/yyyy"))); //RegistrosCuboInformacion["FEC_PREVIS_ENTREGA_OC"] != DBNull.Value ? Convert.ToDateTime(RegistrosCuboInformacion["FEC_PREVIS_ENTREGA_OC"]) : (DateTime?)null;//(string.IsNullOrEmpty(RegistrosCuboInformacion["FEC_PREVIS_ENTREGA_OC"].ToString()) ? "" : (RegistrosCuboInformacion["FEC_PREVIS_ENTREGA_OC"].ToString()).ToString());
                    ObjCuboInformacion.Fch_Proveedor = (string.IsNullOrEmpty(RegistrosCuboInformacion["FEC_PROVEEDOR"].ToString()) ? "" : (Convert.ToDateTime(RegistrosCuboInformacion["FEC_PROVEEDOR"].ToString()).ToString("dd/MM/yyyy"))); //(string.IsNullOrEmpty(RegistrosCuboInformacion["FEC_PROVEEDOR"].ToString()) ? "" : (RegistrosCuboInformacion["FEC_PROVEEDOR"].ToString()).ToString());
                    ObjCuboInformacion.Estado_Item_OC = (string.IsNullOrEmpty(RegistrosCuboInformacion["ESTADO_ITEM_OC"].ToString()) ? "" : (RegistrosCuboInformacion["ESTADO_ITEM_OC"].ToString()).ToString());
                    ObjCuboInformacion.Usuario_Reg_OC = (string.IsNullOrEmpty(RegistrosCuboInformacion["USUARIO_REGIS_ORDCOMPRA"].ToString()) ? "" : (RegistrosCuboInformacion["USUARIO_REGIS_ORDCOMPRA"].ToString()).ToString());
                    ObjCuboInformacion.Fch_Aprobada_Firmante_OC1 = (string.IsNullOrEmpty(RegistrosCuboInformacion["FEC_APROBA_FIRMANTE_OC1"].ToString()) ? "" : (Convert.ToDateTime(RegistrosCuboInformacion["FEC_APROBA_FIRMANTE_OC1"].ToString()).ToString("dd/MM/yyyy"))); //(string.IsNullOrEmpty(RegistrosCuboInformacion["FEC_APROBA_FIRMANTE_OC1"].ToString()) ? "" : (RegistrosCuboInformacion["FEC_APROBA_FIRMANTE_OC1"].ToString()).ToString());
                    ObjCuboInformacion.Login_Firmante_OC1 = (string.IsNullOrEmpty(RegistrosCuboInformacion["LOGIN_FIRMANTE_OC1"].ToString()) ? "" : (RegistrosCuboInformacion["LOGIN_FIRMANTE_OC1"].ToString()).ToString());
                    ObjCuboInformacion.Fch_Aprobada_Firmante_OC2 = (string.IsNullOrEmpty(RegistrosCuboInformacion["FEC_APROBA_FIRMANTE_OC2"].ToString()) ? "" : (Convert.ToDateTime(RegistrosCuboInformacion["FEC_APROBA_FIRMANTE_OC2"].ToString()).ToString("dd/MM/yyyy"))); //(string.IsNullOrEmpty(RegistrosCuboInformacion["FEC_APROBA_FIRMANTE_OC2"].ToString()) ? "" : (RegistrosCuboInformacion["FEC_APROBA_FIRMANTE_OC2"].ToString()).ToString());
                    ObjCuboInformacion.Login_Firmante_OC2 = (string.IsNullOrEmpty(RegistrosCuboInformacion["LOGIN_FIRMANTE_OC2"].ToString()) ? "" : (RegistrosCuboInformacion["LOGIN_FIRMANTE_OC2"].ToString()).ToString());
                    ObjCuboInformacion.Fch_Aprobada_Firmante_OC3 = (string.IsNullOrEmpty(RegistrosCuboInformacion["FEC_APROBA_FIRMANTE_OC3"].ToString()) ? "" : (RegistrosCuboInformacion["FEC_APROBA_FIRMANTE_OC3"].ToString()).ToString());
                    ObjCuboInformacion.Login_Firmante_OC3 = (string.IsNullOrEmpty(RegistrosCuboInformacion["LOGIN_FIRMANTE_OC3"].ToString()) ? "" : (RegistrosCuboInformacion["LOGIN_FIRMANTE_OC3"].ToString()).ToString());
                    ObjCuboInformacion.Cant_Ing_Almacen = RegistrosCuboInformacion["CNT_ING_ALMACEN"] != DBNull.Value ? Convert.ToDecimal(RegistrosCuboInformacion["CNT_ING_ALMACEN"]) : (decimal?)null; //decimal.Parse(RegistrosCuboInformacion["CNT_ING_ALMACEN"].ToString());
                    ObjCuboInformacion.Fch_Ing_Almacen = (string.IsNullOrEmpty(RegistrosCuboInformacion["FCH_ING_ALMACEN"].ToString()) ? "" : (Convert.ToDateTime(RegistrosCuboInformacion["FCH_ING_ALMACEN"].ToString()).ToString("dd/MM/yyyy"))); //(string.IsNullOrEmpty(RegistrosCuboInformacion["FCH_ING_ALMACEN"].ToString()) ? "" : (RegistrosCuboInformacion["FCH_ING_ALMACEN"].ToString()).ToString());
                    ObjCuboInformacion.Fch_Ing_Ultima_Almacen = (string.IsNullOrEmpty(RegistrosCuboInformacion["FCH_ING_ULTI_ALMACEN"].ToString()) ? "" : (Convert.ToDateTime(RegistrosCuboInformacion["FCH_ING_ULTI_ALMACEN"].ToString()).ToString("dd/MM/yyyy"))); //(string.IsNullOrEmpty(RegistrosCuboInformacion["FCH_ING_ULTI_ALMACEN"].ToString()) ? "" : (RegistrosCuboInformacion["FCH_ING_ULTI_ALMACEN"].ToString()).ToString());
                    ObjCuboInformacion.Veces_Ing_Almacen = RegistrosCuboInformacion["VECES_ING_ALMACEN"] != DBNull.Value ? Convert.ToInt32(RegistrosCuboInformacion["VECES_ING_ALMACEN"]) : (int?)null;//int.Parse(RegistrosCuboInformacion["VECES_ING_ALMACEN"].ToString()); //(string.IsNullOrEmpty(RegistrosCuboInformacion["VECES_ING_ALMACEN"].ToString()) ? "" : (RegistrosCuboInformacion["VECES_ING_ALMACEN"].ToString()).ToString());
                    ObjCuboInformacion.Cant_Devuelta = RegistrosCuboInformacion["CANTIDAD_DEVUELTA"] != DBNull.Value ? Convert.ToDecimal(RegistrosCuboInformacion["CANTIDAD_DEVUELTA"]) : (decimal?)null; //decimal.Parse(RegistrosCuboInformacion["CANTIDAD_DEVUELTA"].ToString());
                    ObjCuboInformacion.Cant_Total = RegistrosCuboInformacion["CANTIDAD_TOTAL"] != DBNull.Value ? Convert.ToDecimal(RegistrosCuboInformacion["CANTIDAD_TOTAL"]) : (decimal?)null;//decimal.Parse(RegistrosCuboInformacion["CANTIDAD_TOTAL"].ToString());
                    Lista.Add(ObjCuboInformacion);
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}