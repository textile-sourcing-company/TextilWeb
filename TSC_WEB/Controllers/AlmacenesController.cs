using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using TSC_WEB.Models.Entidades.Logistica.AcumuladoXCuenta;
using TSC_WEB.Models.Entidades.Logistica.AlterarSituacion;
using TSC_WEB.Models.Entidades.Logistica.AprobacionExcedentes;
using TSC_WEB.Models.Entidades.Logistica.AprobacionOC;
using TSC_WEB.Models.Modelos.Logistica.AcumGerCecoCuenta;
using TSC_WEB.Models.Modelos.Logistica.AcumuladoXCuenta;
using TSC_WEB.Models.Modelos.Logistica.AlterarSituacionOC;
using TSC_WEB.Models.Modelos.Logistica.AprobacionExcedentes;
using TSC_WEB.Models.Modelos.Logistica.AprobacionOC;
using TSC_WEB.Models.Modelos.Logistica.Graficos;
using TSC_WEB.Models.Modelos.Logistica.OCPendienteCierre;
using TSC_WEB.Models.Modelos.Logistica.OCPendienteFirma;
using TSC_WEB.Models.Modelos.Logistica.PlanContable;
using TSC_WEB.Models.Modelos.Logistica.ReporteStockFecha;
using TSC_WEB.Models.Entidades.Almacenes.ReporteStockFecha;
using TSC_WEB.Models.Modelos.Sistema;
using TSC_WEB.Util.Sistema;
using System.Web.Mvc;
using TSC_WEB.Models.Modelos.Almacenes.ReporteStockFecha;
using TSC_WEB.Models.Entidades.MovimientoPorPeriodo;
using TSC_WEB.Models.Modelos.Logistica;
using System.IO;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;

namespace TSC_WEB.Controllers
{
    public class AlmacenesController : Controller
    {
        #region INSTACIAS
        AprobacionOcModelo objAprobacionOcM = new AprobacionOcModelo();
        AlterarSituacionOcModelo objAlterarSituacionOcM = new AlterarSituacionOcModelo();
        UsuariosModelo objUsuariosM = new UsuariosModelo();
        CentroCostoModelo objCentroCosto = new CentroCostoModelo();
        ReporteStockFechaModelo objReporteStockFecha = new ReporteStockFechaModelo();
        ReporteStockFechaModelo ReporteModelo = new ReporteStockFechaModelo();

        public static List<ReporteStockFechaEntidad> listaRespaldoExportar = null;



        BSMovimientoPorPeriodo ObjBSMovPeriodo = new BSMovimientoPorPeriodo();
        static List<EBMovimientoPorPeriodo> EntidadMovimientoPorPeriodo = new List<EBMovimientoPorPeriodo>();
        static List<EBMovimientoPorPeriodo> EntidadSaldoFinal = new List<EBMovimientoPorPeriodo>();
        static List<EBMovimientoPorPeriodo> EntidadTotal = new List<EBMovimientoPorPeriodo>();


        #endregion

        #region VISTAS
        public ActionResult ReporteStockFecha()
        {

            if (Session["usuario"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult MovimientoPorPeriodo()
        {
            if (Session["usuario"] != null)
            {
                return View();
            }
            else
            {
                return View();
            }
        }


        #endregion

        #region METODOS

        //CARGAR EL COMBO EN EL REPORTE DE STOCK FECHAS 
        public JsonResult ListarCombosCentroCosto()
        {
            return Json(objCentroCosto.ListarCentrosCosto(), JsonRequestBehavior.AllowGet);
        }


        //LISTA DE DATOS DE REPORTE DE STOCK FECHA
        public JsonResult ListarReporteStockFecha(string v_partida, string v_ubicacion, string v_tipotela, string v_centrocosto, string v_almacenes)
        {
            //Se crear una referencia a JavaScriptSerializer
            var serializer = new JavaScriptSerializer();
            //Se cambia el Length directo a nuestra referencia
            serializer.MaxJsonLength = 500000000;

            listaRespaldoExportar = objReporteStockFecha.ListarReporteStockFecha(v_partida, v_ubicacion, v_tipotela, v_centrocosto, v_almacenes);


            var json = Json(objReporteStockFecha.ListarReporteStockFecha(v_partida, v_ubicacion, v_tipotela, v_centrocosto, v_almacenes), JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        [HttpGet]
        public FileResult ExportarExcelReporteStockFecha()
        {


            return File(ReporteModelo.ExportarExcelReporteStockF(listaRespaldoExportar), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteStockFecha" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }
        #endregion


        public void Excel()
        {
            ExportReports export = new ExportReports();
            export.ToExcel(Response, EntidadMovimientoPorPeriodo, "ReporteMovPeriodo");
        }
        [HttpGet]
        public void ListarMovimientoPeriodoexporta(DateTime? FECHAINI, DateTime? FECHAFIN, string LOTE, string COD
                                                , string CODALM, string NIVEL, string GRUPO, string SUBGRUPO, string ITEM)
        {

            EntidadMovimientoPorPeriodo = ObjBSMovPeriodo.ListarMovimientoPorPeriodo(FECHAINI, FECHAFIN, LOTE, COD, CODALM, NIVEL, GRUPO, SUBGRUPO, ITEM);
            Excel();
        }

        [HttpGet]
        public JsonResult ListarMovimientoPeriodo(DateTime? FECHAINI, DateTime? FECHAFIN, string LOTE, string COD
                                           , string CODALM, string NIVEL, string GRUPO, string SUBGRUPO, string ITEM)
        {
            //Se crear una referencia a JavaScriptSerializer
            var serializer = new JavaScriptSerializer();
            //Se cambia el Length directo a nuestra referencia
            serializer.MaxJsonLength = 500000000;

            EntidadMovimientoPorPeriodo = ObjBSMovPeriodo.ListarMovimientoPorPeriodo(FECHAINI, FECHAFIN, LOTE, COD, CODALM, NIVEL, GRUPO, SUBGRUPO, ITEM);

            var LitsRecorrer = EntidadMovimientoPorPeriodo.Select(x => new
            {
                codigo = x.codigo,
                lote = x.lote,
                deposito = x.codalmacen
            }).Distinct().ToList();


            for (int i = 0; i < LitsRecorrer.Count; i++)
            {
                int contador = 0;
                var coun = EntidadMovimientoPorPeriodo.Where(x => x.codigo == LitsRecorrer[i].codigo && x.codalmacen == LitsRecorrer[i].deposito).ToList();
                foreach (var item in EntidadMovimientoPorPeriodo.Where(x => x.codigo == LitsRecorrer[i].codigo && x.lote == LitsRecorrer[i].lote && x.codalmacen == LitsRecorrer[i].deposito).
                    OrderBy(x => x.entrada_salida).ThenBy(x => x.codigo_transacao).
                    ThenBy(x => x.codalmacen).ToList())
                {
                    if (LitsRecorrer[i].codigo == item.codigo)
                    {
                        if (contador == 0)
                        {
                            EntidadSaldoFinal.Add(new EBMovimientoPorPeriodo()
                            {
                                codigo = item.codigo,
                                narrativa = item.narrativa,
                                fechamovimiento = item.fechamovimiento,
                                lote = item.lote,
                                numero_documento = item.numero_documento,
                                doc_movimiento = item.doc_movimiento,
                                transaccion = item.transaccion,
                                codalmacen = item.codalmacen,
                                nivel = item.nivel,
                                grupo = item.grupo,
                                subgrupo = item.subgrupo,
                                item = item.item,
                                codigo_transacao = item.codigo_transacao,

                                entrada_salida = item.entrada_salida,
                                alm_descripcion = item.alm_descripcion,
                                cantidadinicial = item.cantidadinicial,
                                cantidad_entrada = item.cantidad_entrada,
                                cantidad_salida = item.cantidad_salida,
                                Desc_Transaccion = item.Desc_Transaccion,
                                talla = item.talla,
                                color = item.color,
                                partida = item.partida,
                                usuario_ins = item.usuario_ins,
                                Fecha_Ins_Entrada = item.Fecha_Ins_Entrada,
                                Fecha_Ins_Salida = item.Fecha_Ins_Salida,
                                Hora_Ins_Entrada = item.Hora_Ins_Entrada,
                                Hora_Ins_salida = item.Hora_Ins_salida,
                                cantidad_final = item.cantidadinicial + item.cantidad_entrada - item.cantidad_salida,
                                familia = item.familia,
                                cantidad = item.cantidad,
                                Pedido = item.Pedido,
                                Corte_Destino = item.Corte_Destino,
                                App_Ch_Destino = item.App_Ch_Destino,
                                TipoServicio = item.TipoServicio,
                                NombreTaller = item.NombreTaller
                            });
                        }
                        else
                        {
                            EntidadSaldoFinal.Add(new EBMovimientoPorPeriodo()
                            {
                                codigo = item.codigo,
                                narrativa = item.narrativa,
                                fechamovimiento = item.fechamovimiento,
                                lote = item.lote,
                                codalmacen = item.codalmacen,
                                nivel = item.nivel,
                                doc_movimiento = item.doc_movimiento,
                                grupo = item.grupo,
                                subgrupo = item.subgrupo,
                                item = item.item,
                                talla = item.talla,
                                color = item.color,
                                partida = item.partida,
                                codigo_transacao = item.codigo_transacao,
                                entrada_salida = item.entrada_salida,
                                alm_descripcion = item.alm_descripcion,
                                numero_documento = item.numero_documento,
                                transaccion = item.transaccion,
                                Desc_Transaccion = item.Desc_Transaccion,
                                cantidadinicial = item.cantidadinicial,
                                cantidad_entrada = item.cantidad_entrada,
                                cantidad_salida = item.cantidad_salida,
                                usuario_ins = item.usuario_ins,
                                Fecha_Ins_Entrada = item.Fecha_Ins_Entrada,
                                Fecha_Ins_Salida = item.Fecha_Ins_Salida,
                                Hora_Ins_Entrada = item.Hora_Ins_Entrada,
                                Hora_Ins_salida = item.Hora_Ins_salida,
                                cantidad_final = (EntidadSaldoFinal[EntidadSaldoFinal.Count() - 1].cantidad_final + item.cantidad_entrada + item.cantidadinicial) - item.cantidad_salida,
                                familia = item.familia,
                                cantidad = item.cantidad,
                                Pedido = item.Pedido,
                                Corte_Destino = item.Corte_Destino,
                                App_Ch_Destino = item.App_Ch_Destino,
                                TipoServicio = item.TipoServicio,
                                NombreTaller = item.NombreTaller,
                            });
                        }
                        contador++;
                    }
                }
            }
            EntidadTotal = EntidadSaldoFinal.ToList().OrderBy(x => x.codalmacen).ThenBy(x => x.nivel).ThenBy(x => x.grupo).ThenBy(x => x.subgrupo).ThenBy(x => x.item).ThenBy(x => x.lote).ToList();
            var var_jason = Json(EntidadMovimientoPorPeriodo, JsonRequestBehavior.AllowGet);
            //for (int i = 0; i < dtgMovimiento.ColumnCount; i++)
            //{
            //    dtMyTabla.Columns.Add(dtgMovimiento.Columns[i].HeaderText);
            //}
            //// =>[Detalle]
            //foreach (DataGridViewRow item in dtgMovimiento.Rows)
            //{
            //    object[] x = new object[item.Cells.Count];
            //    for (int i = 0; i < item.Cells.Count; i++)
            //    {

            //        x[i] = item.Cells[i].FormattedValue;
            //    }
            //    dtMyTabla.Rows.Add(x);
            //}
            // Excel();//para probar primero antes de crear un boton nuevo de exportar.
            var_jason.MaxJsonLength = 500000000;
            return var_jason;
            //return Json(ObjBSMovPeriodo.ListarMovimientoPorPeriodo(FECHAINI,FECHAFIN,LOTE,COD, CODALM,NIVEL, GRUPO,SUBGRUPO,ITEM), JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public JsonResult ListarAlmacen(int? CODALM)
        {
            //Se crear una referencia a JavaScriptSerializer
            var serializer = new JavaScriptSerializer();
            //Se cambia el Length directo a nuestra referencia
            serializer.MaxJsonLength = 500000000;
            var var_jason = Json(ObjBSMovPeriodo.ListarAlmacenes(CODALM), JsonRequestBehavior.AllowGet);
            var_jason.MaxJsonLength = 500000000;
            return var_jason;
        }
        public JsonResult ListarTransaccion(int? CODTRAN)
        {
            //Se crear una referencia a JavaScriptSerializer
            var serializer = new JavaScriptSerializer();
            //Se cambia el Length directo a nuestra referencia
            serializer.MaxJsonLength = 500000000;
            var var_jason = Json(ObjBSMovPeriodo.ListarTransacciones(CODTRAN), JsonRequestBehavior.AllowGet);
            var_jason.MaxJsonLength = 500000000;
            return var_jason;
        }
        public MemoryStream ExpExcelAcumuladoMult(List<EBMovimientoPorPeriodo> listado)
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

                workSheet.Cells["A1"].Value = "Fecha Mov.";
                workSheet.Cells["B1"].Value = "Código   ";
                workSheet.Cells["C1"].Value = "Nivel";
                workSheet.Cells["D1"].Value = "Grupo";
                workSheet.Cells["E1"].Value = "SubGrupo";
                workSheet.Cells["F1"].Value = "Talla";
                workSheet.Cells["G1"].Value = "Item";
                workSheet.Cells["H1"].Value = "Color";
                workSheet.Cells["I1"].Value = "Producto";
                workSheet.Cells["J1"].Value = "Código Almacén";
                workSheet.Cells["K1"].Value = "Almacén";
                workSheet.Cells["L1"].Value = "Documento";
                workSheet.Cells["M1"].Value = "Doc. Movimiento";
                workSheet.Cells["N1"].Value = "Transacción";
                workSheet.Cells["O1"].Value = "Desc. Transacción";
                workSheet.Cells["P1"].Value = "E/S";
                workSheet.Cells["Q1"].Value = "Lote";
                workSheet.Cells["R1"].Value = "Partida";
                workSheet.Cells["S1"].Value = "Saldo Inicial";
                workSheet.Cells["T1"].Value = "Entrada";
                workSheet.Cells["U1"].Value = "Salida";
                workSheet.Cells["V1"].Value = "Saldo Final";
                workSheet.Cells["W1"].Value = "Fecha Inserción";
                workSheet.Cells["X1"].Value = "Hora Inserción";
                workSheet.Cells["Y1"].Value = "Usuario Ins.";
                workSheet.Cells["Z1"].Value = "Familia";
                workSheet.Cells["AA1"].Value = "Cantidad";
                workSheet.Cells["AB1"].Value = "Pedido";
                workSheet.Cells["AC1"].Value = "Corte_Destino";
                workSheet.Cells["AD1"].Value = "App_Ch_Destino";
                workSheet.Cells["AE1"].Value = "TipoServicio";
                workSheet.Cells["AF1"].Value = "NombreTaller";
                int i = 2;

                foreach (var item in listado)
                {
                    workSheet.Row(i).Style.Font.Size = 8;
                    workSheet.Cells[i, 1].Value = item.fechamovimiento;
                    workSheet.Cells[i, 2].Value = item.codigo;
                    workSheet.Cells[i, 3].Value = item.nivel;
                    workSheet.Cells[i, 4].Value = item.grupo;
                    workSheet.Cells[i, 5].Value = item.subgrupo;
                    workSheet.Cells[i, 6].Value = item.talla;
                    workSheet.Cells[i, 7].Value = item.item;
                    workSheet.Cells[i, 8].Value = item.color;
                    workSheet.Cells[i, 9].Value = item.narrativa;
                    workSheet.Cells[i, 10].Value = item.codalmacen;
                    workSheet.Cells[i, 11].Value = item.alm_descripcion;
                    workSheet.Cells[i, 12].Value = item.numero_documento;
                    workSheet.Cells[i, 13].Value = item.doc_movimiento;
                    workSheet.Cells[i, 14].Value = item.codigo_transacao;
                    workSheet.Cells[i, 15].Value = item.Desc_Transaccion;
                    workSheet.Cells[i, 16].Value = item.entrada_salida;
                    workSheet.Cells[i, 17].Value = item.lote;
                    workSheet.Cells[i, 18].Value = item.partida;
                    workSheet.Cells[i, 19].Value = item.cantidadinicial;
                    workSheet.Cells[i, 20].Value = item.cantidad_entrada;
                    workSheet.Cells[i, 21].Value = item.cantidad_salida;
                    workSheet.Cells[i, 22].Value = item.cantidad_final;
                    workSheet.Cells[i, 23].Value = item.Fecha_Ins_Entrada;
                    workSheet.Cells[i, 24].Value = item.Hora_Ins_Entrada;
                    workSheet.Cells[i, 25].Value = item.usuario_ins;
                    workSheet.Cells[i, 26].Value = item.familia;
                    workSheet.Cells[i, 27].Value = item.cantidad;
                    workSheet.Cells[i, 28].Value = item.Pedido;
                    workSheet.Cells[i, 29].Value = item.Corte_Destino;
                    workSheet.Cells[i, 30].Value = item.App_Ch_Destino;
                    workSheet.Cells[i, 31].Value = item.TipoServicio;
                    workSheet.Cells[i, 32].Value = item.NombreTaller;
                    i++;
                }
                package.Save();
            }
            stream.Position = 0;
            return stream;
        }

        public FileResult ExportarExcelAcumuladoMov()
        {

            return File(ExpExcelAcumuladoMult(EntidadTotal), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MovPorPeriodo" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }




    }
}