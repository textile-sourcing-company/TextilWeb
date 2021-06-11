using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Sistema;
using TSC_WEB.Models.Modelos.Sistema;


namespace TSC_WEB.Controllers
{
    public class SistemaController : Controller
    {
        //prueba
        #region INSTANCIAS

        SisTareasModelo objSistemaTareas = new SisTareasModelo();
        UsuariosModelo OBJUSUARIO = new UsuariosModelo();
        #endregion


        #region VISTAS

        [HttpGet]
        public ActionResult Error404()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Error500()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ViewLisUsuario()
        {
            return View();
        }
        #endregion


        #region METODOS

        [HttpGet]
        public JsonResult ListarTareas(int idtarea)
        {
            return Json(objSistemaTareas.ListarTareas(idtarea), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarUsuarios(string PDNI, string PFOTOCHECK, string PUSUARIO)
        {
            return Json(OBJUSUARIO.ListarUsuarios(PDNI, PFOTOCHECK, PUSUARIO), JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}