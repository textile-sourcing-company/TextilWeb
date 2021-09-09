using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//MODELOS
using TSC_WEB.Models.Modelos.Sistema;
//ENTIDADES
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Controllers
{
    public class LoginController : Controller
    {
        EmpresasModelo objEmpresasM = new EmpresasModelo(); // EMPRESAS

        // GET: Login
        public ActionResult Index(string nombre1, string apellido1)
        {
            var v_url = Request.Url.OriginalString;

            try
            {
                if (Session["usuario"] == null)
                {
                    return View(objEmpresasM.Listar());
                    //return View();
                }
                else
                {
                    return Redirect("/dashboard/index");
                }
            }
            catch
            {
                return Redirect("/sistema/error500/");
            }
        }


    }
}