using Dapper;
using Dapper.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using KoenZomers.OneDrive.Api;
using KoenZomers.OneDrive.Api.Entities;
using KoenZomers.OneDrive.Api.Enums;
using Newtonsoft.Json;
using System.Web.Configuration;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Facturacion.FacturaProveedores;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.IO.Compression;

namespace TSC_WEB.Models.Modelos.Facturacion.FacturaProveedores
{
    public class FacturaProvModel
    {

        //INSTANCIA ONE DRIVE API
        OneDriveApi OneDriveApi;
        //INSTACIA PARA CRENDECIALES DE ONE DRIVE
        string clienteid = string.Empty;
        string aplicacionid = string.Empty;
        string clientesecreto = string.Empty;

        string RefreshToken = string.Empty;


        public async Task<List<ListaFacturas>> ListarVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();

                    parametros.Add("I_OPCION", 1, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);

                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<ListaFacturas>(sql: "USYSTEX.SPU_GET_FACTURA_PROV",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);
                    return resultado
                              .OrderByDescending(x => x.RUC).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> InsertarFact(Factura entidad)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("USYSTEX.SPU_SET_FACTURA_PROV", conexion);
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add(new OracleParameter("P_RS", entidad.RS));                          //P_RS VARCHAR2 DEFAULT NULL,      
                    comando.Parameters.Add(new OracleParameter("P_SERIE_NUM", entidad.SERIE_NUM));            //P_SERIE_NUM VARCHAR2 DEFAULT NULL,
                    comando.Parameters.Add(new OracleParameter("P_FECHA_EMI", entidad.FECHA_EMI));            //P_FECHA_EMI VARCHAR2 DEFAULT NULL,
                    comando.Parameters.Add(new OracleParameter("P_FECHA_VEN", entidad.FECHA_VEN));            //P_FECHA_VEN      VARCHAR2 DEFAULT NULL,
                    comando.Parameters.Add(new OracleParameter("P_COND_PAGO", entidad.COND_PAGO));            //P_COND_PAGO VARCHAR2 DEFAULT NULL,
                    comando.Parameters.Add(new OracleParameter("P_OC", entidad.OC));                          //P_OC             VARCHAR2 DEFAULT NULL,
                    comando.Parameters.Add(new OracleParameter("P_GR", entidad.GR));                          //P_GR VARCHAR2 DEFAULT NULL,
                    comando.Parameters.Add(new OracleParameter("P_MONEDA", entidad.MONEDA));                  //P_MONEDA VARCHAR2 DEFAULT NULL,
                    comando.Parameters.Add(new OracleParameter("P_V_VENTA", entidad.V_VENTA));                   //P_V_VENTA        NUMBER DEFAULT NULL,
                    comando.Parameters.Add(new OracleParameter("P_IGV", entidad.IGV));                    //P_IGV NUMBER DEFAULT NULL,
                    comando.Parameters.Add(new OracleParameter("P_TOTAL", entidad.TOTAL));                    //P_TOTAL          NUMBER DEFAULT NULL,
                    comando.Parameters.Add(new OracleParameter("P_RUC", entidad.RUC));                        //P_RUC VARCHAR2 DEFAULT NULL


                    await comando.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> ConectarApiOneDrive()
        {
            RefreshToken = WebConfigurationManager.AppSettings["OneDriveApiRefreshToken"].ToString();
            aplicacionid = WebConfigurationManager.AppSettings["GraphApiApplicationId"].ToString();

            OneDriveApi = new OneDriveGraphApi(aplicacionid);

            try
            {
                await OneDriveApi.AuthenticateUsingRefreshToken(RefreshToken);
                return true;
            }
            catch (Exception e)
            {
                string mensaje = e.Message;
                return false;
            }

        }


        public async Task<RespuestaOperacion> LeerArchivos()
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            var data = await OneDriveApi.GetDriveRootChildren();


            foreach (var item in data.Collection)
            {

                if (item.Name.Trim() == "Facturas de Proveedores")
                {
                    var carpetas = await OneDriveApi.GetAllChildrenByFolderId(item.Id);

                    foreach (var item1 in carpetas)
                    {

                        if (item1.Name.Trim() == "TEXTILE SOURCING COMPANY S.A.C")
                        {
                            var carpetas2 = await OneDriveApi.GetAllChildrenByFolderId(item1.Id);

                            foreach (var item2 in carpetas2)
                            {
                                //if (item2.Name == "")
                                //{
                                    var carpetas3 = await OneDriveApi.GetAllChildrenByFolderId(item2.Id);

                                    #region LeerXML
                                    var listatemp = carpetas3.Where(x => x.Name.EndsWith(".XML") || x.Name.EndsWith(".xml"));
                                    var lista = listatemp.Where(x => !x.Name.StartsWith("."));

                                    foreach (var item4 in lista)
                                    {

                                        using (var stream = await OneDriveApi.DownloadItem(item4))
                                        {
                                            using (var writer = new StreamReader(stream))
                                            {

                                                Factura factura = new Factura();

                                                string numSerie = "";
                                                string ruc = "";
                                                string razons = "";
                                                string femi = "";
                                                string fven = "";
                                                string condp = "";
                                                string oc = "";
                                                string gr = "";
                                                string mod = "";
                                                decimal og = 0;
                                                decimal total = 0;
                                                decimal igv = 0;


                                                String archivoXML = await writer.ReadToEndAsync();
                                                archivoXML = archivoXML.Replace("\"", "*");
                                                archivoXML = archivoXML.Replace("\n", "*");


                                                // NUMERO Y SERIE
                                                try
                                                {
                                                    int ig_numSer = archivoXML.IndexOf("</cbc:CustomizationID>");

                                                    if (ig_numSer == -1)
                                                    {
                                                        // enfocado principalmente a recibo por honorario.
                                                        int ig_numSer_t = archivoXML.IndexOf("<cac:OrderReference>");
                                                        string a_numSer_t = archivoXML.Substring(ig_numSer_t, 90);

                                                        int p1_numSer = a_numSer_t.IndexOf("<cbc:ID>");
                                                        int p2_numSer = a_numSer_t.IndexOf("</cbc:ID>");

                                                        var numSerie_tmp_t = a_numSer_t.Substring(p1_numSer, (p2_numSer - p1_numSer));
                                                        numSerie = numSerie_tmp_t.Substring(8, numSerie_tmp_t.Length - 8);
                                                    }
                                                    else
                                                    {
                                                        // enfoncado principalmente a facturas
                                                        string a_numSer = archivoXML.Substring(ig_numSer, 475);
                                                        int p1_numSer = a_numSer.IndexOf("<cbc:ID>");
                                                        int p2_numSer = a_numSer.IndexOf("</cbc:ID>");
                                                        var numSerie_tmp = a_numSer.Substring(p1_numSer, (p2_numSer - p1_numSer));
                                                        numSerie = numSerie_tmp.Substring(8, numSerie_tmp.Length - 8);
                                                    }

                                                }
                                                catch (Exception)
                                                {
                                                    numSerie = null;
                                                }


                                                // RUC
                                                try
                                                {
                                                    int ig_ruc = archivoXML.IndexOf("<cac:SignatoryParty>");
                                                    string a_ruc = archivoXML.Substring(ig_ruc, 540);
                                                    int p2_ruc = a_ruc.IndexOf("</cbc:ID>");

                                                    if (p2_ruc == -1)
                                                    {
                                                        // enfoncado principalmente a recibo por honorario
                                                        int ig_ruc_t = archivoXML.IndexOf("<cac:AccountingSupplierParty>");
                                                        string a_ruc_t = archivoXML.Substring(ig_ruc_t, 170);
                                                        int p2_ruc_t = a_ruc_t.IndexOf("</cbc:CustomerAssignedAccountID>");
                                                        int p1_ruc_t = p2_ruc_t - 18;
                                                        a_ruc_t = a_ruc_t.Substring(0, p2_ruc_t);

                                                        String tmp_ruc_t = a_ruc_t.Substring(p1_ruc_t, a_ruc_t.Length - p1_ruc_t);
                                                        int p1_ruc1_t = tmp_ruc_t.IndexOf(">");
                                                        ruc = tmp_ruc_t.Substring(p1_ruc1_t + 1, (tmp_ruc_t.Length - p1_ruc1_t - 1));

                                                    }
                                                    else
                                                    {
                                                        // enfocado principalmente a facturas normales
                                                        int p1_ruc = p2_ruc - 18;

                                                        a_ruc = a_ruc.Substring(0, p2_ruc);

                                                        String tmp_ruc = a_ruc.Substring(p1_ruc, a_ruc.Length - p1_ruc);
                                                        int p1_ruc1 = tmp_ruc.IndexOf(">");
                                                        ruc = tmp_ruc.Substring(p1_ruc1 + 1, (tmp_ruc.Length - p1_ruc1 - 1));

                                                    }


                                                }
                                                catch (Exception ex)
                                                {
                                                    ruc = null;
                                                }


                                                // Razon Social
                                                try
                                                {
                                                    int ig_razons = archivoXML.IndexOf("<cac:AccountingSupplierParty>");
                                                    string a_razons = archivoXML.Substring(ig_razons, 630);
                                                    int p1_razons_t = a_razons.IndexOf("<cbc:RegistrationName>");

                                                    if (p1_razons_t != -1)
                                                    {
                                                        int p2_razons_t = a_razons.IndexOf("</cbc:RegistrationName>");

                                                        var razons_tmp = a_razons.Substring(p1_razons_t, (p2_razons_t - p1_razons_t));
                                                        String razons_tmp2 = razons_tmp.Substring(22, razons_tmp.Length - 22);

                                                        int posi_rs1 = razons_tmp2.IndexOf("<![CDATA");

                                                        if (posi_rs1 != -1)
                                                        {
                                                            int posi_rs2 = razons_tmp2.IndexOf("]]>");
                                                            razons = razons_tmp2.Substring(posi_rs1 + 9, posi_rs2 - posi_rs1 - 9);
                                                        }
                                                        else
                                                        {
                                                            razons = razons_tmp2;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        int p1_razons = a_razons.IndexOf("<cbc:Name>");
                                                        int p2_razons = a_razons.IndexOf("</cbc:Name>");
                                                        var razons_tmp = a_razons.Substring(p1_razons, (p2_razons - p1_razons));
                                                        razons = razons_tmp.Substring(10, razons_tmp.Length - 10);

                                                        var sss = razons.Substring(0, 9);

                                                        if (razons.Substring(0, 9) == "<![CDATA[")
                                                        {
                                                            String razons_tmp2 = razons.Substring(9, (razons.Length - 9));
                                                            int rs_fin = razons_tmp2.IndexOf("]]>");
                                                            razons = razons_tmp2.Substring(0, rs_fin);
                                                        }
                                                    }

                                                }
                                                catch (Exception ex)
                                                {

                                                    razons = null;
                                                }


                                                // Fecha Emision
                                                try
                                                {

                                                    int p1_femi = archivoXML.IndexOf("<cbc:IssueDate>");
                                                    int p2_femi = archivoXML.IndexOf("</cbc:IssueDate>");
                                                    var femi_tmp = archivoXML.Substring(p1_femi, (p2_femi - p1_femi));
                                                    femi = femi_tmp.Substring(15, femi_tmp.Length - 15);
                                                }
                                                catch (Exception ex)
                                                {

                                                    femi = null;
                                                }


                                                // Fecha Vencimiento
                                                try
                                                {
                                                    int p1_fven = archivoXML.IndexOf("<cbc:DueDate>");
                                                    int p2_fven = archivoXML.IndexOf("</cbc:DueDate>");
                                                    var fven_tmp = archivoXML.Substring(p1_fven, (p2_fven - p1_fven));
                                                    fven = fven_tmp.Substring(13, fven_tmp.Length - 13);
                                                }
                                                catch (Exception ex)
                                                {
                                                    fven = null;

                                                }

                                                // Condicion de Pago
                                                try
                                                {
                                                    int p1_condp = archivoXML.IndexOf("<cbc:Note languageID=*L*>");
                                                    int p2_condp = archivoXML.IndexOf("<cbc:Note languageID=*L*>");
                                                    var condp_tmp = archivoXML.Substring(p1_condp, (p2_condp - p1_condp));
                                                    condp = condp_tmp.Substring(15, condp_tmp.Length - 15);
                                                }
                                                catch (Exception ex)
                                                {

                                                }


                                                // Orden de compra.
                                                try
                                                {
                                                    int ig_oc = archivoXML.IndexOf("<cac:OrderReference>");
                                                    string a_oc = archivoXML.Substring(ig_oc, 100);
                                                    int p1_oc = a_oc.IndexOf("<cbc:ID>");
                                                    int p2_oc = a_oc.IndexOf("</cbc:ID>");
                                                    var oc_tmp = a_oc.Substring(p1_oc, (p2_oc - p1_oc));
                                                    oc = oc_tmp.Substring(8, oc_tmp.Length - 8);
                                                }
                                                catch (Exception ex)
                                                {

                                                }


                                                // Guia de Remision
                                                try
                                                {
                                                    int ig_gr = archivoXML.IndexOf("<cac:DespatchDocumentReference>");
                                                    string a_gr = archivoXML.Substring(ig_gr, 150);
                                                    int p1_gr = a_gr.IndexOf("<cbc:ID>");
                                                    int p2_gr = a_gr.IndexOf("</cbc:ID>");
                                                    var gr_tmp = a_gr.Substring(p1_gr, (p2_gr - p1_gr));
                                                    gr = gr_tmp.Substring(8, gr_tmp.Length - 8);

                                                    int ig_gr2 = archivoXML.IndexOf("<cac:DespatchDocumentReference>");
                                                    String a_gr2 = archivoXML.Substring(ig_gr2, 500);
                                                    int p2_gr2 = a_gr2.IndexOf("</cbc:DocumentTypeCode>");
                                                    int p1_gr2 = p2_gr2 - 30;
                                                    a_gr2 = a_gr2.Substring(0, p2_gr2);

                                                    String tmp_gr = a_gr2.Substring(p1_gr2, a_gr2.Length - p1_gr2);
                                                    int p1_gr1 = tmp_gr.IndexOf(">");

                                                    string gr_2 = tmp_gr.Substring(p1_gr1 + 1, (tmp_gr.Length - p1_gr1 - 1));

                                                    gr = gr + " | " + gr_2;
                                                }
                                                catch (Exception ex)
                                                {

                                                }


                                                // Moneda
                                                try
                                                {
                                                    int p1_mod = archivoXML.IndexOf("<cbc:DocumentCurrencyCode");

                                                    if (p1_mod == -1)
                                                    {
                                                        // enfocado principalmente a recibo por honorario
                                                        int p1_mod_t = archivoXML.IndexOf("<cbc:PayableAmount");
                                                        int p2_mod_t = archivoXML.IndexOf("</cbc:PayableAmount>");
                                                        String moneda_tmp_t = archivoXML.Substring(p1_mod_t, (p2_mod_t - p1_mod_t));
                                                        int p3_mod_t = moneda_tmp_t.IndexOf("currencyID=*");
                                                        mod = moneda_tmp_t.Substring(p3_mod_t + 12, 3);
                                                    }
                                                    else
                                                    {
                                                        // enfocado principalmente a facturas
                                                        int p2_mod = archivoXML.IndexOf("</cbc:DocumentCurrencyCode>");
                                                        var mod_tmp = archivoXML.Substring(p1_mod, (p2_mod - p1_mod));
                                                        mod = mod_tmp.Substring(mod_tmp.Length - 3, 3);

                                                    }


                                                }
                                                catch (Exception ex)
                                                {

                                                }

                                                // Operacion Grabada.
                                                try
                                                {

                                                    int ig_og1 = archivoXML.IndexOf("<cac:LegalMonetaryTotal>");
                                                    string a_og1 = archivoXML.Substring(0, ig_og1);

                                                    int i_og_p1 = a_og1.IndexOf("<cac:TaxTotal>");
                                                    string a_og2 = a_og1.Substring(i_og_p1, a_og1.Length - i_og_p1);
                                                    int i_og_p3 = a_og2.IndexOf("</cbc:TaxableAmount>");
                                                    int i_og_p4 = i_og_p3 - 12;
                                                    a_og2 = a_og2.Substring(0, i_og_p3);

                                                    String tmp_og1 = a_og2.Substring(i_og_p4, a_og2.Length - i_og_p4);
                                                    int p1_og1_t = tmp_og1.IndexOf(">");
                                                    og = Convert.ToDecimal(tmp_og1.Substring(p1_og1_t + 1, (tmp_og1.Length - p1_og1_t - 1)));

                                                    if (og == 0)
                                                    {
                                                        // si no encuentra valor, buscar en la otra opcion.

                                                        int t_p1_og = archivoXML.IndexOf("<cac:LegalMonetaryTotal>");
                                                        int t_p2_og = archivoXML.IndexOf("</cac:LegalMonetaryTotal>");

                                                        String a_og_total = archivoXML.Substring(t_p1_og, t_p2_og - t_p1_og);


                                                        int p2_og2 = a_og_total.IndexOf("</cbc:LineExtensionAmount>");
                                                        int p1_og2 = p2_og2 - 15;
                                                        a_og_total = a_og_total.Substring(0, p2_og2);

                                                        String tmp_ogv = a_og_total.Substring(p1_og2, a_og_total.Length - p1_og2);
                                                        int p1_og1 = tmp_ogv.IndexOf(">");

                                                        string og_2 = tmp_ogv.Substring(p1_og1 + 1, (tmp_ogv.Length - p1_og1 - 1));

                                                        og = Convert.ToDecimal(og_2);

                                                    }


                                                }
                                                catch (Exception ex)
                                                {

                                                }


                                                // Total
                                                try
                                                {

                                                    int p1_total_r = archivoXML.IndexOf("<cac:LegalMonetaryTotal>");
                                                    int p2_total_r = archivoXML.IndexOf("</cac:LegalMonetaryTotal>");

                                                    String a_total = archivoXML.Substring(p1_total_r, p2_total_r - p1_total_r);

                                                    int p1_total = a_total.IndexOf("<cbc:PayableAmount");
                                                    int p2_total = a_total.IndexOf("</cbc:PayableAmount>");

                                                    var total_tmp = a_total.Substring(p1_total, (p2_total - p1_total));
                                                    total = Convert.ToDecimal(total_tmp.Substring(36, total_tmp.Length - 36));


                                                }
                                                catch (Exception ex)
                                                {

                                                }

                                                // IGV
                                                try
                                                {

                                                    int igv_ini = archivoXML.IndexOf("<cac:AccountingCustomerParty>");
                                                    int igv_fin = archivoXML.IndexOf("<cac:LegalMonetaryTotal>");

                                                    string rango = archivoXML.Substring(igv_ini, (igv_fin - igv_ini));

                                                    int p1_igv = rango.IndexOf("<cbc:TaxAmount");
                                                    int p2_igv = rango.IndexOf("</cbc:TaxAmount>");

                                                    var igv_tmp = rango.Substring(p1_igv, (p2_igv - p1_igv));
                                                    igv = Convert.ToDecimal(igv_tmp.Substring(32, igv_tmp.Length - 32));

                                                }
                                                catch (Exception ex)
                                                {

                                                }

                                                factura.SERIE_NUM = numSerie;
                                                factura.RUC = ruc;

                                                factura.RS = (razons == null) ? null : razons.Trim();
                                                factura.FECHA_EMI = femi;
                                                factura.FECHA_VEN = fven;
                                                factura.COND_PAGO = condp;
                                                factura.OC = oc;
                                                factura.GR = gr;
                                                factura.MONEDA = mod;
                                                factura.V_VENTA = og;
                                                factura.TOTAL = total;
                                                factura.IGV = igv;

                                                string nombreOld = "";
                                                string nombreNew = "";

                                                try
                                                {
                                                    if (razons != null)
                                                    {
                                                        var resultado = await InsertarFact(factura);

                                                        if (resultado)
                                                        {
                                                            if (item4.Name.Substring(0, 1) != ".")
                                                            {

                                                                nombreOld = "/" + item.Name + "/" + item1.Name + "/" + item2.Name + "/" + item4.Name;
                                                                nombreNew = "." + item4.Name;

                                                                var renameResult = await OneDriveApi.Rename(nombreOld, nombreNew);

                                                            }
                                                            else if (item4.Name.Substring(0, 3) == ".NL")
                                                            {
                                                                nombreOld = "/" + item.Name + "/" + item1.Name + "/" + item2.Name + "/" + item4.Name;
                                                                nombreNew = "." + item4.Name.Substring(4, item4.Name.Length - 4);

                                                                var renameResult = await OneDriveApi.Rename(nombreOld, nombreNew);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string arhivoOld = item4.Name.ToString();

                                                        if (arhivoOld.Substring(0, 3) == ".NL")
                                                        {
                                                            arhivoOld = arhivoOld.Substring(4, arhivoOld.Length - 4);

                                                            if (arhivoOld.Substring(0, 3) == ".NL")
                                                            {
                                                                string path_Old = "/" + item.Name + "/" + item1.Name + "/" + item2.Name + "/" + item4.Name;
                                                                string path_new = arhivoOld;

                                                                var renameResult2 = await OneDriveApi.Rename(path_Old, path_new);

                                                            }
                                                        }
                                                        else if (arhivoOld.Substring(0, 3) != ".NL")
                                                        {
                                                            var renameResult = await OneDriveApi.Rename("/" + item.Name + "/" + item1.Name + "/" + item2.Name + "/" + item4.Name, ".NL_" + item4.Name);

                                                        }
                                                    }

                                                    respuesta.estado = true;
                                                }
                                                catch (Exception ex)
                                                {
                                                    respuesta.estado = false;
                                                    respuesta.descripcion = ex.Message;
                                                }

                                            }
                                        }
                                    }



                                    #endregion

                                    #region LeerZIP

                                    var listaZIP_tmp = carpetas3.Where(x => x.Name.EndsWith(".zip"));
                                    var listaZIP = listaZIP_tmp.Where(x => !x.Name.StartsWith("."));

                                    foreach (var item5 in listaZIP)
                                    {

                                        using (var stream = await OneDriveApi.DownloadItem(item5))
                                        {
                                            ZipArchive archive = new ZipArchive(stream);

                                            foreach (ZipArchiveEntry entry in archive.Entries)
                                            {
                                                if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) ||
                                                    entry.FullName.EndsWith(".XML", StringComparison.OrdinalIgnoreCase))
                                                {
                                                    using (var zipStream = entry.Open())
                                                    {
                                                        using (var reader = new StreamReader(zipStream))
                                                        {

                                                            Factura zipFactura = new Factura();

                                                            string zipNumSerie = "";
                                                            string zipRuc = "";
                                                            string zipRazons = "";
                                                            string zipFemi = "";
                                                            string zipFven = "";
                                                            string zipCondp = "";
                                                            string zipOc = "";
                                                            string zipGr = "";
                                                            string zipMod = "";
                                                            decimal zipOg = 0;
                                                            decimal zipTotal = 0;
                                                            decimal zipIgv = 0;


                                                            String zipArchivoXML = await reader.ReadToEndAsync();
                                                            zipArchivoXML = zipArchivoXML.Replace("\"", "*");
                                                            zipArchivoXML = zipArchivoXML.Replace("\n", "*");


                                                            // NUMERO Y SERIE
                                                            try
                                                            {
                                                                int ig_numSer = zipArchivoXML.IndexOf("</cbc:CustomizationID>");

                                                                if (ig_numSer == -1)
                                                                {
                                                                    // enfocado principalmente a recibo por honorario.
                                                                    int ig_numSer_t = zipArchivoXML.IndexOf("<cac:OrderReference>");
                                                                    string a_numSer_t = zipArchivoXML.Substring(ig_numSer_t, 90);

                                                                    int p1_numSer = a_numSer_t.IndexOf("<cbc:ID>");
                                                                    int p2_numSer = a_numSer_t.IndexOf("</cbc:ID>");

                                                                    var numSerie_tmp_t = a_numSer_t.Substring(p1_numSer, (p2_numSer - p1_numSer));
                                                                    zipNumSerie = numSerie_tmp_t.Substring(8, numSerie_tmp_t.Length - 8);
                                                                }
                                                                else
                                                                {
                                                                    // enfoncado principalmente a facturas
                                                                    string a_numSer = zipArchivoXML.Substring(ig_numSer, 475);
                                                                    int p1_numSer = a_numSer.IndexOf("<cbc:ID>");
                                                                    int p2_numSer = a_numSer.IndexOf("</cbc:ID>");
                                                                    var numSerie_tmp = a_numSer.Substring(p1_numSer, (p2_numSer - p1_numSer));
                                                                    zipNumSerie = numSerie_tmp.Substring(8, numSerie_tmp.Length - 8);
                                                                }

                                                            }
                                                            catch (Exception)
                                                            {
                                                                zipNumSerie = null;
                                                            }

                                                            // RUC
                                                            try
                                                            {
                                                                int ig_ruc = zipArchivoXML.IndexOf("<cac:SignatoryParty>");
                                                                string a_ruc = zipArchivoXML.Substring(ig_ruc, 540);
                                                                int p2_ruc = a_ruc.IndexOf("</cbc:ID>");

                                                                if (p2_ruc == -1)
                                                                {
                                                                    // enfoncado principalmente a recibo por honorario
                                                                    int ig_ruc_t = zipArchivoXML.IndexOf("<cac:AccountingSupplierParty>");
                                                                    string a_ruc_t = zipArchivoXML.Substring(ig_ruc_t, 170);
                                                                    int p2_ruc_t = a_ruc_t.IndexOf("</cbc:CustomerAssignedAccountID>");
                                                                    int p1_ruc_t = p2_ruc_t - 18;
                                                                    a_ruc_t = a_ruc_t.Substring(0, p2_ruc_t);

                                                                    String tmp_ruc_t = a_ruc_t.Substring(p1_ruc_t, a_ruc_t.Length - p1_ruc_t);
                                                                    int p1_ruc1_t = tmp_ruc_t.IndexOf(">");
                                                                    zipRuc = tmp_ruc_t.Substring(p1_ruc1_t + 1, (tmp_ruc_t.Length - p1_ruc1_t - 1));

                                                                }
                                                                else
                                                                {
                                                                    // enfocado principalmente a facturas normales
                                                                    int p1_ruc = p2_ruc - 18;

                                                                    a_ruc = a_ruc.Substring(0, p2_ruc);

                                                                    String tmp_ruc = a_ruc.Substring(p1_ruc, a_ruc.Length - p1_ruc);
                                                                    int p1_ruc1 = tmp_ruc.IndexOf(">");
                                                                    zipRuc = tmp_ruc.Substring(p1_ruc1 + 1, (tmp_ruc.Length - p1_ruc1 - 1));

                                                                }


                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                zipRuc = null;
                                                            }


                                                            // Razon Social
                                                            try
                                                            {
                                                                int ig_razons = zipArchivoXML.IndexOf("<cac:AccountingSupplierParty>");
                                                                string a_razons = zipArchivoXML.Substring(ig_razons, 630);
                                                                int p1_razons_t = a_razons.IndexOf("<cbc:RegistrationName>");

                                                                if (p1_razons_t != -1)
                                                                {
                                                                    int p2_razons_t = a_razons.IndexOf("</cbc:RegistrationName>");

                                                                    var razons_tmp = a_razons.Substring(p1_razons_t, (p2_razons_t - p1_razons_t));
                                                                    String razons_tmp2 = razons_tmp.Substring(22, razons_tmp.Length - 22);

                                                                    int posi_rs1 = razons_tmp2.IndexOf("<![CDATA");

                                                                    if (posi_rs1 != -1)
                                                                    {
                                                                        int posi_rs2 = razons_tmp2.IndexOf("]]>");
                                                                        zipRazons = razons_tmp2.Substring(posi_rs1 + 9, posi_rs2 - posi_rs1 - 9);
                                                                    }
                                                                    else
                                                                    {
                                                                        zipRazons = razons_tmp2;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    int p1_razons = a_razons.IndexOf("<cbc:Name>");
                                                                    int p2_razons = a_razons.IndexOf("</cbc:Name>");
                                                                    var razons_tmp = a_razons.Substring(p1_razons, (p2_razons - p1_razons));
                                                                    zipRazons = razons_tmp.Substring(10, razons_tmp.Length - 10);

                                                                    var sss = zipRazons.Substring(0, 9);

                                                                    if (zipRazons.Substring(0, 9) == "<![CDATA[")
                                                                    {
                                                                        String razons_tmp2 = zipRazons.Substring(9, (zipRazons.Length - 9));
                                                                        int rs_fin = razons_tmp2.IndexOf("]]>");
                                                                        zipRazons = razons_tmp2.Substring(0, rs_fin);
                                                                    }
                                                                }

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                zipRazons = null;
                                                            }


                                                            // Fecha Emision
                                                            try
                                                            {

                                                                int p1_femi = zipArchivoXML.IndexOf("<cbc:IssueDate>");
                                                                int p2_femi = zipArchivoXML.IndexOf("</cbc:IssueDate>");
                                                                var femi_tmp = zipArchivoXML.Substring(p1_femi, (p2_femi - p1_femi));
                                                                zipFemi = femi_tmp.Substring(15, femi_tmp.Length - 15);
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                                zipFemi = null;
                                                            }


                                                            // Fecha Vencimiento
                                                            try
                                                            {
                                                                int p1_fven = zipArchivoXML.IndexOf("<cbc:DueDate>");
                                                                int p2_fven = zipArchivoXML.IndexOf("</cbc:DueDate>");
                                                                var fven_tmp = zipArchivoXML.Substring(p1_fven, (p2_fven - p1_fven));
                                                                zipFven = fven_tmp.Substring(13, fven_tmp.Length - 13);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                zipFven = null;

                                                            }


                                                            // Condicion de Pago
                                                            try
                                                            {
                                                                int p1_condp = zipArchivoXML.IndexOf("<cbc:Note languageID=*L*>");
                                                                int p2_condp = zipArchivoXML.IndexOf("<cbc:Note languageID=*L*>");
                                                                var condp_tmp = zipArchivoXML.Substring(p1_condp, (p2_condp - p1_condp));
                                                                zipCondp = condp_tmp.Substring(15, condp_tmp.Length - 15);
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }


                                                            // Orden de compra.
                                                            try
                                                            {
                                                                int ig_oc = zipArchivoXML.IndexOf("<cac:OrderReference>");
                                                                string a_oc = zipArchivoXML.Substring(ig_oc, 100);
                                                                int p1_oc = a_oc.IndexOf("<cbc:ID>");
                                                                int p2_oc = a_oc.IndexOf("</cbc:ID>");
                                                                var oc_tmp = a_oc.Substring(p1_oc, (p2_oc - p1_oc));
                                                                zipOc = oc_tmp.Substring(8, oc_tmp.Length - 8);
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }


                                                            // Guia de Remision
                                                            try
                                                            {
                                                                int ig_gr = zipArchivoXML.IndexOf("<cac:DespatchDocumentReference>");
                                                                string a_gr = zipArchivoXML.Substring(ig_gr, 150);
                                                                int p1_gr = a_gr.IndexOf("<cbc:ID>");
                                                                int p2_gr = a_gr.IndexOf("</cbc:ID>");
                                                                var gr_tmp = a_gr.Substring(p1_gr, (p2_gr - p1_gr));
                                                                zipGr = gr_tmp.Substring(8, gr_tmp.Length - 8);
                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }


                                                            // Moneda
                                                            try
                                                            {
                                                                int p1_mod = zipArchivoXML.IndexOf("<cbc:DocumentCurrencyCode");

                                                                if (p1_mod == -1)
                                                                {
                                                                    // enfocado principalmente a recibo por honorario
                                                                    int p1_mod_t = zipArchivoXML.IndexOf("<cbc:PayableAmount");
                                                                    int p2_mod_t = zipArchivoXML.IndexOf("</cbc:PayableAmount>");
                                                                    String moneda_tmp_t = zipArchivoXML.Substring(p1_mod_t, (p2_mod_t - p1_mod_t));
                                                                    int p3_mod_t = moneda_tmp_t.IndexOf("currencyID=*");
                                                                    zipMod = moneda_tmp_t.Substring(p3_mod_t + 12, 3);
                                                                }
                                                                else
                                                                {
                                                                    // enfocado principalmente a facturas
                                                                    int p2_mod = zipArchivoXML.IndexOf("</cbc:DocumentCurrencyCode>");
                                                                    var mod_tmp = zipArchivoXML.Substring(p1_mod, (p2_mod - p1_mod));
                                                                    zipMod = mod_tmp.Substring(mod_tmp.Length - 3, 3);

                                                                }


                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }

                                                            // Operacion Grabada.
                                                            try
                                                            {

                                                                int ig_og = zipArchivoXML.IndexOf("<cac:LegalMonetaryTotal>");
                                                                string a_og = zipArchivoXML.Substring(ig_og, 500);
                                                                int p2_og = a_og.IndexOf("</cbc:LineExtensionAmount>");
                                                                int p1_og = p2_og - 18;
                                                                a_og = a_og.Substring(0, p2_og);

                                                                String tmp_og = a_og.Substring(p1_og, a_og.Length - p1_og);
                                                                int p1_og1 = tmp_og.IndexOf(">");
                                                                zipOg = Convert.ToDecimal(tmp_og.Substring(p1_og1 + 1, (tmp_og.Length - p1_og1 - 1)));

                                                                if (zipOg == 0)
                                                                {
                                                                    // si no encuentra valor, buscar en la otra opcion.

                                                                    int t_p1_og = zipArchivoXML.IndexOf("<cac:LegalMonetaryTotal>");
                                                                    int t_p2_og = zipArchivoXML.IndexOf("</cac:LegalMonetaryTotal>");

                                                                    String a_og_total = zipArchivoXML.Substring(t_p1_og, t_p2_og - t_p1_og);


                                                                    int p2_og2 = a_og_total.IndexOf("</cbc:LineExtensionAmount>");
                                                                    int p1_og2 = p2_og2 - 15;
                                                                    a_og_total = a_og_total.Substring(0, p2_og2);

                                                                    String tmp_ogv = a_og_total.Substring(p1_og2, a_og_total.Length - p1_og2);
                                                                    int p1_og1z = tmp_ogv.IndexOf(">");

                                                                    string og_2 = tmp_ogv.Substring(p1_og1z + 1, (tmp_ogv.Length - p1_og1z - 1));

                                                                    zipOg = Convert.ToDecimal(og_2);

                                                                }


                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }


                                                            // Total
                                                            try
                                                            {
                                                                int p1_total_r = zipArchivoXML.IndexOf("<cac:LegalMonetaryTotal>");
                                                                int p2_total_r = zipArchivoXML.IndexOf("</cac:LegalMonetaryTotal>");

                                                                String a_total = zipArchivoXML.Substring(p1_total_r, p2_total_r - p1_total_r);

                                                                int p1_total = a_total.IndexOf("<cbc:PayableAmount");
                                                                int p2_total = a_total.IndexOf("</cbc:PayableAmount>");

                                                                var total_tmp = a_total.Substring(p1_total, (p2_total - p1_total));
                                                                zipTotal = Convert.ToDecimal(total_tmp.Substring(36, total_tmp.Length - 36));

                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }

                                                            // IGV
                                                            try
                                                            {
                                                                int igv_ini = zipArchivoXML.IndexOf("<cac:AccountingCustomerParty>");
                                                                int igv_fin = zipArchivoXML.IndexOf("<cac:LegalMonetaryTotal>");

                                                                string rango = zipArchivoXML.Substring(igv_ini, (igv_fin - igv_ini));

                                                                int p1_igv = rango.IndexOf("<cbc:TaxAmount");
                                                                int p2_igv = rango.IndexOf("</cbc:TaxAmount>");

                                                                var igv_tmp = rango.Substring(p1_igv, (p2_igv - p1_igv));
                                                                zipIgv = Convert.ToDecimal(igv_tmp.Substring(32, igv_tmp.Length - 32));

                                                            }
                                                            catch (Exception ex)
                                                            {

                                                            }

                                                            zipFactura.SERIE_NUM = zipNumSerie;
                                                            zipFactura.RUC = zipRuc;

                                                            zipFactura.RS = (zipRazons == null) ? null : zipRazons.Trim();
                                                            zipFactura.FECHA_EMI = zipFemi;
                                                            zipFactura.FECHA_VEN = zipFven;
                                                            zipFactura.COND_PAGO = zipCondp;
                                                            zipFactura.OC = zipOc;
                                                            zipFactura.GR = zipGr;
                                                            zipFactura.MONEDA = zipMod;
                                                            zipFactura.V_VENTA = zipOg;
                                                            zipFactura.TOTAL = zipTotal;
                                                            zipFactura.IGV = zipIgv;


                                                            string nombreOld = "";
                                                            string nombreNew = "";


                                                            try
                                                            {
                                                                if (zipRazons != null)
                                                                {
                                                                    var resultado = await InsertarFact(zipFactura);

                                                                    if (resultado)
                                                                    {
                                                                        if (item5.Name.Substring(0, 1) != ".")
                                                                        {

                                                                            nombreOld = "/" + item.Name + "/" + item1.Name + "/" + item2.Name + "/" + item5.Name;
                                                                            nombreNew = "." + item5.Name;

                                                                            var renameResult = await OneDriveApi.Rename(nombreOld, nombreNew);

                                                                        }
                                                                        else if (item5.Name.Substring(0, 3) == ".NL")
                                                                        {
                                                                            nombreOld = "/" + item.Name + "/" + item1.Name + "/" + item2.Name + "/" + item5.Name;
                                                                            nombreNew = "." + item5.Name.Substring(4, item5.Name.Length - 4);

                                                                            var renameResult = await OneDriveApi.Rename(nombreOld, nombreNew);
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    string arhivoOld = item5.Name.ToString();

                                                                    if (arhivoOld.Substring(0, 3) == ".NL")
                                                                    {
                                                                        arhivoOld = arhivoOld.Substring(4, arhivoOld.Length - 4);

                                                                        if (arhivoOld.Substring(0, 3) == ".NL")
                                                                        {
                                                                            string path_Old = "/" + item.Name + "/" + item1.Name + "/" + item2.Name + "/" + item5.Name;
                                                                            string path_new = arhivoOld;

                                                                            var renameResult2 = await OneDriveApi.Rename(path_Old, path_new);

                                                                        }

                                                                    }
                                                                    else if (arhivoOld.Substring(0, 3) != ".NL")
                                                                    {
                                                                        var renameResult3 = await OneDriveApi.Rename("/" + item.Name + "/" + item1.Name + "/" + item2.Name + "/" + item5.Name, ".NL_" + item5.Name);

                                                                    }
                                                                }

                                                                respuesta.estado = true;
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                respuesta.estado = false;
                                                                respuesta.descripcion = ex.Message;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }

                                    #endregion
                                //}
                            }

                        }
                    }
                }
            }

            return respuesta;
        }


        public List<ListaExcel> ListarVentasExcel(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();

                    parametros.Add("I_OPCION", 2, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    return conexion.Query<ListaExcel>(sql: "USYSTEX.SPU_GET_FACTURA_PROV",
                                                            param: parametros,
                                                            commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public MemoryStream ReporteExcel(List<ListaExcel> listado)
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Hoja1");

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.Font.Size = 9;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(1).Style.Fill.BackgroundColor.SetColor(Color.White);
                workSheet.Cells[1, 1, 1, 14].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells["A1"].Value = "RUC";
                workSheet.Cells["B1"].Value = "SERIE NUMERO";
                workSheet.Cells["C1"].Value = "RAZON SOCIAL";
                workSheet.Cells["D1"].Value = "FECHA EMISION";
                workSheet.Cells["E1"].Value = "FECHA VENCIMIENTO";
                workSheet.Cells["F1"].Value = "CONDICIÓN DE PAGO";
                workSheet.Cells["G1"].Value = "OC";
                workSheet.Cells["H1"].Value = "GR";
                workSheet.Cells["I1"].Value = "MONEDA";
                workSheet.Cells["J1"].Value = "VALOR VENTA";
                workSheet.Cells["K1"].Value = "VALOR IGV";
                workSheet.Cells["L1"].Value = "TOTAL VENTA";
                workSheet.Cells["M1"].Value = "TOTAL DOLARES";
                workSheet.Cells["N1"].Value = "TOTAL SOLES";

                workSheet.View.FreezePanes(2, 1);


                int i = 2;

                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 9;

                    workSheet.Cells[i, 1].Value = item.RUC;
                    workSheet.Cells[i, 2].Value = item.SERIE_NUM;
                    workSheet.Cells[i, 3].Value = item.RS;
                    workSheet.Cells[i, 4].Value = item.FECHA_EMI;
                    workSheet.Cells[i, 5].Value = item.FECHA_VEN;
                    workSheet.Cells[i, 6].Value = item.COND_PAGO;
                    workSheet.Cells[i, 7].Value = item.OC;
                    workSheet.Cells[i, 8].Value = item.GR;
                    workSheet.Cells[i, 9].Value = item.MONEDA;
                    workSheet.Cells[i, 10].Value = item.V_VENTA;
                    workSheet.Cells[i, 11].Value = item.V_IGV;
                    workSheet.Cells[i, 12].Value = item.TOTAL;
                    workSheet.Cells[i, 13].Value = item.TOTAL_D;
                    workSheet.Cells[i, 14].Value = item.TOTAL_S;

                    i++;
                }

                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                package.Save();
            }

            stream.Position = 0;
            return stream;

        }



        public async Task<string> UltimaActualizacion()
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();

                    parametros.Add("I_OPCION", 3, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    return await conexion.QuerySingleAsync<string>(sql: "USYSTEX.SPU_GET_FACTURA_PROV",
                                                            param: parametros,
                                                            commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> InsertFechaActualizacion(string usuario)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    conexion.Open();
                    OracleCommand comando = new OracleCommand("USYSTEX.SPU_SET_FACTURA_PROV_ULTACT", conexion);
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add(new OracleParameter("USUARIO", usuario));

                    await comando.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }



    }
}