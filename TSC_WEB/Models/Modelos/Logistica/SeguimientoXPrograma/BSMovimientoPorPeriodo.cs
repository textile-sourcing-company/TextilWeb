using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.MovimientoPorPeriodo;

namespace TSC_WEB.Models.Modelos.Logistica.SeguimientoXPrograma
{
    public class BSMovimientoPorPeriodo
    {
        DBAccess conexion = new DBAccess();

        public List<EBMovimientoPorPeriodo>ListarMovimientoPorPeriodo (DateTime? FechaIni
                                                                        , DateTime? FechaFin
                                                                        , string Lote
                                                                        , string CodTran
                                                                        , string CodAlm
                                                                        //, string Transaccion
                                                                        , string Nivel
                                                                        , string Grupo
                                                                        , string Subgrupo
                                                                        , string Item
                                                                        )
        {
            List<EBMovimientoPorPeriodo> Lista = new List<EBMovimientoPorPeriodo>(); // Creamos una lista
            EBMovimientoPorPeriodo ObjEntMovPorPeriodo = new EBMovimientoPorPeriodo(); // Creamos un objeto para capturar nuestras entidades
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.getsys_reportemov_perio_falsep", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {

                comando.Parameters.Add(new OracleParameter("p_fecha_inicio", FechaIni));
                comando.Parameters.Add(new OracleParameter("p_fecha_fin", FechaFin));
                comando.Parameters.Add(new OracleParameter("p_nivel", Nivel));
                comando.Parameters.Add(new OracleParameter("p_grupo", Grupo));
                comando.Parameters.Add(new OracleParameter("p_subgru", Subgrupo));
                comando.Parameters.Add(new OracleParameter("p_item", Item));
                comando.Parameters.Add(new OracleParameter("p_cod_transac", CodTran));
                comando.Parameters.Add(new OracleParameter("p_cod_almacen", CodAlm));
                comando.Parameters.Add(new OracleParameter("p_lote", Lote));
                comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                //DATA READER 
                OracleDataReader RegistrosMov = comando.ExecuteReader();
                while (RegistrosMov.Read())
                {
                    ObjEntMovPorPeriodo = new EBMovimientoPorPeriodo();
                    ObjEntMovPorPeriodo.codigo = RegistrosMov["codigo"].ToString();
                    ObjEntMovPorPeriodo.narrativa = RegistrosMov["narrativa"].ToString();
                    ObjEntMovPorPeriodo.entrada_salida = RegistrosMov["entrada_saida"].ToString();
                    ObjEntMovPorPeriodo.codigo_transacao = Convert.ToInt32(RegistrosMov["codigo_transacao"].ToString());
                    ObjEntMovPorPeriodo.transaccion = RegistrosMov["transaccion"].ToString();
                    ObjEntMovPorPeriodo.lote = int.Parse(RegistrosMov["numero_lote"].ToString());
                    ObjEntMovPorPeriodo.cantidad = decimal.Parse(RegistrosMov["cantidad"].ToString());
                    ObjEntMovPorPeriodo.numero_documento = RegistrosMov["numero_documento"].ToString();
                    ObjEntMovPorPeriodo.fechamovimiento = (string.IsNullOrEmpty((RegistrosMov["data_movimento"].ToString()).ToString()) ? null : (RegistrosMov["data_movimento"].ToString()).ToString());//(DateTime.Parse(RegistrosMov["data_movimento"].ToString())).ToString();
                    ObjEntMovPorPeriodo.cantidadinicial = decimal.Parse(RegistrosMov["saldo_inicial"].ToString());
                    ObjEntMovPorPeriodo.cantidad_entrada = decimal.Parse(RegistrosMov["CANTIDA_ENTRADA"].ToString());
                    ObjEntMovPorPeriodo.cantidad_salida = decimal.Parse(RegistrosMov["CANTIDA_SALIDA"].ToString());
                    ObjEntMovPorPeriodo.cantidad_final = decimal.Parse(RegistrosMov["Saldo_dia"].ToString());
                    ObjEntMovPorPeriodo.doc_movimiento = RegistrosMov["doc_movimento"].ToString();
                    ObjEntMovPorPeriodo.alm_descripcion = RegistrosMov["alm_descripcion"].ToString();
                    ObjEntMovPorPeriodo.codalmacen = int.Parse(RegistrosMov["codigo_deposito"].ToString());
                    ObjEntMovPorPeriodo.nivel = RegistrosMov["nivel_estrutura"].ToString();
                    ObjEntMovPorPeriodo.grupo = RegistrosMov["grupo_estrutura"].ToString();
                    ObjEntMovPorPeriodo.subgrupo = RegistrosMov["subgrupo_estrutura"].ToString();
                    ObjEntMovPorPeriodo.item = RegistrosMov["item_estrutura"].ToString();
                    ObjEntMovPorPeriodo.color = RegistrosMov["color"].ToString();
                    ObjEntMovPorPeriodo.talla = RegistrosMov["talla"].ToString();
                    ObjEntMovPorPeriodo.partida = RegistrosMov["partida"].ToString();
                    ObjEntMovPorPeriodo.Desc_Transaccion = RegistrosMov["TRANSACCIONES_DESC"].ToString();
                    ObjEntMovPorPeriodo.Fecha_Ins_Entrada = (string.IsNullOrEmpty((RegistrosMov["FECHA_INSER_ENTRADA"].ToString()).ToString()) ? null : (RegistrosMov["FECHA_INSER_ENTRADA"].ToString()).ToString());//DateTime.Parse(RegistrosMov["FECHA_INSER_ENTRADA"].ToString());
                    ObjEntMovPorPeriodo.Fecha_Ins_Salida = (string.IsNullOrEmpty((RegistrosMov["FECHA_INSER_SALIDA"].ToString()).ToString()) ? null : (RegistrosMov["FECHA_INSER_SALIDA"].ToString()).ToString());//DateTime.Parse(RegistrosMov["FECHA_INSER_SALIDA"].ToString());
                    ObjEntMovPorPeriodo.Hora_Ins_Entrada = RegistrosMov["HORA_INSER_ENTRADA"].ToString();
                    ObjEntMovPorPeriodo.Hora_Ins_salida = RegistrosMov["HORA_INSER_SALIDA"].ToString();
                    ObjEntMovPorPeriodo.usuario_ins = RegistrosMov["USUARIO_INS"].ToString();
                    ObjEntMovPorPeriodo.familia = RegistrosMov["FAMILIA"].ToString();
                    ObjEntMovPorPeriodo.Pedido = (string.IsNullOrEmpty(RegistrosMov["pedido_venda"].ToString()) ? 0 : Convert.ToInt32(int.Parse(RegistrosMov["pedido_venda"].ToString())));
                    ObjEntMovPorPeriodo.Corte_Destino = (string.IsNullOrEmpty(RegistrosMov["Corte_Destino"].ToString()) ? "" : (RegistrosMov["Corte_Destino"].ToString()).ToString());//RegistrosMov["Corte_Destino"].ToString();
                    ObjEntMovPorPeriodo.App_Ch_Destino = (string.IsNullOrEmpty(RegistrosMov["AppChn_Destino"].ToString()) ? "" : (RegistrosMov["AppChn_Destino"].ToString()).ToString());//RegistrosMov["AppChn_Destino"].ToString();
                    ObjEntMovPorPeriodo.TipoServicio = (string.IsNullOrEmpty(RegistrosMov["AppChn_Destino"].ToString()) ? "" : (RegistrosMov["AppChn_Destino"].ToString()).ToString());//RegistrosMov["DespachoDestino"].ToString();
                    ObjEntMovPorPeriodo.NombreTaller = (string.IsNullOrEmpty(RegistrosMov["TipoServicio"].ToString()) ? "" : (RegistrosMov["TipoServicio"].ToString()).ToString());//RegistrosMov["TipoServicio"].ToString();
                    Lista.Add(ObjEntMovPorPeriodo);
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EBObtenerAlmacen  ListarAlmacenes(int? CODALM)
        {
            //EBObtenerAlmacen ObjLista = new EBObtenerAlmacen(); // Creamos una lista
            EBObtenerAlmacen ObjEntListAlm = new EBObtenerAlmacen(); // Creamos un objeto para capturar nuestras entidades
            OracleCommand comando = new OracleCommand("UERP.SP_GENESYS_GETWAREHOUSE", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try 
            {
                comando.Parameters.Add(new OracleParameter("P_CODALM", CODALM));
                comando.Parameters.Add(new OracleParameter("P_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                //DATA READER 
                OracleDataReader RegistrosAlm = comando.ExecuteReader();
                while (RegistrosAlm.Read())
                {
                    ObjEntListAlm = new EBObtenerAlmacen();
                    ObjEntListAlm.Almacen = RegistrosAlm["DESCRIPCION"].ToString();

                }
                return ObjEntListAlm;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public EBObtenerTransaccion ListarTransacciones(int? CODTRAN)
        {
            //EBObtenerAlmacen ObjLista = new EBObtenerAlmacen(); // Creamos una lista
            EBObtenerTransaccion ObjListTran = new EBObtenerTransaccion(); // Creamos un objeto para capturar nuestras entidades
            OracleCommand comando = new OracleCommand("UERP.SP_GENESYS_GETTRANSACCION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("P_CODTRAN", CODTRAN));
                comando.Parameters.Add(new OracleParameter("P_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                //DATA READER 
                OracleDataReader RegistrosTran = comando.ExecuteReader();
                while (RegistrosTran.Read())
                {
                    ObjListTran = new EBObtenerTransaccion();
                    ObjListTran.Transaccion = RegistrosTran["TRANSACCION"].ToString();

                }
                return ObjListTran;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
      
}