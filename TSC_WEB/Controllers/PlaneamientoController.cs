using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSC_WEB.Models.Modelos.Planeamiento;

namespace TSC_WEB.Controllers
{
    public class PlaneamientoController : Controller
    {
        #region INSTANCIAS

        PedidosModelo objPedidosM = new PedidosModelo();
        BSRegistroMerma OBJMERMACLIENTE = new BSRegistroMerma();
        #endregion

        #region VISTAS
        public ActionResult PedidosAmbulancia()
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
        public ActionResult AsignacionMermas()
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
        public JsonResult BuscarPedidosAmbulancia(int? pedido,string flag)
        {
            flag = flag == "0" ? null : "'"+flag+"'";
            return Json(objPedidosM.BuscarPedidos(flag), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GenerarMermaCliente(string pcgc9, string pcgc4, string pcgc2, int pqtdini, int pqtdfin, int pqtdperd, decimal pporcperd)
        {
            string rptmensaje = "";
            string rpterror = "";
            bool isRedirect = false;
            string redirectUrl = "";
            string mensaje = "";
            try
            {
                if (Session["usuario"] != null)
                {
                    OBJMERMACLIENTE.GenerarPorcMerma(pcgc9, pcgc4, pcgc2,0, pqtdini, pqtdfin, pqtdperd, pporcperd,"I", out rptmensaje, out rpterror);
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
            }

            return Json(new { mensaje = rptmensaje, success = isRedirect, redurl = redirectUrl, rsperror = rpterror }, JsonRequestBehavior.AllowGet);
        }
        //GENERAR MERMA POR CLIENTE DE FORMA MASIVA
        [HttpGet]
        public JsonResult GenerarMermaClienteMasiva(int pqtdini, int pqtdfin, int pqtdperd, decimal pporcperd)
        {
            string rptmensaje = "";
            string rpterror = "";
            bool isRedirect = false;
            string redirectUrl = "";
            string mensaje = "";
            var v_user = Session["usuario"].ToString();
            try
            {
                if (Session["usuario"] != null)
                {
                    OBJMERMACLIENTE.GenerarPorcMermaMasiva(0, pqtdini, pqtdfin, pqtdperd, pporcperd, "I", out rptmensaje, out rpterror, v_user);
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
            }

            return Json(new { mensaje = rptmensaje, success = isRedirect, redurl = redirectUrl, rsperror = rpterror }, JsonRequestBehavior.AllowGet);
        }
        //METODO QUE PERMITE EDITAR LOS VALORES DE LA MERMA
         [HttpGet]
        public JsonResult EditMermaCliente(string pcgc9, string pcgc4, string pcgc2,int pseq, int pqtdini, int pqtdfin, int pqtdperd, decimal pporcperd)
        {
            string rptmensaje = "";
            string rpterror = "";
            bool isRedirect = false;
            string redirectUrl = "";
            string mensaje = "";
            try
            {
                if (Session["usuario"] != null)
                {
                    OBJMERMACLIENTE.GenerarPorcMerma(pcgc9, pcgc4, pcgc2, pseq, pqtdini, pqtdfin, pqtdperd, pporcperd, "U", out rptmensaje, out rpterror);
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
            }

            return Json(new { mensaje = rptmensaje, success = isRedirect, redurl = redirectUrl, rsperror = rpterror }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeletMermaCliente(string pcgc9, string pcgc4, string pcgc2, int pseq)
         {
             string rptmensaje = "";
             string rpterror = "";
             bool isRedirect = false;
             string redirectUrl = "";
             string mensaje = "";
             try
             {
                 if (Session["usuario"] != null)
                 {
                     OBJMERMACLIENTE.GenerarPorcMerma(pcgc9, pcgc4, pcgc2, pseq, 0, 0, 0, 0, "D", out rptmensaje, out rpterror);
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
             }

             return Json(new { mensaje = rptmensaje, success = isRedirect, redurl = redirectUrl, rsperror = rpterror }, JsonRequestBehavior.AllowGet);
         }
        [HttpGet]
        public JsonResult BuscMermaxCliente(string pcgc9, string pcgc4, string pcgc2)
        {
            return Json(OBJMERMACLIENTE.ListarMermaCliente(pcgc9, pcgc4, pcgc2), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarClientesMermaMas(string pcgc9, string pcgc4, string pcgc2)
        {
            var v_user = Session["usuario"].ToString();
            OBJMERMACLIENTE = new BSRegistroMerma();
            return Json(OBJMERMACLIENTE.ListarClienteMasiva(pcgc9, pcgc4, pcgc2, v_user), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AgregaClientesMerma(string pcgc9, string pcgc4, string pcgc2)
        {
            OBJMERMACLIENTE = new BSRegistroMerma();
            string rptmensaje = "";
            string rpterror = "";
            bool isRedirect = false;
            string redirectUrl = "";
            string mensaje = "";
            var v_user = Session["usuario"].ToString();
            
            try
            {
                if (Session["usuario"] != null)
                {
                    OBJMERMACLIENTE.AgrDelPedidosMermaCliente(pcgc9, pcgc4, pcgc2, v_user, "I", out rptmensaje, out rpterror);
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
            }
            return Json(new { mensaje = rptmensaje, success = isRedirect, redurl = redirectUrl, rsperror = rpterror }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteClientesMermaMas(int pseq)
        {
            OBJMERMACLIENTE = new BSRegistroMerma();
            string rptmensaje = "";
            string rpterror = "";
            bool isRedirect = false;
            string redirectUrl = "";
            string mensaje = "";
            var v_user = Session["usuario"].ToString();

            try
            {
                if (Session["usuario"] != null)
                {
                    OBJMERMACLIENTE.DeleteClienteMasivo(pseq, out rptmensaje, out rpterror, v_user);
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
            }
            return Json(new { mensaje = rptmensaje, success = isRedirect, redurl = redirectUrl, rsperror = rpterror }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}