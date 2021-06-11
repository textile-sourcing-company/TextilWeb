using Dapper;
using Dapper.Oracle;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Contabilidad.ReporteGuiasConta;

namespace TSC_WEB.Models.Modelos.Contabilidad.ReporteGuiasConta
{
    public class ReporteGuiasConta
    { 
        public async Task<List<ReporteGuiasContaEntidad>> ListarReporteGuiasConta(DateTime fechaInicio, DateTime fechaFin, int? numguia, string serieguia)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("PFECHAINI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("PFECHAFIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("PNUMERGUI", numguia, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("PSERIEGUI", serieguia, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add(name: ":PCURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<ReporteGuiasContaEntidad>(sql: "USYSTEX.GETSYS_CONTA_GUIAFACTURA",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MemoryStream ReporteExcel(List<ReporteGuiasContaEntidad> listado)
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Hoja1");

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.Font.Size = 8;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(1).Style.Fill.BackgroundColor.SetColor(Color.White);

                workSheet.Cells["A1"].Value = "EMPRESA";
                workSheet.Cells["B1"].Value = "NRO. GUÍA";
                workSheet.Cells["C1"].Value = "SERIE GUÍA";
                workSheet.Cells["D1"].Value = "FCH. EMISIÓN GUÍA";
                workSheet.Cells["E1"].Value = "CNT. GUÍA";
                workSheet.Cells["F1"].Value = "NRO. FACTURA";
                workSheet.Cells["G1"].Value = "SERIE FACTURA";
                workSheet.Cells["H1"].Value = "FCH. EMISIÓN FACTURA";
                workSheet.Cells["I1"].Value = "CNT. FACTURA";
                workSheet.Cells["J1"].Value = "VALOR FACTURADO"; 
                workSheet.Cells["K1"].Value = "VALOR IGV FAC";
                workSheet.Cells["L1"].Value = "VALOR TOTAL FAC";
                workSheet.Cells["M1"].Value = "CLIENTE";
                workSheet.Cells["N1"].Value = "CONCEPTO";
                workSheet.Cells["O1"].Value = "ESTADO";
                workSheet.Cells["P1"].Value = "DIFERENCIA";
                 
                workSheet.Column(1).Width = 30;      // EMPRESA 
                workSheet.Column(2).Width = 8;       // NRO. GUÍA
                workSheet.Column(3).Width = 30;      // SERIE GUÍA
                workSheet.Column(4).Width = 20;      // FCH. EMISIÓN GUÍA
                workSheet.Column(5).Width = 20;      // CNT. GUÍA
                workSheet.Column(6).Width = 20;      // NRO. FACTURA 
                workSheet.Column(7).Width = 20;      // SERIE FACTURA
                workSheet.Column(8).Width = 20;      // FCH. EMISIÓN FACTURA
                workSheet.Column(9).Width = 50;      // CNT. FACTURA
                workSheet.Column(10).Width = 50;     // VALOR FACTURAD 
                workSheet.Column(11).Width = 30;     // VALOR IGV FAC
                workSheet.Column(12).Width = 30;     // VALOR TOTAL FAC
                workSheet.Column(13).Width = 30;     // CLIENTE
                workSheet.Column(14).Width = 30;     // CONCEPTO
                workSheet.Column(15).Width = 30;     // ESTADO
                workSheet.Column(16).Width = 30;     // DIFERENCIA
                 
                int i = 2;
                 
                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.EMPRESA;
                    workSheet.Cells[i, 2].Value = item.NRO_GUIA;
                    workSheet.Cells[i, 3].Value = item.SERIE_GUIA;
                    workSheet.Cells[i, 4].Value = item.FECHA_EMISION_GUIA;
                    workSheet.Cells[i, 5].Value = item.CANTIDAD_GUIA;
                    workSheet.Cells[i, 6].Value = item.NRO_FACTURA;
                    workSheet.Cells[i, 7].Value = item.SERIE_FACTURA;
                    workSheet.Cells[i, 8].Value = item.FECHA_EMISION_FACTURA;
                    workSheet.Cells[i, 9].Value = item.CANTIDAD_FACTURA;
                    workSheet.Cells[i, 10].Value = item.VALOR_FACTURADO; 
                    workSheet.Cells[i, 11].Value = item.VALOR_IGV_FAC;
                    workSheet.Cells[i, 12].Value = item.VALOR_TOTAL_FAC;
                    workSheet.Cells[i, 13].Value = item.CLIENTE;
                    workSheet.Cells[i, 14].Value = item.CONCEPTO;
                    workSheet.Cells[i, 15].Value = item.ESTADO;
                    workSheet.Cells[i, 16].Value = item.DIFERENCIA;

                    i++;
                }

                package.Save();
            }

            stream.Position = 0;
            return stream;

        }
         
    }
}