using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela.reporteliquidacion;
using TSC_WEB.Models.Modelos.Corte.LiquidacionTela;
using TSC_WEB.Models.Modelos.Corte.LiquidacionTela.reporteliquidacion;
using TSC_WEB.Models.Modelos.Corte.LiquidacionTela.registrofichaspcp;
using TSC_WEB.Models.Modelos.Corte.LiquidacionTela.aperturaficha;
using TSC_WEB.Models.Modelos.Corte.LiquidacionRectilineos;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela.tablasgenerales;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela.indicadorliquidacion;
using TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos;
using Newtonsoft.Json;
using System.Data;
using System.Web.Script.Serialization;
using Rotativa;
using Rotativa.Options;
using TSC_WEB.Models.Modelos.Sistema;
using OfficeOpenXml;


namespace TSC_WEB.Controllers
{
    public class CorteController : Controller
    {
        #region INSTANCIAS
        //private static ExcelWorksheet workSheet;

        FichasModelo objFichas = new FichasModelo();
        ReporteControlUsoModelo objControlUsoM = new ReporteControlUsoModelo();
        ReporteLiquidacionModelo objLiquidacionM = new ReporteLiquidacionModelo();
        AperturarFichasTendidoModelo objAperturarFichaM = new AperturarFichasTendidoModelo();
        AperturarFichasCorteModelo objAperturarCorteM = new AperturarFichasCorteModelo();
        ReporteContraCorrienteModelo objReporteContraCorrienteM = new ReporteContraCorrienteModelo();
        CaidasModelo objCaidas = new CaidasModelo();
        IndicadorPcpLiquidacionModelo objIndicadorPcp = new IndicadorPcpLiquidacionModelo();
        registroFichasPcpModelo objRegistroFichasPcp = new registroFichasPcpModelo();
        EjecutaReporteModelo objReporteLiquidacionM = new EjecutaReporteModelo();
        AperturarFichaModelo objAperturarFichaNewM = new AperturarFichaModelo();
        RecursosModelo objRecursosModeloM = new RecursosModelo();
        LiquidacionRectilineosModelo objRectilineosM = new LiquidacionRectilineosModelo();
        LiquidacionRectilineosReporteModelo objRectilineosReporteM = new LiquidacionRectilineosReporteModelo();


        #endregion

        #region VISTAS
        public ActionResult LiquidacionCorte()
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
        public ActionResult BuscarLiquidacion()
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
        public ActionResult MermasCorte()
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
        public ActionResult LiquidacionPorFichas(string partida, int combo, int version, string tipotela)
        {

            var response = objIndicadorPcp.getReportePCP(partida, combo, version, tipotela);

            if (Session["usuario"] != null)
            {
                return View(response);
            }
            else
            {
                return Redirect("/");
            }
        }
        public ActionResult ReporteControlUso()
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
        public ActionResult ReporteControlUsoIndicador()
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
        public ActionResult ReporteLiquidacionTela()
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

        // REGISTRO DE RECTILINEOS
        public ActionResult LiquidacionRectilineos(int? ficha, string tipo)
        {
            if (Session["usuario"] != null)
            {
                var datosficha = objRectilineosM.getDatosFicha(1, ficha, tipo);
                var fichastallas = objRectilineosM.getTallasFicha(2, ficha, tipo);
                var segundastallas = objRectilineosM.getTallasFichaSegundas(3, ficha, tipo);



                return View(
                    new LiquidacionRectilineosEntidad
                    {
                        FichaCabecera = datosficha,
                        FichaTallas = fichastallas,
                        SegundasTallas = segundastallas
                    }
                );
            }
            else
            {
                return Redirect("/");
            }
        }


        // REPORTE DE TRANSACCIONES POR CONFIRMAR
        public ActionResult ReporteTransaccionesConfirmar()
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

        // APERTURA FICHA DE TENDIDO Y CORTE
        public ActionResult AperturaFichas()
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

        // APERTURA FICHA DE TENDIDO
        public ActionResult AperturaFichaTendido()
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

        // APERTURA FICHA DE CORTE
        public ActionResult AperturaFichaCorte()
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

        // REPORTE DE CONTRA CORRIENTE
        public ActionResult ReporteContraCorriente()
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

        // REPORTE DE CONTRA CORRIENTE
        public ActionResult CaidasPorFicha()
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

        // REGISTRO DE FICHAS PROGRAMADAS
        public ActionResult setFichasProgramadas()
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

        // REPORTE RECTILINEOS
        public ActionResult getReporteRectilineos(string fechai, string fechaf, string ficha, string partida, string tipo)
        {
            if (Session["usuario"] != null)
            {
                List<ReporteEntidad> response = new List<ReporteEntidad>();

                if (fechai != string.Empty || fechaf != string.Empty || ficha != string.Empty || partida != string.Empty || tipo != string.Empty)
                {
                    response = objRectilineosM.getReporte(fechai, fechaf, ficha, partida, tipo);
                    Session["dato_reporte_rectilineos"] = response;
                }

                return View(response);
            }
            else
            {
                return Redirect("/");
            }
        }

        // LIQUIDACION RECTILINEOS PCP
        [HttpGet]
        public ActionResult getLiquidacionRectilineosPCP(string fechai, string fechaf, string partida, string estado, string tipo, string busqueda)
        {

            if (Session["usuario"] != null)
            {
                List<LiquidacionRectilineosPCPEntidad> response = new List<LiquidacionRectilineosPCPEntidad>();

                if (busqueda != "" && busqueda != null)
                {
                    response = objRectilineosM.getLiquidacionRectilineosPCP(fechai, fechaf, partida, estado, tipo);
                    Session["dato_liquidacion_rectilineos_pcp"] = response;
                }

                return View(response);
            }
            else
            {
                return Redirect("/");
            }
        }

        // LIQUIDACION RECTILINEOS PCP INDIVIDUAL
        [HttpGet]
        public ActionResult getLiquidacionRectilineosPCPIndividual(int idrectilineo)
        {

            if (Session["usuario"] != null)
            {
                //List<LiquidacionRectilineosPCPEntidad> response = new List<LiquidacionRectilineosPCPEntidad>();

                //if (busqueda != "" && busqueda != null)
                //{
                //    response = objRectilineosM.getLiquidacionRectilineosPCP(fechai, fechaf, partida, estado, tipo);
                //    Session["dato_liquidacion_rectilineos_pcp"] = response;
                //}
                var response = objRectilineosM.getRectilineosPCP(idrectilineo);
                return View(response);
            }
            else
            {
                return Redirect("/");
            }
        }


        #endregion

        #region METODOS




        #region BUSCAR LIQUIDACION
        // LISTA DE VERSIONES
        [HttpGet]
        public JsonResult ListarVersiones(string ficha)
        {
            return Json(objFichas.ListarVersiones(ficha, "", "", "", "1"), JsonRequestBehavior.AllowGet);
        }
        // LISTAR CABECERA DE CORT_001
        [HttpGet]
        public JsonResult ListarCabeceraCort001(string ficha, string version, string tela, string combo)
        {
            return Json(objFichas.ListarCabeceraCort001(ficha, version, tela, "2", combo), JsonRequestBehavior.AllowGet);
        }
        // CANTIDA_PRENDAS / CANTIDAD PAÑOS
        [HttpGet]
        public JsonResult ObjCant(string ficha, string version, string tela, string combo)
        {
            return Json(objFichas.ObjCant(ficha, version, tela, "3", combo), JsonRequestBehavior.AllowGet);
        }
        //DATOS DE LA TELA REGISTRADOS
        [HttpGet]
        public JsonResult ListaDatosTela(string ficha, string version, string tela, string combo)
        {
            return Json(objFichas.ListaDatosTela(ficha, version, tela, "4", combo), JsonRequestBehavior.AllowGet);
        }
        //FICHAS - CONSUMO
        [HttpGet]
        public JsonResult ListaConsumos(string ficha, string version, string tela, string combo)
        {
            return Json(JsonConvert.SerializeObject(objFichas.ListaConsumos(ficha, version, tela, "15", combo), Formatting.Indented), JsonRequestBehavior.AllowGet);

        }
        //LISTA DE DATOS CALCULADOS POR EL TIZADOR
        [HttpGet]
        public JsonResult ListaTotal(string ficha, string version, string tela, string combo)
        {
            return Json(objFichas.ListaTotal(ficha, version, tela, "6", combo), JsonRequestBehavior.AllowGet);
        }
        //LISTA DE PROPORCIONES
        [HttpGet]
        public JsonResult ListaProporciones(string ficha, string version, string tela, string combo)
        {
            return Json(JsonConvert.SerializeObject(objFichas.ListaProporciones(ficha, version, tela, "7", combo), Formatting.Indented), JsonRequestBehavior.AllowGet);
        }

        //LISTA DE PROPORCIONES CONTRACORRIOENTE
        [HttpGet]
        public JsonResult ListaProporcionesContraCorriente(string ficha, string version, string tela, string combo)
        {
            return Json(JsonConvert.SerializeObject(objFichas.ListaProporcionesContraCorriente(ficha, version, tela, "17", combo), Formatting.Indented), JsonRequestBehavior.AllowGet);
        }
        //FICHAS X CANTIDADES
        [HttpGet]
        public JsonResult FichasxCantidades(string ficha, string version, string tela, string combo)
        {
            return Json(JsonConvert.SerializeObject(objFichas.FichasxCantidades(ficha, version, tela, "8", combo), Formatting.Indented), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region REGISTRAR TENDIDO

        // BUSCAR SI EXISTE REGISTRO DEL TIZADOR PARA PODER REGISTRAR ETAPAS
        [HttpGet]
        public JsonResult BuscarExiste(string ficha)
        {
            return Json(objFichas.BuscarExiste(ficha, "1", "P", "11"), JsonRequestBehavior.AllowGet);
        }
        // LISTA DE FICHAS X CANTIDADES - PARA REFERANCIA PARA EL TENDEDOR
        [HttpGet]
        public JsonResult BuscarFicha(string ficha, string version, string tela, string combo)
        {
            return Json(objFichas.Listarfichas(ficha, version, tela, "9", combo), JsonRequestBehavior.AllowGet);
        }
        // REGISTRAR ETAPAS CORT_006
        [HttpPost]
        public JsonResult RegistrarEtapas(string PARTIDA, string ETAPAS, decimal NUM_PANOS, decimal LARGO_PANOS, decimal PESO_PANOS, decimal ANCHO_TELA_REAL, decimal KXETAPAS, string U_REGISTRO, string VERSION, string TELA, int TURNO, string TONO, string CELULA, int combo)
        {
            bool resul = false;
            string usuario = Session["usuario"].ToString();
            resul = objFichas.RegistrarCorte007(PARTIDA, ETAPAS, NUM_PANOS, LARGO_PANOS, PESO_PANOS, ANCHO_TELA_REAL, KXETAPAS, usuario, VERSION, TELA, TURNO, TONO, CELULA, combo);
            return Json(resul);
        }
        //LISTA DE ETAPAS REGISTRADAS
        [HttpGet]
        public JsonResult ListarCorte007(string ficha, string version, string tela, string combo)
        {
            return Json(objFichas.ListarCorte(ficha, version, tela, "10", combo), JsonRequestBehavior.AllowGet);
        }
        // REGISTRAR CABECERA CORT_006
        [HttpPost]
        public JsonResult RegistrarCabecera(cort006Entidad cort006)
        {
            bool resul = false;
            cort006.u_registro = Session["usuario"].ToString();
            string mensaje = string.Empty;
            resul = objFichas.RegistrarCorte006(cort006, out mensaje);
            return Json(new { success = resul, mensaje = mensaje });
        }
        //MODIFICAR CORT007
        [HttpPost]
        public JsonResult Modificar_cort007(int id, decimal num_panos, decimal largo_panos, decimal peso_panos, decimal ancho_tela_real, decimal kxetapas, string tono, string celula)
        {
            bool resul = false;
            string usuario = Session["usuario"].ToString();
            resul = objFichas.ModificarCort007(id, num_panos, largo_panos, peso_panos, ancho_tela_real, kxetapas, tono, celula);
            return Json(resul);
        }

        // REGISTRAR CORT 011
        [HttpPost]
        public JsonResult registrar_cort11(string partida, int versiones, string tipotela, int combo, int[] motivos = null)
        {
            bool resul = false;
            bool resul2 = false;
            // ELIMINAMOS
            resul = objFichas.EliminarCorte011(partida, versiones, tipotela, combo);

            if (resul)
            {
                if (motivos != null)
                {
                    for (int x = 0; x < motivos.Length; x++)
                    {
                        resul2 = objFichas.RegistrarCorte011(partida, versiones, tipotela, motivos[x], combo);
                    }
                }
            }



            return Json(resul);
        }

        //LISTA DE CABECERA DE CORT_006
        [HttpGet]
        public JsonResult BuscarCabecera(string ficha, string version, string tela, string combo)
        {
            return Json(objFichas.BuscarCabe(ficha, version, tela, "12", combo), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BuscarMotivos(string ficha, string version, string tela, int combo)
        {
            var motivos = objFichas.BuscarMotivos(ficha, version, tela, "16", combo);
            return Json(new { motivos = motivos }, JsonRequestBehavior.AllowGet);
        }

        // ESTADO DE LAS LIQUIDACIONES
        [HttpGet]
        public JsonResult GetEstadosLiquidacion(int ficha, string combo, string version, string tela)
        {
            return Json(objFichas.getEstadoLiquidacion(ficha, combo, version, tela), JsonRequestBehavior.AllowGet);
        }


        //RETORNA LISTA PARA COPARACION DE KILOS TENDIDO
        [HttpGet]
        public JsonResult getComparacionKilos(int ficha, int version, string tela, int combo)
        {
            return Json(objFichas.getComparacionLiquidacion(ficha, version, tela, combo), JsonRequestBehavior.AllowGet);
        }

        // BUSCAR SI EXISTE REGISTRO DEL TENDEDOR PARA EL USUARIO DE CORTE PUEDA SEGUIR CON EL PROCESO
        [HttpGet]
        public JsonResult BuscarRegistro008(string ficha, string version, string tela, string combo)
        {
            return Json(objFichas.BuscarRegistro008(ficha, version, tela, "18", combo), JsonRequestBehavior.AllowGet);
        }

        // ELIMINAR ETAPA
        [HttpGet]
        public JsonResult EliminarEtapas(int idetapa)
        {
            return Json(new { success = objFichas.EliminarEtapa(idetapa) }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region REGISTRAR CORTE
        // BUSCAR SI EXISTE REGISTRO DE ETAPAS INGRESADAS POR EL TENDEDOR
        [HttpGet]
        public JsonResult BuscarExisteCort007(string ficha, string version, string tela, string combo)
        {
            return Json(objFichas.BuscarExisteCort007(ficha, version, tela, "13", combo), JsonRequestBehavior.AllowGet);
        }

        // REGISTRA CORT_008
        [HttpGet]
        public JsonResult RegistrarCorte008(cort008Entidad cort008)
        {
            //bool resul = false;
            string usuario = Session["usuario"].ToString();
            cort008.u_registro = usuario;
            var resul = objFichas.RegistrarCorte008(cort008);
            return Json(resul, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region REPORTELIQUIDACION
        [HttpGet]
        public JsonResult getReporteLiquidacion(string i_fechai, string i_fechaf, string i_partida, string i_turno, string i_programa, string i_cliente, int? i_ficha, int? i_combo, string i_estadotendido, string i_estadocorte)
        {
            var obj = objLiquidacionM.getReporteLiquidacion(i_fechai, i_fechaf, i_partida, i_turno, i_programa, i_cliente, i_ficha, i_combo, i_estadotendido, i_estadocorte);

            Session["datos_corte_liquidacion"] = obj;
            //Se crear una referencia a JavaScriptSerializer
            var serializer = new JavaScriptSerializer();
            //Se cambia el Length directo a nuestra referencia
            serializer.MaxJsonLength = 500000000;

            var json = Json(obj, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;


        }




        #endregion

        #region REPORTE CONTROL USO

        public JsonResult ReporteControlUsoListar(string partida, string fechainicio, string fechafin, int? ficha)
        {
            string mensaje = string.Empty;
            var resultado = new List<ReporteControlUsoEntidad>();
            try
            {
                resultado = objControlUsoM.Listar(fechainicio, fechafin, partida, ficha);
            }
            catch (Exception e)
            {
                mensaje = e.Message.ToString();
            }

            return Json(new { mensaje = mensaje, registros = resultado });

        }

        [HttpGet]
        public JsonResult ReporteControlUsoListar2(int semana1, int semana2)
        {
            string mensaje = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                ds = objControlUsoM.Listar2(semana1, semana2);
            }
            catch (Exception e)
            {
                mensaje = e.Message.ToString();
            }

            return Json(new { mensaje = mensaje, registros = JsonConvert.SerializeObject(ds) }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult getIndicadorUso(int? anio, int? mes, int? semana, string fecha)
        {
            string mensaje = string.Empty;
            bool success = true;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            try
            {
                success = true;
                //dt = objControlUsoM.getIndicadorUso(anio,mes,semana,fecha);
                ds = objControlUsoM.getIndicadorUso(anio, mes, semana, fecha);

            }
            catch (Exception e)
            {
                success = false;
                mensaje = e.Message.ToString();
            }

            //return Json(new { success= success, mensaje = mensaje, registros = JsonConvert.SerializeObject(dt) }, JsonRequestBehavior.AllowGet);
            return Json(new { success = success, mensaje = mensaje, registros = JsonConvert.SerializeObject(ds) }, JsonRequestBehavior.AllowGet);


        }

        #endregion

        // APERTURAR FICHA BUSCAR
        [HttpGet]
        public JsonResult BuscarFichasAperturar(string partida, string ficha, string estadotendido, string estadocorte)
        {
            var response = objAperturarFichaM.BuscarFichasAperturar(ficha, partida, estadotendido, estadocorte);
            return Json(JsonConvert.SerializeObject(response), JsonRequestBehavior.AllowGet);
        }

        // APERTURAR FICHA
        public JsonResult AperturarFichasTendido(int operacion, string partida, int combo, int version, string tipotela)
        {
            var response = objAperturarFichaM.AperturarRechazar(operacion, partida, combo, version, tipotela, Session["cod_funcionario"].ToString());
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BuscarFichasAperturarCorte(string partida, string ficha)
        {

            var response = objAperturarCorteM.BuscarFichasAperturar(ficha, partida);
            return Json(JsonConvert.SerializeObject(response), JsonRequestBehavior.AllowGet);
        }

        // APERTURAR FICHA
        public JsonResult AperturarFichasCorte(int operacion, string partida, int combo, int version, string tipotela)
        {
            var response = objAperturarCorteM.AperturarRechazar(operacion, partida, combo, version, tipotela, Session["cod_funcionario"].ToString());
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // BUSCAR ESTADO DE CORTE
        public JsonResult getEstadoCorte(string partida, int combo, int version)
        {
            var response = objAperturarCorteM.getestadocorte(partida, combo, version);
            return Json(new { response = response }, JsonRequestBehavior.AllowGet);
        }

        // SET ESTADO CORTE
        public JsonResult CerrarRegistroMerma(cort008Entidad cort008)
        {
            string mensaje = string.Empty;
            var response = objAperturarCorteM.CerrarRegistroMerma(cort008, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getReporteTransaccion()
        {
            var response = objFichas.getReporteTransaccion();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // REPORTE CONTRA CORRIENTE
        [HttpGet]
        public JsonResult getCabeceraReporteContracorriente(int operacion, int ficha, int combo, int version, string tipotela)
        {
            var response = objReporteContraCorrienteM.getCabecera(operacion, ficha, combo, version, tipotela);
            return Json(JsonConvert.SerializeObject(response), JsonRequestBehavior.AllowGet);
        }

        // DIVIDIR ETAPAS
        [HttpGet]
        public JsonResult DividirEtapasTendido(int id, int cantidad)
        {

            string mensaje = string.Empty;
            var response = objFichas.DivirEtapas(id, cantidad, Session["usuario"].ToString(), out mensaje);
            return Json(new { mensaje = mensaje, success = response }, JsonRequestBehavior.AllowGet);

        }


        // OBTENER LAS FICHAS QUE TIENE CAIDA
        [HttpGet]
        public JsonResult getCaidas(string pendiente, string realizado, string ficha)
        {
            return Json(objCaidas.getCaidas(pendiente, realizado, ficha), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult setCaidas(int fichacaida, int id)
        {
            string mensaje = string.Empty;
            var response = objCaidas.setCaidas(id, fichacaida, out mensaje);
            return Json(new { rpt = mensaje, success = response }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public FileResult getReporteLiquidacionExcel()
        {

            var datos = (List<TSC_WEB.Models.Entidades.Corte.LiquidacionTela.ReporteLiquidacionEntidad>)Session["datos_corte_liquidacion"];
            var file = objLiquidacionM.ReporteExcel(datos);

            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteLiquidaciónTela" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");

        }

        #region REPORTELIQUIDACION

        // BUSCAR DATOS DE LIQUIDACION
        [HttpGet]
        public JsonResult getReporteLiquidacionNew(string fechainicio, string fechafin, int? ficha, int? turno, string partida, string programa, string cliente, string estadotendido, string estadocorte, int? combo)
        {
            var response = objReporteLiquidacionM.getReporteLiquidacion(fechainicio, fechafin, ficha, turno, partida, programa, cliente, estadotendido, estadocorte, combo);

            // GUARDAMOS DATOS
            Session["dato_reporte_liquidacion"] = response;

            //Se crear una referencia a JavaScriptSerializer
            var serializer = new JavaScriptSerializer();
            //Se cambia el Length directo a nuestra referencia
            serializer.MaxJsonLength = 500000000;


            var json = Json(response, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;

        }

        [HttpGet]
        public FileResult getReporteLiquidacionExcelNew()
        {

            var datos = (List<datosReporteLiquidacionEntidad>)Session["dato_reporte_liquidacion"];
            var file = objReporteLiquidacionM.getReporteExcel(datos);

            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteLiquidaciónTela" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");

        }

        #endregion

        #region INDICADOR DE USO

        // GUARDAR IMAGEN
        [HttpPost]
        public ActionResult SaveImagenIndicador
        (
            string[] image, string[] thead,
            string[] tbody1, string[] tbody2,
            string[] tbody3, string[] tbody4,
            string[] tfoot, string tipo
        )
        {
            string mensaje = string.Empty;
            bool response = false;
            // VALORES TABLA
            IndicadorPdfEntidad objPdf = new IndicadorPdfEntidad();
            objPdf.thead = thead;
            objPdf.tbody1 = tbody1;
            objPdf.tbody2 = tbody2;
            objPdf.tbody3 = tbody3;
            objPdf.tbody4 = tbody4;
            objPdf.tfoot = tfoot;
            objPdf.tipo = tipo;

            Session["tmppdfindicador"] = objPdf;
            string codigousuario = Session["cod_funcionario"].ToString();

            int cont = 0;
            foreach (var img in image)
            {
                cont++;
                string nombreimg = "imgtmpindicador" + cont + codigousuario;
                response = objRecursosModeloM.SaveImage(img, nombreimg, out mensaje);
            }
            return Json(new { response = response, mensaje = mensaje });
        }


        [HttpGet]
        public ActionResult PDFIndicadoruso()
        {

            IndicadorPdfEntidad pdfentidad = (IndicadorPdfEntidad)Session["tmppdfindicador"];
            //ReporteEntidad resultado = (ReporteEntidad)Session["datasetmaestrofichatecnica"];
            //ASIGNAMOS 
            //resultado.op = Session["opfichatecnica"] == "true" ? true : false;

            //string _headerUrl = Url.Action("HeaderDinamico", "DesarrolloProducto", resultado.objFichaEntidad, "http");
            //string _footerUrl = Url.Action("FooterDinamico", "DesarrolloProducto", null, "http");


            return new ViewAsPdf("PDFIndicadoruso", pdfentidad)
            {

                //CustomSwitches = "--header-html " + _headerUrl + " --header-spacing 13 "
                //+ "--footer-html " + _footerUrl + " --footer-spacing 0"
                //,
                PageMargins = { Top = 20, Bottom = 20 },
                PageOrientation = Rotativa.Options.Orientation.Landscape,

            };
        }
        // REGISTRO Y CONSULTAS DE FICHAS PROGRAMADAS POR PCP
        [HttpGet]
        public JsonResult GSFichasPcp(int? ficha, string fecha, string opcion)
        {
            string mensaje = string.Empty;
            var response = objRegistroFichasPcp.GSFichasProgramadasPCP(ficha, fecha, Session["usuario"].ToString(), opcion, out mensaje);
            var data = JsonConvert.SerializeObject(response, Formatting.Indented);
            return Json(new { rpt = mensaje, data = data }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region APERTURAR FICHAS

        [HttpGet]
        public JsonResult getFichasAperturar(int? semana, int? aniosemana, string fecha, string partida, int? ficha, string estadotendido, string estadocorte)
        {
            var response = objAperturarFichaNewM.getFichasApertura(semana, aniosemana, fecha, partida, ficha, estadotendido, estadocorte);
            //Se crear una referencia a JavaScriptSerializer
            var serializer = new JavaScriptSerializer();
            //Se cambia el Length directo a nuestra referencia
            serializer.MaxJsonLength = 500000000;

            var json = Json(response, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;

            return json;
        }

        [HttpGet]
        public JsonResult setAperturaFicha(string partida, int combo, int version, string tipotela, string estado, string opcion)
        {
            string mensaje = string.Empty;
            var response = objAperturarFichaNewM.AperturaFichas(partida, combo, version, tipotela, estado, opcion, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region LIQUIDACION RECTILINEOS

        // CABECERA DE RECTILINEO
        [HttpPost]
        public JsonResult saveHeadRectilineo(string partida,/*int lote,*/decimal mermarecorte, decimal mermahilos, string tipo, string estado)
        {
            string mensaje = string.Empty;
            var response = objRectilineosM.saveHead(partida, Session["usuario"].ToString() /*, lote*/, mermarecorte, mermahilos, tipo, estado, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // FICHAS DE RECTILINEO
        [HttpGet]
        public JsonResult saveFichaRectilineo(int? idrectilineoficha, int? idrectilineohead, int ficha, string usuario, int pedido, string estilotsc, string estilocliente, string combo, string tipo)
        {
            string mensaje = string.Empty;
            var response = objRectilineosM.saveFichas(idrectilineoficha, idrectilineohead, ficha, Session["usuario"].ToString(), pedido, estilotsc, estilocliente, combo, tipo, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // TALLAS DE RECTILINEO
        [HttpGet]
        public JsonResult saveTallasRectilineo(int? idrectilineoficha, string talla, int realprimera, decimal pesoneto, decimal programado, decimal pesoprogramado, int orden, string tipo)
        {
            string mensaje = string.Empty;
            var response = objRectilineosM.saveTallas(idrectilineoficha, talla, realprimera, pesoneto, programado, orden, pesoprogramado, tipo, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // TALLAS DE RECTILINEO
        [HttpGet]
        public JsonResult saveTallasRectilineoSegundas(int? idrectilineo, string talla, int realsegunda, decimal pesonetosegunda, decimal programadosegunda, int orden)
        {
            string mensaje = string.Empty;
            var response = objRectilineosM.saveTallasSegundas(idrectilineo, talla, realsegunda, pesonetosegunda, programadosegunda, orden, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // REGISTRO DE PARTIDA DE TELA
        [HttpGet]
        public JsonResult savePartidaTela(PartidaTelaEntidad objpartida)
        {
            string mensaje = string.Empty;
            var response = objRectilineosM.savePartidaTela(objpartida, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // REGISTRO DE PARTIDA DE TELA RECTILINEOS
        [HttpGet]
        public JsonResult savePartidaTelaRectilineo(PartidaTelaRectilineoEntidad objpartidarec)
        {
            string mensaje = string.Empty;
            var response = objRectilineosM.savePartidaTelaRectilineos(objpartidarec, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // REGISTRO DE PARTIDA DE TELA RECTILINEOS TALLA
        [HttpGet]
        public JsonResult savePartidaTelaRectilineoTalla(PartidaRectilineoTallasEntidad objpartidarectalla)
        {
            string mensaje = string.Empty;
            var response = objRectilineosM.savePartidaTelaRectilineos_Talla(objpartidarectalla, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult deletepartidarectilineos(int idpartidarectilineo)
        {
            string mensaje = string.Empty;
            var response = objRectilineosM.deletepartidarectilineos(idpartidarectilineo, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult adddeletetallasrectilineos(int opcion, string partidatela, string talla)
        {
            string mensaje = string.Empty;
            var response = objRectilineosM.adddeletetallasrectilineos(opcion, partidatela, talla, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        // INGRESO DE RECTILINEOS ALMACEN
        [HttpGet]
        public JsonResult saveRectilineosAlmacen(IngresoRectilineosEntidad obj)
        {
            string mensaje = string.Empty;
            obj.usuario = Session["usuario"].ToString();
            var response = objRectilineosM.saveRectilineoDespacho(obj, out mensaje);
            return Json(new { success = response, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult getReporteRectilineosExcel()
        {

            var datos = (List<ReporteEntidad>)Session["dato_reporte_rectilineos"];
            var file = objRectilineosReporteM.getReporteExcel(datos);

            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteRectilineos" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");

        }

        [HttpPost]
        public JsonResult setEstadoRectilineos(string estado, int idrectilineo)
        {
            string mensaje = string.Empty;
            var response = objRectilineosM.setEstadoRectilineos(estado, idrectilineo, out mensaje);
            return Json(new { success = response, mensaje = mensaje });
        }

        // REPORTE
        //[HttpGet]
        //public JsonResult getReporteRectilineo()
        //{
        //    var response = objRectilineosM.getReporte();
        //    return Json(response,JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #endregion
    }

}