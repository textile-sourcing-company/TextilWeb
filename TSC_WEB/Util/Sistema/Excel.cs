using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Style;


namespace TSC_WEB.Util.Sistema
{
    public class Excel
    {
        private const string formatoporcentaje = "0.00%";
        private const string formatonumero = "0.00";
        private static ExcelWorksheet workSheet;
        MemoryStream stream = new MemoryStream();
        ExcelPackage package = new ExcelPackage();

        public Excel()
        {
            package = new ExcelPackage(stream);
            workSheet = package.Workbook.Worksheets.Add("Reporte");
        }


        // CEROS DECIMALES
        public string getcerosdecimales(int deci)
        {
            string devolver = "0.";

            for (int x = 1; x <= deci; x++)
            {
                devolver += "0";
            }

            return devolver;
        }
        // COMBINAR CELDAS
        public void merge(string rango, string valor, bool merge, int backcolor, Color fontcolor, bool negrita = true, bool bordes = true, bool wraptext = false, int tamanoletra = 11, int columna = 0, double widthcolumna = 0)
        {
            // RANGO COMBINADO
            workSheet.Cells[rango].Merge = merge;
            // VALOR
            workSheet.Cells[rango].Value = valor;

            workSheet.Cells[rango].Style.Fill.PatternType = ExcelFillStyle.Solid;
            // TAMAÑO DE LETRA
            workSheet.Cells[rango].Style.Font.Size = tamanoletra;

            // AJUSTAR TEXTO A COLUMNA
            workSheet.Cells[rango].Style.WrapText = wraptext;

            // TAMAÑO DE CELDA
            if (columna != 0)
            {
                workSheet.Column(columna).Width = widthcolumna;
            }


            // BACKGROUND
            switch (backcolor)
            {
                case 1:
                    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 51, 63, 79);
                    break;
                case 2:
                    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 128, 128, 128);
                    break;
                case 3:
                    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 55, 86, 35);
                    break;
                case 4:
                    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 128, 96, 0);
                    break;
                case 5:
                    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 198, 89, 17);
                    break;
                case 6:
                    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 82, 82, 82);
                    break;

            }


            // COLOR DE TEXTO
            workSheet.Cells[rango].Style.Font.Color.SetColor(fontcolor);

            // NEGRITA
            workSheet.Cells[rango].Style.Font.Bold = negrita;
            // ALINEAMIENTO
            workSheet.Cells[rango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells[rango].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // BORDES
            if (bordes)
            {
                workSheet.Cells[rango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[rango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[rango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[rango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            }

        }
        // CELDA SOLA VALOR STRING
        public void cells(int fila, int columna, string valor, Color backcolor, Color fontcolor, bool negrita = true, bool bordes = true, bool porcentaje = false)
        {
            decimal numero = 0;
            if (!decimal.TryParse(valor, out numero))
                workSheet.Cells[fila, columna].Value = valor;
            else
                workSheet.Cells[fila, columna].Value = numero;

            workSheet.Cells[fila, columna].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


            if (bordes)
            {
                workSheet.Cells[fila, columna].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[fila, columna].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[fila, columna].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[fila, columna].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            }



        }
        // CELDA SOLA VALOR DECIMAL
        public void cells(int fila, int columna, double valor, Color backcolor, Color fontcolor, bool negrita = true, bool bordes = true, bool porcentaje = false, int decimales = 0)
        {

            // FORMATO DE PORCENTAJE
            if (porcentaje)
            {
                ExcelRange rango = workSheet.SelectedRange[fila, columna];
                rango.Style.Numberformat.Format = formatoporcentaje;

            }

            if (decimales != 0)
            {

                ExcelRange rango = workSheet.SelectedRange[fila, columna];
                rango.Style.Numberformat.Format = getcerosdecimales(decimales);
                workSheet.Cells[fila, columna].Value = valor;
            }
            else
            {
                workSheet.Cells[fila, columna].Value = valor;
            }



            workSheet.Cells[fila, columna].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


            if (bordes)
            {
                workSheet.Cells[fila, columna].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[fila, columna].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[fila, columna].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[fila, columna].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            }


        }

        public MemoryStream getestream()
        {
            return stream;
        }

    }
}