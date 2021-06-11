using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TSC_WEB.Models.Entidades.Finanzas.FlujoCaja;
using TSC_WEB.Models.Entidades.Finanzas.FlujoEfectivo;
using TSC_WEB.Models.Entidades.Finanzas.RegistrarProyectado;
using TSC_WEB.Models.Entidades.Finanzas.RendicionGastos;
using TSC_WEB.Models.Entidades.Finanzas.ReporteEjecutivo;
using TSC_WEB.Models.Modelos.Finanzas.FlujoCaja;
using TSC_WEB.Models.Modelos.Finanzas.FlujoEfectivo;
using TSC_WEB.Models.Modelos.Finanzas.RegistrarProyectado;
using TSC_WEB.Models.Modelos.Finanzas.RendicionGastos;
using TSC_WEB.Models.Modelos.Finanzas.ReporteEjecutivo;

namespace TSC_WEB.Controllers
{
    public class FinanzasController : Controller
    {
        public ActionResult FlujoEfectivo()
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

        public ActionResult FlujoCaja()
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

        public ActionResult ReporteEjecutivo()
        {
            //if (Session["usuario"] != null)
            //{
            return View();
            //}
            //else
            //{
            //    return Redirect("/");
            //}
        }



        public ActionResult RegistrarProyectado()
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


        public ActionResult SolicitudGastos()
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


        public ActionResult AprobacionGastos()
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


        public ActionResult AprobacionGastosFinanzas()
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


        public ActionResult RendicionGastos()
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

        public ActionResult CierreRendicion()
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




        #region Flujo Efectivo

        [HttpGet]
        public async Task<JsonResult> FluEfe_GetTipoPeriodo()
        {
            IEnumerable<EFE_TipoPeriodo> lista = null;

            if (Session["usuario"] != null)
            {
                FlujoEfectivoModel objModel = new FlujoEfectivoModel();

                lista = await objModel.Get_TipoPeriodo();

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
        public async Task<JsonResult> FluEfe_GetPeriodos(int tipoPeriodo)
        {
            IEnumerable<EFE_Periodo> lista = null;

            if (Session["usuario"] != null)
            {
                FlujoEfectivoModel objModel = new FlujoEfectivoModel();

                lista = await objModel.Get_Periodos(tipoPeriodo);

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
        public async Task<JsonResult> FluEfe_ListarFlujoEfectivo(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            FlujoEfectivoModel objModel = new FlujoEfectivoModel();
            IEnumerable<EFE_FlujoEfectivoLista> lista = null;
            IEnumerable<EFE_FlujoEfectivoLista> listaOrdenada = null;

            if (Session["usuario"] != null)
            {
                lista = await objModel.ListarFlujoEfectivo(TipoPeriodo, IdPeriodoIni, IdPeriodoFin);
                listaOrdenada = lista.OrderBy(x => x.efIdTipoAct).ThenBy(x => x.efCodEFx).ToList();

                return Json(new
                {
                    lista = listaOrdenada,
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
        public FileResult FluEfe_ExportarFlujoEfectivo(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            FlujoEfectivoModel objModel = new FlujoEfectivoModel();
            IEnumerable<EFE_FlujoEfectivoLista> lista = null;
            IEnumerable<EFE_FlujoEfectivoLista> listaOrdenada = null;

            lista = objModel.ExpListarFlujoEfectivo(TipoPeriodo, IdPeriodoIni, IdPeriodoFin);
            listaOrdenada = lista.OrderBy(x => x.efIdTipoAct).ThenBy(x => x.efCodEFx).ToList();

            return File(objModel.ExportarFlujoEfectivo(listaOrdenada.ToList()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EstadoEfectivo_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");

        }


        [HttpGet]
        public FileResult FluEfe_ExcelDetFlujoEfectivo(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            FlujoEfectivoModel objModel = new FlujoEfectivoModel();
            IEnumerable<EFE_ExcelDetalle> lista = null;
            IEnumerable<EFE_ExcelDetalle> listaOrdenada = null;

            lista = objModel.ListExcelDetFlujoEfectivo(TipoPeriodo, IdPeriodoIni, IdPeriodoFin);
            listaOrdenada = lista.OrderBy(x => x.efIdTipoAct).ThenBy(x => x.efCodEFx).ToList();

            return File(objModel.ExpExcelDetFlujoEfectivo(listaOrdenada.ToList()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EstadoEfectivoDetalle_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");

        }

        #endregion


        #region Flujo Caja

        [HttpGet]
        public async Task<JsonResult> FluCaj_GetTipoPeriodo()
        {
            IEnumerable<EFC_TipoPeriodo> lista = null;

            //if (Session["usuario"] != null)
            //{
            FlujoCajaModel objModel = new FlujoCajaModel();

            lista = await objModel.Get_TipoPeriodo();

            return Json(new
            {
                lista = lista,
                isRedirect = false,
                redirectUrl = ""
            }, JsonRequestBehavior.AllowGet);


            //}
            //else
            //{
            //    return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            //}
        }


        [HttpGet]
        public async Task<JsonResult> FluCaj_GetPeriodos(int tipoPeriodo)
        {
            IEnumerable<EFC_Periodo> lista = null;

            //if (Session["usuario"] != null)
            //{
            FlujoCajaModel objModel = new FlujoCajaModel();

            lista = await objModel.Get_Periodos(tipoPeriodo);

            return Json(new
            {
                lista = lista,
                isRedirect = false,
                redirectUrl = ""
            }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            //}
        }


        [HttpGet]
        public async Task<JsonResult> FluCaj_ListarFlujoCaja(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            FlujoCajaModel objModel = new FlujoCajaModel();
            IEnumerable<EFC_FlujoCajaLista> lista = null;
            IEnumerable<EFC_FlujoCajaLista> listaOrdenada = null;

            //if (Session["usuario"] != null)
            //{


            lista = await objModel.ListarFlujoCaja(TipoPeriodo, IdPeriodoIni, IdPeriodoFin);
            listaOrdenada = lista.OrderBy(x => x.efIdTipoAct).ThenBy(x => x.efCodEFx).ToList();

            return Json(new
            {
                lista = listaOrdenada,
                isRedirect = false,
                redirectUrl = ""
            }, JsonRequestBehavior.AllowGet);


            //}
            //else
            //{
            //    return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            //}
        }



        [HttpGet]
        public FileResult FluCaj_ExportarFlujoCaja(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            FlujoCajaModel objModel = new FlujoCajaModel();
            IEnumerable<EFC_FlujoCajaLista> lista = null;
            IEnumerable<EFC_FlujoCajaLista> listaOrdenada = null;

            lista = objModel.ExpListarFlujoCaja(TipoPeriodo, IdPeriodoIni, IdPeriodoFin);
            listaOrdenada = lista.OrderBy(x => x.efIdTipoAct).ThenBy(x => x.efCodEFx).ToList();

            return File(objModel.ExportarFlujoCaja(listaOrdenada.ToList()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EstadoCaja_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }


        [HttpGet]
        public FileResult FluCaj_ExcelDetFlujoCaja(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            FlujoCajaModel objModel = new FlujoCajaModel();
            IEnumerable<EFC_ExcelDetalle> lista = null;
            IEnumerable<EFC_ExcelDetalle> listaOrdenada = null;

            lista = objModel.ListExcelDetFlujoCaja(TipoPeriodo, IdPeriodoIni, IdPeriodoFin);
            listaOrdenada = lista.OrderBy(x => x.efIdTipoAct).ThenBy(x => x.efCodEFx).ToList();

            return File(objModel.ExpExcelDetFlujoCaja(listaOrdenada.ToList()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EstadoCajaDetalle_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");

        }

        #endregion


        #region Registrar Proyectado

        [HttpGet]
        public async Task<JsonResult> RegPro_GetUbicacion()
        {
            IEnumerable<ERP_Ubicacion> lista = null;

            //if (Session["usuario"] != null)
            //{
            RegistrarProyModel objModel = new RegistrarProyModel();

            lista = await objModel.GetUbicaciones();

            return Json(new
            {
                lista = lista,
                isRedirect = false,
                redirectUrl = ""
            }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpGet]
        public async Task<JsonResult> RegPro_GetTiposPeriodos()
        {
            IEnumerable<ERP_TipoPeriodo> lista = null;

            //if (Session["usuario"] != null)
            //{
            RegistrarProyModel objModel = new RegistrarProyModel();

            lista = await objModel.GetTiposPeriodos();

            return Json(new
            {
                lista = lista,
                isRedirect = false,
                redirectUrl = ""
            }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpGet]
        public async Task<JsonResult> RegPro_GetEF(int idTipoAct)
        {
            IEnumerable<ERP_EF> lista = null;

            //if (Session["usuario"] != null)
            //{
            RegistrarProyModel objModel = new RegistrarProyModel();

            lista = await objModel.GetEF(idTipoAct);

            return Json(new
            {
                lista = lista,
                isRedirect = false,
                redirectUrl = ""
            }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpGet]
        public async Task<JsonResult> RegPro_GetPeriodos(int tipoPeriodo)
        {
            IEnumerable<ERP_Periodos> lista = null;

            //if (Session["usuario"] != null)
            //{
            RegistrarProyModel objModel = new RegistrarProyModel();

            lista = await objModel.GetPeriodos(tipoPeriodo);

            return Json(new
            {
                lista = lista,
                isRedirect = false,
                redirectUrl = ""
            }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpGet]
        public async Task<JsonResult> RegPro_GetConceptos(string codEFx)
        {
            IEnumerable<ERP_Concepto> lista = null;

            //if (Session["usuario"] != null)
            //{
            RegistrarProyModel objModel = new RegistrarProyModel();

            lista = await objModel.GetConceptos(codEFx);

            return Json(new
            {
                lista = lista,
                isRedirect = false,
                redirectUrl = ""
            }, JsonRequestBehavior.AllowGet);

            //}
            //else
            //{
            //    return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpPost]
        public JsonResult RegPro_InsertarProyectado(ERP_ParamReg entidad)
        {
            string tmpusuario = "JMORAN";

            RegistrarProyModel objModel = new RegistrarProyModel();
            ERP_RespuestaOp respuesta = new ERP_RespuestaOp();


            respuesta = objModel.InsertarProyectado(entidad.idRSx, entidad.idTipoPeriodo, entidad.idPeriodo, entidad.monto, tmpusuario);


            return Json(new
            {
                respuesta = respuesta,
                isRedirect = false,
                redirectUrl = ""
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> RegPro_ListarRegistrado()
        {
            IEnumerable<ERP_ListarRegistrados> lista = null;

            //if (Session["usuario"] != null)
            //{
            RegistrarProyModel objModel = new RegistrarProyModel();
            lista = await objModel.GetListarRegistrados();

            return Json(new
            {
                lista = lista,
                isRedirect = false,
                redirectUrl = ""
            }, JsonRequestBehavior.AllowGet);

            //}
            //else
            //{
            //    return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            //}
        }


        #endregion


        #region Reporte Ejecutivo Efectivo

        [HttpGet]
        public async Task<JsonResult> RpteEjecEfe_ListarFlujoEfectivo2(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            ReporteEjecutivoModel objModel = new ReporteEjecutivoModel();
            IEnumerable<REE_FlujoEfectivoLista2> lista = null;
            IEnumerable<REE_FlujoEfectivoLista2> listaOrdenada = null;

            if (Session["usuario"] != null)
            {

                lista = await objModel.ListarFlujoEfectivo2(TipoPeriodo, IdPeriodoIni, IdPeriodoFin);
                listaOrdenada = lista.OrderBy(x => x.efIdTipoAct).ThenBy(x => x.efCodEFx).ToList();

                return Json(new
                {
                    lista = listaOrdenada,
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
        public async Task<JsonResult> RpteEjecEfe_ListarFlujoEfectivo3(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            FlujoEfectivoModel objModel = new FlujoEfectivoModel();
            IEnumerable<EFE_FlujoEfectivoLista> lista = null;
            IEnumerable<EFE_FlujoEfectivoLista> listaOrdenada = null;

            if (Session["usuario"] != null)
            {

                lista = await objModel.ListarFlujoEfectivo(TipoPeriodo, IdPeriodoIni, IdPeriodoFin);
                listaOrdenada = lista.OrderBy(x => x.efIdTipoAct).ThenBy(x => x.efCodEFx).ToList();

                return Json(new
                {
                    lista = listaOrdenada,
                    isRedirect = false,
                    redirectUrl = ""
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion


        #region RendicionGastos

        public async Task<JsonResult> RG_SetRendicionGastos(SPS_Parametro parametro)
        {
            StatusResponse response = new StatusResponse();
            SPG6_SolicitudCab solicitudCab = new SPG6_SolicitudCab();

            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();

                try
                {
                    response = model.SetRendicionGastos(parametro);

                    if (response.idResponse > 0)
                    {
                        solicitudCab = await model.SolicitudCab(response.idResponse);
                    }
                }
                catch (Exception ex)
                {
                    response.idResponse = 0;
                    response.statusResponse = ex.Message;
                }

                return Json(new
                {
                    status = response,
                    responseEntity = solicitudCab,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = response, responseEntity = new SPG6_SolicitudCab(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public async Task<JsonResult> RG_PermisosXCeCo(int nivelInterfaz)
        {
            IEnumerable<SPG4_PermisoXCeCo> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.PermisosXCeCo(nivelInterfaz, Session["usuario"].ToString());

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
        public async Task<JsonResult> RG_ConceptoCab()
        {
            IEnumerable<SPG1_ConceptoCab> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ConceptosCab();

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
        public async Task<JsonResult> RG_TiposDeMoneda()
        {
            IEnumerable<G01_TipoMoneda> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.TiposDeMoneda();

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
        public async Task<JsonResult> RG_SolicitudDet(int idSolicitud)
        {
            IEnumerable<SPG7_SolicitudDet> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.SolicitudDet(idSolicitud);

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
        public async Task<JsonResult> RG_IdSolicitudDet(int idSolicitud, int secuencia)
        {
            SPG8_IdSolicitudDet entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = await objModel.IdSolicitudDet(idSolicitud, secuencia);

                return Json(new
                {
                    entidad = entidad,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { entidad = new object(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult RG_EliminarSolicitud(SPS_Parametro parametro)
        {
            StatusResponse response = new StatusResponse();
            SPG6_SolicitudCab solicitudCab = new SPG6_SolicitudCab();

            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();

                try
                {
                    response = model.SetRendicionGastos(parametro);
                }
                catch (Exception ex)
                {
                    response.idResponse = 0;
                    response.statusResponse = ex.Message;
                }

                return Json(new
                {
                    status = response,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = response, isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public async Task<JsonResult> RG_BuscarHistorial(DateTime fechaInicio, DateTime fechaFin)
        {
            IEnumerable<SPG9_ListaHistorial> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ListaHistorial(fechaInicio.ToString("yyyyMMdd"), fechaFin.ToString("yyyyMMdd"));

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
        public async Task<JsonResult> RG_SolicitudXCodigo(string codigo)
        {
            SPG10_SolicitudXCodigo solicitud = null;
            IEnumerable<SPG7_SolicitudDet> listaDet = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                solicitud = await objModel.SolicitudXCodigo(codigo);
                listaDet = await objModel.SolicitudDet(solicitud.idSolicitud);

                solicitud.listaDetalle = new List<SPG7_SolicitudDet>();
                solicitud.listaDetalle = listaDet.ToList();

                return Json(new
                {
                    entidad = solicitud,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { entidad = new object(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public async Task<JsonResult> RG_PendientesAprobacion20(DateTime fechaInicio, DateTime fechaFin, int nivelInterfaz)
        {
            IEnumerable<SPG11_ListaPendientes> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.PendientesAprobacion20(fechaInicio.ToString("yyyyMMdd"), fechaFin.ToString("yyyyMMdd"), nivelInterfaz, Session["usuario"].ToString());

                return Json(new
                {
                    lista = lista,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { lista = new object(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public async Task<JsonResult> RG_PendAprob20Det(int idSolicitud)
        {
            SPG12_SolicitudCab entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = await objModel.PendAprob20Cab(idSolicitud);
                entidad.listaDetalle = new List<SPG13_SolicitudDet>();
                entidad.listaDetalle = objModel.PendAprob20Det(idSolicitud);


                return Json(new
                {
                    entidad = entidad,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { entidad = new object(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult RG_AprobRech20(SPS_Parametro parametro)
        {
            StatusResponse response = new StatusResponse();
            SPG6_SolicitudCab solicitudCab = new SPG6_SolicitudCab();

            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();

                try
                {
                    response = model.SetRendicionGastos(parametro);
                }
                catch (Exception ex)
                {
                    response.idResponse = 0;
                    response.statusResponse = ex.Message;
                }

                return Json(new
                {
                    status = response,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = response, isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public async Task<JsonResult> RG_Historial20(DateTime fechaInicio, DateTime fechaFin, string usuario)
        {
            IEnumerable<SPG14_Historial20> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.Historial20(fechaInicio.ToString("yyyyMMdd"), fechaFin.ToString("yyyyMMdd"), usuario);

                return Json(new
                {
                    lista = lista,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { lista = new object(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public async Task<JsonResult> RG_PendientesAprobacion30(DateTime fechaInicio, DateTime fechaFin, int nivelInterfaz, int idEstado, string codigo)
        {
            IEnumerable<SPG15_ListaPendientes30> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.PendientesAprobacion30(fechaInicio.ToString("yyyyMMdd"), fechaFin.ToString("yyyyMMdd"), nivelInterfaz, Session["usuario"].ToString(), idEstado, codigo);

                return Json(new
                {
                    lista = lista,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { lista = new object(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public async Task<JsonResult> RG_PendAprob30Det(int idSolicitud)
        {
            SPG16_SolicitudCab entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = await objModel.PendAprob30Cab(idSolicitud);
                entidad.listaDetalle = new List<SPG17_SolicitudDet>();
                entidad.listaDetalle = objModel.PendAprob30Det(idSolicitud).ToList();

                return Json(new
                {
                    entidad = entidad,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { entidad = new object(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult RG_PendAprob30TablaDet(int idSolicitud)
        {
            IEnumerable<SPG17_SolicitudDet> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = objModel.PendAprob30Det(idSolicitud).ToList();

                return Json(new
                {
                    lista = lista,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { lista = new object(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }





        public JsonResult RG_AprobRech30(SPA_Parametro parametro)
        {
            StatusResponse response = new StatusResponse();
            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();

                try
                {
                    response = model.AprobRendicionGastos(parametro);
                }
                catch (Exception ex)
                {
                    response.idResponse = 0;
                    response.statusResponse = ex.Message;
                }

                return Json(new
                {
                    status = response,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = response, isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public async Task<JsonResult> RG_Historial30(DateTime fechaInicio, DateTime fechaFin, string usuario)
        {
            IEnumerable<SPG18_Historial30> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.Historial30(fechaInicio.ToString("yyyyMMdd"), fechaFin.ToString("yyyyMMdd"), usuario);

                return Json(new
                {
                    lista = lista,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { lista = new object(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public async Task<JsonResult> RG_Tipos()
        {
            IEnumerable<SPG19_Tipos> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.Tipos();

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
        public async Task<JsonResult> RG_ConceptoDet(int idConceptoCab)
        {
            IEnumerable<SPG20_ConceptoDet> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ConceptosDet(idConceptoCab);

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
        public async Task<JsonResult> RG_EstadosSolcitud()
        {
            IEnumerable<SPG21_Estados> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.EstadosSolcitud();

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
        public async Task<JsonResult> RG_TipoComprobantes()
        {
            IEnumerable<SPG22_TipoComprobante> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.TipoComprobantes();

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
        public async Task<JsonResult> RG_SolBusquedaXCod(string codigo)
        {
            SPG23_BusquedaXCod entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = await objModel.SolBusquedaXCod(codigo);

                return Json(new
                {
                    entidad = entidad,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { entidad = new object(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion

    }
}