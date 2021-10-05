using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TextilSoftware.Entidad.Logistica.SeguimientoXPrograma;
using TSC_WEB.Models.Entidades.Logistica;
using TSC_WEB.Models.Entidades.Logistica.AcumuladoXCuenta;
using TSC_WEB.Models.Entidades.Logistica.AlterarSituacion;
using TSC_WEB.Models.Entidades.Logistica.AprobacionExcedentes;
using TSC_WEB.Models.Entidades.Logistica.AprobacionOC;
using TSC_WEB.Models.Entidades.Logistica.Cubo;
using TSC_WEB.Models.Entidades.Logistica.Graficos;
using TSC_WEB.Models.Entidades.MovimientoPorPeriodo;

using TSC_WEB.Models.Entidades.Logistica.VisualizacionOC;

using TSC_WEB.Models.Modelos;
using TSC_WEB.Models.Modelos.Logistica;
using TSC_WEB.Models.Modelos.Logistica.AcumGerCecoCuenta;
using TSC_WEB.Models.Modelos.Logistica.AcumuladoXCuenta;
using TSC_WEB.Models.Modelos.Logistica.AlterarSituacionOC;
using TSC_WEB.Models.Modelos.Logistica.AprobacionExcedentes;
using TSC_WEB.Models.Modelos.Logistica.AprobacionOC;
using TSC_WEB.Models.Modelos.Logistica.Cubo;
using TSC_WEB.Models.Modelos.Logistica.Graficos;
using TSC_WEB.Models.Modelos.Logistica.OCPendienteCierre;
using TSC_WEB.Models.Modelos.Logistica.OCPendienteFirma;
using TSC_WEB.Models.Modelos.Logistica.PlanContable;
using TSC_WEB.Models.Modelos.Sistema;
using TSC_WEB.Util.Sistema;
using Rotativa;

using TSC_WEB.Models.Modelos.Logistica.VisualizacionOC;

namespace TSC_WEB.Controllers
{
    public class LogisticaController : Controller
    {

        #region INSTANCIAS

        AprobacionOcModelo objAprobacionOcM = new AprobacionOcModelo();
        AlterarSituacionOcModelo objAlterarSituacionOcM = new AlterarSituacionOcModelo();
        UsuariosModelo objUsuariosM = new UsuariosModelo();
        SegAvioProgModelo objsegavioprg = new SegAvioProgModelo();
        SegAvioProgEntidad objsegavioent = new SegAvioProgEntidad();
        VisualizacionOC objVvisualizarOC = new VisualizacionOC();

        BSMovimientoPorPeriodo ObjBSMovPeriodo = new BSMovimientoPorPeriodo();
        static List<EBMovimientoPorPeriodo> EntidadMovimientoPorPeriodo = new List<EBMovimientoPorPeriodo>();
        static List<EBMovimientoPorPeriodo> EntidadSaldoFinal = new List<EBMovimientoPorPeriodo>();
        static List<EBMovimientoPorPeriodo> EntidadTotal = new List<EBMovimientoPorPeriodo>();

        EBSegOCMes objSegOcMes = new EBSegOCMes();
        List<EBListarCentroCosto> ListCCosto = new List<EBListarCentroCosto>();
        static List<EBSegOCMes> LisSegOCMes = new List<EBSegOCMes>();
        static List<SegAvioProgEntidad> LisSegAvioPrg = new List<SegAvioProgEntidad>();

        private static List<EBCuboInfoActualizado> EntidadCuboInformacionActualizado = new List<EBCuboInfoActualizado>();
        CuboModel ObjBSCuboInfoActualizado = new CuboModel();


        #endregion

        #region VISTAS

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AprobacionOCold()
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
        public ActionResult AprobacionOC()
        {
            if (Session["usuario"] != null)
            {
                return View(objAprobacionOcM.ListarOcLiberarV2(Session["usuario"].ToString(),
                            Convert.ToInt32(Session["empresa"].ToString())));
            }
            else
            {
                return Redirect("/");
            }
        }
        public ActionResult AlterarSituacion()
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
        public ActionResult AsignacionPresupuesto()
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
        public ActionResult AprobacionExcedente()
        {

            if (URL_EXTERNO.URL == null) { URL_EXTERNO.URL = Request.Url.OriginalString; }


            if (Session["usuario"] != null)
            {
                AprobExcOcModelo modelo = new AprobExcOcModelo();
                var list = modelo.UsuariosPermitidos();
                string usuarioLogin = Session["usuario"].ToString();

                if (list.Exists(x => x == usuarioLogin))
                {
                    URL_EXTERNO.URL = null;
                    return View();
                }
                else
                {
                    URL_EXTERNO.URL = null;
                    return Redirect("/");
                }
            }
            else
            {
                return Redirect("/");
            }
        }
        public ActionResult RegistroConsumo()
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
        public ActionResult AcumuladoXCuenta()
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
        public ActionResult AcumGerCecoCuenta()
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

        //public ActionResult OcPendientesFirma()
        //{
        //    if (Session["usuario"] != null)
        //    {
        //        return View(objAprobacionOcM.ListarOcPendientesLiberar(0, string.Empty));
        //    }
        //    else
        //    {
        //        return Redirect("/");
        //    }
        //}

        public ActionResult OCPendientesFirma()
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
        public ActionResult Graficos()
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
        public ActionResult OcPendienteCierre()
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
        public ActionResult SegAvioProgra()
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
        public ActionResult Seg_OcMes()
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
        public ActionResult Cubo()
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

        //Visualizacion de OC
        public ActionResult VisualizacionOc()
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

        #endregion

        #region METODOS
        [HttpGet]
        //CAMBIA ESTADO DETALLE DE ORDEN COMPRA
        public JsonResult ValidarLiberacion(int ordencompra, int empresa, string usuario)
        {
            AprobacionOcModelo ocModelo = new AprobacionOcModelo();
            return Json(ocModelo.CambiarEstadoDetalle(ordencompra, empresa, usuario), JsonRequestBehavior.AllowGet);
        }


        ///VISUALIZACION OC
        [HttpGet]
        public JsonResult VisualizarOcCabezera(int ordencompra, decimal? porcetela)
        {
            var DetalleOcImpresion = objVvisualizarOC.ListarReporteOC_1(ordencompra, porcetela);
            var CabezeraOCImpresion = objVvisualizarOC.ListarReporteOC_Cabezera(ordencompra, Session["usuario"].ToString());

            ImpresionOC ImpresionOcView = new ImpresionOC();
            ImpresionOcView.Cab = CabezeraOCImpresion;
            ImpresionOcView.Det = DetalleOcImpresion;

            Session["imprimiroc"] = ImpresionOcView;

            return Json(objVvisualizarOC.ListarReporteOC_1(ordencompra, porcetela), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult HeaderImpresionOC(VisualizacionOcCab Impresionocpag)
        {
            return View(Impresionocpag);
        }

        public ActionResult FooterImpresionOC()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ImpresionOC()
        {
            if (Session["usuario"] != null)
            {
                ImpresionOC resultado = (ImpresionOC)Session["imprimiroc"];
                //ASIGNAMOS 
                //resultado.op = Session["opfichatecnica"] == "true" ? true : false;

                string _headerUrl = Url.Action("HeaderImpresionOC", "Logistica", resultado.Cab, "http");
                string _footerUrl = Url.Action("FooterImpresionOC", "Logistica", null, "http");


                return new ViewAsPdf("ImpresionOC", resultado)
                {

                    CustomSwitches = "--header-html " + _headerUrl + " --header-spacing 13 "
                    + "--footer-html " + _footerUrl + " --footer-spacing 0"
                    ,
                    PageMargins = { Top = 50, Bottom = 40 },
                };
            }
            else
            {
                return Redirect("/");
            }
        }

        /// FIN DE VISUALISAR OC 





        [HttpGet]
        public JsonResult ListarOCAlterarSituacion()
        {
            bool isRedirect = false;
            string redirectUrl = "";
            IEnumerable<ListaAlterarSituacionCab> lista = null;
            var valor = Session["usuario"].ToString();

            try
            {
                if (Session["usuario"] != null)
                {
                    AlterarSituacionOcModelo modelo = new AlterarSituacionOcModelo();
                    lista = modelo.ListarOcAlterarSituacion(Session["usuario"].ToString());
                    isRedirect = false;
                    redirectUrl = "";
                }
                else
                {
                    redirectUrl = "/login/index";
                    isRedirect = true;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return Json(new
            {
                lista = lista,
                isRedirect = isRedirect,
                redirectUrl = redirectUrl,
            }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult ListarOcExcentePendientes(int codperiodo, int codgerencia)
        {
            bool isRedirect = false;
            string redirectUrl = "";
            bool estado = false;
            IEnumerable<ListaPendiente> lista = null;
            string mensaje = "";

            try
            {
                if (Session["usuario"] != null)
                {
                    AprobExcOcModelo modelo = new AprobExcOcModelo();
                    lista = modelo.ListarOcExcentePendientes(codgerencia, codperiodo, Session["usuario"].ToString()).ToList();
                    estado = true;
                    isRedirect = false;
                    redirectUrl = "";
                    mensaje = "";
                }
                else
                {
                    estado = false;
                    lista = null;
                    redirectUrl = "/login/index";
                    isRedirect = true;
                    mensaje = "";
                }
            }
            catch (Exception e)
            {
                isRedirect = true;
                mensaje = e.Message;
            }

            return Json(new
            {
                lista = lista,
                estado = estado,
                isRedirect = isRedirect,
                redirectUrl = redirectUrl,
                mensaje = mensaje
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        //VALIDA USUARIO
        public JsonResult ValidarUsuario(string usuario, string clave)
        {

            var rpt = objUsuariosM.Login(usuario, clave, false);

            if (rpt.usuario != null)
            {
                rpt = objUsuariosM.Login(usuario, clave, true);
                if (rpt.usuario != null)
                {
                    return Json(new { result = true, mensaje = "Acceso concedido" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = false, mensaje = "Clave incorrecta" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { result = false, mensaje = "Usuario no existe" }, JsonRequestBehavior.AllowGet);
            }
        }

        //LISTA CABECERA
        [HttpGet]
        public JsonResult ListarCabeceraOC(int ordencompra)
        {
            return Json(objAprobacionOcM.ListarCabecera(ordencompra), JsonRequestBehavior.AllowGet);
        }

        //LISTA DETALLE
        [HttpGet]
        public JsonResult ListarDetalleOC(int ordencompra)
        {
            return Json(objAprobacionOcM.ListarDetalle(ordencompra), JsonRequestBehavior.AllowGet);
        }




        //LISTA DETALLE - ALTERAR SITUACION
        [HttpGet]
        public JsonResult ListarDetalleOCAlterSit(int ordencompra)
        {
            return Json(objAlterarSituacionOcM.ListarDetalleOCAlter(ordencompra), JsonRequestBehavior.AllowGet);
        }

        //OBTIENE PERIODO ACTUAL
        [HttpGet]
        public JsonResult DetalleCabecera(int ordencompra)
        {
            return Json(objAprobacionOcM.DetalleCabecera(ordencompra), JsonRequestBehavior.AllowGet);
        }

        //OBTIENE GERENCIAS
        [HttpGet]
        public JsonResult ListarGerencias()
        {
            AlterarSituacionOcModelo modelo = new AlterarSituacionOcModelo();
            return Json(modelo.ListarCbxGerencias(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPlanContable(int codperiodo, int codgerencia)
        {
            PlanContableModelo modelo = new PlanContableModelo();
            return Json(modelo.ListaPlanContable(codperiodo, codgerencia), JsonRequestBehavior.AllowGet);
        }



        //OBTIENE RESUMEN INDICADORES.
        [HttpGet]
        public JsonResult ListarResumen(int codgerencia, int codperiodo)
        {
            return Json(objAlterarSituacionOcM.ListarResumen(
                    codgerencia,
                    codperiodo),
                    JsonRequestBehavior.AllowGet);
        }


        // OBTIENE PERIODO DE PRESUPUESTO.
        [HttpGet]
        public JsonResult ListarPeriodos()
        {
            AlterarSituacionOcModelo objModel = new AlterarSituacionOcModelo();
            return Json(objModel.ListarCbxPeriodo(), JsonRequestBehavior.AllowGet);
        }

        //LIBERAR OC
        [HttpGet]
        public JsonResult LiberarOC(int ordencompra, int empresa, string usuario)
        {
            return Json(objAprobacionOcM.LiberarOc(ordencompra, usuario, empresa), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LiberarOcMasivo(int[] ordenes)
        {
            int contador = 0;
            string mensaje = string.Empty;
            bool rpt = true;
            try
            {
                for (int x = 0; x < ordenes.Length; x++)
                {
                    var obj = objAprobacionOcM.LiberarOc(ordenes[x],
                                                         Session["usuario"].ToString(),
                                                         Convert.ToInt32(Session["empresa"].ToString()));
                    if (obj.respuestabool)
                    {
                        contador++;
                    }
                }

                mensaje = contador == ordenes.Length ? "Se liberaron todas las ordenes" : "Se liberaron: " + contador.ToString() + "ordenes de un total de :" + ordenes.Length.ToString();
                rpt = true;
            }
            catch (Exception e)
            {
                mensaje = e.Message;
                rpt = false;
            }

            return Json(new { mensaje = mensaje, success = rpt });
        }

        //RECHAZAR
        [HttpPost]
        public JsonResult RechazarOcMasivo(int[] ordenes, string motivo)
        {
            int contador = 0;
            string mensaje = string.Empty;
            bool rpt = true;
            bool isRedirect = false;
            string redirectUrl = "";
            bool estado = false;

            try
            {
                if (Session["usuario"] != null)
                {
                    for (int x = 0; x < ordenes.Length; x++)
                    {
                        var obj = objAprobacionOcM.RechazarOcV2(ordenes[x],
                                                                Session["usuario"].ToString(),
                                                                motivo);
                        if (obj.respuestabool)
                        {
                            contador++;
                        }
                    }

                    mensaje = contador == ordenes.Length ? "Se Recharazon todas las ordenes" : "Se liberaron: " + contador.ToString() + "ordenes de un total de :" + ordenes.Length.ToString();
                    rpt = true;
                    estado = true;
                    isRedirect = false;
                    redirectUrl = "";
                    mensaje = "";

                }
                else
                {
                    estado = false;
                    redirectUrl = "/login/index";
                    isRedirect = true;
                    mensaje = "";
                }

            }
            catch (Exception e)
            {
                mensaje = e.Message;
                rpt = false;
            }


            return Json(new
            {
                estado = estado,
                isRedirect = isRedirect,
                redirectUrl = redirectUrl,
                mensaje = mensaje,
                success = rpt

            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AlterarSituacionOcMasivo(ParamAlterSit[] parametros)
        {
            int contador = 0;
            string mensaje = "";
            string mensajedet = "";
            string oc_error = "";
            bool rpt = true;
            int opcion_mensje = 0;
            int id_status = 0;
            int oc = 0;
            bool isRedirect = false;
            string redirectUrl = "";

            try
            {
                if (Session["usuario"] != null)
                {
                    for (int x = 0; x < parametros.Length; x++)
                    {
                        if (parametros[x].gerencia.Trim() != "0" && parametros[x].periodo.Trim() != "0")
                        {
                            AlterarSituacionOcModelo modelo = new AlterarSituacionOcModelo();
                            var validacion = modelo.AlterSituacionValidacion(Convert.ToInt32(parametros[x].oc), Convert.ToInt32(parametros[x].periodo));

                            if (validacion.id_status == 1)
                            {
                                var obj = modelo.AlterarSituacionOc(Convert.ToInt32(parametros[x].oc), Session["usuario"].ToString(), Convert.ToInt32(parametros[x].periodo));

                                if (obj.id_status == 1)
                                {
                                    contador++;
                                }
                                else
                                {
                                    rpt = false;
                                    mensaje = obj.desc_status;
                                    mensajedet = obj.desc_status;
                                    oc_error = parametros[x].oc.ToString();
                                    break;
                                }
                            }
                            else
                            {
                                oc = Convert.ToInt32(parametros[x].oc);
                                id_status = validacion.id_status;
                                mensaje = validacion.desc_status;
                                mensajedet = validacion.desc_status;
                                rpt = false;
                                opcion_mensje = 1;
                            }
                        }
                    }

                    if (opcion_mensje == 0)
                    {
                        if (contador == parametros.Length)
                        {
                            mensaje = "Se alteró la situación de todas las OC";
                            mensajedet = "Textile Sourcing Company";
                        }
                        else
                        {
                            mensaje = "La Orden de Compra, no se altero de situación.";
                            rpt = false;
                        }
                    }
                }
                else
                {
                    isRedirect = true;
                    redirectUrl = "/login/index";
                }
            }
            catch (Exception e)
            {
                mensaje = "Controller Alterar: " + e.Message;
                mensajedet = e.StackTrace;
                rpt = false;
            }

            return Json(new
            {
                isRedirect = isRedirect,
                redirectUrl = redirectUrl,
                mensaje = mensaje,
                success = rpt,
                id_estado = id_status,
                oc = oc,
                mensajedet = mensajedet
            });
        }



        [HttpPost]
        public JsonResult AlterarSituacionOcMassivo(int[] ordenes, int codperiodo)
        {
            int contador = 0;
            string mensaje = "";
            string mensajedet = "";
            string oc_error = "";
            bool rpt = true;
            int opcion_mensje = 0;
            int id_status = 0;
            int oc = 0;
            bool isRedirect = false;
            string redirectUrl = "";

            try
            {
                if (Session["usuario"] != null)
                {
                    for (int x = 0; x < ordenes.Length; x++)
                    {
                        AlterarSituacionOcModelo modelo = new AlterarSituacionOcModelo();
                        var validacion = modelo.AlterSituacionValidacion(ordenes[x], codperiodo);

                        if (validacion.id_status == 1)
                        {
                            var obj = modelo.AlterarSituacionOc(ordenes[x], Session["usuario"].ToString(), codperiodo);

                            if (obj.id_status == 1)
                            {
                                contador++;
                            }
                            else
                            {
                                rpt = false;
                                mensaje = obj.desc_status;
                                mensajedet = obj.desc_status;
                                oc_error = ordenes[x].ToString();
                                break;
                            }
                        }
                        else
                        {
                            oc = ordenes[x];
                            id_status = validacion.id_status;
                            mensaje = validacion.desc_status;
                            mensajedet = validacion.desc_status;
                            rpt = false;
                            opcion_mensje = 1;
                        }
                    }

                    if (opcion_mensje == 0)
                    {
                        if (contador == ordenes.Length)
                        {
                            mensaje = "Se alteró la situación de todas las OC";
                            mensajedet = "Textile Sourcing Company";
                        }
                        else
                        {
                            mensaje = "La Orden de Compra: " + oc_error + ", no se altero de situación.";
                        }
                    }
                }
                else
                {
                    isRedirect = true;
                    redirectUrl = "/login/index";
                }
            }
            catch (Exception e)
            {
                mensaje = "Controller Alterar: " + e.Message;
                mensajedet = e.StackTrace;
                rpt = false;
            }

            return Json(new
            {
                isRedirect = isRedirect,
                redirectUrl = redirectUrl,
                mensaje = mensaje,
                success = rpt,
                id_estado = id_status,
                oc = oc,
                mensajedet = mensajedet
            });
        }

        [HttpPost]
        public JsonResult RegistrarSoliExc(int ordencompra, string motivo)
        {
            string mensaje = "";
            bool rpt = true;
            bool isRedirect = false;
            string redirectUrl = "";

            try
            {
                if (Session["usuario"] != null)
                {
                    AlterarSituacionOcModelo modelo = new AlterarSituacionOcModelo();
                    var obj = modelo.RegistrarSoliExc(ordencompra,
                                                      Session["usuario"].ToString(),
                                                      motivo);

                    if (obj.id_status == 0)
                    {
                        mensaje = obj.desc_status;
                        rpt = false;
                    }
                    else
                    {
                        // Enviar Correo.
                        modelo = new AlterarSituacionOcModelo();
                        var objEmail = modelo.OC_EnviarEmail(ordencompra, Session["usuario"].ToString(), motivo);

                        if (objEmail.id_status == 0)
                        {
                            mensaje = obj.desc_status;
                            rpt = false;
                        }
                        else
                        {
                            mensaje = obj.desc_status;
                            rpt = true;
                        }
                    }
                }
                else
                {
                    isRedirect = true;
                    redirectUrl = "/login/index";
                }
            }
            catch (Exception e)
            {
                mensaje = e.Message;
                rpt = false;
            }
            return Json(new
            {
                isRedirect = isRedirect,
                redirectUrl = redirectUrl,
                mensaje = mensaje,
                success = rpt
            });
        }

        [HttpGet]
        public JsonResult ListarPlanContDetalle(int codpedcompra, int codperiodo, int codcc)
        {
            AlterarSituacionOcModelo objModel = new AlterarSituacionOcModelo();
            return Json(objModel.ListarPlanContDetalle(codpedcompra, codperiodo, codcc),
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarConsumoPlan(int codperiodo, int codcc, int codplan, decimal consumo)
        {
            string mensaje = string.Empty;
            bool rpt = true;

            PlanContableModelo modelo = new PlanContableModelo();
            try
            {
                var obj = modelo.RegistrarConsumo(codperiodo,
                                                  codcc,
                                                  codplan,
                                                  consumo,
                                                  Session["usuario"].ToString());
                if (obj.id_status == 0)
                {
                    mensaje = obj.desc_status;
                    rpt = false;
                }
                else
                {
                    mensaje = obj.desc_status;
                    rpt = true;
                }
            }
            catch (Exception e)
            {
                mensaje = e.Message;
                rpt = false;
            }
            return Json(new { mensaje = mensaje, success = rpt });
        }

        [HttpPost]
        public JsonResult AprobacionRechazo(int periodo, int ordencompra, int estado, string observacion)
        {
            string mensaje = "";
            string mensaje_det = "";
            bool rpt = true;
            bool isRedirect = false;
            string redirectUrl = "";

            try
            {
                if (Session["usuario"] != null)
                {
                    AprobExcOcModelo modelo = new AprobExcOcModelo();

                    var obj = modelo.AprobacionRechazo(periodo,
                                                        ordencompra,
                                                        Session["usuario"].ToString(),
                                                        estado,
                                                        observacion);
                    if (obj.id_status == 0)
                    {
                        mensaje = obj.desc_status;
                        mensaje_det = "";
                        rpt = false;
                    }
                    else
                    {
                        mensaje = obj.desc_status;
                        rpt = true;

                        var objEmail = modelo.EnviarEmail(estado, ordencompra, Session["usuario"].ToString(), observacion);

                        if (objEmail.id_status == 0)
                        {
                            mensaje = obj.desc_status;
                            mensaje_det = "";
                            rpt = false;
                        }
                        else
                        {
                            mensaje_det = obj.desc_status;
                            rpt = true;
                        }
                    }
                }
                else
                {
                    isRedirect = true;
                    redirectUrl = "/login/index";
                }

            }
            catch (Exception e)
            {
                mensaje = e.Message;
                mensaje_det = "";
                rpt = false;
            }

            return Json(new
            {
                isRedirect = isRedirect,
                redirectUrl = redirectUrl,
                mensaje = mensaje,
                success = rpt,
                mensaje_det = mensaje_det
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarAprobados(int codperiodo)
        {
            AprobExcOcModelo modelo = new AprobExcOcModelo();
            return Json(modelo.ListarAprobados(codperiodo, Session["usuario"].ToString()), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarRechazados(int codperiodo)
        {
            AprobExcOcModelo modelo = new AprobExcOcModelo();
            return Json(modelo.ListarRechazados(codperiodo, Session["usuario"].ToString()), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult InformacionExcedente(int periodo, int codgerencia, int codcc, int pedido)
        {
            AprobExcOcModelo modelo = new AprobExcOcModelo();
            return Json(modelo.InformacionExcedente(periodo, codgerencia, codcc, pedido),
                JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public FileResult ReporteExcelAprobados(DateTime fechaIni, DateTime fechaFin)
        {
            AlterarSituacionOcModelo objModel = new AlterarSituacionOcModelo();
            var lista = objModel.ListarExcelAprobados(Session["usuario"].ToString(), fechaIni, fechaFin);
            return File(objModel.ExcelAprobados(lista), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OC_Aprobadas_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }


        [HttpGet]
        public FileResult ReporteExcelOCPendiente()
        {
            AlterarSituacionOcModelo objModel = new AlterarSituacionOcModelo();
            var lista = objModel.ListarOcAlterarSituacionExcel(Session["usuario"].ToString());
            return File(objModel.ExcelOCPendiente(lista), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OC_Pendientes_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }


        [HttpPost]
        public JsonResult ListarSecciones(int[] periodos, int[] secciones)
        {
            List<ListaSeccion> lista = new List<ListaSeccion>();
            List<ListaSeccion> lista_temp = null;
            AcumuladoXCuentaModelo modelo = new AcumuladoXCuentaModelo();
            List<int> ListaCodSeccion = new List<int>();

            if (secciones != null)
            {
                for (int j = 0; j < secciones.Length; j++)
                {
                    ListaCodSeccion.Add(secciones[j]);
                }
            }

            if (periodos != null)
            {
                for (int i = 0; i < periodos.Length; i++)
                {
                    lista_temp = new List<ListaSeccion>();
                    lista_temp = modelo.ListarSeccionMult(periodos[i]);

                    foreach (var item in lista_temp)
                    {
                        if (!ListaCodSeccion.Contains(item.CODSECCION))
                        {
                            lista.Add(new ListaSeccion
                            {
                                CODSECCION = item.CODSECCION,
                                SECCION = item.SECCION
                            });
                            ListaCodSeccion.Add(item.CODSECCION);
                        }
                    }
                }
            }

            ListaCodSeccion.Clear();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarFamilias(int[] periodos, int[] secciones, int[] familias, int opcion)
        {
            AcumuladoXCuentaModelo modelo = new AcumuladoXCuentaModelo();
            List<ListaFamilia> lista_temp = null;
            List<ListaFamilia> lista = new List<ListaFamilia>();
            List<int> ListaCodFamilia = new List<int>();
            string lista_secciones = "";

            if (familias != null && opcion == 1)
            {
                for (int j = 0; j < familias.Length; j++)
                {
                    ListaCodFamilia.Add(familias[j]);
                }
            }

            // Obtener lista de Secciones.
            if (secciones != null)
            {
                for (int i = 0; i < secciones.Length; i++)
                {
                    lista_secciones = lista_secciones + secciones[i].ToString() + ",";
                }
                lista_secciones = lista_secciones.TrimEnd(',');
            }


            if (periodos != null)
            {
                for (int i = 0; i < periodos.Length; i++)
                {
                    lista_temp = new List<ListaFamilia>();

                    lista_temp = modelo.ListarFamiliaMult(periodos[i], lista_secciones);

                    foreach (var item in lista_temp)
                    {
                        if (!ListaCodFamilia.Contains(item.CODFAMILIA))
                        {
                            lista.Add(new ListaFamilia
                            {
                                CODFAMILIA = item.CODFAMILIA,
                                FAMILIA = item.FAMILIA
                            });

                            ListaCodFamilia.Add(item.CODFAMILIA);
                        }
                    }
                }
            }

            ListaCodFamilia.Clear();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult ListarCuenta(int[] periodos, int[] secciones, int[] familias, int[] cuentas, int opcion)
        {
            AcumuladoXCuentaModelo modelo = new AcumuladoXCuentaModelo();
            List<ListaCuenta> lista_temp = null;
            List<ListaCuenta> lista = new List<ListaCuenta>();
            List<int> ListaCodCuenta = new List<int>();

            string lista_secciones = "";
            string lista_familias = "";

            if (cuentas != null && opcion == 1)
            {
                for (int j = 0; j < cuentas.Length; j++)
                {
                    ListaCodCuenta.Add(cuentas[j]);
                }
            }

            // Obtener lista de Secciones.
            if (secciones != null)
            {
                for (int i = 0; i < secciones.Length; i++)
                {
                    lista_secciones = lista_secciones + secciones[i].ToString() + ",";
                }
                lista_secciones = lista_secciones.TrimEnd(',');
            }

            // Obtener lista de Familia.
            if (familias != null)
            {
                for (int i = 0; i < familias.Length; i++)
                {
                    lista_familias = lista_familias + familias[i].ToString() + ",";
                }
                lista_familias = lista_familias.TrimEnd(',');
            }

            if (periodos != null)
            {
                for (int i = 0; i < periodos.Length; i++)
                {
                    lista_temp = new List<ListaCuenta>();

                    lista_temp = modelo.ListarCuentaMult(periodos[i], lista_secciones, lista_familias);

                    foreach (var item in lista_temp)
                    {
                        if (!ListaCodCuenta.Contains(item.CODPLACONT))
                        {
                            lista.Add(new ListaCuenta
                            {
                                CODPLACONT = item.CODPLACONT,
                                PLANCONT = item.PLANCONT
                            });

                            ListaCodCuenta.Add(item.CODPLACONT);
                        }
                    }
                }
            }

            ListaCodCuenta.Clear();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public JsonResult ListarIndicadores(int[] periodos, int[] secciones, int[] familias, int[] cuentas)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            AcumuladoXCuentaModelo modelo = new AcumuladoXCuentaModelo();
            IndicadoresPresupuesto obj_temp = new IndicadoresPresupuesto();

            string lista_secciones = "";
            string lista_familias = "";
            string lista_cuentas = "";

            decimal presupuesto = 0;
            decimal consumido = 0;
            decimal disponible = 0;
            string simbolo = "";

            // Obtener lista de Secciones.
            if (secciones != null)
            {
                for (int i = 0; i < secciones.Length; i++)
                {
                    lista_secciones = lista_secciones + secciones[i].ToString() + ",";
                }
                lista_secciones = lista_secciones.TrimEnd(',');
            }

            // Obtener lista de Familia.
            if (familias != null)
            {
                for (int i = 0; i < familias.Length; i++)
                {
                    lista_familias = lista_familias + familias[i].ToString() + ",";
                }
                lista_familias = lista_familias.TrimEnd(',');
            }

            // Obtener lista de Cuentas.
            if (cuentas != null)
            {
                for (int i = 0; i < cuentas.Length; i++)
                {
                    if (cuentas[i].ToString() == "-1")
                    {
                        lista_cuentas = "-1";
                        break;
                    }
                    else
                    {
                        lista_cuentas = lista_cuentas + cuentas[i].ToString() + ",";
                    }
                }

                lista_cuentas = lista_cuentas.TrimEnd(',');
            }

            // Periodos.
            if (periodos != null)
            {
                for (int i = 0; i < periodos.Length; i++)
                {

                    obj_temp = modelo.ListarIndicadoresMult(periodos[i],
                                                            lista_secciones,
                                                            lista_familias,
                                                            lista_cuentas);

                    presupuesto = presupuesto + obj_temp.PRESUPUESTO;
                    consumido = consumido + obj_temp.CONSUMIDO;
                    disponible = disponible + obj_temp.DISPONIBLE;
                    simbolo = obj_temp.SIMBOLO;
                }
            }

            return Json(new
            {
                PRESUPUESTO = presupuesto.ToString("N", nfi),
                CONSUMIDO = consumido.ToString("N", nfi),
                DISPONIBLE = disponible.ToString("N", nfi),
                SIMBOLO = simbolo
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarAcumuladoMultiple(int[] periodos, int[] secciones, int[] familias, int[] cuentas)
        {
            AcumuladoXCuentaModelo modelo = new AcumuladoXCuentaModelo();
            List<ListaAcumulado> lista_temp = null;
            List<ListaAcumulado> lista = new List<ListaAcumulado>();
            List<int> ListaCodCuenta = new List<int>();

            //VisualizarOSModelo modeloOS = new VisualizarOSModelo();
            //var obj = modeloOS.ActualizarOS();


            // Validacion.
            string codSecciones = "";
            string codFamilias = "";
            string codCuentas = "";

            // 1. Secciones
            if (secciones != null)
            {
                for (int i = 0; i < secciones.Length; i++)
                {
                    codSecciones = codSecciones + secciones[i].ToString() + ",";
                }
                codSecciones = codSecciones.TrimEnd(',');
            }

            // 2. Familias
            if (familias != null)
            {
                for (int i = 0; i < familias.Length; i++)
                {
                    codFamilias = codFamilias + familias[i].ToString() + ",";
                }
                codFamilias = codFamilias.TrimEnd(',');
            }


            // 2. Cuentas
            if (cuentas != null)
            {
                for (int i = 0; i < cuentas.Length; i++)
                {
                    if (cuentas[i].ToString() == "-1")
                    {
                        codCuentas = "-1";
                        break;
                    }
                    else
                    {
                        codCuentas = codCuentas + cuentas[i].ToString() + ",";
                    }
                }
                codCuentas = codCuentas.TrimEnd(',');
            }

            // Construir Lista Principal
            if (periodos != null)
            {
                ListaAcumulado entidad = new ListaAcumulado();

                for (int i = 0; i < periodos.Length; i++)
                {
                    lista_temp = new List<ListaAcumulado>();

                    lista_temp = modelo.ListarAcumuladoMultiple(periodos[i],
                                                                codSecciones,
                                                                codFamilias,
                                                                codCuentas);

                    foreach (var item in lista_temp)
                    {
                        entidad = new ListaAcumulado();
                        entidad.MODULO = item.MODULO;
                        entidad.CODAUTORIZA = item.CODAUTORIZA;
                        entidad.CODIGO = item.CODIGO;
                        entidad.PROVEEDOR = item.PROVEEDOR;

                        if (item.FECHA == "" || item.FECHA == null)
                        {
                            entidad.FECHA = "";
                        }
                        else
                        {
                            entidad.FECHA = Convert.ToDateTime(item.FECHA.ToString()).ToShortDateString();
                        };
                        entidad.SIMBOLO = item.SIMBOLO;
                        entidad.SIMBOLO_S = item.SIMBOLO_S;
                        entidad.SIMBOLO_D = item.SIMBOLO_D;
                        entidad.CC = item.CC;
                        entidad.TIPO_PAGO = item.TIPO_PAGO;
                        entidad.VALOR_SOLES = item.VALOR_SOLES;
                        entidad.VALOR_DOLAR = item.VALOR_DOLAR;
                        entidad.VALOR_CONSUMIDO = item.VALOR_CONSUMIDO;

                        lista.Add(entidad);
                    }
                }
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ActualizarOS()
        {
            AcumuladoXCuentaModelo modelo = new AcumuladoXCuentaModelo();
            string mensaje = "";
            if (modelo.ActualizasOS()) { mensaje = "Correcto"; } else { mensaje = "Error"; };
            return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarCbxGerencias(int[] codPeriodo)
        {
            string codPeriodos = "";

            if (codPeriodo != null)
            {
                for (int i = 0; i < codPeriodo.Length; i++)
                {
                    codPeriodos = codPeriodos + codPeriodo[i].ToString() + ",";
                }
                codPeriodos = codPeriodos.TrimEnd(',');
            }

            AcumGerCecoCuentaModelo modelo = new AcumGerCecoCuentaModelo();

            return Json(modelo.ListarReporteGerencia(codPeriodos)
                , JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ListarReporteCeCo(int[] codperiodo, int codcc, int[] codfamilias)
        {
            string periodos = "";
            string cc = "";
            string familias = "";

            if (codfamilias != null)
            {


                for (int i = 0; i < codperiodo.Length; i++)
                {
                    if (codperiodo[i].ToString() == "-1")
                    {
                        periodos = "-1";
                        break;
                    }
                    else
                    {
                        periodos = periodos + codperiodo[i].ToString() + ",";
                    }
                }
                periodos = periodos.TrimEnd(',');

                //for (int i = 0; i < codcc.Length; i++)
                //{
                //    if (codcc[i].ToString() == "-1")
                //    {
                //        periodos = "-1";
                //        break;
                //    }
                //    else
                //    {
                //        cc = cc + codcc[i].ToString() + ",";
                //    }
                //}
                //cc = cc.TrimEnd(',');



                for (int i = 0; i < codfamilias.Length; i++)
                {
                    if (codfamilias[i].ToString() == "-1")
                    {
                        familias = "-1";
                        break;
                    }
                    else
                    {
                        familias = familias + codfamilias[i].ToString() + ",";
                    }
                }
                familias = familias.TrimEnd(',');


            }

            AcumGerCecoCuentaModelo modelo = new AcumGerCecoCuentaModelo();
            return Json(modelo.ListarReporteCeCo(periodos, codcc, familias)
                , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult ExportarExcelAcumuladoMult(string periodos, string secciones, string familias, string cuentas)
        {

            AcumuladoXCuentaModelo modelo = new AcumuladoXCuentaModelo();
            List<ListaAcumulado> lista_temp = null;
            List<ListaAcumulado> lista = new List<ListaAcumulado>();
            List<int> ListaCodCuenta = new List<int>();
            ListaAcumulado entidad = new ListaAcumulado();

            string[] _array_periodo_pre = periodos.Split(',');
            int[] _array_periodo = new int[_array_periodo_pre.Length];

            for (int i = 0; i < _array_periodo_pre.Length; i++)
            {
                _array_periodo[i] = Convert.ToInt32(_array_periodo_pre[i]);
            }

            for (int i = 0; i < _array_periodo.Length; i++)
            {
                lista_temp = new List<ListaAcumulado>();

                lista_temp = modelo.ListarAcumuladoMultiple(_array_periodo[i],
                                                            secciones,
                                                            familias,
                                                            cuentas);
                foreach (var item in lista_temp)
                {
                    entidad = new ListaAcumulado();

                    entidad.MODULO = item.MODULO;
                    entidad.MODULO = item.MODULO;
                    entidad.CODAUTORIZA = item.CODAUTORIZA;
                    entidad.CODIGO = item.CODIGO;
                    entidad.PROVEEDOR = item.PROVEEDOR;
                    if (item.FECHA == "" || item.FECHA == null) { entidad.FECHA = ""; } else { entidad.FECHA = Convert.ToDateTime(item.FECHA.ToString()).ToShortDateString(); };
                    entidad.SIMBOLO = item.SIMBOLO;
                    entidad.SIMBOLO_S = item.SIMBOLO_S;
                    entidad.SIMBOLO_D = item.SIMBOLO_D;
                    entidad.CC = item.CC;
                    entidad.TIPO_PAGO = item.TIPO_PAGO;
                    entidad.VALOR_SOLES = item.VALOR_SOLES;
                    entidad.VALOR_DOLAR = item.VALOR_DOLAR;
                    entidad.VALOR_CONSUMIDO = item.VALOR_CONSUMIDO;
                    entidad.V_SOLES = item.V_SOLES;
                    entidad.V_DOLAR = item.V_DOLAR;
                    entidad.V_CONSUMIDO = item.V_CONSUMIDO;

                    lista.Add(entidad);
                }
            }
            return File(modelo.ExpExcelAcumuladoMult(lista), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AcumuladoXCuenta" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }










        [HttpGet]
        public JsonResult PeriodosGerCeCoCuenta()
        {
            AcumGerCecoCuentaModelo objModel = new AcumGerCecoCuentaModelo();
            return Json(objModel.ListarCbxPeriodo(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GerenciasGerCeCoCuenta()
        {
            AcumGerCecoCuentaModelo objModel = new AcumGerCecoCuentaModelo();
            return Json(objModel.ListarCbxGerencias(), JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult CeCoGerCeCoCuenta(int[] codperiodo, int codgerencia)
        {

            AcumGerCecoCuentaModelo objModel = new AcumGerCecoCuentaModelo();
            string codPeriodo = "";



            for (int i = 0; i < codperiodo.Length; i++)
            {
                codPeriodo = codPeriodo + codperiodo[i].ToString() + ",";
            }
            codPeriodo = codPeriodo.TrimEnd(',');



            return Json(objModel.ListarCbxCentroCosto(codPeriodo, codgerencia),
                JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult FamiliaGerCeCoCuenta(int[] codperiodo, int codcc)
        {
            AcumGerCecoCuentaModelo objModel = new AcumGerCecoCuentaModelo();

            string codPeriodos = null;


            if (codperiodo != null)
            {
                for (int i = 0; i < codperiodo.Length; i++)
                {
                    codPeriodos = codPeriodos + codperiodo[i].ToString() + ",";
                }
                codPeriodos = codPeriodos.TrimEnd(',');
            }

            return Json(objModel.ListarCbxFamilias(codPeriodos, codcc.ToString()),
                JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ListarAcumuladoXCC(int[] codperiodo, int[] codgerencia)
        {
            AcumGerCecoCuentaModelo modelo = new AcumGerCecoCuentaModelo();

            string codPeriodos = "";
            string codGerencia = "";

            if (codperiodo != null)
            {
                for (int i = 0; i < codperiodo.Length; i++)
                {
                    codPeriodos = codPeriodos + codperiodo[i].ToString() + ",";
                }
                codPeriodos = codPeriodos.TrimEnd(',');
            }

            if (codgerencia != null)
            {
                for (int i = 0; i < codgerencia.Length; i++)
                {
                    codGerencia = codGerencia + codgerencia[i].ToString() + ",";
                }
                codGerencia = codGerencia.TrimEnd(',');
            }

            return Json(modelo.ListarAcumuladoXCC(codPeriodos, codGerencia), JsonRequestBehavior.AllowGet);
        }


        // Reporte de Firmas pendientes de ordenes de compra.
        [HttpGet]
        public async Task<JsonResult> ListarPendienteFirma(string firmas, DateTime fechaIni, DateTime fechaFin)
        {
            if (Session["usuario"] != null)
            {
                OCPendFirmaModel objModel = new OCPendFirmaModel();
                var lista = await objModel.ListarPendienteFirma(firmas,
                                                                Session["usuario"].ToString(),
                                                                Session["empresa"].ToString(),
                                                                fechaIni,
                                                                fechaFin);

                return Json(new
                {
                    lista = lista,
                    isRedirect = false,
                    redirectUrl = ""
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> ListarPendOCDetalle(int oc)
        {
            OCPendFirmaModel objModel = new OCPendFirmaModel();
            return Json(await objModel.ListarDetalleOC(oc), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> ListarPendCboFirmas()
        {
            OCPendFirmaModel objModel = new OCPendFirmaModel();
            return Json(await objModel.ListarCboFirmas(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult ReporteExcelPendientes(string firmas, DateTime fechaIni, DateTime fechaFin)
        {
            OCPendFirmaModel objModel = new OCPendFirmaModel();
            var lista = objModel.ListaReporteExcel(firmas,
                                                    Session["usuario"].ToString(),
                                                    Session["empresa"].ToString(),
                                                    fechaIni,
                                                    fechaFin);

            return File(objModel.ReporteExcel(lista), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PendienteFirmas" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");



        }



        // Reporte de OC Pendiente de Cierre.
        [HttpGet]
        public async Task<JsonResult> ListarPendienteCierre(string niveles,
                                                            DateTime fechaIni,
                                                            DateTime fechaFin)
        {

            OCPendCierreModel objModel = new OCPendCierreModel();

            return Json(await objModel.ListarPendCierre(niveles, fechaIni, fechaFin), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult ReporteExcelPendCierre(string niveles,
                                                DateTime fechaIni,
                                                DateTime fechaFin)
        {
            OCPendCierreModel objModel = new OCPendCierreModel();
            var lista = objModel.ListaReporteExcel(niveles, fechaIni, fechaFin);
            return File(objModel.ReporteExcel(lista), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PendienteCierre" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }

        [HttpGet]
        public JsonResult ListarNivelItems()
        {
            OCPendCierreModel objModel = new OCPendCierreModel();
            return Json(objModel.ListarNivelItems(), JsonRequestBehavior.AllowGet);
        }



        // Aprobacion OC - Lista de Aprobados.
        [HttpGet]
        public async Task<JsonResult> ListarAprobTercFirma(DateTime fechaIni, DateTime fechaFin)
        {
            bool isRedirect = false;
            string redirectUrl = "";
            IEnumerable<Lista2_AprobTercFirma> lista = null;

            try
            {
                if (Session["usuario"] != null)
                {
                    AprobacionOcModelo modelo = new AprobacionOcModelo();
                    lista = await modelo.ListaAprobadas(Session["usuario"].ToString(),
                                                        Convert.ToInt32(Session["empresa"].ToString()),
                                                        fechaIni,
                                                        fechaFin);
                    isRedirect = false;
                    redirectUrl = "";
                }
                else
                {
                    redirectUrl = "/login/index";
                    isRedirect = true;
                }
            }
            catch (Exception e)
            {
                isRedirect = true;
            }

            return Json(new
            {
                lista = lista,
                isRedirect = isRedirect,
                redirectUrl = redirectUrl,
            }, JsonRequestBehavior.AllowGet);
        }


        // Aprobacion OC - Lista de Aprobados.
        [HttpGet]
        public async Task<JsonResult> ListarAprobTercFirmaDet(int ordencompra)
        {
            AprobacionOcModelo objModel = new AprobacionOcModelo();
            return Json(await objModel.ListaDetOCTerFirma(ordencompra), JsonRequestBehavior.AllowGet);
        }



        // Aprobacion OC - Lista de Aprobados.  -----------------------------------------------------------------------------------
        [HttpGet]
        public FileResult ReportExcel3eraF_Aprob(DateTime fechaIni, DateTime fechaFin)
        {
            AprobacionOcModelo modelo = new AprobacionOcModelo();

            var lista = modelo.ListaExcelAprobados(Session["usuario"].ToString(),
                                                        Convert.ToInt32(Session["empresa"].ToString()),
                                                        fechaIni,
                                                        fechaFin);

            return File(modelo.ReporteExcelAprob(lista),
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "OC_Aprobados_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");

        }

        // Aprobacion OC - Lista de Aprobados.  -----------------------------------------------------------------------------------
        [HttpGet]
        public FileResult ReportExcel3eraF_Rech(DateTime fechaIni, DateTime fechaFin)
        {
            AprobacionOcModelo modelo = new AprobacionOcModelo();

            var lista = modelo.ListaExcelRechazados(Session["usuario"].ToString(),
                                                    Convert.ToInt32(Session["empresa"].ToString()),
                                                    fechaIni,
                                                    fechaFin);

            return File(modelo.ReporteExcelRech(lista),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "OC_Rechazados_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");

        }


        // Aprobacion OC - Lista de Rechazados
        [HttpGet]
        public async Task<JsonResult> ListarRechTercFirma(DateTime fechaIni, DateTime fechaFin)
        {
            bool isRedirect = false;
            string redirectUrl = "";
            IEnumerable<Lista3_RechTercFirma> lista = null;

            try
            {
                if (Session["usuario"] != null)
                {
                    AprobacionOcModelo modelo = new AprobacionOcModelo();
                    lista = await modelo.ListaRechazados(Session["usuario"].ToString(),
                                                        Convert.ToInt32(Session["empresa"].ToString()),
                                                        fechaIni,
                                                        fechaFin);
                    isRedirect = false;
                    redirectUrl = "";
                }
                else
                {
                    redirectUrl = "/login/index";
                    isRedirect = true;
                }
            }
            catch (Exception e)
            {
                isRedirect = true;
            }

            return Json(new
            {
                lista = lista,
                isRedirect = isRedirect,
                redirectUrl = redirectUrl,
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<JsonResult> AlterSitListaAprobados(DateTime fechaIni, DateTime fechaFin)
        {
            bool isRedirect = false;
            string redirectUrl = "";
            IEnumerable<Lista9_Aprobados> lista = new List<Lista9_Aprobados>();

            try
            {
                if (Session["usuario"] != null)
                {
                    AlterarSituacionOcModelo modelo = new AlterarSituacionOcModelo();
                    lista = await modelo.ListarAprobados(Session["usuario"].ToString(),
                                                        fechaIni,
                                                        fechaFin);
                    isRedirect = false;
                    redirectUrl = "";
                }
                else
                {
                    redirectUrl = "/login/index";
                    isRedirect = true;
                }
            }
            catch (Exception e)
            {
                isRedirect = true;
            }

            return Json(new
            {
                lista = lista,
                isRedirect = isRedirect,
                redirectUrl = redirectUrl,
            }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public async Task<JsonResult> AltSit_ListarTotales(int periodo)
        {

            bool isRedirect = false;
            string redirectUrl = "";
            IEnumerable<Lista10_Totales> lista = null;

            try
            {
                if (Session["usuario"] != null)
                {
                    AlterarSituacionOcModelo objModel = new AlterarSituacionOcModelo();
                    lista = await objModel.ListarTotales(periodo, Session["usuario"].ToString());
                    isRedirect = false;
                    redirectUrl = "";
                }
                else
                {
                    redirectUrl = "/login/index";
                    isRedirect = true;
                }
            }
            catch (Exception e)
            {
                isRedirect = true;
            }

            return Json(new
            {
                lista = lista,
                isRedirect = isRedirect,
                redirectUrl = redirectUrl,
            }, JsonRequestBehavior.AllowGet);
        }

        // Movimiento por Periodo.















        #region Graficos

        [HttpGet]
        public async Task<JsonResult> Graf_ListarPeriodos()
        {
            GraficoModelo objModel = new GraficoModelo();
            return Json(await objModel.ListarPeriodos(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> Graf_ListarGerencias()
        {
            GraficoModelo objModel = new GraficoModelo();
            return Json(await objModel.ListarGerencias(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> Graf_ListarCeCo(string pCodGerencias)
        {
            GraficoModelo objModel = new GraficoModelo();
            return Json(await objModel.ListarCeCo(pCodGerencias), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> Graf_ListarFamilia(string pCodPeriodos, string pCodGerencias, string pCodCeCo)
        {
            GraficoModelo objModel = new GraficoModelo();
            return Json(await objModel.ListarFamilias(pCodPeriodos, pCodGerencias, pCodCeCo), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<JsonResult> Graf_Grafico1_Lineas(string pCodPeriodos, string pCodGerencias, string pCodCeCo, string pCodFamilia)
        {
            GraficoModelo objModel = new GraficoModelo();

            string vLabel1 = "Presupuesto";
            string vLabel2 = "Comprado";

            List<Grafico1Lineas> lista = await objModel.Grafico1Linea(pCodPeriodos, pCodGerencias, pCodCeCo, pCodFamilia);

            string[] vLabels = new string[lista.Count];
            decimal[] vData1 = new decimal[lista.Count];
            decimal[] vData2 = new decimal[lista.Count];


            for (int i = 0; i < lista.Count; i++)
            {
                vLabels[i] = lista[i].PERIODO;
                vData1[i] = lista[i].PRESUPUESTO;
                vData2[i] = lista[i].CONSUMIDO;
            }


            return Json(new
            {
                vLabels = vLabels,
                vData1 = vData1,
                vData2 = vData2,
                vLabel1 = vLabel1,
                vLabel2 = vLabel2
            }, JsonRequestBehavior.AllowGet); ;

        }


        [HttpGet]
        public async Task<JsonResult> Graf_Grafico2_Torta(string pCodPeriodos, string pCodGerencias, string pCodCeCo)
        {
            GraficoModelo objModel = new GraficoModelo();

            List<Grafico2TortaPresupuesto> listaPre = await objModel.Grafico2TortaPresupuesto(pCodPeriodos, pCodGerencias, pCodCeCo);
            List<Grafico2TortaConsumido> listaCon = await objModel.Grafico2TortaConsumido(pCodPeriodos, pCodGerencias, pCodCeCo);

            string[] vLabelsPre = new string[listaPre.Count];
            decimal[] vDataPre = new decimal[listaPre.Count];
            string[] vColorPre = new string[listaPre.Count];
            Random rndPre = new Random();

            for (int i = 0; i < listaPre.Count; i++)
            {
                vLabelsPre[i] = listaPre[i].FAMILIA;
                vDataPre[i] = listaPre[i].PRESUPUESTO;
                vColorPre[i] = "rgb(" + rndPre.Next(1, 255) + ", " + rndPre.Next(1, 255) + ", " + rndPre.Next(1, 255) + ")";
                listaPre[i].COLOR = vColorPre[i];
            }

            string[] vLabelsCon = new string[listaCon.Count];
            decimal[] vDataCon = new decimal[listaCon.Count];
            string[] vColorCon = new string[listaCon.Count];
            Random rndCon = new Random();
            int codFamilia = 0;

            for (int i = 0; i < listaCon.Count; i++)
            {
                vLabelsCon[i] = listaCon[i].FAMILIA;
                vDataCon[i] = listaCon[i].CONSUMIDO;
                codFamilia = listaCon[i].CODFAMILIA;

                for (int j = 0; j < listaPre.Count; j++)
                {
                    if (listaPre[j].CODFAMILIA == codFamilia)
                    {
                        vColorCon[i] = listaPre[j].COLOR;
                        break;
                    }
                    else
                    {
                        vColorCon[i] = "rgb(" + rndCon.Next(1, 255) + ", " + rndCon.Next(1, 255) + ", " + rndCon.Next(1, 255) + ")";
                    }
                }
            }


            return Json(new
            {
                labelPre = vLabelsPre,
                dataPre = vDataPre,
                colorPre = vColorPre,

                labelCon = vLabelsCon,
                dataCon = vDataCon,
                colorCon = vColorCon

            }, JsonRequestBehavior.AllowGet); ;

        }

        [HttpGet]
        public async Task<JsonResult> Graf_Grafico2_TortaV2(string pCodPeriodos, string pCodGerencias, string pCodCeCo)
        {
            GraficoModelo objModel = new GraficoModelo();

            List<Grafico2TortaPresupuesto> listaPre = await objModel.Grafico2TortaPresupuesto(pCodPeriodos, pCodGerencias, pCodCeCo);
            List<Grafico2TortaConsumido> listaCon = await objModel.Grafico2TortaConsumido(pCodPeriodos, pCodGerencias, pCodCeCo);

            string[] vLabelsPre = new string[listaPre.Count];
            decimal[] vDataPre = new decimal[listaPre.Count];
            string[] vColorPre = new string[listaPre.Count];
            Random rndPre = new Random();

            for (int i = 0; i < listaPre.Count; i++)
            {
                vLabelsPre[i] = listaPre[i].FAMILIA;
                vDataPre[i] = listaPre[i].PRESUPUESTO;
                vColorPre[i] = "rgb(" + rndPre.Next(1, 255) + ", " + rndPre.Next(1, 255) + ", " + rndPre.Next(1, 255) + ")";
                listaPre[i].COLOR = vColorPre[i];
            }

            string[] vLabelsCon = new string[listaPre.Count];
            decimal[] vDataCon = new decimal[listaPre.Count];
            string[] vColorCon = new string[listaPre.Count];
            Random rndCon = new Random();


            for (int i = 0; i < listaPre.Count; i++)
            {
                for (int j = 0; j < listaCon.Count; j++)
                {
                    if (listaCon[j].CODFAMILIA == listaPre[i].CODFAMILIA)
                    {
                        vLabelsCon[i] = listaCon[j].FAMILIA;
                        vDataCon[i] = listaCon[j].CONSUMIDO;
                        vColorCon[i] = listaPre[j].COLOR;

                        break;
                    }

                }
            }

            return Json(new
            {
                labelPre = vLabelsPre,
                dataPre = vDataPre,
                colorPre = vColorPre,

                labelCon = vLabelsCon,
                dataCon = vDataCon,
                colorCon = vColorCon

            }, JsonRequestBehavior.AllowGet); ;

        }



        [HttpGet]
        public async Task<JsonResult> Graf_Grafico3_Bar(string pCodPeriodos, string pCodFamilia)
        {
            GraficoModelo objModel = new GraficoModelo();

            List<Grafico3Bar> lista = await objModel.Grafico3Bar(pCodPeriodos, pCodFamilia);

            string[] vLabels = new string[lista.Count];
            decimal[] vData1 = new decimal[lista.Count];
            decimal[] vData2 = new decimal[lista.Count];

            for (int i = 0; i < lista.Count; i++)
            {
                vLabels[i] = lista[i].GERENCIA;
                vData1[i] = lista[i].PRESUPUESTO;
                vData2[i] = lista[i].CONSUMIDO;
            }

            return Json(new
            {
                vLabels = vLabels,
                vData1 = vData1,
                vData2 = vData2
            }, JsonRequestBehavior.AllowGet); ;

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


        [HttpGet]
        public JsonResult ListarSeguimientoAvioProgr(string PPROG, string PPEDIDO, string PPO)
        {
            LisSegAvioPrg = new List<SegAvioProgEntidad>();
            LisSegAvioPrg = objsegavioprg.Listar_SegAvioProg(PPROG, PPEDIDO, PPO);
            return Json(LisSegAvioPrg, JsonRequestBehavior.AllowGet);
        }

        public FileResult ExportarExcelSegOCAvio()
        {
            ExportarExcel_din obj = new ExportarExcel_din();
            return File(obj.ExpExcelSegAvio(LisSegAvioPrg), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SeguimientoAvios" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }


        public FileResult ExportarExcelOC()
        {
            ExportarExcel_din obj = new ExportarExcel_din();
            return File(obj.ExpExcelOCMes(LisSegOCMes), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SeguimientoOcXmES" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
            //return File(ExportarExcel_dinExpExcel(LisSegOCMes), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MovPorPeriodo" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }



        [HttpGet]
        public JsonResult ListarSeg_OcporMes(string P_OC,
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
            //Se crear una referencia a JavaScriptSerializer
            var serializer = new JavaScriptSerializer();
            //Se cambia el Length directo a nuestra referencia
            serializer.MaxJsonLength = 500000000;
            LisSegOCMes = new List<EBSegOCMes>();
            LisSegOCMes = objsegavioprg.ListarSeguiProgAvio(P_OC,
                                                            P_PROVEEDOR,
                                                            P_COMPRADOR,
                                                            P_PROGRAMA,
                                                            P_FAMILIA,
                                                            P_NIVEL,
                                                            P_GRUPO,
                                                            P_SUBGRUPO,
                                                            P_ITEM,
                                                            P_MES_INI,
                                                            P_MES_FIN,
                                                            P_ANIO_INI,
                                                            P_ANIO_FIN,
                                                            P_CCO);

            var var_jason = Json(LisSegOCMes, JsonRequestBehavior.AllowGet);
            var_jason.MaxJsonLength = 500000000;
            return var_jason;
        }
        [HttpGet]
        public JsonResult ListarCCosto()
        {
            ListCCosto = new List<EBListarCentroCosto>();
            ListCCosto = objsegavioprg.ListarCentroCosto();
            var var_jason = Json(ListCCosto, JsonRequestBehavior.AllowGet);
            return var_jason;
        }



        public JsonResult ListarDataCuboInformacionActualizado(DateTime? FECHAINI,
                                                                DateTime? FECHAFIN, string PROVEEDOR, string COMPRADOR
                                         , string ALMACEN, string OC)
        {
            //Se crear una referencia a JavaScriptSerializer
            var serializer = new JavaScriptSerializer();
            //Se cambia el Length directo a nuestra referencia
            serializer.MaxJsonLength = 500000000;

            EntidadCuboInformacionActualizado = ObjBSCuboInfoActualizado.ListarCuboInformacion(FECHAINI, FECHAFIN, PROVEEDOR, COMPRADOR, ALMACEN, OC);
            var var_jason = Json(EntidadCuboInformacionActualizado, JsonRequestBehavior.AllowGet);
            // Excel();//para probar primero antes de crear un boton nuevo de exportar.
            var_jason.MaxJsonLength = 500000000;
            return var_jason;
            //return Json(ObjBSMovPeriodo.ListarMovimientoPorPeriodo(FECHAINI,FECHAFIN,LOTE,COD, CODALM,NIVEL, GRUPO,SUBGRUPO,ITEM), JsonRequestBehavior.AllowGet);
        }

        public MemoryStream ExpExcelCuboInfo(List<EBCuboInfoActualizado> listado)
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

                workSheet.Cells["A1"].Value = "N° Requisición ";
                workSheet.Cells["B1"].Value = "Cantidad Requerida";
                workSheet.Cells["C1"].Value = "Pedido Compra";
                workSheet.Cells["D1"].Value = "Fecha Registro OC";
                workSheet.Cells["E1"].Value = "Fecha Entrega Prevista OC";
                workSheet.Cells["F1"].Value = "Codigo Proveedor";
                workSheet.Cells["G1"].Value = "Descripción Proveedor";
                workSheet.Cells["H1"].Value = "Codigo Local Entrega";
                workSheet.Cells["I1"].Value = "Descripción Local Entrega";
                workSheet.Cells["J1"].Value = "Codigo Local Cobranza";
                workSheet.Cells["K1"].Value = "Descripción Local Cobranza";
                workSheet.Cells["L1"].Value = "Código Condición Pago";
                workSheet.Cells["M1"].Value = "Descripción Condición Pago";
                workSheet.Cells["N1"].Value = "Código Comprador";
                workSheet.Cells["O1"].Value = "Descripción Comprador";
                workSheet.Cells["P1"].Value = "Contacto";
                workSheet.Cells["Q1"].Value = "Código Moneda";
                workSheet.Cells["R1"].Value = "Descripción Moneda";
                workSheet.Cells["S1"].Value = "Observacion OC";
                workSheet.Cells["T1"].Value = "Secuencia OC";
                workSheet.Cells["U1"].Value = "Código Producto OC";
                workSheet.Cells["V1"].Value = "Descripción Producto OC";
                workSheet.Cells["W1"].Value = "Unidad Consumo";
                workSheet.Cells["X1"].Value = "Unidad Compra";
                workSheet.Cells["Y1"].Value = "Cantidad Solicitada Unidad Consumo";
                workSheet.Cells["Z1"].Value = "Cantidad Solicitud Unidad Compra";
                workSheet.Cells["AA1"].Value = "Valor Unidad Consumo";
                workSheet.Cells["AB1"].Value = "Valor Total Unidad Consumo";
                workSheet.Cells["AC1"].Value = "% Descuento";
                workSheet.Cells["AD1"].Value = "% Impuesto";
                workSheet.Cells["AE1"].Value = "Código Almacén";
                workSheet.Cells["AF1"].Value = "Descripción Almacén";
                workSheet.Cells["AG1"].Value = "Código Centro Costo";
                workSheet.Cells["AH1"].Value = "Descripción Centro Costo";
                workSheet.Cells["AI1"].Value = "Orden Planeamiento";
                workSheet.Cells["AJ1"].Value = "Grupo Planeamiento";
                workSheet.Cells["AK1"].Value = "Fecha Prevista Entrega OC";
                workSheet.Cells["AL1"].Value = "Fecha Proveedor";
                workSheet.Cells["AM1"].Value = "Estado Ítem OC";
                workSheet.Cells["AN1"].Value = "Usuario Registro OC";
                workSheet.Cells["AO1"].Value = "Fecha Aprobación Firmante OC 1";
                workSheet.Cells["AP1"].Value = "Login Firmante OC 1";
                workSheet.Cells["AQ1"].Value = "Fecha Aprobación Firmante OC 2";
                workSheet.Cells["AR1"].Value = "Login Firmante OC 2";
                workSheet.Cells["AS1"].Value = "Fecha Aprobación Firmante OC 3";
                workSheet.Cells["AT1"].Value = "Login Firmante OC 3";
                workSheet.Cells["AU1"].Value = "Cantidad Ingresada al almacén";
                workSheet.Cells["AV1"].Value = "Fecha ingresada al almacén";
                workSheet.Cells["AW1"].Value = "Última fecha ingresada al almacén";
                workSheet.Cells["AX1"].Value = "Veces ingresada al almacén";
                workSheet.Cells["AY1"].Value = "Cantidad devuelta";
                workSheet.Cells["AZ1"].Value = "Cantidad Total";
                int i = 2;

                foreach (var item in listado)
                {
                    workSheet.Row(i).Style.Font.Size = 8;
                    workSheet.Cells[i, 1].Value = item.num_requisicion;
                    workSheet.Cells[i, 2].Value = item.cant_req;
                    workSheet.Cells[i, 3].Value = item.pedido_compra;
                    workSheet.Cells[i, 4].Value = item.Fch_Reg_OC;
                    workSheet.Cells[i, 5].Value = item.Fch_Ent_Prevista_OC;
                    workSheet.Cells[i, 6].Value = item.Cod_Proveedor;
                    workSheet.Cells[i, 7].Value = item.Des_Proveedor;
                    workSheet.Cells[i, 8].Value = item.Cod_Local_Entrega;
                    workSheet.Cells[i, 9].Value = item.Des_Local_Entrega;
                    workSheet.Cells[i, 10].Value = item.Cod_Local_Cobranza;
                    workSheet.Cells[i, 11].Value = item.Des_Local_Cobranza;
                    workSheet.Cells[i, 12].Value = item.Cod_Condicion_pago;
                    workSheet.Cells[i, 13].Value = item.Des_Condicion_pago;
                    workSheet.Cells[i, 14].Value = item.Cod_Comprador;
                    workSheet.Cells[i, 15].Value = item.Des_Comprador;
                    workSheet.Cells[i, 16].Value = item.Contacto;
                    workSheet.Cells[i, 17].Value = item.Cod_Moneda;
                    workSheet.Cells[i, 18].Value = item.Des_Moneda;
                    workSheet.Cells[i, 19].Value = item.Observacion_OC;
                    workSheet.Cells[i, 20].Value = item.Secuencia_OC;
                    workSheet.Cells[i, 21].Value = item.Cod_producto2;
                    workSheet.Cells[i, 22].Value = item.Des_producto;
                    workSheet.Cells[i, 23].Value = item.Unidad_Consumo;
                    workSheet.Cells[i, 24].Value = item.Unidad_Compra;
                    workSheet.Cells[i, 25].Value = item.Cant_Soli_UnidadConsumo;
                    workSheet.Cells[i, 26].Value = item.Cant_Soli_UnidadCompra;
                    workSheet.Cells[i, 27].Value = item.Valor_Unita_UnidadConsumo;
                    workSheet.Cells[i, 28].Value = item.Valor_Total_UnidadConsumo;
                    workSheet.Cells[i, 29].Value = item.Porc_Descuento;
                    workSheet.Cells[i, 30].Value = item.Porc_Impuesto;
                    workSheet.Cells[i, 31].Value = item.Cod_Almacen;
                    workSheet.Cells[i, 32].Value = item.Des_Almacen;
                    workSheet.Cells[i, 33].Value = item.Cod_CCO;
                    workSheet.Cells[i, 34].Value = item.Des_CCO;
                    workSheet.Cells[i, 35].Value = item.Orden_Planeamiento;
                    workSheet.Cells[i, 36].Value = item.Grupo_Planeamiento;
                    workSheet.Cells[i, 37].Value = item.Fch_Prevista_Entrega_OC;
                    workSheet.Cells[i, 38].Value = item.Fch_Proveedor;
                    workSheet.Cells[i, 39].Value = item.Estado_Item_OC;
                    workSheet.Cells[i, 40].Value = item.Usuario_Reg_OC;
                    workSheet.Cells[i, 41].Value = item.Fch_Aprobada_Firmante_OC1;
                    workSheet.Cells[i, 42].Value = item.Login_Firmante_OC1;
                    workSheet.Cells[i, 43].Value = item.Fch_Aprobada_Firmante_OC2;
                    workSheet.Cells[i, 44].Value = item.Login_Firmante_OC2;
                    workSheet.Cells[i, 45].Value = item.Fch_Aprobada_Firmante_OC3;
                    workSheet.Cells[i, 46].Value = item.Login_Firmante_OC3;
                    workSheet.Cells[i, 47].Value = item.Cant_Ing_Almacen;
                    workSheet.Cells[i, 48].Value = item.Fch_Ing_Almacen;
                    workSheet.Cells[i, 49].Value = item.Fch_Ing_Ultima_Almacen;
                    workSheet.Cells[i, 50].Value = item.Veces_Ing_Almacen;
                    workSheet.Cells[i, 51].Value = item.Cant_Devuelta;
                    workSheet.Cells[i, 52].Value = item.Cant_Total;
                    i++;
                }
                package.Save();
            }
            stream.Position = 0;
            return stream;
        }
        public FileResult ExpExcelCubo()
        {
            return File(ExpExcelCuboInfo(EntidadCuboInformacionActualizado), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CuboInformacion" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }





    }

    public class ExportReports
    {
        public void ToExcel(HttpResponseBase Response, object clientsList, string nombreArchivo)
        {
            //var grid = new System.Web.UI.WebControls.GridView();
            var grid = new GridView();
            grid.DataSource = clientsList;
            grid.DataBind();
            Response.ClearContent();
            Response.ContentType = "application/excel";
            //Response.AddHeader("content-disposition", "attachment; filename=" + nombreArchivo + ".xls");
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".xls");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.Clear();
            Response.BufferOutput = true;
            //Change the Header Row back to white color
            //grid.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < grid.HeaderRow.Cells.Count; i++)
            {
                grid.HeaderRow.Cells[i].Style.Add("background-color", "#15a4df");
            }

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

    }



}



#endregion
