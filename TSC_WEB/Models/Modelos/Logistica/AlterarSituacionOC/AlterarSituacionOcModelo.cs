using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Logistica.AlterarSituacion;
using System.Data;
using TSC_WEB.Models.Entidades.Sistema;
using Dapper.Oracle;
using Dapper;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace TSC_WEB.Models.Modelos.Logistica.AlterarSituacionOC
{
    public class AlterarSituacionOcModelo
    {
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

        DBAccess conexion = null;

        public AlterarSituacionOcModelo()
        {
            conexion = new DBAccess();
        }

        //LISTA TODAS LA OC QUE PUEDEN PARA SER ALTERADAS DE SITUACION
        public List<ListaAlterarSituacionCab> ListarOcAlterarSituacion(string usuario)
        {
            List<ListaAlterarSituacionCab> lista = new List<ListaAlterarSituacionCab>();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "1"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", usuario));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", null));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", null));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", null));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_INI", null));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_INI", null));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                lista.Add(
                    new ListaAlterarSituacionCab
                    {
                        COD_PERIODO = Convert.ToInt32(registros["COD_PERIODO"].ToString()),
                        COD_GERENCIA = Convert.ToInt32(registros["COD_GERENCIA"].ToString()),
                        CODAUTORIZA = registros["CODAUTORIZA"].ToString(),
                        DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi),
                        PEDIDO = Convert.ToInt32(registros["PEDIDO"].ToString()),
                        FECHA = Convert.ToDateTime(registros["FECHA"].ToString()).ToShortDateString(),
                        DESCRIPCIONPAGO = registros["DESCRIPCIONPAGO"].ToString(),
                        MONEDA = registros["MONEDA"].ToString(),
                        TOTAL_PEDIDO = Convert.ToDecimal(registros["TOTAL_PEDIDO"].ToString()).ToString("N", nfi),
                        PROVEEDOR = registros["PROVEEDOR"].ToString(),
                        COMPRADOR = registros["COMPRADOR"].ToString(),
                        CENTROCOSTO = registros["CENTROCOSTO"].ToString(),
                        SIMBOLO_MOEDA = registros["SIMBOLO_MOEDA"].ToString(),
                        vDisponible = Convert.ToDecimal(registros["DISPONIBLE"]),
                        vTotalPedido = Convert.ToDecimal(registros["TOTAL_PEDIDO_V"])
                    }
                );
            }
            conexion.Desconectar();
            return lista.OrderByDescending(x => x.PEDIDO).ToList();
        }


        //LISTA DETALLE OC - ALTERAR SITUACION.
        public List<ListaAlterarSituacionDet> ListarDetalleOCAlter(int ordencompra)
        {
            List<ListaAlterarSituacionDet> lista = new List<ListaAlterarSituacionDet>();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "2"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", ""));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", ordencompra));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", 1));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", 1));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_INI", null));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_INI", null));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                lista.Add(
                      new ListaAlterarSituacionDet
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
                          QTDE_PEDIDA_ITEM = Convert.ToDecimal(registros["QTDE_PEDIDA_ITEM"].ToString()).ToString("N", nfi),
                          VALOR_CONV = registros["VALOR_CONV"].ToString(),
                          total = Convert.ToDecimal(registros["total"].ToString()).ToString("N", nfi),
                          DESCRICAO = registros["DESCRICAO"].ToString(),
                      }
                );
            }
            conexion.Desconectar();
            return lista;
        }


        //  LISTAR OC COMBOBOX GERENCIAS
        public List<ListarOCGerencias> ListarCbxGerencias()
        {
            List<ListarOCGerencias> listarOCGerencias = new List<ListarOCGerencias>();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "3"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", "0"));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", null));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", null));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", null));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_INI", null));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_INI", null));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    listarOCGerencias.Add(
                          new ListarOCGerencias
                          {
                              CodGerencia = registros["COD_GER"].ToString(),
                              DescGerencia = registros["DESC_GER"].ToString(),
                          }
                    );
                }
            }
            conexion.Desconectar();
            return listarOCGerencias;
        }




        public List<ListaOCPeriodo> ListarCbxPeriodo()
        {
            List<ListaOCPeriodo> lista = new List<ListaOCPeriodo>();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "5"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", ""));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", 1));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", 1));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", 1));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_INI", null));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_FIN", null));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    lista.Add(
                          new ListaOCPeriodo
                          {
                              COD_PERIODO = Convert.ToInt32(registros["COD_PERIODO"].ToString()),
                              DESC_PERIODO = registros["DESC_PERIODO"].ToString(),
                          }
                    );
                }
            }
            conexion.Desconectar();
            return lista;
        }

        //  LISTAR RESUMEN PRESUPUSETO POR CENTRO DE COSTO.
        public ResumenPresupuesto ListarResumen(int codgerencia, int codperiodo)
        {
            ResumenPresupuesto resumen = new ResumenPresupuesto();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "6"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", ""));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", codgerencia));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", codperiodo));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", 1));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_INI", null));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_FIN", null));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader(CommandBehavior.SingleResult);

            if (registros.HasRows)
            {
                if (registros.Read())
                {
                    resumen = new ResumenPresupuesto();
                    resumen.Presupuesto = Convert.ToDecimal(registros["PRESUPUESTO"].ToString()).ToString("N", nfi);
                    resumen.Consumido = Convert.ToDecimal(registros["CONSUMIDO"].ToString()).ToString("N", nfi);
                    resumen.Disponible = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi);
                    resumen.Simbolo = registros["SIMBOLO"].ToString();
                }
            }
            conexion.Desconectar();
            return resumen;
        }


        //ALTERERAR SITUACION OC 
        public RespuestaOperacion AlterarSituacionOc(int ordencompra, string usuario, int periodo)
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            DBAccess con = new DBAccess();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_SET_OC_ALTERSITUACION", con.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                con.Conectar();

                comando.Parameters.Add(new OracleParameter("P_I_ORDENCOMPRA", ordencompra));
                comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", usuario));
                comando.Parameters.Add(new OracleParameter("P_I_CODPERIODO", periodo));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader registros = comando.ExecuteReader();

                if (registros.HasRows)
                {
                    while (registros.Read())
                    {
                        respuesta.id_status = Convert.ToInt32(registros["id_status"]);
                        respuesta.desc_status = registros["desc_status"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.id_status = 0;
                respuesta.desc_status = "Error Alterar: " + e.Message + " " + e.StackTrace;
            }

            con.Desconectar();
            return respuesta;
        }

        // 
        public RespuestaOperacion AlterSituacionValidacion(int ordencompra, int periodo)
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            DBAccess con = new DBAccess();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_GET_ALTERSITUACION_VALID", con.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                con.Conectar();

                comando.Parameters.Add(new OracleParameter("IP_PEDIDOCOMPRA", ordencompra));
                comando.Parameters.Add(new OracleParameter("IP_CODPERIODO", periodo));
                comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader registros = comando.ExecuteReader();

                if (registros.HasRows)
                {
                    while (registros.Read())
                    {
                        respuesta.id_status = Convert.ToInt32(registros["ID_RESPUESTA"]);
                        respuesta.desc_status = registros["DESC_RESPUESTA"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.id_status = 0;
                respuesta.desc_status = "Error Validación: " + e.Message + " " + e.StackTrace;
            }

            con.Desconectar();
            return respuesta;
        }


        public RespuestaOperacion RegistrarSoliExc(int ordencompra, string usuario, string motivo)
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            DBAccess conexion = new DBAccess();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_SET_OC_EXCEDENTE", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("P_I_ORDENCOMPRA", ordencompra));
                comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", usuario));
                comando.Parameters.Add(new OracleParameter("P_CH_MOTIVO", motivo));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader registros = comando.ExecuteReader();

                if (registros.HasRows)
                {
                    while (registros.Read())
                    {
                        respuesta.id_status = Convert.ToInt32(registros["id_status"]);
                        respuesta.desc_status = registros["desc_status"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.id_status = 0;
                respuesta.desc_status = e.Message;
            }

            conexion.Desconectar();
            return respuesta;
        }


        public RespuestaOperacion OC_EnviarEmail(int ordencompra, string usuario, string motivo)
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            DBAccess conexion = new DBAccess();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_OC_EXCEDENTE_EMAIL", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("P_I_ORDENCOMPRA", ordencompra));
                comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", usuario));
                comando.Parameters.Add(new OracleParameter("P_CH_MOTIVO", motivo));
                comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader registros = comando.ExecuteReader();

                if (registros.HasRows)
                {
                    while (registros.Read())
                    {
                        respuesta.id_status = Convert.ToInt32(registros["ID_STATUS"]);
                        respuesta.desc_status = registros["DESC_STATUS"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.id_status = 0;
                respuesta.desc_status = e.Message;
            }

            conexion.Desconectar();
            return respuesta;
        }


        public List<ListaDispXCuenta> ListarPlanContDetalle(int codpedcompra,
                                                       int codperiodo,
                                                       int codcc)
        {
            List<ListaDispXCuenta> lista = new List<ListaDispXCuenta>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "8"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", "0"));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", codpedcompra));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", codperiodo));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", codcc));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_INI", null));
            comando.Parameters.Add(new OracleParameter("I_DT_FECHA_FIN", null));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader(CommandBehavior.SingleResult);

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    lista.Add(new ListaDispXCuenta
                    {
                        CUENTA = registros["CUENTA"].ToString(),
                        PRESUPUESTO = Convert.ToDecimal(registros["PRESUPUESTO"].ToString()).ToString("N", nfi),
                        CONSUMIDO = Convert.ToDecimal(registros["CONSUMIDO"].ToString()).ToString("N", nfi),
                        DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi),
                        VALOR_OC = Convert.ToDecimal(registros["VALOR_OC_C"].ToString()).ToString("N", nfi),
                        SIM_PRE = registros["SIM_PRE"].ToString(),
                        SIM_OC = registros["SIM_OC"].ToString()
                    });

                }
            }
            conexion.Desconectar();
            return lista;
        }


        public async Task<List<Lista9_Aprobados>> ListarAprobados(string usuario,
                                                       DateTime fecha_ini,
                                                       DateTime fecha_fin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("I_OPCION", "9", OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("I_CH_FILTRO_1", usuario, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_1", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_2", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_3", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_DT_FECHA_INI", fecha_ini.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("I_DT_FECHA_FIN", fecha_fin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Lista9_Aprobados>(sql: "USYSTEX.SPU_GET_OC_ALTERSITUACION",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderByDescending(x => x.PEDIDO).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }











        public void GuardarErrorMensaje(string modulo,
                                        string metodo,
                                        string error_mensaje,
                                        string error_stacktrace,
                                        string data_1,
                                        string data_2,
                                        string data_3,
                                        string usuario)
        {
            DBAccess con = new DBAccess();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_SET_ERROR_PPTO_CONTROL", con.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                con.Conectar();


                comando.Parameters.Add(new OracleParameter("V_MODULO", modulo));
                comando.Parameters.Add(new OracleParameter("V_METODO", metodo));
                comando.Parameters.Add(new OracleParameter("V_ERROR_MESSAGE", error_mensaje));
                comando.Parameters.Add(new OracleParameter("V_ERROR_STACKTRACE", error_stacktrace));
                comando.Parameters.Add(new OracleParameter("V_DATA_1", data_1));
                comando.Parameters.Add(new OracleParameter("V_DATA_2", data_2));
                comando.Parameters.Add(new OracleParameter("V_DATA_3", data_3));
                comando.Parameters.Add(new OracleParameter("V_USUARIO", usuario));

                OracleDataReader registros = comando.ExecuteReader();
            }
            catch (Exception e)
            {
            }

            con.Desconectar();
        }




        public async Task<List<Lista10_Totales>> ListarTotales(int periodo, string usuario)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("I_OPCION", "10", OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("I_CH_FILTRO_1", usuario, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_1", periodo, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_2", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_3", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_DT_FECHA_INI", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("I_DT_FECHA_FIN", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Lista10_Totales>(sql: "USYSTEX.SPU_GET_OC_ALTERSITUACION",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderByDescending(x => x.CECO).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public MemoryStream ExcelAprobados(List<Lista11_AprobExcel> listado)
        {
            var stream = new MemoryStream();

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
                workSheet.Cells[1, 1, 1, 10].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells["A1"].Value = "PEDIDO";
                workSheet.Cells["B1"].Value = "ESTADO";
                workSheet.Cells["C1"].Value = "FECHA";
                workSheet.Cells["D1"].Value = "COND. PAGO";
                workSheet.Cells["E1"].Value = "PROVEEDOR";
                workSheet.Cells["F1"].Value = "COMPRADOR";
                workSheet.Cells["G1"].Value = "MONEDA";
                workSheet.Cells["H1"].Value = "VALOR TOTAL";
                workSheet.Cells["I1"].Value = "USUARIO APROB.";
                workSheet.Cells["J1"].Value = "FECHA APROB.";

                workSheet.Column(1).Width = 10;     // PEDIDO
                workSheet.Column(2).Width = 15;     // ESTADO
                workSheet.Column(3).Width = 15;     // FECHA
                workSheet.Column(4).Width = 25;     // COND. PAGO
                workSheet.Column(5).Width = 30;     // PROVEEDOR
                workSheet.Column(6).Width = 30;     // COMPRADOR
                workSheet.Column(7).Width = 15;     // MONEDA
                workSheet.Column(8).Width = 15;     // VALOR TOTAL
                workSheet.Column(9).Width = 15;     // USUARIO APROB.
                workSheet.Column(10).Width = 15;    // FECHA APROB

                int i = 2;

                foreach (var item in listado)
                {
                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.PEDIDO;
                    workSheet.Cells[i, 2].Value = item.ESTADO_OC;
                    workSheet.Cells[i, 3].Value = item.FECHA;
                    workSheet.Cells[i, 4].Value = item.DESCRIPCIONPAGO;
                    workSheet.Cells[i, 5].Value = item.PROVEEDOR;
                    workSheet.Cells[i, 6].Value = item.COMPRADOR;
                    workSheet.Cells[i, 7].Value = item.MONEDA;
                    workSheet.Cells[i, 8].Value = item.TOTAL_PEDIDO;
                    workSheet.Cells[i, 9].Value = item.USUARIO;
                    workSheet.Cells[i, 10].Value = item.FECHA_APROB;

                    i++;
                }

                package.Save();
            }

            stream.Position = 0;
            return stream;

        }



        public List<Lista11_AprobExcel> ListarExcelAprobados(string usuario, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("I_OPCION", "11", OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("I_CH_FILTRO_1", usuario, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_1", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_2", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_3", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_DT_FECHA_INI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("I_DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = conexion.Query<Lista11_AprobExcel>(sql: "USYSTEX.SPU_GET_OC_ALTERSITUACION",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderByDescending(x => x.PEDIDO).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }





        public List<Lista12_OCExcelPend> ListarOcAlterarSituacionExcel(string usuario)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("I_OPCION", "12", OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("I_CH_FILTRO_1", usuario, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_1", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_2", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_I_FILTRO_3", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("I_DT_FECHA_INI", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("I_DT_FECHA_FIN", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = conexion.Query<Lista12_OCExcelPend>(sql: "USYSTEX.SPU_GET_OC_ALTERSITUACION",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderByDescending(x => x.PEDIDO).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }





        public MemoryStream ExcelOCPendiente(List<Lista12_OCExcelPend> listado)
        {
            var stream = new MemoryStream();

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
                workSheet.Cells[1, 1, 1, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells["A1"].Value = "PEDIDO";
                workSheet.Cells["B1"].Value = "ESTADO";
                workSheet.Cells["C1"].Value = "FECHA";
                workSheet.Cells["D1"].Value = "COND. PAGO";
                workSheet.Cells["E1"].Value = "PROVEEDOR";
                workSheet.Cells["F1"].Value = "COMPRADOR";
                workSheet.Cells["G1"].Value = "MONEDA";
                workSheet.Cells["H1"].Value = "VALOR TOTAL";

                workSheet.Column(1).Width = 10;     // "PEDIDO";
                workSheet.Column(2).Width = 15;     // "ESTADO";
                workSheet.Column(3).Width = 15;     // "FECHA";
                workSheet.Column(4).Width = 25;     // "COND. PAGO";
                workSheet.Column(5).Width = 30;     // "PROVEEDOR";
                workSheet.Column(6).Width = 30;     // "COMPRADOR";
                workSheet.Column(7).Width = 15;     // "MONEDA";
                workSheet.Column(8).Width = 15;     // "VALOR TOTAL";

                int i = 2;

                foreach (var item in listado)
                {
                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.PEDIDO;
                    workSheet.Cells[i, 2].Value = item.ESTADO_OC;
                    workSheet.Cells[i, 3].Value = item.FECHA;
                    workSheet.Cells[i, 4].Value = item.DESCRIPCIONPAGO;
                    workSheet.Cells[i, 5].Value = item.PROVEEDOR;
                    workSheet.Cells[i, 6].Value = item.COMPRADOR;
                    workSheet.Cells[i, 7].Value = item.MONEDA;
                    workSheet.Cells[i, 8].Value = item.TOTAL_PEDIDO;

                    i++;
                }

                package.Save();
            }

            stream.Position = 0;
            return stream;

        }




    }
}