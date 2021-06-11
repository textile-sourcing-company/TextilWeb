using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TSC_WEB.Models.Entidades.Facturacion.FacturaProveedores;
using TSC_WEB.Models.Modelos.Facturacion.FacturaProveedores;
using TSC_WEB.Models.Modelos.Facturacion.ReporteVentas;
using TSC_WEB.Models.Modelos.Sistema;

namespace TSC_WEB.Controllers
{
    public class FacturacionController : Controller
    {


        public ActionResult ReporteVentas()
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

        public ActionResult FacturaProveedores()
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


        public ActionResult PowerBI()
        {
            //if (Session["usuario"] != null)
            //{
                return View();
            //}
            //else
            //{
                //return Redirect("/");
            //}
        }


        // Reporte de Ventas.


        [HttpGet]
        public async Task<JsonResult> ListarVentas(string series, DateTime fechaIni, DateTime fechaFin)
        {
            if (Session["usuario"] != null)
            {
                ReporteVentasModelo objModel = new ReporteVentasModelo();

                var lista = await objModel.ListarVentas(series, fechaIni, fechaFin);

                var json = Json(new
                {
                    lista = lista,
                    isRedirect = false,
                    redirectUrl = ""
                }, JsonRequestBehavior.AllowGet);

                json.MaxJsonLength = 500000000;

                return json;

            }
            else
            {
                return Json(new { lista = new List<object>(), isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public async Task<JsonResult> ListarSeries()
        {
            if (Session["usuario"] != null)
            {
                ReporteVentasModelo objModel = new ReporteVentasModelo();
                var lista = await objModel.ListarSeries();

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
        public FileResult RepExcelVentas(string series, DateTime fechaIni, DateTime fechaFin)
        {
            ReporteVentasModelo objModel = new ReporteVentasModelo();
            var lista = objModel.ListaReporte(series, fechaIni, fechaFin);
            return File(objModel.ReporteExcel(lista), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVentas_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }







        // Factura De Proveedores.

        [HttpGet]
        public async Task<JsonResult> ListarFacturas(DateTime fechaIni, DateTime fechaFin)
        {
            if (Session["usuario"] != null)
            {
                FacturaProvModel objModel = new FacturaProvModel();
                Factura entidad = new Factura();


                var lista = await objModel.ListarVentas(fechaIni, fechaFin);

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
        public async Task<JsonResult> ActualizacionFactura()
        {
            if (Session["usuario"] != null)
            {
                FacturaProvModel objModel = new FacturaProvModel();
                RespuestaOperacion respuesta = null;
                string usuario = Session["usuario"].ToString();

                try
                {
                    var resultado = await objModel.ConectarApiOneDrive();

                    if (resultado)
                    {
                        respuesta = await objModel.LeerArchivos();
                        await objModel.InsertFechaActualizacion(usuario);
                    }
                    else
                    {
                        respuesta.estado = false;
                        respuesta.descripcion = "Error al conectar al API";
                    }
                }
                catch (Exception ex)
                {
                    respuesta.estado = false;
                    respuesta.descripcion = "Error al procesar informacion: " + ex.Message;
                }


                return Json(new
                {
                    resultado = respuesta.estado,
                    isRedirect = false,
                    redirectUrl = "",
                    mensaje = respuesta.descripcion

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { lista = false, isRedirect = true, redirectUrl = "/login/index", mensaje = "" }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public FileResult ReporteExcelFact(DateTime fechaIni, DateTime fechaFin)
        {
            FacturaProvModel objModel = new FacturaProvModel();
            var lista = objModel.ListarVentasExcel(fechaIni, fechaFin);

            return File(objModel.ReporteExcel(lista), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FacturaProveedores_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }



        [HttpGet]
        public async Task<JsonResult> UltimaActualizacion()
        {
            if (Session["usuario"] != null)
            {
                FacturaProvModel objModel = new FacturaProvModel();
                Factura entidad = new Factura();


                var ultAct = await objModel.UltimaActualizacion();

                return Json(new
                {
                    ultAct = ultAct,
                    isRedirect = false,
                    redirectUrl = ""
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ultAct = "", isRedirect = true, redirectUrl = "/login/index" }, JsonRequestBehavior.AllowGet);
            }
        }






    }
}