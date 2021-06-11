using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Data;
using System.Linq;
using System.Web;
using TSC_WEB.Models.Entidades.Logistica;

namespace TSC_WEB.Models.Modelos
{
    public class ExportarExcel_din
    {
        public MemoryStream ExpExcelOCMes(List<EBSegOCMes> listado )
        {
            var stream = new MemoryStream();
            DataTable dt = new DataTable() ;
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

                workSheet.Cells["A1"].Value = "CLIENTE";
                workSheet.Cells["B1"].Value = "MES";
                workSheet.Cells["C1"].Value = "PROGRAMA";
                workSheet.Cells["D1"].Value = "FAMILIA";
                workSheet.Cells["E1"].Value = "COD. PRODUCTO";
                workSheet.Cells["F1"].Value = "PRODUCTO";
                workSheet.Cells["G1"].Value = "ORDEN COMPRA";
                workSheet.Cells["H1"].Value = "PROVEEDOR";
                workSheet.Cells["I1"].Value = "COMPRADOR";
                workSheet.Cells["J1"].Value = "FECHA EMISION OC";
                workSheet.Cells["K1"].Value = "FECHA COMPROMISO";
                workSheet.Cells["L1"].Value = "COND. PAGO";
                workSheet.Cells["M1"].Value = "CANTIDAD OC";
                workSheet.Cells["N1"].Value = "PRECIO UNITARIO";
                workSheet.Cells["O1"].Value = "TOTAL";
                workSheet.Cells["P1"].Value = "CODIGO CENTRO DE COSTO";
                workSheet.Cells["Q1"].Value = "DESCR. CENTRO DE COSTO";
                int i = 2;

                foreach (var item in listado)
                {
                    workSheet.Row(i).Style.Font.Size = 8;
                    workSheet.Cells[i, 1].Value = item.CLIENTE;
                    workSheet.Cells[i, 2].Value = item.MES;
                    workSheet.Cells[i, 3].Value = item.PROGRAMA;
                    workSheet.Cells[i, 4].Value = item.FAMILIA;
                    workSheet.Cells[i, 5].Value = item.COD_PROD;
                    workSheet.Cells[i, 6].Value = item.DSC_PROD;
                    workSheet.Cells[i, 7].Value = item.OC;
                    workSheet.Cells[i, 8].Value = item.PROVEEDOR;
                    workSheet.Cells[i, 9].Value = item.COMPRADOR;
                    workSheet.Cells[i, 10].Value = item.FECHA_EMI_OC;
                    workSheet.Cells[i, 11].Value = item.FECHA_COMP_OC;
                    workSheet.Cells[i, 12].Value = item.DESCRIPCIONPAGO;
                    workSheet.Cells[i, 13].Value = item.CNT_OC;
                    workSheet.Cells[i, 14].Value = item.PREC_UNIT;
                    workSheet.Cells[i, 15].Value = item.TOTAL;
                    workSheet.Cells[i, 16].Value = item.COD_CENTROCOSTO;
                    workSheet.Cells[i, 17].Value = item.DESC_CENTROCOSTO;

                    i++;
                }
                /*Ajustando columnas*/
                for (int xi = 1; xi < listado.Count(); xi++)
                {
                    workSheet.Column(xi).AutoFit();
                }

                package.Save();
            }
            stream.Position = 0;
            return stream;
        }
        public MemoryStream ExpExcelSegAvio(List<SegAvioProgEntidad> listado)
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

                workSheet.Cells["A1"].Value = "CLIENTE";
                workSheet.Cells["B1"].Value = "FECHA CREACION PEDIDO";
                workSheet.Cells["C1"].Value = "FECHA EXFACTORY";
                workSheet.Cells["D1"].Value = "PROGRAMA";
                workSheet.Cells["E1"].Value = "ESTILO CLIENTE";
                workSheet.Cells["F1"].Value = "PEDIDO";
                workSheet.Cells["G1"].Value = "FECHA EMISIÓN REQ.";
                workSheet.Cells["H1"].Value = "REQUERIMIENTO";
                workSheet.Cells["I1"].Value = "PO";
                workSheet.Cells["J1"].Value = "FECHA ASIGNACION AVIO";
                workSheet.Cells["K1"].Value = "AVIO";
                workSheet.Cells["L1"].Value = "DESCRIPCION AVIO";
                workSheet.Cells["M1"].Value = "ORDEN COMPRA";
                workSheet.Cells["N1"].Value = "FECHA EMISION OC";
                workSheet.Cells["O1"].Value = "FECHA COMPROMISO OC";
                workSheet.Cells["P1"].Value = "QTDE. REQUERIDA PROGRAMA";
                workSheet.Cells["Q1"].Value = "QTDE. REQUERIMIENTO";
                workSheet.Cells["R1"].Value = "QTDE. OC";
                workSheet.Cells["S1"].Value = "QTDE. RECIBIDA";
                workSheet.Cells["T1"].Value = "FECHA INGRESO ALMACEN";
                workSheet.Cells["U1"].Value = "PERIODO BOOKING";
                workSheet.Cells["V1"].Value = "ORDEM PLANEAMIENTO";

                int i = 2;

                foreach (var item in listado)
                {
                    workSheet.Row(i).Style.Font.Size = 8;
                    workSheet.Cells[i, 1].Value = item.Cliente;
                    workSheet.Cells[i, 2].Value = item.Fecha_creacion_pedido;
                    workSheet.Cells[i, 3].Value = item.Fecha_Exfactory;
                    workSheet.Cells[i, 4].Value = item.Programa;
                    workSheet.Cells[i, 5].Value = item.Estilo_cliente;
                    workSheet.Cells[i, 6].Value = item.Pedido;
                    workSheet.Cells[i, 7].Value = item.Fecha_emision_req;
                    workSheet.Cells[i, 8].Value = item.Requerimiento;
                    workSheet.Cells[i, 9].Value = item.Po;
                    workSheet.Cells[i, 10].Value = item.Fecha_asig_avio;
                    workSheet.Cells[i, 11].Value = item.Codigo_avio;
                    workSheet.Cells[i, 12].Value = item.Descripcion_avio;
                    workSheet.Cells[i, 13].Value = item.Orden_compra;
                    workSheet.Cells[i, 14].Value = item.Fecha_Emision_Oc;
                    workSheet.Cells[i, 15].Value = item.Fecha_compromiso_Oc;

                    workSheet.Cells[i, 16].Value = item.Cantidad_req_programa;
                    workSheet.Cells[i, 17].Value = item.Cantidad_requerimiento;
                    workSheet.Cells[i, 18].Value = item.Cantidad_Oc;
                    workSheet.Cells[i, 19].Value = item.Cantidad_Recibida;
                    workSheet.Cells[i, 20].Value = item.Fecha_Ingreso_alm;
                    workSheet.Cells[i, 21].Value = item.Periodo_booking;
                    workSheet.Cells[i, 22].Value = item.Orden_Planeamiento;

                    i++;
                }
                /*Ajustando columnas*/
                for (int xi = 1; xi < listado.Count(); xi++)
                {
                    workSheet.Column(xi).AutoFit();
                }

                package.Save();
            }
            stream.Position = 0;
            return stream;
        }
    }
}