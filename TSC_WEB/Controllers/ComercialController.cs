using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TSC_WEB.Models.Entidades.Comercial;
using TSC_WEB.Models.Entidades.Comercial.ReportePenalidades;
using TSC_WEB.Models.Entidades.Sistema;
using TSC_WEB.Models.Modelos.Comercial;
using TSC_WEB.Models.Modelos.Comercial.PedidoVenta;
using TSC_WEB.Models.Modelos.Comercial.ReportePenalidades;
using TSC_WEB.Models.Modelos.Gerencia.ReporteCambio;
using TSC_WEB.Models.Modelos.Sistema;
using TSC_WEB.Config;
using System.Collections.Generic;
using System.Security.Cryptography;
using TSC_WEB.Models.Modelos.Almacenes.ReporteStockFecha;

namespace TSC_WEB.Controllers
{
    public class ComercialController : Controller
    {
        #region  INSTACIAS
        // INSTANCIAS
        Come002Modelo objEtapasM = new Come002Modelo();
        OneDriveApiModelo objOneDrive = new OneDriveApiModelo();

        BSPedidoVenta objPedidoVenta = new BSPedidoVenta();
        CentroCostoModelo objCentroCosto = new CentroCostoModelo();
        #endregion

        #region VISTAS
        [HttpGet]
        public ActionResult SubirPO()
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
        public ActionResult Actipedido()
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


        public ActionResult ReportePenalidades()
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
        [HttpPost]
        public async Task<JsonResult> RegistrarPOS(string po, string observacion, string cliente, HttpPostedFileBase archivo, string nomarchivo)
        {
            Come_002Entidad objFichaE = new Come_002Entidad();
            string usuario = Session["usuario"].ToString();
            int empresa = 1;
            //GUARDAMOS EL REGISTRO PARA OBTNER EL NOMBRE DEL ARCHIVO
            objFichaE = objEtapasM.RegistrarCome002(po, observacion, usuario, empresa);
            //VERIFICANDO SI HAY ERROR EN REGISTRAR

 

            if (!objFichaE.errorbool)
            {
                string rutaarchivo = @"D:/tmp_POs/";
                //CREANDO RUTA SI NO EXISTE
                try
                {
                    if (!Directory.Exists(rutaarchivo))
                    {
                        Directory.CreateDirectory(rutaarchivo);
                    }
                    objFichaE.ARCHIVO = objFichaE.ARCHIVO.Replace('/', '-').ToString();
                    rutaarchivo += objFichaE.ARCHIVO;
                    //GUARDANDO ARCHIVO
                    archivo.SaveAs(rutaarchivo);
                    //CONECTANDO CON API ONE DRIVE
                    try
                    {
                        //CONECTANDO ONE DRIVE
                        var resultado = await objOneDrive.ConectarApiOneDrive();
                        //SUBIENDO ARCHIVO
                        if (resultado)
                        {
                            //SUBIENDO ARCHIVO
                            var data = await objOneDrive.SubirArchivoPOS(rutaarchivo, cliente);
                            if (data != null)
                            {
                                //RUTA Y NOMBRE DEL ARCHIVO CREADO 
                                string rutanombrearchivo = "POs/" + cliente + "/" + objFichaE.ARCHIVO;
                                var datac = await objOneDrive.Compartir(rutanombrearchivo);

                                string json = datac.OriginalJson;
                                var jsonParse = JsonConvert.DeserializeObject<OneDriveApiEntidadMaster>(json);

                                var ruta = jsonParse.link.webUrl;
                                //CAMBIANDO RUTA
                                objEtapasM.ActualizarPO(ruta, objFichaE.IDUPLOAD);
                                //BORRANDO EL ARCHIVO
                                System.IO.File.Delete(rutaarchivo);
                                //RETORNANDO VALOR
                                return Json(new { error = false, mensaje = "PO registrada correctamente" });
                            }
                            else
                            {
                                return Json(new { error = true, mensaje = "No se pudo subir archivo" });
                            }
                        }
                        else
                        {
                            return Json(new { error = true, mensaje = "No se pudo conectar con el API OneDrive" });
                        }
                    }
                    catch
                    {
                        return Json(new { error = true, mensaje = "Error con API OneDrive" });
                    }
                }
                catch
                {
                    return Json(new { error = true, mensaje = "Error en guardar el archivo en la carpeta raiz" });
                }
            }
            else
            {
                return Json(new { error = true, mensaje = objFichaE.error });
            }



        }

        [HttpGet]
        public JsonResult BuscxPO(string po)
        {
            return Json(objEtapasM.BuscarxPO(po), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RptCome002(string po, string pedido ,string programa)
        {
            return Json(objEtapasM.RptCome002(po, pedido,programa), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ActiAnulpedido(string ppedidos)
        {
            bool isRedirect = false;
            string redirectUrl = "";
            bool rpt = false;
            string mensaje = "";

            try
            {
                if (Session["usuario"] != null)
                {
                    rpt = objPedidoVenta.Actipedido(ppedidos, Session["usuario"].ToString());
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

            return Json(new
            {
                rpt = rpt,
                isRedirect = isRedirect,
                redirectUrl = redirectUrl

            }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult Anulpedido(string ppedidos)
        {
            bool isRedirect = false;
            string redirectUrl = "";
            bool rpt = false;
            string mensaje = "";

            try
            {
                if (Session["usuario"] != null)
                {
                    rpt = objPedidoVenta.Anularpedido(ppedidos, Session["usuario"].ToString());
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

            return Json(new
            {
                rpt = rpt,
                isRedirect = isRedirect,
                redirectUrl = redirectUrl

            }, JsonRequestBehavior.AllowGet);

        }


        #region ReportePenalidades
        //Listar los centros de costos 
        public JsonResult ListarCombosCentroCostoModal(string v_centrocosto)
        {
            return Json(objCentroCosto.ListarCentrosCostoTotal(v_centrocosto), JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public async Task<JsonResult> ReportePenalidadesControler(string Cliente, string Documento , string Fechainicio, string Fechafin)
        {
            ReportePenalidadesModelo objModel = new ReportePenalidadesModelo();
            IEnumerable<ReportePenalidadesEntidad> lista = null;
            IEnumerable<ReportePenalidadesEntidad> listaOrdenada = null;           
            if (Session["usuario"] != null)
            {
                lista = await objModel.ListarPenalidades(Cliente,Documento,Fechainicio,Fechafin);
                listaOrdenada = lista;
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
        public JsonResult InsertarDatosPenalidadesControler(string documento, string concepto, string descripcion, string centrocosto, string usuarioregistro)
        {
            ReportePenalidadesModelo objpenalidades = new ReportePenalidadesModelo();
            string rptmensaje = "";
            //string rpterror = "";
            bool isRedirect = false;
            string redirectUrl = "";
            string mensaje = "";
            try
            {
                if (Session["usuario"] != null)
                {
                    objpenalidades.InsertarDatosPenalidades(documento, concepto, descripcion, centrocosto, Session["usuario"].ToString());                    
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

            return Json(new { mensaje = rptmensaje, success = isRedirect, redurl = redirectUrl }, JsonRequestBehavior.AllowGet);
        }





        #endregion

        #endregion
    }
}