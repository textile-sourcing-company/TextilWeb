using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSC_WEB.Models.Modelos.ControlInterno.Liquidacion;
using TSC_WEB.Models.Entidades.ControlInterno.Liquidacion;
using TSC_WEB.Models.Entidades.ControlInterno ;
using TSC_WEB.Models.Modelos.ControlInterno.ReporteStock;
using TSC_WEB.Models.Entidades.MovimientoPorPeriodo;
using System.Web.Script.Serialization;


namespace TSC_WEB.Controllers
{
    public class ControlInternoController : Controller
    {

        #region INSTANCIAS

        LiquidacionModelo objLiquiM = new LiquidacionModelo();
        ReporteStock objReporteStock = new ReporteStock();
        #endregion

        #region VISTAS
        public ActionResult FichasLiquidacionConfirmar()
        {   
            //string mensaje
            if (Session["usuario"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }
        //  EPORTE DE STOCK
        public ActionResult ReporteStock()
        {
            ////string mensaje
            //if (Session["usuario"] != null)
            //{
            //    return View(objReporteStock.ConsultaReporteStock("74"));
            //}
            //else
            //{
            //    return Redirect("/");
            //}
            return View();
        }

        public ActionResult FichasStockConfirmadas()
        {
            //string mensaje
            if (Session["usuario"] != null)
            {
                return View(objLiquiM.GetFichasStock());
            }
            else
            {
                return Redirect("/");
            }
        }



        #endregion

        #region METODOS

            #region LIQUIDACION

                // EJECUTAOS TRANSACCION
                [HttpPost]
                public JsonResult setTmpFichas(string[] fichas)
                {

                    List<string> fichasError = new List<string>();

                    foreach (var ficha in fichas)
                    {
                        string mensaje = string.Empty;
                        var response = objLiquiM.SetStockInicial(Session["usuario"].ToString(),ficha , out mensaje);


                        if (!response)
                        {
                            fichasError.Add(ficha);
                        }
                    }


                    //  RETORNAMOS
                    if (fichasError.Count == 0)
                    {
                        return Json(new { success = true, mensaje = "Realizado correctamente" });
                }
                else
                    {
                        return Json(new { success = false, mensaje = "Error al insertar fichas en temporal" ,fichas = fichasError });
                    }

            }
            
                // OBTENEMOS LAS FICHAS POR CONFIRMAR
                [HttpGet]
                public JsonResult getFichasPorConfirmar()
                {
                    return Json(objLiquiM.GetFichasPendientes(),JsonRequestBehavior.AllowGet);
                }
        //Reporte de stock//

        [HttpPost]
        public JsonResult ListarReporteStock(string CodAlmacen)
        {
            ReporteStock modelo = new ReporteStock();

            var serializer = new JavaScriptSerializer();
            //Se cambia el Length directo a nuestra referencia
            serializer.MaxJsonLength = 500000000;


            var lista_temp = new List<EBMovimientoPorPeriodo>();
            lista_temp = modelo.ConsultaReporteStock(CodAlmacen);

            var json = Json(lista_temp, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;

            //return Json(lista_temp, JsonRequestBehavior.AllowGet);
        }

        

        #endregion

        #region LIQUIDACION


        #endregion
        #endregion

    }
}