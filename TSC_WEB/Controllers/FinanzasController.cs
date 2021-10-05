using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TSC_WEB.Models.Entidades.Finanzas;
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
using TSC_WEB.Models.Modelos.Sistema;

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
            if (Session["usuario"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
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

        public ActionResult AprobacionSolicitud()
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


        public ActionResult AprobSolicitudFinanzas()
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


        public ActionResult AprobacionGastosTest()
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

        public ActionResult AprobExcedenteGerencia()
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


        public ActionResult AprobacionFinal()
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

        public ActionResult HistorialRendicion()
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

        public ActionResult Reembolso()
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

        public ActionResult PendienteCierre()
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


        public ActionResult ReporteRendicion()
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




        [HttpPost]
        public JsonResult RG_SetRendicionGastos(SPS_Parametro parametro)
        {
            StatusResponse response = new StatusResponse();
            StatusResponse responseDetalle = new StatusResponse();
            SPG6_SolicitudCab solicitudCab = new SPG6_SolicitudCab();

            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();
                SPS_Parametro parametroDetalle = new SPS_Parametro();

                try
                {
                    response = model.SetRendicionGastos(parametro);

                    if (response.idResponse > 0)
                    {
                        if (parametro.idTipo != 4)
                        {
                            // Los que sean diferente a reembolso

                            parametroDetalle.opcion = 2; // Opcion para insertar detalle
                            parametroDetalle.idSolicitud = response.idResponse;

                            foreach (var item in parametro.conceptoDetArray)
                            {
                                parametroDetalle.idConceptoDet = item.idConceptoDet;
                                parametroDetalle.codCeCo = item.codCeCo;
                                parametroDetalle.secuencia = 0;
                                parametroDetalle.seleccionadoDet = item.seleccionadoDet;
                                parametroDetalle.cantDias = item.cantDias;
                                parametroDetalle.montoSolicitado = item.montoSolicitado;
                                parametroDetalle.observacionDet = item.observacionDet;

                                responseDetalle = model.SetRendicionGastos(parametroDetalle);
                            }
                        }
                    }

                    if (response.idResponse > 0)
                    {
                        solicitudCab = model.SolicitudCab(response.idResponse);
                    }
                    else
                    {
                        solicitudCab = new SPG6_SolicitudCab();

                        if (parametro.idTipo != 4)
                        {
                            response.statusResponse = response.statusResponse;
                        }
                        else
                        {
                            response.statusResponse = responseDetalle.statusResponse;
                        }
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



        [HttpPost]
        public JsonResult RG_SetRendicionGastosDetalle(SPS_ParametroDet parametro)
        {
            StatusResponse response = new StatusResponse();
            SPG6_SolicitudCab solicitudCab = new SPG6_SolicitudCab();

            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();

                try
                {
                    SPS_Parametro entidad = new SPS_Parametro();

                    entidad.opcion = parametro.opcion;
                    entidad.idSolicitud = parametro.idSolicitud;
                    entidad.codigo = parametro.codigo;
                    entidad.idEmpresa = parametro.idEmpresa;
                    entidad.idEstado = parametro.idEstado;
                    entidad.idAnulado = parametro.idAnulado;
                    entidad.idTipoMod = parametro.idTipoMod;
                    entidad.observacion = parametro.observacion;
                    entidad.codCeCo = parametro.codCeCo;
                    entidad.usuario = parametro.usuario;
                    entidad.usuarioCompleto = parametro.usuarioCompleto;
                    entidad.nivelInterfaz = parametro.nivelInterfaz;
                    entidad.idTipo = parametro.idTipo;


                    foreach (var item in parametro.conceptoDetArray)
                    {
                        entidad.idConceptoDet = item.idConceptoDet;
                        entidad.codCeCo = item.codCeCo;
                        entidad.secuencia = 0;
                        entidad.seleccionadoDet = item.seleccionadoDet;
                        entidad.cantDias = item.cantDias;
                        entidad.montoSolicitado = item.montoSolicitado;
                        entidad.observacionDet = item.observacionDet;

                        response = model.SetRendicionGastos(entidad);

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
        public async Task<JsonResult> RG_ConceptoCab(int idSolicitud, int idTipo)
        {
            IEnumerable<SPG1_ConceptoCab> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ConceptosCab(idSolicitud, idTipo);

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
        public async Task<JsonResult> RG_ConceptoCabReembolso()
        {
            IEnumerable<SPG65_ConceptoCab> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ConceptosCabReembolso();

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
        public async Task<JsonResult> RG_SolicitudDetalle(int idSolicitud, int idConceptoCab)
        {
            SPG8_SolicitudDetalle entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();
                entidad = new SPG8_SolicitudDetalle();
                entidad.conceptoDetalle = new List<SPG25_ConceptoDetalle>();

                entidad = await objModel.IdSolicitudDetalle(idSolicitud);
                entidad.conceptoDetalle = objModel.ConceptosDetalleVisualizar(idSolicitud, idConceptoCab);

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

        [HttpPost]
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
        public async Task<JsonResult> RG_SolicitudXCodigo(string codigo, string tipoInterfaz)
        {
            SPG10_SolicitudXCodigo solicitud = null;
            IEnumerable<SPG7_SolicitudDet> listaDet = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                solicitud = await objModel.SolicitudXCodigo(codigo, tipoInterfaz);
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
        public async Task<JsonResult> RG_PendientesAprobacion20(int nivelInterfaz)
        {
            IEnumerable<SPG11_ListaPendientes> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.PendientesAprobacion20(nivelInterfaz, Session["usuario"].ToString());

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


        [HttpPost]
        public JsonResult RG_AprobacionSolicitud20(SPS_ParametroAprob parametro)
        {

            StatusResponse response = new StatusResponse();
            SPA_Parametro parametroAprob = new SPA_Parametro();

            if (Session["usuario"] != null)
            {

                try
                {
                    RendicionGastosModel model = new RendicionGastosModel();


                    foreach (var item in parametro.solicitudesArray)
                    {
                        parametroAprob = new SPA_Parametro();

                        parametroAprob.opcion = parametro.opcion;
                        parametroAprob.opcionTipoAprob = parametro.opcionTipoAprob;
                        parametroAprob.usuario = Session["usuario"].ToString();
                        parametroAprob.nivelInterfaz = parametro.nivelInterfaz;
                        parametroAprob.idSolicitud = Convert.ToInt32(item.idsolicitud);

                        response = model.AprobRendicionGastos(parametroAprob);
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
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = response, isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }

        }


        //[HttpPost]
        //public JsonResult RG_AprobRech20(SPS_ParametroAprob parametro)
        //{
        //    SPS_Parametro parametroAprob = new SPS_Parametro();
        //    StatusResponse response = new StatusResponse();
        //    SPG6_SolicitudCab solicitudCab = new SPG6_SolicitudCab();

        //    if (Session["usuario"] != null)
        //    {
        //        RendicionGastosModel model = new RendicionGastosModel();

        //        try
        //        {
        //            foreach (var item in parametro.solicitudesArray)
        //            {
        //                parametroAprob = new SPS_Parametro();

        //                parametroAprob.idSolicitud = Convert.ToInt32(item.idsolicitud);
        //                parametroAprob.opcion = parametro.opcion;
        //                parametroAprob.usuario = parametro.usuario;
        //                parametroAprob.nivelInterfaz = parametro.nivelInterfaz;

        //                response = model.SetRendicionGastos(parametroAprob);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            response.idResponse = 0;
        //            response.statusResponse = ex.Message;
        //        }

        //        return Json(new
        //        {
        //            status = response,
        //            isRedirect = false,
        //            redirectUrl = ""

        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { status = response, isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
        //    }
        //}


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
        public async Task<JsonResult> RG_PendientesAprobacion30(int nivelInterfaz)
        {
            IEnumerable<SPG15_ListaPendientes30> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.PendientesAprobacion30(nivelInterfaz, Session["usuario"].ToString());

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
        public async Task<JsonResult> RG_Pendientes30Det(int idSolicitud)
        {
            SPG16_SolicitudCab entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = await objModel.Pendientes30Cab(idSolicitud);
                entidad.listaDetalle = new List<SPG17_SolicitudDet>();
                entidad.listaDetalle = objModel.Pendientes30Det(idSolicitud).ToList();

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
        public JsonResult RG_Pendientes30Detalle(int idSolicitud)
        {
            List<SPG17_SolicitudDet> lista = new List<SPG17_SolicitudDet>();

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();
                lista = objModel.Pendientes30Det(idSolicitud).ToList();

                return Json(new
                {
                    lista = lista,
                    isRedirect = false,
                    redirectUrl = ""
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { entidad = new object(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }

        // *** Trabajando aqui.


        [HttpGet]
        public async Task<JsonResult> RG_Aprobados30(int idSolicitud)
        {
            SPG16_SolicitudCab entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = await objModel.Pendientes30Cab(idSolicitud);

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


        [HttpPost]
        public JsonResult RG_AprobRech30(SPA_ParametroAprob parametro)
        {
            StatusResponse response = new StatusResponse();
            SPA_Parametro parametroAprob = new SPA_Parametro();
            bool todoCorrecto = true;
            string mensaje = "";

            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();

                try
                {
                    foreach (var item in parametro.solicitudesArray)
                    {
                        parametroAprob = new SPA_Parametro();

                        parametroAprob.opcion = item.opcion;
                        parametroAprob.opcionTipoAprob = item.opcionAprob;
                        parametroAprob.idSolicitud = parametro.idsolicitud;
                        parametroAprob.secuencia = item.secuencia;
                        parametroAprob.valorEntrega = item.montoIngresado;

                        response = model.AprobRendicionGastos(parametroAprob);
                        mensaje = response.statusResponse;
                    }


                    if (response.idResponse == 0)
                    {
                        todoCorrecto = false;
                        mensaje = response.statusResponse;
                    }


                    if (todoCorrecto)
                    {
                        // Guardar Serie y Numero
                        parametroAprob = new SPA_Parametro();

                        parametroAprob.opcion = 9;
                        parametroAprob.idSolicitud = parametro.idsolicitud;

                        response = model.AprobRendicionGastos(parametroAprob);

                        mensaje = response.statusResponse;
                    }

                    if (response.idResponse == 0)
                    {
                        todoCorrecto = false;
                        mensaje = response.statusResponse;
                    }



                    // Tercera Firma
                    if (todoCorrecto)
                    {
                        parametroAprob = new SPA_Parametro();

                        parametroAprob.opcion = 5;
                        parametroAprob.idSolicitud = parametro.idsolicitud;
                        parametroAprob.usuario = Session["usuario"].ToString();
                        parametroAprob.nivelInterfaz = parametro.nivelInterfaz;

                        response = model.AprobRendicionGastos(parametroAprob);
                    }

                    if (response.idResponse == 0)
                    {
                        todoCorrecto = false;
                        mensaje = response.statusResponse;
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
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = response, isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult RG_AnularItem30(SPA_Parametro parametro)
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

        [HttpPost]
        public JsonResult RG_AnularSolicitud(SPA_Parametro parametro)
        {
            StatusResponse response = new StatusResponse();

            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();

                try
                {
                    response = model.AprobRendicionGastos(parametro);
                    string mensajeRespuesta = response.statusResponse;

                    if (response.idResponse > 0)
                    {
                        SPA_Parametro parametroAprob = new SPA_Parametro();

                        parametroAprob.opcion = 5;
                        parametroAprob.idSolicitud = parametro.idSolicitud;
                        parametroAprob.usuario = Session["usuario"].ToString();
                        parametroAprob.nivelInterfaz = parametro.nivelInterfaz;

                        response = model.AprobRendicionGastos(parametroAprob);
                    }

                    if (response.idResponse > 0)
                    {
                        response.statusResponse = mensajeRespuesta;
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
        public async Task<JsonResult> RG_HistorialRendiciones(string fechaInicio, string fechaFin, string usuario)
        {
            IEnumerable<SPG18_Historial> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.HistorialRendiciones(fechaInicio, fechaFin, usuario);

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
        public async Task<JsonResult> RG_ConceptoDetalleInsertar(int idConceptoCab)
        {
            IEnumerable<SPG20_ConceptoDetalle> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ConceptosDetalleInsertar(idConceptoCab);

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
        public async Task<JsonResult> RG_ConceptoDetalle(int idConceptoCab, int idTipoComp)
        {
            IEnumerable<SPG20_ConceptoDetalle> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ConceptosDetalleInsertar(idConceptoCab, idTipoComp);

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
        public JsonResult RG_SolBusquedaXCod(string codigo, int idSolicitud)
        {
            SPG23_BusquedaXCod entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();
                entidad = new SPG23_BusquedaXCod();

                entidad = objModel.SolBusquedaXCod(codigo);

                entidad.listaDetalle = new List<SPG32_DetalleSolicitud>();
                entidad.listaDetalle = objModel.SolicitudDetalleRendicion(idSolicitud);

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
        public JsonResult RG_SolicitudDetalleRendicion(int idSolicitud)
        {
            List<SPG32_DetalleSolicitud> lista = new List<SPG32_DetalleSolicitud>(); ;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();
                lista = objModel.SolicitudDetalleRendicion(idSolicitud).ToList();

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

        [HttpPost]
        public JsonResult RG_ValidarEstadoRendicion(SPS_ParametroValid parametro)
        {
            StatusResponse response = new StatusResponse();

            if (Session["usuario"] != null)
            {
                RendicionModelValidacion model = new RendicionModelValidacion();

                try
                {
                    response = model.ValidarEstadoRendicion(parametro);
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
        public async Task<JsonResult> RG_Dias()
        {
            IEnumerable<SPG27_Dias> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.Dias();

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
        public async Task<JsonResult> RG_DetalleEditar(int idSolicitud, int idConceptoCab)
        {
            SPG28_DetalleEditar entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();
                entidad = new SPG28_DetalleEditar();
                entidad.conceptoDetalle = new List<SPG30_ConceptoDetalle>();

                entidad = await objModel.DetalleEditar(idSolicitud, idConceptoCab);

                if (entidad != null)
                {
                    entidad.conceptoDetalle = objModel.ConceptosDetalleEditar(idSolicitud, idConceptoCab);
                }


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
        public async Task<JsonResult> RG_ConceptoCabEdit(int idSolicitud)
        {
            IEnumerable<SPG29_ConceptoCabEdit> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ConceptosCabEdit(idSolicitud);

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


        [HttpPost]
        public async Task<JsonResult> RG_SetProveedorValidar(SPS_Proveedor parametro)
        {
            SPS1_Proveedor response = new SPS1_Proveedor();

            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();

                try
                {
                    response = await model.SetProveedorValidar(parametro);
                }
                catch (Exception ex)
                {
                    response.idResponse = 0;
                    response.statusResponse = ex.Message;
                }

                return Json(new
                {
                    entidad = response,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { entidad = response, isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public async Task<JsonResult> RG_SetProveedorRegistrar(SPS_Proveedor parametro)
        {
            SPS2_Proveedor response = new SPS2_Proveedor();

            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();

                try
                {
                    response = await model.SetProveedorRegistrar(parametro);
                }
                catch (Exception ex)
                {
                    response.idResponse = 0;
                    response.statusResponse = ex.Message;
                }

                return Json(new
                {
                    entidad = response,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { entidad = response, isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public async Task<JsonResult> RG_PendientesRendicion()
        {
            IEnumerable<SPG31_PendientesRendicion> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.PendientesRendicion(Session["usuario"].ToString());

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


        [HttpPost]
        public JsonResult RG_SetRendicionComprobante(SPS_ComprobanteParametroTodo parametroTodo)
        {
            StatusResponse responseValid = new StatusResponse();
            StatusResponse responseCab = new StatusResponse();
            StatusResponse responseSeqSol = new StatusResponse();
            StatusResponse responseDet = new StatusResponse();
            StatusResponse responseEliminar = new StatusResponse();

            StatusResponse response = new StatusResponse();

            SPS_ComprobanteParametro parametro = new SPS_ComprobanteParametro();

            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();
                RendicionModelValidacion modelValidacion = new RendicionModelValidacion();
                SPS_ParametroValid parametroValid = new SPS_ParametroValid();

                bool finRegistroDetalle = true;
                bool finSecueniaComp = true;

                try
                {
                    parametro = new SPS_ComprobanteParametro();

                    parametro.opcion = parametroTodo.opcion;
                    parametro.idRendCompte = parametroTodo.idRendCompte;
                    parametro.idTipoComp = parametroTodo.idTipoComp;

                    parametro.nserie = parametroTodo.nserie;
                    parametro.ndocof = parametroTodo.ndocof;

                    parametro.rucDNI = parametroTodo.rucDNI;
                    parametro.rs = parametroTodo.rs;
                    parametro.codigoPC = parametroTodo.codigoPC;
                    parametro.obs = parametroTodo.obs;
                    parametro.fechaEmision = parametroTodo.fechaEmision;
                    parametro.periodo = parametroTodo.periodo;
                    parametro.ceco = parametroTodo.codceco;
                    parametro.idTipoMod = parametroTodo.idTipoMod;
                    parametro.porcentajeIGV = parametroTodo.porcentajeIGV;

                    parametro.obs1 = parametroTodo.obs1;
                    parametro.obs2 = parametroTodo.obs2;

                    parametro.fecha1 = parametroTodo.fecha1;
                    parametro.fecha2 = parametroTodo.fecha2;
                    parametro.idSolicitud = parametroTodo.idSolicitud;
                    parametro.secuencia = parametroTodo.secuencia;


                    // Validacion que no exista una serie un ruc registrada.
                    if (parametroTodo.opcion == 1)
                    {
                        // 1: Registrar

                        // Solo validar si es Factura o Boleta.
                        if (parametroTodo.idTipoComp == 1 || parametroTodo.idTipoComp == 2)
                        {
                            parametroValid.opcion = 3;

                            parametroValid.nserie = parametroTodo.nserie;
                            parametroValid.ndocof = parametroTodo.ndocof;
                            parametroValid.rucDNI = parametroTodo.rucDNI;
                            parametroValid.idTipoComp = parametroTodo.idTipoComp;

                            responseValid = modelValidacion.ValidarEstadoRendicion(parametroValid);
                        }
                        else
                        {
                            responseValid.idResponse = 0;
                            responseValid.statusResponse = "ok";
                        }

                        response.idResponse = responseValid.idResponse;
                        response.statusResponse = responseValid.statusResponse;

                    }
                    else if (parametroTodo.opcion == 6)
                    {
                        // 6: Editar
                        response.idResponse = 0;

                    }
                    else if (parametroTodo.opcion == 10)
                    {
                        // 10: Eliminar.
                    }




                    if (responseValid.idResponse == 0 && (parametroTodo.opcion == 1 || parametroTodo.opcion == 6))
                    {
                        // 1. REGISTRO COMPROBANTE CABECERA
                        responseCab = model.SetRendicionComprobante(parametro);

                        if (responseCab.idResponse > 0)
                        {

                            // 2. REGISTRAR DETALLE DEL COMPROBANTE

                            if (parametroTodo.opcion == 6)
                            {
                                // 6: Editar: Eliminar todo el detalle para volver a insertar.
                                SPS_ComprobanteParametro parametroEliminar = new SPS_ComprobanteParametro();

                                parametroEliminar.opcion = 8;
                                parametroEliminar.idRendCompte = parametroTodo.idRendCompte;

                                responseEliminar = model.SetRendicionComprobante(parametroEliminar);
                            }


                            // Registrar detalle de Comprobante.
                            foreach (var item in parametroTodo.detalleComprobanteArray)
                            {
                                if (finRegistroDetalle)
                                {
                                    parametro.opcion = 2;
                                    parametro.idRendCompte = (parametroTodo.opcion == 1 ? responseCab.idResponse : parametroTodo.idRendCompte);
                                    parametro.fecha = item.fechaBD;
                                    parametro.detalle1 = item.detalle1;
                                    parametro.detalle2 = item.detalle2;
                                    parametro.codum = item.codum;
                                    parametro.valorunit = Math.Round(item.valorunit, 4);
                                    parametro.cantidad = item.cantidad;
                                    parametro.seccion = item.seccion;

                                    responseDet = model.SetRendicionComprobante(parametro);

                                    if (responseDet.idResponse == 0)
                                    {
                                        response.idResponse = responseDet.idResponse;
                                        response.statusResponse = responseDet.statusResponse;

                                        finRegistroDetalle = false;
                                    }
                                }
                            }

                            // Solo cuando se registra se realacion con el comprobante y detalle de solicitud
                            if (parametroTodo.opcion == 1)
                            {
                                if (finRegistroDetalle)
                                {
                                    // 3. Registrar Relacion Comprobante con Solicitud
                                    foreach (var item in parametroTodo.solitudSeqArray)
                                    {
                                        if (finSecueniaComp)
                                        {
                                            parametro.opcion = 3;
                                            parametro.idRendCompte = responseCab.idResponse;
                                            parametro.idSolicitud = item.idSolicitud;
                                            parametro.secuencia = item.secuencia;

                                            responseSeqSol = model.SetRendicionComprobante(parametro);

                                            if (responseSeqSol.idResponse == 0)
                                            {
                                                finSecueniaComp = false;
                                                response.idResponse = responseSeqSol.idResponse;
                                                response.statusResponse = responseSeqSol.statusResponse;

                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Como esta editando se asigna true.
                                finSecueniaComp = true;
                            }
                        }
                        else
                        {
                            finSecueniaComp = false;
                            response.idResponse = responseCab.idResponse;
                            response.statusResponse = responseCab.statusResponse;
                        }
                    }
                    else if (parametroTodo.opcion == 10 || parametroTodo.opcion == 11)
                    {
                        // 10. ELIMINAR COMPROBANTE
                        responseCab = model.SetRendicionComprobante(parametro);

                        // Eliminar Comprobante.
                        finSecueniaComp = true;
                    }
                    else
                    {
                        finSecueniaComp = false;
                        response.idResponse = responseValid.idResponse;
                        response.statusResponse = responseValid.statusResponse;
                    }




                    // Resultado Final
                    if (finSecueniaComp)
                    {
                        // Registrar
                        response.idResponse = 1;

                        if (parametroTodo.opcion == 1)
                        {
                            response.statusResponse = "Registro Correcto";
                        }
                        else if (parametroTodo.opcion == 6)
                        {
                            response.statusResponse = "Actualización Correcta";
                        }
                        else if (parametroTodo.opcion == 10 || parametroTodo.opcion == 11)
                        {
                            response.statusResponse = "Eliminación Correcta";
                        }

                    }
                    else
                    {
                        response.idResponse = 0;
                    }
                }
                catch (Exception ex)
                {
                    response.idResponse = 0;
                    response.statusResponse = ex.Message;
                }



                return Json(new
                {
                    entidad = response,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { entidad = responseDet, isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult RG_SetReembolsoComprobante(SPS_CpteReembolsoParametro parametroTodo)
        {
            StatusResponse responseValid = new StatusResponse();
            StatusResponse responseCab = new StatusResponse();
            StatusResponse responseSeqSol = new StatusResponse();
            StatusResponse responseDet = new StatusResponse();
            StatusResponse responseEliminar = new StatusResponse();

            StatusResponse response = new StatusResponse();

            SPS_ComprobanteParametro parametro = new SPS_ComprobanteParametro();
            SPS_CpteReembolsoParametroDet parametroDet = new SPS_CpteReembolsoParametroDet();

            if (Session["usuario"] != null)
            {
                RendicionGastosModel model = new RendicionGastosModel();
                RendicionModelValidacion modelValidacion = new RendicionModelValidacion();
                SPS_ParametroValid parametroValid = new SPS_ParametroValid();

                bool finSecueniaComp = true;

                try
                {
                    parametro = new SPS_ComprobanteParametro();

                    parametro.opcion = parametroTodo.opcion;
                    parametro.idRendCompte = parametroTodo.idRendCompte;
                    parametro.idTipoComp = parametroTodo.idTipoComp;

                    parametro.nserie = parametroTodo.nserie;
                    parametro.ndocof = parametroTodo.ndocof;

                    parametro.rucDNI = parametroTodo.rucDNI;
                    parametro.rs = parametroTodo.rs;
                    parametro.codigoPC = parametroTodo.codigoPC;
                    parametro.obs = parametroTodo.obs;
                    parametro.fechaEmision = parametroTodo.fechaEmision;
                    parametro.periodo = parametroTodo.periodo;
                    parametro.ceco = parametroTodo.codceco;
                    parametro.idTipoMod = parametroTodo.idTipoMod;
                    parametro.porcentajeIGV = parametroTodo.porcentajeIGV;

                    parametro.obs1 = parametroTodo.obs1;
                    parametro.obs2 = parametroTodo.obs2;

                    parametro.fecha1 = parametroTodo.fecha1;
                    parametro.fecha2 = parametroTodo.fecha2;
                    parametro.sede = parametroTodo.sede;

                    // Validacion que no exista una serie un ruc registrada.
                    if (parametroTodo.opcion == 1)
                    {
                        // 1: Registrar
                        parametroValid.opcion = 3;

                        parametroValid.nserie = parametroTodo.nserie;
                        parametroValid.ndocof = parametroTodo.ndocof;
                        parametroValid.rucDNI = parametroTodo.rucDNI;
                        parametroValid.idTipoComp = parametroTodo.idTipoComp;

                        responseValid = modelValidacion.ValidarEstadoRendicion(parametroValid);

                        response.idResponse = responseValid.idResponse;
                        response.statusResponse = responseValid.statusResponse;

                    }
                    else if (parametroTodo.opcion == 6)
                    {
                        // 6: Editar
                        response.idResponse = 0;

                    }
                    else if (parametroTodo.opcion == 10)
                    {
                        // 10: Eliminar.

                    }



                    if (responseValid.idResponse == 0 && (parametroTodo.opcion == 1 || parametroTodo.opcion == 6))
                    {
                        // 1. REGISTRO COMPROBANTE CABECERA
                        responseCab = model.SetRendicionComprobante(parametro);

                        if (responseCab.idResponse > 0)
                        {
                            // 2. REGISTRAR DETALLE DEL COMPROBANTE
                            if (parametroTodo.opcion == 6)
                            {
                                // 6: Editar: Eliminar todo el detalle para volver a insertar.
                                SPS_ComprobanteParametro parametroEliminar = new SPS_ComprobanteParametro();

                                parametroEliminar.opcion = 8;
                                parametroEliminar.idRendCompte = parametroTodo.idRendCompte;

                                responseEliminar = model.SetRendicionComprobante(parametroEliminar);
                            }

                            SPS_Parametro parametroDetSolicitud = null;
                            bool primerDetalleXCpte = true;

                            foreach (var item in parametroTodo.detalleComprobanteArray)
                            {

                                if (primerDetalleXCpte)
                                {
                                    if (parametroTodo.opcion == 1)
                                    {
                                        // Registrar.

                                        // 3. Detalle de Solicitud - FZA006
                                        parametroDetSolicitud = new SPS_Parametro();

                                        parametroDetSolicitud.opcion = 2;
                                        parametroDetSolicitud.idSolicitud = item.idSolicitud;
                                        parametroDetSolicitud.idConceptoDet = item.idConcepDet;
                                        parametroDetSolicitud.codCeCo = item.codCeCo;
                                        parametroDetSolicitud.secuencia = 0;
                                        parametroDetSolicitud.seleccionadoDet = 1;
                                        parametroDetSolicitud.cantDias = 1;
                                        parametroDetSolicitud.montoSolicitado = parametroTodo.detalleComprobanteArray.Sum(x => x.total);
                                        parametroDetSolicitud.observacionDet = "";
                                        parametroDetSolicitud.idTipo = item.idTipo;
                                        parametroDetSolicitud.observacionDet = parametroTodo.observacionDet;

                                        response = model.SetRendicionGastos(parametroDetSolicitud);

                                        // 5. Comprobante y solicitud - FZA0017

                                        parametro.opcion = 3;
                                        parametro.idRendCompte = responseCab.idResponse;
                                        parametro.idSolicitud = item.idSolicitud;
                                        parametro.secuencia = response.idResponse; // secuencia generada.

                                        responseSeqSol = model.SetRendicionComprobante(parametro);
                                    }

                                    primerDetalleXCpte = false;
                                }


                                // 4. Detalle de Comprobante - FZA018
                                parametro.opcion = 2;
                                parametro.idRendCompte = (parametroTodo.opcion == 1 ? responseCab.idResponse : parametroTodo.idRendCompte);
                                parametro.fecha = item.fechaBD;
                                parametro.detalle1 = item.detalle1;
                                parametro.detalle2 = item.detalle2;
                                parametro.codum = item.codum;
                                parametro.valorunit = item.valorunit;
                                parametro.cantidad = item.cantidad;
                                parametro.seccion = item.seccion;

                                responseDet = model.SetRendicionComprobante(parametro);

                                if (responseDet.idResponse == 0)
                                {
                                    response.idResponse = responseDet.idResponse;
                                    response.statusResponse = responseDet.statusResponse;
                                }

                            }
                        }
                        else
                        {
                            finSecueniaComp = false;
                            response.idResponse = responseCab.idResponse;
                            response.statusResponse = responseCab.statusResponse;
                        }
                    }
                    else if (parametroTodo.opcion == 10)
                    {
                        // 10. ELIMINAR COMPROBANTE
                        responseCab = model.SetRendicionComprobante(parametro);

                        // Eliminar Comprobante.
                        finSecueniaComp = true;
                    }
                    else
                    {
                        finSecueniaComp = false;
                        response.idResponse = responseValid.idResponse;
                        response.statusResponse = responseValid.statusResponse;
                    }




                    // Resultado Final
                    if (finSecueniaComp)
                    {
                        // Registrar
                        response.idResponse = 1;

                        if (parametroTodo.opcion == 1)
                        {
                            response.statusResponse = "Registro Correcto";
                        }
                        else if (parametroTodo.opcion == 6)
                        {
                            response.statusResponse = "Actualización Correcta";
                        }
                        else if (parametroTodo.opcion == 10)
                        {
                            response.statusResponse = "Eliminación Correcta";
                        }

                    }
                    else
                    {
                        response.idResponse = 0;
                    }
                }
                catch (Exception ex)
                {
                    response.idResponse = 0;
                    response.statusResponse = ex.Message;
                }


                return Json(new
                {
                    entidad = response,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { entidad = responseDet, isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }







        [HttpGet]
        public async Task<JsonResult> RG_PendientesAprobRendGastos(int nivelAprobacion)
        {
            IEnumerable<SPG33_PendAprobRendGastos> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                if (nivelAprobacion == 40)
                {
                    lista = await objModel.PendientesAprobRendGastos(nivelAprobacion, Session["usuario"].ToString());
                }
                else
                {
                    lista = await objModel.PendientesAprobRendGastosGer(nivelAprobacion, Session["usuario"].ToString());
                }


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
        public async Task<JsonResult> RG_PendAprobRendGerencia(int nivelInterfaz)
        {
            IEnumerable<SPG68_PendAprobRendGer> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.PendientesAprobRendGerencia(nivelInterfaz, Session["usuario"].ToString());

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



        [HttpPost]
        public JsonResult RG_SetDevolucionRend(SPS_ParametroDevolucion parametro)
        {
            StatusResponse respuesta = new StatusResponse();

            if (Session["usuario"] != null)
            {

                RendicionGastosModel model = new RendicionGastosModel();
                SPS_ComprobanteParametro parametroSP = new SPS_ComprobanteParametro();

                parametroSP.opcion = parametro.opcion;
                parametroSP.idSolicitud = parametro.idSolicitud;
                parametroSP.secuencia = parametro.secuencia;
                parametroSP.montoDevolucion = parametro.monto;

                respuesta = model.SetRendicionComprobante(parametroSP);


                return Json(new
                {
                    entidad = respuesta,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { entidad = respuesta, isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult RG_PendientesAprobRendGastosDet(int idSolicitud)
        {
            SPG34_PendApbRdGstsCab entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = objModel.PendientesAprobRendGastosCab(idSolicitud);
                entidad.listaDetalle = objModel.PendientesAprobRendGastosDet(idSolicitud).ToList();

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



        [HttpPost]
        public JsonResult RG_AprobSolicitudRendido20(SPA_Parametro parametro)
        {
            StatusResponse response = new StatusResponse();
            SPA_Parametro parametroAprob = new SPA_Parametro();

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





        [HttpPost]
        public JsonResult RG_AprobGastoRendido20(SPA_Parametro parametro)
        {
            StatusResponse response = new StatusResponse();
            SPA_Parametro parametroAprob = new SPA_Parametro();

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
        public async Task<JsonResult> RG_ListaRendicionFinal(DateTime fechaInicio, DateTime fechaFin, int nivelInterfaz, string codigo)
        {
            IEnumerable<SPG36_RndGstsAprobGstsFnzas> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ListaRendicionFinal(nivelInterfaz, Session["usuario"].ToString(), fechaInicio.ToString("yyyyMMdd"), fechaFin.ToString("yyyyMMdd"), codigo);

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
        public FileResult RG_ExcelRendicionFinal(string fechaInicio, string fechaFin, int nivelInterfaz, string codigo)
        {
            RendicionGastosModel objModel = new RendicionGastosModel();
            IEnumerable<SPG55_AprobFinalExcel> lista = null;
            lista = objModel.AprobacionFinalLista(nivelInterfaz, Session["usuario"].ToString(), fechaInicio, fechaFin, codigo);
            return File(objModel.AprobacionFinalExcel(lista.ToList()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OC_Aprobadas_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }



        [HttpGet]
        public JsonResult RG_RendGstsPendDetalle30(int idSolicitud)
        {
            SPG37_DetalleCab entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = objModel.RendGstsPend30Cab(idSolicitud);
                entidad.listaDetalle = new List<SPG38_DetalleDet>();
                entidad.listaDetalle = objModel.RendGstsPend30Det(idSolicitud).ToList();

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


        [HttpPost]
        public JsonResult RG_AprobRendicionGastos40(SPA_Parametro parametro)
        {
            StatusResponse response = new StatusResponse();
            SPA_Parametro parametroAprob = new SPA_Parametro();

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
        public async Task<JsonResult> RG_TiposAprobGstsFnzs()
        {
            IEnumerable<SPG39_Tipos> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.TiposAprobGstsFnzs();

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
        public async Task<JsonResult> RG_ColaboradorSofya(string filtro, int nivelInterfaz)
        {
            IEnumerable<SPG40_Colaborador> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ColaboradorSofya(filtro, Session["usuario"].ToString(), nivelInterfaz);

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
        public async Task<JsonResult> RG_RendicionDetalle(int idRendCompte)
        {
            IEnumerable<SPG41_RendicionDetalle> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.RendicionDetalle(idRendCompte);

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
        public async Task<JsonResult> RG_BuscarColaboradorXDNI(string dni)
        {
            SPG42_ColaboradorSofya entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = await objModel.BuscarColaboradorXDNI(dni);

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
        public async Task<JsonResult> RG_EstadoSolicitud(int idSolicitud)
        {
            SPG43_EstadoSolicitud entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = await objModel.EstadoSolicitud(idSolicitud);

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
        public async Task<JsonResult> RG_UnidadesMedida()
        {
            IEnumerable<SPG44_UnidadesMedida> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.UnidadesMedida();

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
        public async Task<JsonResult> RG_SolicitudDetReembolso(int idSolicitud)
        {
            IEnumerable<SPG45_SolicitudDetalle> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.SolicitudDetReembolso(idSolicitud);

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
        public async Task<JsonResult> RG_ComprobantesRegistrados(int idSolicitud)
        {
            IEnumerable<SPG46_CmptsRegistrados> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ComprobantesRegistrados(idSolicitud);

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
        public JsonResult RG_ComprobateCabecera(int idRendCompte)
        {
            SPG47_CmpbteCabecera entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = objModel.ComprobateCabecera(idRendCompte);

                return Json(new
                {
                    entidad = entidad,
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
        public JsonResult RG_ComprobateDetalle(int idRendCompte)
        {
            IEnumerable<SPG48_CmpbteDetalle> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = objModel.ComprobateDetalle(idRendCompte);

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
        public JsonResult RG_ComprobanteDetalleReembolso(int idRendCompte)
        {
            IEnumerable<SPG48_CmpbteDetalle> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = objModel.ComprobateDetalle(idRendCompte);

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
        public async Task<JsonResult> RG_Periodos()
        {
            IEnumerable<SPG56_Periodos> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.Periodos();

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
        public async Task<JsonResult> RG_ListarAprobados30Cab(int nivelInterfaz, string fechaInicio, string fechaFin)
        {
            IEnumerable<SPG57_Aprobados30> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.Aprobados30Cab(Session["usuario"].ToString(), nivelInterfaz, fechaInicio, fechaFin);

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
        public async Task<JsonResult> RG_ListarRechazados30Cab(int nivelInterfaz, string fechaInicio, string fechaFin)
        {
            IEnumerable<SPG58_Rechazados30> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.Rechazados30Cab(Session["usuario"].ToString(), nivelInterfaz, fechaInicio, fechaFin);

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
        public async Task<JsonResult> RG_TiposAnulacionCabecera()
        {
            IEnumerable<SPG59_TiposAnulacionCab> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.TiposAnulacionCabecera();

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
        public async Task<JsonResult> RG_TiposAnulacionDetalle()
        {
            IEnumerable<SPG60_TiposAnulacionDet> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.TiposAnulacionDetalle();

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
        public async Task<JsonResult> RG_SolicitudComprobante(int idRendCompte)
        {
            IEnumerable<SPG62_SolicitudCpte> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.SolicitudComprobante(idRendCompte);

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
        public async Task<JsonResult> RG_Sedes()
        {
            IEnumerable<SPG63_Sede> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.Sedes();

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
        public async Task<JsonResult> RG_ListaPendienteCierre(int nivelInterfaz, string fechaInicio, string fechaFin)
        {
            IEnumerable<SPG67_PendienteCierre> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.ListaPendienteCierre(nivelInterfaz, Session["usuario"].ToString(), fechaInicio, fechaFin);

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
        public JsonResult RG_AprobJefDetalleSol(int idSolicitud)
        {
            SPG69_DetalleCabecera entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = objModel.AprobJefSolCabecera(idSolicitud);
                entidad.listaDetalle = objModel.AprobJefSolDetalle(idSolicitud).ToList();

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
        public JsonResult RG_TipoAprobacionRend()
        {
            IEnumerable<SPG71_TipoAprob> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = objModel.tipoAprobacionRend(Session["usuario"].ToString());

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
        public JsonResult RG_AprobFinalfDetalleSol(int idSolicitud)
        {
            SPG72_DetalleCabecera entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = objModel.AprobFinalSolCabecera(idSolicitud);
                entidad.listaDetalle = objModel.AprobFinalSolDetalle(idSolicitud).ToList();

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
        public async Task<JsonResult> RG_CptesRegistrados(int idsolicitud, int secuencia)
        {
            IEnumerable<SPG75_CptesRegistrados> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.CptesRegistrados(idsolicitud, secuencia);

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
        public async Task<JsonResult> RG_TienePendienteRend()
        {
            string estado = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                estado = await objModel.TienePendienteRend(Session["usuario"].ToString());

                return Json(new
                {
                    estado = estado,
                    isRedirect = false,
                    redirectUrl = ""

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { estado = "", isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public async Task<JsonResult> RG_PendientesAprobacionGeneral(int nivelAprobacion)
        {
            IEnumerable<SPG78_PendientesAprobacion> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = await objModel.PendientesAprobacionGeneral(nivelAprobacion, Session["usuario"].ToString());

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
        public JsonResult RG_TipoAprobacionGeneral()
        {
            IEnumerable<SPG79_TipoAprobacion> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = objModel.tipoAprobacionGeneral(Session["usuario"].ToString());

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




        [HttpPost]
        public JsonResult RG_AprobarSolicitudGeneral(SPS_ParametroAprob parametro)
        {
            StatusResponse response = new StatusResponse();
            SPA_Parametro parametroAprob = new SPA_Parametro();
            bool todoCorrecto = true;

            if (Session["usuario"] != null)
            {
                try
                {
                    RendicionGastosModel model = new RendicionGastosModel();

                    foreach (var item in parametro.solicitudesArray)
                    {
                        if (todoCorrecto)
                        {
                            parametroAprob = new SPA_Parametro();

                            parametroAprob.opcion = parametro.opcion;
                            parametroAprob.opcionTipoAprob = parametro.opcionTipoAprob;
                            parametroAprob.usuario = Session["usuario"].ToString();
                            parametroAprob.nivelInterfaz = parametro.nivelInterfaz;
                            parametroAprob.idSolicitud = Convert.ToInt32(item.idsolicitud);
                            parametroAprob.idAnulado = parametro.idAnulado;

                            response = model.AprobRendicionGastos(parametroAprob);

                            if (response.idResponse == 0)
                            {
                                todoCorrecto = false;
                            }
                        }
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
        public JsonResult RG_ReembolsoCpteCabecera(int idRendCompte)
        {
            SPG80_CpteCabecera entidad = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                entidad = objModel.ReembolsoCpteCabecera(idRendCompte);

                return Json(new
                {
                    entidad = entidad,
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
        public JsonResult RG_ReembolsoCpteDetalle(int idRendCompte)
        {
            IEnumerable<SPG81_CpteDetalle> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = objModel.ReembolsoCpteDetalle(idRendCompte);

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
        public JsonResult RG_DetalleComprobantes(int idSolicitud)
        {
            IEnumerable<SPG82_CptesRegistrados> lista = null;

            if (Session["usuario"] != null)
            {
                RendicionGastosModel objModel = new RendicionGastosModel();

                lista = objModel.DetalleComprobantes(idSolicitud);

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



        /* ************************************************************************************************** */


        public static int ID_SOLICITUD = 0;
        public static int ID_REND_COMPTE = 0;
        public static int TIPO_COMPROBANTE = 0;


        [HttpGet]
        public JsonResult RG_BuscarIdCompte(ParamtroRpteInd parametro)
        {
            RendicionGastosModel objModel = new RendicionGastosModel();

            ID_REND_COMPTE = parametro.idRendCompte;
            TIPO_COMPROBANTE = parametro.idTipoComp;

            return Json(new { respuesta = 1 }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult MapearReportePLPDF()
        {
            return new ActionAsPdf("PlanillaMovReportePDF") { FileName = "Reporte.pdf" };
        }

        public ActionResult PlanillaMovReportePDF()
        {
            RendicionGastosModel objModel = new RendicionGastosModel();
            ReportePMPDFViewModel model = new ReportePMPDFViewModel();

            model.planillaMovilidadCab = objModel.ComprobateCabecera(ID_REND_COMPTE);
            model.planillaMovilidadDet = objModel.ComprobateDetalle(ID_REND_COMPTE).ToList();

            model.planillaMovilidadCab.firma_usuario = @"/finanzas/getFile?ruta=" + model.planillaMovilidadCab.firma_usuario;
            model.planillaMovilidadCab.firma_jefe = @"/finanzas/getFile?ruta=" + model.planillaMovilidadCab.firma_jefe;

            return View(model);
        }



        [HttpGet]
        public ActionResult MapearReporteDJPDF()
        {
            return new ActionAsPdf("DeclaracionJurReportePDF") { FileName = "Reporte.pdf" };
        }

        public ActionResult DeclaracionJurReportePDF()
        {
            RendicionGastosModel objModel = new RendicionGastosModel();
            ReporteDJPDFViewModel model = new ReporteDJPDFViewModel();

            model.declaracionJuradaCab = objModel.RpteCpbteIndivualDJCab(ID_REND_COMPTE);

            model.declaracionJuradaCab.firma_usuario = @"/finanzas/getFile?ruta=" + model.declaracionJuradaCab.firma_usuario;
            model.declaracionJuradaCab.firma_jefe = @"/finanzas/getFile?ruta=" + model.declaracionJuradaCab.firma_jefe;


            List<SPG52_DecJurDet> listaDetalle = objModel.RpteCpbteIndivualDJDet(ID_REND_COMPTE).ToList();

            model.declaracionJuradaDet_1 = listaDetalle.Where(x => x.seccion == 1).ToList();
            model.declaracionJuradaDet_2 = listaDetalle.Where(x => x.seccion == 2).ToList();

            model.declaracionJuradaCab.parrafo1 = " " + model.declaracionJuradaCab.nomapellidos + " , identificado(a) con DNI N° " + model.declaracionJuradaCab.dni + "  , " + " " + model.declaracionJuradaCab.cargo + " de " + model.declaracionJuradaCab.ceco + " de la empresa Textile Sourcing Company S.A.C. con número de RUC 20550330050, declaro bajo juramento haber realizado el viaje a la ciudad " + model.declaracionJuradaCab.ciudad + " en el país de " + model.declaracionJuradaCab.pais + "  durante el periodo comprendido desde el (" + model.declaracionJuradaCab.diaDesde + ") hasta el (" + model.declaracionJuradaCab.diaHasta + ") del mes de " + model.declaracionJuradaCab.mesDesde + " de (" + model.declaracionJuradaCab.anioHasta + "), incurriendo en los gastos que se detallan a continuación, los cuales no han podido ser sustentados con documentos emitidos por el prestador del servicios:";
            model.declaracionJuradaCab.parrafo2 = "Me afirmo y ratifico en lo expresado, en señal de lo cual firmo el presente documentos en la ciudad de Lima, a los " + model.declaracionJuradaCab.dia + "   días del mes de " + model.declaracionJuradaCab.mes + "   del año " + model.declaracionJuradaCab.anio + "   .";

            return View(model);
        }





        [HttpGet]
        public JsonResult RG_BuscarIdSolicitud(int idSolicitud)
        {
            ID_SOLICITUD = idSolicitud;
            return Json(new { respuesta = 1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult MapearReporteCXSPDF()
        {

            string _headerUrl = Url.Action("HeaderCpteXSolicitudPDF", "Finanzas", null, "http");
            string _footerUrl = Url.Action("FooterCpteXSolicitudPDF", "Finanzas", null, "http");

            string customSwitches = string.Format("--header-html \"{0}\" " +
                            " --header-spacing \"0\" " +
                            "--footer-html \"{1}\" " +
                            " --footer-spacing \"0\" " +
                            " --header-font-size \"12\" ", _headerUrl, _footerUrl);

            return new ActionAsPdf("ComprobanteXSolicitudPDF")
            {
                FileName = "Reporte.pdf",
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = { Top = 30, Bottom = 20 },
                CustomSwitches = customSwitches
            };
        }



        public ActionResult HeaderCpteXSolicitudPDF()
        {
            return View();
        }

        public ActionResult FooterCpteXSolicitudPDF()
        {
            ViewBag.usuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return View();
        }

        public ActionResult ComprobanteXSolicitudPDF()
        {
            RendicionGastosModel objModel = new RendicionGastosModel();
            ReportePDFViewModel model = new ReportePDFViewModel();
            int c = 1;

            model.cabecera = objModel.RpteCpbteIndivualCab(ID_SOLICITUD);
            model.detalle = objModel.RpteCpbteIndivualDet(ID_SOLICITUD).ToList();

            model.cabecera.firma_usuario = @"/finanzas/getFile?ruta=" + model.cabecera.firma_usuario;
            model.cabecera.firma_jefe = @"/finanzas/getFile?ruta=" + model.cabecera.firma_jefe;

            foreach (var item in model.detalle)
            {
                item.contador = c;
                c++;
            }

            return View(model);
        }





        [HttpGet]
        public ActionResult MapearCpteSalidaCajaPDF()
        {
            return new ActionAsPdf("CpteSalidaCajaPDF") { FileName = "Comprobante.pdf" };
        }

        public ActionResult CpteSalidaCajaPDF()
        {
            RendicionGastosModel objModel = new RendicionGastosModel();
            SPG61_CpteSalidaCajaViewModel model = new SPG61_CpteSalidaCajaViewModel();

            model = objModel.CpteSalidaCaja(ID_SOLICITUD);

            return View(model);
        }






        [HttpGet]
        public FileContentResult getFile(string ruta)
        {
            ArchivosModelo objArchivosM = new ArchivosModelo();
            var file = objArchivosM.getFile(ruta, "nombre");
            string extencion = Path.GetExtension(ruta);
            return File(file, "image/" + extencion);
        }





        #endregion

    }
}