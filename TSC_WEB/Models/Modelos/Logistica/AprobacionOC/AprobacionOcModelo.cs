using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Globalization;
using TSC_WEB.Models.Entidades.Sistema;
using TSC_WEB.Models.Entidades.Logistica.AprobacionOC;
using System.Threading.Tasks;
using Dapper.Oracle;
using Dapper;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace TSC_WEB.Models.Modelos.Logistica.AprobacionOC
{
    public class AprobacionOcModelo
    {

        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

        DBAccess conexion = new DBAccess();
        SistemaEntidad objSistemaE = new SistemaEntidad();
        OrdenCompraCabeceraEntidad objOrdenCompraE = new OrdenCompraCabeceraEntidad();
        List<OrdenCompraEntidad> objOrdenCompraeLista = new List<OrdenCompraEntidad>();
        List<OrdenCompraDetalleEntidad> objOrdenCompraDetalleL = new List<OrdenCompraDetalleEntidad>();

        //CAMBIA EL ESTADO DE TODOS LOS DETALLES DE LA OC
        public SistemaEntidad CambiarEstadoDetalle(int ordencompra, int empresa, string usuario)
        {


            OracleCommand comando = new OracleCommand("spu_validarliberacionoc", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("i_ordencompra", ordencompra));
                comando.Parameters.Add(new OracleParameter("i_empresa", empresa));
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader objDevolver = comando.ExecuteReader();

                while (objDevolver.Read())
                {
                    objSistemaE.mensajesistema = objDevolver["mensaje"].ToString();
                    objSistemaE.respuestabool = objDevolver["validador"].ToString() == "1" ? true : false;
                }

            }
            catch (Exception e)
            {
                objSistemaE.mensajesistema = e.Message;
                objSistemaE.respuestabool = false;
            }
            conexion.Desconectar();
            return objSistemaE;
        }

        //LISTA CABECERA DE OC
        public OrdenCompraCabeceraEntidad ListarCabecera(int ordencompra)
        {
            OracleCommand comando = new OracleCommand("spu_getinfocabeceraoc", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_oc", ordencompra));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objOrdenCompraE.codigo_empresa = Convert.ToInt32(registros["CODIGO_EMPRESA"].ToString());
                objOrdenCompraE.pedido_compra = Convert.ToInt32(registros["pedido_compra"].ToString());
                objOrdenCompraE.data_prev_entr = Convert.ToDateTime(registros["data_prev_entr"].ToString()).ToShortDateString();
                objOrdenCompraE.datetime_pedido = Convert.ToDateTime(registros["datetime_pedido"].ToString()).ToShortDateString();
                objOrdenCompraE.FORN_PED_FORNE9 = registros["FORN_PED_FORNE9"].ToString();
                objOrdenCompraE.FORN_PED_FORNE4 = registros["FORN_PED_FORNE4"].ToString();
                objOrdenCompraE.FORN_PED_FORNE2 = registros["FORN_PED_FORNE2"].ToString();
                objOrdenCompraE.COND_PGTO_COMPRA = registros["COND_PGTO_COMPRA"].ToString();
                objOrdenCompraE.DESCR_COND_PGTO = registros["DESCR_COND_PGTO"].ToString();
                objOrdenCompraE.COD_MOEDA = Convert.ToInt32(registros["COD_MOEDA"].ToString());
                objOrdenCompraE.DESCRICAO = registros["DESCRICAO"].ToString();
                objOrdenCompraE.SIMBOLO_MOEDA = registros["SIMBOLO_MOEDA"].ToString();

                objOrdenCompraE.TOTAL_PEDIDO = registros["TOTAL_PEDIDO"].ToString();
                objOrdenCompraE.SITUACAO_PEDIDO = registros["SITUACAO_PEDIDO"].ToString();
                objOrdenCompraE.NOME_FORNECEDOR = registros["NOME_FORNECEDOR"].ToString();
                objOrdenCompraE.CODIGO_COMPRADOR = registros["CODIGO_COMPRADOR"].ToString();
                objOrdenCompraE.NOME_COMPRADOR = registros["NOME_COMPRADOR"].ToString();
            }

            conexion.Desconectar();

            return objOrdenCompraE;

        }

        //LISTA DETALLE DE OC
        public List<OrdenCompraDetalleEntidad> ListarDetalle(int ordencompra)
        {
            OracleCommand comando = new OracleCommand("spu_getinfodetalleoc", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_oc", ordencompra));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objOrdenCompraDetalleL.Add(
                      new OrdenCompraDetalleEntidad
                      {
                          seq_item_pedido = registros["seq_item_pedido"].ToString(),
                          ITEM_100_NIVEL99 = registros["ITEM_100_NIVEL99"].ToString(),
                          item_100_grupo = registros["item_100_grupo"].ToString(),
                          item_100_subgrupo = registros["item_100_subgrupo"].ToString(),
                          item_100_item = registros["item_100_item"].ToString(),
                          descricao_item = registros["descricao_item"].ToString(),
                          centro_custo = registros["centro_custo"].ToString(),
                          unidade_conv = registros["unidade_conv"].ToString(),
                          reservaplane = registros["reservaplane"].ToString(),
                          QTDE_PEDIDA_ITEM = Convert.ToDecimal(registros["QTDE_PEDIDA_ITEM"].ToString()).ToString("N", nfi),// registros["QTDE_PEDIDA_ITEM"].ToString(),
                          VALOR_CONV = registros["VALOR_CONV"].ToString(),
                          //total = Convert.ToDecimal(registros["total"].ToString()).ToString("N0"),
                          total = Convert.ToDecimal(registros["total"].ToString()).ToString("N", nfi),
                          DESCRICAO = registros["DESCRICAO"].ToString(),
                      }
                );
            }

            conexion.Desconectar();

            return objOrdenCompraDetalleL;
        }

        //TRAE PERIODO ACTUAL
        public OrdenCompraCabeceraEntidad DetalleCabecera(int ordencompra)
        {
            OracleCommand comando = new OracleCommand("spu_getperiodoactual", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_ordencompra", ordencompra));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objOrdenCompraE.centrocosto = registros["centrocosto"].ToString();
                objOrdenCompraE.periodo = registros["periodo"].ToString();
                objOrdenCompraE.presupuesto = Convert.ToDecimal(registros["presupuesto"].ToString());
                objOrdenCompraE.valorusado = Convert.ToDecimal(registros["valorusado"].ToString());
                objOrdenCompraE.disponible = Convert.ToDecimal(registros["disponible"].ToString());
                objOrdenCompraE.aprobado = Convert.ToDecimal(registros["aprobado"].ToString());
            }

            conexion.Desconectar();

            return objOrdenCompraE;
        }

        //LIBERA OC
        public SistemaEntidad LiberarOc(int ordencompra, string usuario, int empresa)
        {
            OracleCommand comando = new OracleCommand("SPU_LIBERAROCGENERAL", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_ordencompra", ordencompra));
            comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
            comando.Parameters.Add(new OracleParameter("i_empresa", empresa));
            comando.Parameters.Add(new OracleParameter("nivel", 3));

            try
            {
                comando.ExecuteNonQuery();
                objSistemaE.respuestabool = true;
                objSistemaE.mensajesistema = "Orden de compra liberada";

            }
            catch (Exception e)
            {
                objSistemaE.respuestabool = false;
                objSistemaE.mensajesistema = e.Message;
            }

            conexion.Desconectar();
            return objSistemaE;
        }

       



        //LISTA TODAS LA OC QUE PUEDEN SER LIBERADAS
        public List<OrdenCompraEntidad> ListarOcLiberar(string usuario, int empresa)
        {
            OracleCommand comando = new OracleCommand("spu_getocliberar", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
            comando.Parameters.Add(new OracleParameter("i_empresa", empresa));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objOrdenCompraeLista.Add(
                    new OrdenCompraEntidad
                    {
                        pedido = Convert.ToInt32(registros["pedido"].ToString()),
                        fecha = Convert.ToDateTime(registros["fecha"].ToString()).ToShortDateString(),
                        descripcionpago = registros["descripcionpago"].ToString(),
                        moneda = registros["moneda"].ToString(),
                        //total_pedido        = Convert.ToDecimal(registros["total_pedido"].ToString()).ToString("N0"),
                        total_pedido = Convert.ToDecimal(registros["total_pedido"].ToString()).ToString("N", nfi),

                        proveedor = registros["proveedor"].ToString(),
                        comprador = registros["comprador"].ToString(),
                        niveles = Convert.ToInt32(registros["niveles"].ToString()),
                        centrocosto = registros["centrocosto"].ToString(),
                        accesocentrocosto = Convert.ToInt32(registros["accesocentrocosto"].ToString())
                    }
                );
            }

            conexion.Desconectar();

            return objOrdenCompraeLista;
        }

        //RECHAZA OC
        public SistemaEntidad RechazarOc(int ordencompra)
        {
            OracleCommand comando = new OracleCommand("systextilrpt.spu_rechazarocweb", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_oc", ordencompra));

            try
            {
                comando.ExecuteNonQuery();
                objSistemaE.respuestabool = true;
                objSistemaE.mensajesistema = "Orden de compra Rechazada";

            }
            catch (Exception e)
            {
                objSistemaE.respuestabool = false;
                objSistemaE.mensajesistema = e.Message;
            }

            conexion.Desconectar();
            return objSistemaE;
        }

        //RECHAZA OC
        public SistemaEntidad RechazarOcV2(int ordencompra, string usuario, string motivo)
        {
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_RECHAZAROCWEBV2", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OC", ordencompra));
            comando.Parameters.Add(new OracleParameter("CH_USUARIO", usuario));
            comando.Parameters.Add(new OracleParameter("CH_MOTIVO", motivo));

            try
            {
                comando.ExecuteNonQuery();
                objSistemaE.respuestabool = true;
                objSistemaE.mensajesistema = "Orden de compra Rechazada";

            }
            catch (Exception e)
            {
                objSistemaE.respuestabool = false;
                objSistemaE.mensajesistema = e.Message;
            }

            conexion.Desconectar();
            return objSistemaE;
        }


        public Restriccion TieneRestriccion(string usuario, int ordencompra)
        {
            Restriccion restriccion = new Restriccion();
            OracleCommand comando = new OracleCommand("USYSTEX.SPU_OC_RESTRICCION_CECO_MONTO", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("P_USUARIO", ordencompra));
            comando.Parameters.Add(new OracleParameter("P_PEDIDO_COMPRA", usuario));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            try
            {
                OracleDataReader reader = comando.ExecuteReader(CommandBehavior.Default);

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        restriccion = new Restriccion();

                        restriccion.TIENE_RESTRICCION = Convert.ToInt32(reader["TIENE_RESTRICCION"].ToString());
                        restriccion.RESPUESTA = Convert.ToInt32(reader["RESPUESTA"].ToString());

                    }
                }

                reader.Close();

            }
            catch (Exception e)
            {
                restriccion.TIENE_RESTRICCION = 0;
                restriccion.RESPUESTA = 0;
            }

            conexion.Desconectar();
            

            return restriccion;
        }


        //OBTIENE TODAS LAS OC PENDIENTES POR LIBERAR
        public List<OrdenCompraEntidad> ListarOcPendientesLiberar(int centrocosto, string firmante)
        {
            OracleCommand comando = new OracleCommand("spu_getobliberarfiltro", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            if (centrocosto == 0)
            {
                comando.Parameters.Add(new OracleParameter("i_centrocosto", null));
            }
            else
            {
                comando.Parameters.Add(new OracleParameter("i_centrocosto", centrocosto));
            }

            if (firmante.Trim() == string.Empty)
            {
                comando.Parameters.Add(new OracleParameter("i_firmante", null));
            }
            else
            {
                comando.Parameters.Add(new OracleParameter("i_firmante", firmante.Trim()));
            }

            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objOrdenCompraeLista.Add(
                    new OrdenCompraEntidad
                    {
                        pedido = Convert.ToInt32(registros["pedido"].ToString()),
                        fecha = Convert.ToDateTime(registros["fecha"].ToString()).ToShortDateString(),
                        descripcionpago = registros["descripcionpago"].ToString(),
                        moneda = registros["moneda"].ToString(),
                        //total_pedido        = Convert.ToDecimal(registros["total_pedido"].ToString()).ToString("N0"),
                        total_pedido = Convert.ToDecimal(registros["total_pedido"].ToString()).ToString("N", nfi),

                        proveedor = registros["proveedor"].ToString(),
                        comprador = registros["comprador"].ToString(),
                        niveles = Convert.ToInt32(registros["niveles"].ToString()),
                        centrocosto = registros["centrocosto"].ToString(),
                        accesocentrocosto = Convert.ToInt32(registros["accesocentrocosto"].ToString()),
                        firmantes = registros["firmantes"].ToString(),

                    }
                );
            }

            conexion.Desconectar();

            return objOrdenCompraeLista;
        }


        public List<Lista1_PendAprobacion> ListarOcLiberarV2(string usuario, int empresa)
        {
            try
            {
                List<Lista1_PendAprobacion> lista = new List<Lista1_PendAprobacion>();
                OracleCommand comando = new OracleCommand("USYSTEX.spu_getocliberar2", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("I_USUARIO", usuario));
                comando.Parameters.Add(new OracleParameter("I_EMPRESA", empresa));
                comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    lista.Add(
                        new Lista1_PendAprobacion
                        {
                            PEDIDO = Convert.ToInt32(registros["PEDIDO"].ToString()),
                            CECO = registros["CECO"].ToString(),
                            FECHA = Convert.ToDateTime(registros["FECHA"].ToString()).ToShortDateString(),
                            PROVEEDOR = registros["PROVEEDOR"].ToString(),
                            COMPRADOR = registros["COMPRADOR"].ToString(),
                            DESCRIPCIONPAGO = registros["DESCRIPCIONPAGO"].ToString(),
                            SIMBOLO_MOEDA = registros["SIMBOLO_MOEDA"].ToString(),
                            TOTAL_PEDIDO = Convert.ToDecimal(registros["TOTAL_PEDIDO"].ToString()).ToString("N", nfi)
                        }
                    );
                }

                conexion.Desconectar();

                return lista;

            }
            catch (Exception EX)
            {
                throw;
            }
        }


        public async Task<List<Lista2_AprobTercFirma>> ListaAprobadas(string usuario, int empresa, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("I_OPCION", 2, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("I_USUARIO", usuario, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_EMPRESA", empresa, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaIni.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("I_PEDIDOCOMPRA", null, OracleMappingType.Int32, ParameterDirection.Input);

                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Lista2_AprobTercFirma>(sql: "USYSTEX.SPU_GETOCLIBERARV2",
                                                                                    param: parametros,
                                                                                    commandType: CommandType.StoredProcedure);
                    return resultado.OrderBy(x => x.PEDIDO).ToList();
                }
            }
            catch (Exception EX)
            {
                throw;
            }
        }

        public async Task<List<Lista3_RechTercFirma>> ListaRechazados(string usuario, int empresa, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("I_OPCION", 3, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("I_USUARIO", usuario, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_EMPRESA", empresa, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaIni.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("I_PEDIDOCOMPRA", null, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Lista3_RechTercFirma>(sql: "USYSTEX.SPU_GETOCLIBERARV2",
                                                                                param: parametros,
                                                                                commandType: CommandType.StoredProcedure);
                    return resultado.OrderBy(x => x.PEDIDO).ToList();
                }
            }
            catch (Exception EX)
            {
                throw;
            }
        }


        public List<Lista4_ListExcelAprob> ListaExcelAprobados(string usuario,
                                                                            int empresa,
                                                                            DateTime fechaIni,
                                                                            DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("I_OPCION", 4, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("I_USUARIO", usuario, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_EMPRESA", empresa, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaIni.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("I_PEDIDOCOMPRA", null, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = conexion.Query<Lista4_ListExcelAprob>(sql: "USYSTEX.SPU_GETOCLIBERARV2",
                                                                                    param: parametros,
                                                                                    commandType: CommandType.StoredProcedure);
                    return resultado.OrderByDescending(x => x.PEDIDO).ToList();
                }
            }
            catch (Exception EX)
            {
                throw;
            }
        }

        public List<Lista5_ListaExcelRech> ListaExcelRechazados(string usuario,
                                                                           int empresa,
                                                                           DateTime fechaIni,
                                                                           DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("I_OPCION", 5, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("I_USUARIO", usuario, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_EMPRESA", empresa, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaIni.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("I_PEDIDOCOMPRA", null, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = conexion.Query<Lista5_ListaExcelRech>(sql: "USYSTEX.SPU_GETOCLIBERARV2",
                                                                                    param: parametros,
                                                                                    commandType: CommandType.StoredProcedure);
                    return resultado.OrderByDescending(x => x.PEDIDO).ToList();
                }
            }
            catch (Exception EX)
            {
                throw;
            }
        }


        public async Task<List<Lista6_DetalleOC>> ListaDetOCTerFirma(int pedido)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("I_OPCION", 6, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("I_USUARIO", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_EMPRESA", null, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("I_PEDIDOCOMPRA", pedido, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var lista = await conexion.QueryAsync<Lista6_DetalleOC>(sql: "USYSTEX.SPU_GETOCLIBERARV2",
                                                                                    param: parametros,
                                                                                    commandType: CommandType.StoredProcedure);

                    return lista.ToList();
                }
            }
            catch (Exception EX)
            {
                throw;
            }
        }


        public MemoryStream ReporteExcelAprob(List<Lista4_ListExcelAprob> listado)
        {
            MemoryStream stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Hoja1");

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.Font.Size = 8;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(1).Style.Fill.BackgroundColor.SetColor(Color.White);
                workSheet.Cells[1, 1, 1, 17].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells["A1"].Value = "PEDIDO";
                workSheet.Cells["B1"].Value = "FECHA";
                workSheet.Cells["D1"].Value = "COD. CODIGO";
                workSheet.Cells["C1"].Value = "CECO";
                workSheet.Cells["E1"].Value = "PROVEEDOR";
                workSheet.Cells["F1"].Value = "COMPRADOR";
                workSheet.Cells["G1"].Value = "DESCRIPCION PAGO";
                workSheet.Cells["H1"].Value = "PRODUCTO";
                workSheet.Cells["I1"].Value = "COD. CUENTA";
                workSheet.Cells["J1"].Value = "CUENTA";
                workSheet.Cells["K1"].Value = "ESTADO";
                workSheet.Cells["L1"].Value = "MONEDA";
                workSheet.Cells["M1"].Value = "CANTIDAD";
                workSheet.Cells["N1"].Value = "UM";
                workSheet.Cells["O1"].Value = "TOTAL PEDIDO";
                workSheet.Cells["P1"].Value = "USUARIO APROB.";
                workSheet.Cells["Q1"].Value = "OBSERVACION";

                workSheet.Column(1).Width = 10;     // PEDIDO";
                workSheet.Column(2).Width = 10;     // FECHA";
                workSheet.Column(3).Width = 20;     // COD. CODIGO";
                workSheet.Column(4).Width = 30;     // CECO";
                workSheet.Column(5).Width = 25;     // PROVEEDOR";
                workSheet.Column(6).Width = 30;     // COMPRADOR";
                workSheet.Column(7).Width = 25;     // DESCRIPCION PAGO";
                workSheet.Column(8).Width = 30;     // PRODUCTO";
                workSheet.Column(9).Width = 10;     // COD. CUENTA";
                workSheet.Column(10).Width = 25;    // CUENTA";
                workSheet.Column(11).Width = 15;    // ESTADO";
                workSheet.Column(12).Width = 15;    // MONEDA"; 
                workSheet.Column(13).Width = 15;    // CANTIDAD";
                workSheet.Column(14).Width = 15;    // UM";
                workSheet.Column(15).Width = 20;    // TOTAL PEDIDO";
                workSheet.Column(16).Width = 20;    // USUARIO APROB.";
                workSheet.Column(17).Width = 35;    // USUARIO APROB.";

                int i = 2;

                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.PEDIDO;
                    workSheet.Cells[i, 2].Value = item.FECHA;
                    workSheet.Cells[i, 3].Value = item.COD_CC;
                    workSheet.Cells[i, 4].Value = item.CENTRO_COSTO;
                    workSheet.Cells[i, 5].Value = item.PROVEEDOR;
                    workSheet.Cells[i, 6].Value = item.COMPRADOR;
                    workSheet.Cells[i, 7].Value = item.DESCRIPCIONPAGO;
                    workSheet.Cells[i, 8].Value = item.PRODUCTO;
                    workSheet.Cells[i, 9].Value = item.CODIGO_CUENTA;
                    workSheet.Cells[i, 10].Value = item.CUENTA;
                    workSheet.Cells[i, 11].Value = item.ESTADO_ITEM;
                    workSheet.Cells[i, 12].Value = item.MONEDA;
                    workSheet.Cells[i, 13].Value = item.CANTIDAD;
                    workSheet.Cells[i, 14].Value = item.UNIDADE_MEDIDA;
                    workSheet.Cells[i, 15].Value = item.TOTAL_PEDIDO;
                    workSheet.Cells[i, 16].Value = item.CH_USUARIO_APROB;
                    workSheet.Cells[i, 17].Value = item.OBSERVACION;

                    i++;
                }

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }


        public MemoryStream ReporteExcelRech(List<Lista5_ListaExcelRech> listado)
        {
            MemoryStream stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Hoja1");

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.Font.Size = 8;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(1).Style.Fill.BackgroundColor.SetColor(Color.White);

                workSheet.Cells["A1"].Value = "PEDIDO";
                workSheet.Cells["B1"].Value = "FECHA";
                workSheet.Cells["D1"].Value = "COD. CODIGO";
                workSheet.Cells["C1"].Value = "CECO";
                workSheet.Cells["E1"].Value = "PROVEEDOR";
                workSheet.Cells["F1"].Value = "COMPRADOR";
                workSheet.Cells["G1"].Value = "DESCRIPCION PAGO";
                workSheet.Cells["H1"].Value = "PRODUCTO";
                workSheet.Cells["I1"].Value = "COD. CUENTA";
                workSheet.Cells["J1"].Value = "CUENTA";
                workSheet.Cells["K1"].Value = "ESTADO";
                workSheet.Cells["L1"].Value = "MONEDA";
                workSheet.Cells["M1"].Value = "CANTIDAD";
                workSheet.Cells["N1"].Value = "UM";
                workSheet.Cells["O1"].Value = "TOTAL PEDIDO";
                workSheet.Cells["P1"].Value = "USUARIO APROB.";

                workSheet.Column(1).Width = 10;     // PEDIDO";
                workSheet.Column(2).Width = 10;     // FECHA";
                workSheet.Column(3).Width = 20;     // COD. CODIGO";
                workSheet.Column(4).Width = 10;     // CECO";
                workSheet.Column(5).Width = 25;     // PROVEEDOR";
                workSheet.Column(6).Width = 30;     // COMPRADOR";
                workSheet.Column(7).Width = 25;     // DESCRIPCION PAGO";
                workSheet.Column(8).Width = 30;     // PRODUCTO";
                workSheet.Column(9).Width = 10;     // COD. CUENTA";
                workSheet.Column(10).Width = 25;     // CUENTA";
                workSheet.Column(11).Width = 15;     // ESTADO";
                workSheet.Column(12).Width = 15;     // MONEDA"; 
                workSheet.Column(13).Width = 15;    // CANTIDAD";
                workSheet.Column(14).Width = 15;    // UM";
                workSheet.Column(15).Width = 20;    // TOTAL PEDIDO";
                workSheet.Column(16).Width = 20;    // USUARIO APROB.";


                int i = 2;

                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.PEDIDO;
                    workSheet.Cells[i, 2].Value = item.FECHA;
                    workSheet.Cells[i, 3].Value = item.COD_CC;
                    workSheet.Cells[i, 4].Value = item.CENTRO_COSTO;
                    workSheet.Cells[i, 5].Value = item.PROVEEDOR;
                    workSheet.Cells[i, 6].Value = item.COMPRADOR;
                    workSheet.Cells[i, 7].Value = item.DESCRIPCIONPAGO;
                    workSheet.Cells[i, 8].Value = item.PRODUCTO;
                    workSheet.Cells[i, 9].Value = item.CODIGO_CUENTA;
                    workSheet.Cells[i, 10].Value = item.CUENTA;
                    workSheet.Cells[i, 11].Value = item.ESTADO_ITEM;
                    workSheet.Cells[i, 12].Value = item.MONEDA;
                    workSheet.Cells[i, 13].Value = item.CANTIDAD;
                    workSheet.Cells[i, 14].Value = item.UNIDADE_MEDIDA;
                    workSheet.Cells[i, 15].Value = item.TOTAL_PEDIDO;
                    workSheet.Cells[i, 16].Value = item.CH_USUARIO_APROB;

                    i++;
                }

                package.Save();
            }

            stream.Position = 0;
            return stream;

        }

    }
}