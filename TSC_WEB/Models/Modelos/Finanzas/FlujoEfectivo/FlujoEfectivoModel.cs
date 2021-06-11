using Dapper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Finanzas.FlujoEfectivo;

namespace TSC_WEB.Models.Modelos.Finanzas.FlujoEfectivo
{
    public class FlujoEfectivoModel
    {

        public async Task<IEnumerable<EFE_TipoPeriodo>> Get_TipoPeriodo()
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 1, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", 0, DbType.Int32, ParameterDirection.Input);

                    sp_parametros.Add("@IdTipoAct", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@CodEFx", "", DbType.String, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<EFE_TipoPeriodo>("uspGetFlujoEfectivo",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<EFE_Periodo>> Get_Periodos(int tipoPeriodo)
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 2, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", tipoPeriodo, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", 0, DbType.Int32, ParameterDirection.Input);

                    sp_parametros.Add("@IdTipoAct", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@CodEFx", "", DbType.String, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<EFE_Periodo>("uspGetFlujoEfectivo",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<EFE_FlujoEfectivoLista>> ListarFlujoEfectivo(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 3, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", TipoPeriodo, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", IdPeriodoIni, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", IdPeriodoFin, DbType.Int32, ParameterDirection.Input);

                    sp_parametros.Add("@IdTipoAct", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@CodEFx", "", DbType.String, ParameterDirection.Input);

                    var resultado = await conexion.QueryAsync<EFE_FlujoEfectivoLista>("uspGetFlujoEfectivo",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<EFE_FlujoEfectivoLista> ExpListarFlujoEfectivo(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 3, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", TipoPeriodo, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", IdPeriodoIni, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", IdPeriodoFin, DbType.Int32, ParameterDirection.Input);

                    sp_parametros.Add("@IdTipoAct", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@CodEFx", "", DbType.String, ParameterDirection.Input);

                    var resultado = conexion.Query<EFE_FlujoEfectivoLista>("uspGetFlujoEfectivo",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public MemoryStream ExportarFlujoEfectivo(List<EFE_FlujoEfectivoLista> listado)
        {

            #region Variables 
            var stream = new MemoryStream();

            List<string> Titulo = new List<string>() { "Ubicación", "Cod EF-x", "Descrip Cod EF-x", "RS-X" };
            List<string> Footer = new List<string>() { "", "", "", "" };

            List<int> lstTitPerTmp = new List<int>();
            List<string> lstTitPer = new List<string>();
            List<classTituloPeriodo> lstTituloPeriodo = new List<classTituloPeriodo>();
            List<classTituloPeriodo> lstTitulo = new List<classTituloPeriodo>();

            List<classConceptos> lstConceptos = new List<classConceptos>();
            List<EFE_FlujoEfectivoLista> listaTmp = new List<EFE_FlujoEfectivoLista>();
            List<classOrdenMontos> lstOrdenMontos = new List<classOrdenMontos>();
            List<classTotalGrupos> lstTotalGrupos = new List<classTotalGrupos>();
            List<classDistinctCptos> lstDistinctCptos = new List<classDistinctCptos>();
            List<EFE_FlujoEfectivoLista> lstGrupo = new List<EFE_FlujoEfectivoLista>();
            List<classDistinctCptos> lstValidUlt = new List<classDistinctCptos>();
            List<EFE_FlujoEfectivoLista> lstSeccion = new List<EFE_FlujoEfectivoLista>();
            List<classDistinctSeccion> lstDistinctSeccion = new List<classDistinctSeccion>();
            List<classDistinctSeccion> lstValidUltSeccion = new List<classDistinctSeccion>();
            List<classTotalSeccion> lstTotalSeccion = new List<classTotalSeccion>();
            List<classTotal> lstTotal = new List<classTotal>();
            List<classDistinctTotal> lstDistinctTotal = new List<classDistinctTotal>();
            List<classDistinctTotal> lstValidTotal = new List<classDistinctTotal>();

            List<object[]> lstTr = new List<object[]>();
            #endregion

            foreach (var item in listado)
            {
                if (!lstTitPerTmp.Contains(item.efperorden))
                {
                    lstTitPerTmp.Add(item.efperorden);
                }
            }

            foreach (var item in listado)
            {
                foreach (var orden in lstTitPerTmp)
                {
                    if (item.efperorden == orden)
                    {
                        if (!lstTitPer.Contains(item.efperiodo))
                        {
                            lstTitPer.Add(item.efperiodo);
                            lstTituloPeriodo.Add(new classTituloPeriodo() { orden = item.efperorden, periodo = item.efperiodo });
                        }
                    }
                }
            }

            lstTitulo = lstTituloPeriodo.OrderBy(x => x.orden).ToList();

            foreach (var titulo in lstTitulo)
            {
                Titulo.Add(titulo.periodo);
            }

            object[] objTrArray = new object[Titulo.Count];

            foreach (var item in listado)
            {
                if (!lstDistinctTotal.Exists(x => x.idTipoAct == item.efIdTipoAct && x.codEFx == item.efCodEFx && x.idRSx == item.efIdRSx))
                {
                    lstDistinctTotal.Add(new classDistinctTotal() { idTipoAct = item.efIdTipoAct, codEFx = item.efCodEFx, idRSx = item.efIdRSx });
                }
            }

            foreach (var item in listado)
            {
                lstSeccion = listado.Where(x => x.efIdTipoAct == item.efIdTipoAct).ToList();

                foreach (var item2 in lstSeccion)
                {
                    var v1 = lstDistinctSeccion.Exists(x => x.idTipoAct == item2.efIdTipoAct && x.codEFx == item2.efCodEFx && x.idRSx == item2.efIdRSx);

                    if (!lstDistinctSeccion.Exists(x => x.idTipoAct == item2.efIdTipoAct && x.codEFx == item2.efCodEFx && x.idRSx == item2.efIdRSx))
                    {
                        lstDistinctSeccion.Add(new classDistinctSeccion() { idTipoAct = item2.efIdTipoAct, codEFx = item2.efCodEFx, idRSx = item2.efIdRSx, registrado = false });
                    }
                }

                if (!lstConceptos.Exists(x => x.IdTipoAct == item.efIdTipoAct && x.CodEFx == item.efCodEFx && x.IdRSx == item.efIdRSx))
                {

                    lstGrupo = listado.Where(x => x.efCodEFx == item.efCodEFx).ToList();

                    foreach (var grupo in lstGrupo)
                    {
                        if (!lstDistinctCptos.Exists(x => x.idRSx == grupo.efIdRSx && x.codEFx == grupo.efCodEFx))
                        {
                            lstDistinctCptos.Add(new classDistinctCptos() { idRSx = grupo.efIdRSx, codEFx = grupo.efCodEFx, registrado = false });
                        }
                    }
                    lstDistinctCptos = lstDistinctCptos.OrderBy(x => x.idRSx).ToList();

                    int ii = 0;
                    objTrArray[ii] = item.efActividad; ii++;
                    objTrArray[ii] = item.efCodEFx; ii++;
                    objTrArray[ii] = item.efDescCodEf; ii++;
                    objTrArray[ii] = item.efConcepto; ii++;

                    listaTmp.Clear();
                    lstOrdenMontos.Clear();
                    listaTmp = listado.Where(x => x.efIdTipoAct == item.efIdTipoAct && x.efCodEFx == item.efCodEFx && x.efIdRSx == item.efIdRSx).ToList();

                    foreach (var item3 in listaTmp)
                    {
                        if (!lstConceptos.Exists(x => x.IdTipoAct == item3.efIdTipoAct && x.CodEFx == item3.efCodEFx && x.IdRSx == item3.efIdRSx && x.perorden == item3.efperorden))
                        {
                            foreach (var titulo in lstTitulo)
                            {
                                if (titulo.orden == item3.efperorden)
                                {
                                    EFE_FlujoEfectivoLista objeto = listado.Find(x => x.efIdTipoAct == item3.efIdTipoAct && x.efCodEFx == item3.efCodEFx && x.efIdRSx == item3.efIdRSx && x.efperorden == item3.efperorden);

                                    if (objeto != null)
                                    {
                                        lstOrdenMontos.Add(new classOrdenMontos() { orden = titulo.orden, monto = objeto.mdolar });
                                    }
                                }
                                else
                                {
                                    if (!listaTmp.Exists(x => x.efperorden == titulo.orden))
                                    {
                                        if (!lstOrdenMontos.Exists(x => x.orden == titulo.orden))
                                        {
                                            lstOrdenMontos.Add(new classOrdenMontos() { orden = titulo.orden, monto = 0 });
                                        }
                                    }
                                }
                            }
                            lstConceptos.Add(new classConceptos() { IdTipoAct = item3.efIdTipoAct, CodEFx = item3.efCodEFx, IdRSx = item3.efIdRSx, perorden = item3.efperorden });
                        }
                    }


                    foreach (var titulo in lstTitulo)
                    {
                        if (lstOrdenMontos.Exists(x => x.orden == titulo.orden))
                        {
                            classOrdenMontos objeto = lstOrdenMontos.Find(x => x.orden == titulo.orden);
                            objTrArray[ii] = objeto.monto;
                            ii++;

                            if (lstTotalGrupos.Exists(x => x.orden == titulo.orden))
                            {
                                foreach (var tGrupo in lstTotalGrupos)
                                {
                                    if (tGrupo.orden == titulo.orden)
                                    {
                                        tGrupo.monto = tGrupo.monto + objeto.monto;
                                    }
                                }
                            }
                            else
                            {
                                lstTotalGrupos.Add(new classTotalGrupos() { orden = titulo.orden, monto = objeto.monto });
                            }

                        }
                    }

                    lstTr.Add(objTrArray); ii = 0; objTrArray = new object[Titulo.Count];


                    foreach (var cpto in lstDistinctCptos)
                    {
                        if (cpto.idRSx == item.efIdRSx)
                        {
                            cpto.registrado = true;
                        }
                    }

                    lstValidUlt = lstDistinctCptos.Where(x => x.registrado == false).ToList();

                    if (lstValidUlt.Count == 0)
                    {
                        objTrArray[ii] = item.efActividad; ii++;
                        objTrArray[ii] = item.efCodEFx; ii++;
                        objTrArray[ii] = item.efDescCodEf; ii++;
                        objTrArray[ii] = "SubTotal"; ii++;

                        foreach (var titulo in lstTitulo)
                        {
                            if (lstTotalGrupos.Exists(x => x.orden == titulo.orden))
                            {
                                var objeto = lstTotalGrupos.Find(x => x.orden == titulo.orden);
                                objTrArray[ii] = objeto.monto; ii++;
                            }
                        }

                        lstTr.Add(objTrArray); ii = 0; objTrArray = new object[Titulo.Count];

                        foreach (var titulo in lstTitulo)
                        {
                            if (lstTotalGrupos.Exists(x => x.orden == titulo.orden))
                            {
                                var objeto = lstTotalGrupos.Find(x => x.orden == titulo.orden);

                                if (lstTotalSeccion.Exists(x => x.orden == titulo.orden))
                                {
                                    foreach (var seccion in lstTotalSeccion)
                                    {
                                        if (seccion.orden == titulo.orden)
                                        {
                                            seccion.monto = seccion.monto + objeto.monto;
                                        }
                                    }
                                }
                                else
                                {
                                    lstTotalSeccion.Add(new classTotalSeccion() { orden = titulo.orden, monto = objeto.monto });
                                }
                            }
                        }

                        lstTotalGrupos.Clear();
                    }


                    foreach (var seccion in lstDistinctSeccion)
                    {
                        if (seccion.idRSx == item.efIdRSx)
                        {
                            seccion.registrado = true;
                        }
                    }


                    lstValidUltSeccion = lstDistinctSeccion.Where(x => x.registrado == false).ToList();

                    if (lstValidUltSeccion.Count == 0)
                    {
                        objTrArray[ii] = item.efActividad; ii++;
                        objTrArray[ii] = ""; ii++;
                        objTrArray[ii] = ""; ii++;
                        objTrArray[ii] = "Total"; ii++;

                        foreach (var titulo in lstTitulo)
                        {
                            if (lstTotalSeccion.Exists(x => x.orden == titulo.orden))
                            {
                                var objeto = lstTotalSeccion.Find(x => x.orden == titulo.orden);
                                objTrArray[ii] = objeto.monto; ii++;
                            }
                        }

                        lstTr.Add(objTrArray); ii = 0; objTrArray = new object[Titulo.Count];


                        foreach (var titulo in lstTitulo)
                        {
                            if (lstTotalSeccion.Exists(x => x.orden == titulo.orden))
                            {
                                var objeto = lstTotalSeccion.Find(x => x.orden == titulo.orden);

                                if (lstTotal.Exists(x => x.orden == titulo.orden))
                                {
                                    foreach (var stotal in lstTotal)
                                    {
                                        if (stotal.orden == titulo.orden)
                                        {
                                            stotal.monto += objeto.monto;
                                        }
                                    }
                                }
                                else
                                {
                                    lstTotal.Add(new classTotal() { orden = titulo.orden, monto = objeto.monto });
                                }
                            }
                        }
                        lstTotalSeccion.Clear();
                    }


                    foreach (var total in lstDistinctTotal)
                    {
                        if (total.idRSx == item.efIdRSx)
                        {
                            total.registrado = true;
                        }
                    }

                    lstValidTotal = lstDistinctTotal.Where(x => x.registrado == false).ToList();

                    if (lstValidTotal.Count == 0)
                    {
                        objTrArray[ii] = ""; ii++;
                        objTrArray[ii] = ""; ii++;
                        objTrArray[ii] = ""; ii++;
                        objTrArray[ii] = "Total General"; ii++;

                        foreach (var titulo in lstTitulo)
                        {
                            if (lstTotal.Exists(x => x.orden == titulo.orden))
                            {
                                var objeto = lstTotal.Find(x => x.orden == titulo.orden);
                                objTrArray[ii] = objeto.monto; ii++;
                            }
                        }

                        lstTr.Add(objTrArray); ii = 0; objTrArray = new object[Titulo.Count];

                        lstTotal.Clear();
                    }
                }
            }


            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Hoja1");
                Color colSubTotal = ColorTranslator.FromHtml("#85C1E9");
                Color colTotal = ColorTranslator.FromHtml("#2874A6");
                Color colTotalGeneral = ColorTranslator.FromHtml("#566573");

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.Font.Size = 9;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                workSheet.Cells[1, 1, 1, Titulo.Count].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                workSheet.Cells[1, 1, 1, Titulo.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 1, 1, Titulo.Count].Style.Fill.BackgroundColor.SetColor(colTotalGeneral);
                workSheet.Cells[1, 1, 1, Titulo.Count].Style.Font.Color.SetColor(Color.White);


                for (int i = 0; i < Titulo.Count; i++)
                {
                    workSheet.Cells[1, i + 1].Value = Titulo[i].ToString();
                }

                workSheet.View.FreezePanes(2, 1);

                int jrow = 2, icolumn = 1;
                foreach (var item in lstTr)
                {
                    workSheet.Row(jrow).Style.Font.Size = 9;

                    foreach (var item2 in item)
                    {
                        workSheet.Cells[jrow, icolumn].Value = item2;

                        if (icolumn > 4)
                        {
                            workSheet.Cells[jrow, icolumn].Style.Numberformat.Format = "#,##0.00";
                        }


                        if (icolumn == 4 && item2.ToString().Trim() == "SubTotal")
                        {
                            int it = 1;
                            while (it <= Titulo.Count)
                            {
                                workSheet.Cells[jrow, it].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                workSheet.Cells[jrow, it].Style.Fill.BackgroundColor.SetColor(colSubTotal);
                                workSheet.Cells[jrow, it].Style.Font.Bold = true;
                                it++;
                            }
                        }
                        else if (icolumn == 4 && item2.ToString().Trim() == "Total")
                        {
                            int it = 1;
                            while (it <= Titulo.Count)
                            {
                                workSheet.Cells[jrow, it].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                workSheet.Cells[jrow, it].Style.Fill.BackgroundColor.SetColor(colTotal);
                                workSheet.Cells[jrow, it].Style.Font.Bold = true;
                                it++;
                            }
                        }
                        else if (icolumn == 4 && item2.ToString().Trim() == "Total General")
                        {
                            int it = 1;
                            while (it <= Titulo.Count)
                            {
                                workSheet.Cells[jrow, it].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                workSheet.Cells[jrow, it].Style.Fill.BackgroundColor.SetColor(colTotalGeneral);
                                workSheet.Cells[jrow, it].Style.Font.Color.SetColor(Color.White);
                                workSheet.Cells[jrow, it].Style.Font.Bold = true;
                                it++;
                            }
                        }

                        icolumn++;
                    }

                    icolumn = 1;
                    jrow++;
                }

                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                package.Save();

                stream.Position = 0;
            }
            return stream;
        }

        public IEnumerable<EFE_ExcelDetalle> ListExcelDetFlujoEfectivo(int TipoPeriodo, int IdPeriodoIni, int IdPeriodoFin)
        {
            try
            {
                using (var conexion = DBAccess.ObtenerConexionSQL())
                {
                    var sp_parametros = new DynamicParameters();

                    sp_parametros.Add("@Opcion", 4, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdTipo", TipoPeriodo, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoIni", IdPeriodoIni, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@IdPeriodoFin", IdPeriodoFin, DbType.Int32, ParameterDirection.Input);

                    sp_parametros.Add("@IdTipoAct", 0, DbType.Int32, ParameterDirection.Input);
                    sp_parametros.Add("@CodEFx", "", DbType.String, ParameterDirection.Input);

                    var resultado = conexion.Query<EFE_ExcelDetalle>("uspGetFlujoEfectivo",
                                                        sp_parametros,
                                                        commandType: CommandType.StoredProcedure);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public MemoryStream ExpExcelDetFlujoEfectivo(List<EFE_ExcelDetalle> listado)
        {

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Hoja1");

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.Font.Size = 9;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                workSheet.Cells[1, 1, 1, 24].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                workSheet.Cells[1, 1, 1, 24].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 1, 1, 24].Style.Fill.BackgroundColor.SetColor(Color.White);
                workSheet.Cells[1, 1, 1, 24].Style.Font.Color.SetColor(Color.Black);

                workSheet.Cells["A1"].Value = "Actividad";
                workSheet.Cells["B1"].Value = "Cod.EF";
                workSheet.Cells["C1"].Value = "Descripción Cod.EF";
                workSheet.Cells["D1"].Value = "Razon Social";
                workSheet.Cells["E1"].Value = "Periodo";

                workSheet.Cells["F1"].Value = "Cod.T.O";
                workSheet.Cells["G1"].Value = "Tipo Origen";
                workSheet.Cells["H1"].Value = "Voucher";
                workSheet.Cells["I1"].Value = "Cuenta";
                workSheet.Cells["J1"].Value = "Descripcion Cuenta";

                workSheet.Cells["K1"].Value = "Debe Soles";
                workSheet.Cells["L1"].Value = "Haber Soles";
                workSheet.Cells["M1"].Value = "Debe Dolar";
                workSheet.Cells["N1"].Value = "Haber Dolar";

                workSheet.Cells["O1"].Value = "Moneda";
                workSheet.Cells["P1"].Value = "T.C.";
                workSheet.Cells["Q1"].Value = "Fecha";
                workSheet.Cells["R1"].Value = "Glosa";
                workSheet.Cells["S1"].Value = "RUC";
                workSheet.Cells["T1"].Value = "Razon Social";
                workSheet.Cells["U1"].Value = "Cod.Tipo Doc.";
                workSheet.Cells["V1"].Value = "Tipo Documento";
                workSheet.Cells["W1"].Value = "Numero";
                workSheet.Cells["X1"].Value = "Fecha Emisión";
                workSheet.Cells["Y1"].Value = "Fecha Vencimiento";
                workSheet.Cells["Z1"].Value = "Centro Costo";

                workSheet.View.FreezePanes(2, 1);

                int i = 2;

                foreach (var item in listado)
                {
                    workSheet.Row(i).Style.Font.Size = 9;

                    workSheet.Cells[i, 1].Value = item.efActividad;
                    workSheet.Cells[i, 2].Value = item.efCodEFx;
                    workSheet.Cells[i, 3].Value = item.efDescCodEf;
                    workSheet.Cells[i, 4].Value = item.efConcepto;
                    workSheet.Cells[i, 5].Value = item.efperiodo;
                    workSheet.Cells[i, 6].Value = item.cvouch;
                    workSheet.Cells[i, 7].Value = item.dvouch;
                    workSheet.Cells[i, 8].Value = item.ncpbte;
                    workSheet.Cells[i, 9].Value = item.cctaco;
                    workSheet.Cells[i, 10].Value = item.dctaco;

                    workSheet.Cells[i, 11].Value = item.sdebe;
                    workSheet.Cells[i, 12].Value = item.shaber;
                    workSheet.Cells[i, 13].Value = item.ddebe;
                    workSheet.Cells[i, 14].Value = item.dhaber;

                    workSheet.Cells[i, 11].Style.Numberformat.Format = "#,##0.00";
                    workSheet.Cells[i, 12].Style.Numberformat.Format = "#,##0.00";
                    workSheet.Cells[i, 13].Style.Numberformat.Format = "#,##0.00";
                    workSheet.Cells[i, 14].Style.Numberformat.Format = "#,##0.00";

                    workSheet.Cells[i, 15].Value = item.moneda;
                    workSheet.Cells[i, 16].Value = item.itpcam;
                    workSheet.Cells[i, 17].Value = item.fecha;
                    workSheet.Cells[i, 18].Value = item.dglosa;
                    workSheet.Cells[i, 19].Value = item.ruc;
                    workSheet.Cells[i, 20].Value = item.razonSocial;
                    workSheet.Cells[i, 21].Value = item.td;
                    workSheet.Cells[i, 22].Value = item.documento;
                    workSheet.Cells[i, 23].Value = item.numero;
                    workSheet.Cells[i, 24].Value = item.fechaEmision;
                    workSheet.Cells[i, 25].Value = item.fechaVencimiento;
                    workSheet.Cells[i, 26].Value = item.cc;

                    i++;
                }

                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                package.Save();

                stream.Position = 0;
            }
            return stream;
        }

    }

    public class classTituloPeriodo
    {
        public int orden { get; set; }
        public string periodo { get; set; }
    }
    public class classConceptos
    {
        public int IdTipoAct { get; set; }
        public string CodEFx { get; set; }
        public int IdRSx { get; set; }
        public int perorden { get; set; }
    }
    public class classOrdenMontos
    {
        public int orden { get; set; }
        public decimal monto { get; set; }
    }

    public class classTotalGrupos
    {
        public int orden { get; set; }
        public decimal monto { get; set; }
    }

    public class classDistinctCptos
    {
        public int idRSx { get; set; }
        public string codEFx { get; set; }
        public bool registrado { get; set; }
    }

    public class classDistinctSeccion
    {
        public int idTipoAct { get; set; }
        public string codEFx { get; set; }
        public int idRSx { get; set; }
        public bool registrado { get; set; }
    }

    public class classTotalSeccion
    {
        public int orden { get; set; }
        public decimal monto { get; set; }
    }

    public class classTotal
    {
        public int orden { get; set; }
        public decimal monto { get; set; }
    }

    public class classDistinctTotal
    {
        public int idTipoAct { get; set; }
        public string codEFx { get; set; }
        public int idRSx { get; set; }
        public bool registrado { get; set; }
    }

}