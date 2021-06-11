using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TSC_WEB.Models.Entidades.OperacionesProceso.CambioTaller;
using TSC_WEB.Models.Entidades.Sistema;
using TSC_WEB.Models.Modelos.Contabilidad.ReporteGuiasConta;
using TSC_WEB.Models.Entidades.Contabilidad.ReporteGuiasConta;
using System.Web;

namespace TSC_WEB.Controllers
{
    public class ContabilidadController : Controller
    {
        #region VISTAS

        public ActionResult ReporteGuiasConta()
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
        public async Task<JsonResult> ListarReporteGuiasConta(DateTime fechaIni, DateTime fechaFin, int? numguia, string serieguia)
        {
            if (Session["usuario"] != null)
            {
                ReporteGuiasConta objModel = new ReporteGuiasConta();

                var lista = await objModel.ListarReporteGuiasConta(fechaIni, fechaFin, numguia, serieguia);

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

        //[HttpGet]
        //public FileResult RepExcelCambio(DateTime fechaIni, DateTime fechaFin, int? numguia, string serieguia)
        //{
        //    //ReporteGuiasConta objModel = new ReporteGuiasConta();
        //    //var lista = objModel.ListarReporteGuiasConta(fechaIni, fechaFin, numguia, serieguia);
        //    //return File(objModel.ReporteExcel(lista), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteGuiasContabilidad_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        //}

        #endregion
    }
}