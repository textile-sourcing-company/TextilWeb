using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using TSC_WEB.Models.Modelos.Seguridad;
using TSC_WEB.Models.Modelos.Seguridad.Requerimientos;
using System.Web.Script.Serialization;

namespace TSC_WEB.Controllers
{
    public class SeguridadController : Controller
    {
        //HOLIS
        #region INSTANCIAS
        CambioFechaModelo objCambioFecha = new CambioFechaModelo();
        ResponsblesModelo objResponsablesM = new ResponsblesModelo();
        AreasModelo objAreasM = new AreasModelo();
        GerenciaModelo objGerenciasM = new GerenciaModelo();
        RequerimientosModelo objRequerimientoM = new RequerimientosModelo();



        #endregion

        #region VISTAS
        public ActionResult VerCambioFecha()
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

        public ActionResult RequerimientosProgramador()
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


        [HttpGet]
        public ActionResult ViewReporteRequerimientos()
        {
            return View();
        }

        #endregion

        #region METODOS
        public JsonResult VerCambioFechaLista()
        {
            return Json(objCambioFecha.Listar(), JsonRequestBehavior.AllowGet);
        }


        #region     REQUERIMIENTOS
        //Listamos los Responsables para llenar el combo
        public JsonResult ListarResponsbles()
        {
            return Json(objResponsablesM.ListarResponsables(), JsonRequestBehavior.AllowGet);
        }

        //Listamos LAs Areas para llenar el combo
        public JsonResult ListarAreas()
        {
            return Json(objAreasM.ListarAreas(), JsonRequestBehavior.AllowGet);
        }

        //Listamos las gerencias para llenar el combo
        public JsonResult ListarGerencias()
        {
            return Json(objGerenciasM.ListarGerencias(), JsonRequestBehavior.AllowGet);
        }


        //Listamos Responsables
        [HttpGet]
        public async Task<JsonResult> ListarResponsablesREQ()
        {
            if (Session["usuario"] != null)
            {
                // ReporteVentasModelo objModel = new ReporteVentasModelo();
                ResponsblesModelo objResponsablesM = new ResponsblesModelo();
                var lista = await objResponsablesM.ListarResponsablesREQ();

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


        //Listamos GERENCIAS 
        [HttpGet]
        public async Task<JsonResult> ListarGerencias2()
        {
            if (Session["usuario"] != null)
            {
                // ReporteVentasModelo objModel = new ReporteVentasModelo();
                GerenciaModelo objGerenciaM = new GerenciaModelo();
                var lista = await objGerenciaM.ListarGerencias2();

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


        //Listamos LOS ESTADOS 
        [HttpGet]
        public async Task<JsonResult> ListarEstados()
        {
            if (Session["usuario"] != null)
            {
                // ReporteVentasModelo objModel = new ReporteVentasModelo();
                EstadosReqModelo objEstadosM = new EstadosReqModelo();
                var lista = await objEstadosM.ListarEstadosReq();

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

        public JsonResult ListarREquerimientosWeb(string tipo_fecha, string fechaInicio, string fechaFin,
                                                        string gerencia,
                                                                    string solicitante, string num_req, string responsable, string area, string estado)
        {
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            var json = Json(objRequerimientoM.ListarReporteRequerimientos(tipo_fecha, fechaInicio, fechaFin,gerencia, solicitante, num_req,responsable, area, estado), JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = 500000000;
            return json;

        }



        // LISTAMOS LOS REQUERIMIENTOS 
        [HttpGet]
        public async Task<JsonResult> ListarREquerimientosWeb2(string tipo_fecha, string fechaInicio, string fechaFin, string gerencia,
                                                                    string solicitante, string num_req, string responsable, string area, string estado)
        {
            if (Session["usuario"] != null)
            {
                RequerimientosModelo objModel = new RequerimientosModelo();

                var lista = await objModel.ListarReporteRequerimientos2(tipo_fecha, fechaInicio, fechaFin, gerencia, solicitante, num_req,
                                                                       responsable, area, estado);

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



        #endregion

        #endregion
    }
}