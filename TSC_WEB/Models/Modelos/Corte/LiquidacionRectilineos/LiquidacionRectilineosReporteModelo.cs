using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos;


namespace TSC_WEB.Models.Modelos.Corte.LiquidacionRectilineos
{
    public class LiquidacionRectilineosReporteModelo
    {
        //private const string formatoporcentaje = "0.00%";
        private const string formatoporcentaje = "0.0%";

        private const string formatonumero = "0.00";
        public string[] LETRAS = new[] 
        { 
             "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" ,
             "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
             "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ" ,
             "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ" ,
             "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH", "DI", "DJ", "DK", "DL", "DM", "DN", "DO", "DP", "DQ", "DR", "DS", "DT", "DU", "DV", "DW", "DX", "DY", "DZ" ,
             "EA", "EB", "EC", "ED", "EE", "EF", "EG", "EH", "EI", "EJ", "EK", "EL", "EM", "EN", "EO", "EP", "EQ", "ER", "ES", "ET", "EU", "EV", "EW", "EX", "EY", "EZ" ,
             "FA", "FB", "FC", "FD", "FE", "FF", "FG", "FH", "FI", "FJ", "FK", "FL", "FM", "FN", "FO", "FP", "FQ", "FR", "FS", "FT", "FU", "FV", "FW", "FX", "FY", "FZ" ,
             "GA", "GB", "GC", "GD", "GE", "GF", "GG", "GH", "GI", "GJ", "GK", "GL", "GM", "GN", "GO", "GP", "GQ", "GR", "GS", "GT", "GU", "GV", "GW", "GX", "GY", "GZ" ,

        }
        ;

        ExcelWorksheet workSheet;

        // EXPORTAR EXCEL
        public MemoryStream getReporteExcel(List<ReporteEntidad> listadatos)
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                workSheet = package.Workbook.Worksheets.Add("Reporte de Rectilineos");

                int cantregistros = listadatos.Count;
                int colinicio = 10;


                // CABECERAS
                merge("A1:A2", "Tipo", true, 1, Color.White, true, true, false, 11, 1, 12);
                merge("B1:B2", "Usuario", true, 1, Color.White, true, true, false, 11, 1, 12);
                merge("C1:C2", "Fecha de Liquidación", true, 1, Color.White, true, true, false, 11, 1, 12);
                merge("D1:D2", "Ficha", true, 1, Color.White, true, true, false, 11, 1, 12);
                merge("E1:E2", "Partida R.", true, 1, Color.White, true, true, false, 11, 1, 12);
                merge("F1:F2", "Pedido", true, 1, Color.White, true, true, false, 11, 1, 12);
                merge("G1:G2", "Combo", true, 1, Color.White, true, true, false, 11, 1, 12);
                merge("H1:H2", "Estilo TSC", true, 1, Color.White, true, true, false, 11, 1, 12);
                merge("I1:I2", "Estilo Cliente", true, 1, Color.White, true, true, false, 11, 1, 12);
                merge("J1:J2", "Cliente", true, 1, Color.White, true, true, false, 11, 1, 12);


                // MERMA REAL
                //merge("K1:L1", "Merma Real", true, 1, Color.White, true, true, true, 11, 1, 12);
                //merge("K2", "Recorte", true, 1, Color.White, true, true, true, 11, 1, 12);
                //merge("L2", "Hilo", true, 1, Color.White, true, true, true, 11, 1, 12);


                // BALANCE GENERAL
                merge("K1:R1", "Balance General", true, 8, Color.White, true, true, false, 11, 1, 12);
                merge("K2", "Programado total (UN)", true, 8, Color.White, true, true, false, 11, 1, 12);
                merge("L2", "Liquidado total (UN)", true, 8, Color.White, true, true, false, 11, 1, 12);
                merge("M2", "Diferencia (UN)", true, 8, Color.White, true, true, false, 11, 1, 12);
                merge("N2", "Porcentaje (%)", true, 8, Color.White, true, true, false, 11, 1, 12);

                merge("O2", "Programado total (Kg)", true, 9, Color.White, true, true, false, 11, 1, 12);
                merge("P2", "Liquidado total (Kg)", true, 9, Color.White, true, true, false, 11, 1, 12);
                merge("Q2", "Diferencia (kg)", true, 9, Color.White, true, true, false, 11, 1, 12);
                merge("R2", "Porcentaje (%)", true, 9, Color.White, true, true, false , 11, 1, 12);


                int filainicio = 3;
                // RECORREMOS VALORES
                foreach (var item in listadatos)
                {   

                    // DATOS GENERALES
                    cells(filainicio, 1, item.tipo, Color.White, Color.Black, false, true);
                    cells(filainicio, 2, item.usuariocrea , Color.White, Color.Black, false, true);
                    cells(filainicio, 3, item.fechaliquidacion, Color.White, Color.Black, false, true);
                    cells(filainicio, 4, item.ficha, Color.White, Color.Black, false, true);
                    cells(filainicio, 5, item.partida, Color.White, Color.Black, false, true);
                    cells(filainicio, 6, item.pedido, Color.White, Color.Black, false, true);
                    cells(filainicio, 7, item.combo, Color.White, Color.Black, false, true);
                    cells(filainicio, 8, item.estilotsc, Color.White, Color.Black, false, true);
                    cells(filainicio, 9, item.estilocliente, Color.White, Color.Black, false, true);
                    cells(filainicio, 10, item.cliente, Color.White, Color.Black, false, true);


                   

                    // MERMA RECORTE
                    //cells(filainicio, 11,Convert.ToDouble( item.mermarecorte.ToString()) , Color.White, Color.Black, false, true,false,3);

                    // MERMA HILOS
                    //cells(filainicio, 12, Convert.ToDouble(item.mermahilos.ToString()) , Color.White, Color.Black, false, true,false,3);

                    //iniciocolumnadatos++;

                    //decimal difunidades = totalliquidado - totalprogramado;
                    //decimal difkilos = totalpesonetoreal - totalprogramadokg;

                    cells(filainicio, 11, item.programado.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, 12, item.realprimera.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, 13, item.pendienteunidades.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, 14, item.porcentajeliquidacion_excel.ToString(), Color.White, Color.Black, false, true,true);

                    cells(filainicio, 15, item.pesoprogramado.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, 16, item.pesonetoreal.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, 17, item.pendienteliquidacionkg.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, 18, item.porcentajeliquidacionkg_excel.ToString(), Color.White, Color.Black, false, true,true);



                    filainicio++;

                }


                workSheet.Cells["A1:R"+ filainicio].AutoFitColumns();






                package.Save();
            }

            stream.Position = 0;
            return stream;

        }


        #region EXCELES

        private void setTallas(int colinicio, List<ReporteEntidad> tallas, int canttallas, int backcolor = 1)
        {
            // ARMAMOS TALLAS
            for (int x = colinicio, index = 0; x < (colinicio + canttallas); x++)
            {
                merge(LETRAS[x] + "2", tallas[index].talla, true, backcolor, Color.White, true, true, true, 11, 1, 12);
                index++;
            }
        }

        private void merge(string rango, string valor, bool merge, int backcolor, Color fontcolor, bool negrita = true, bool bordes = true, bool wraptext = false, int tamanoletra = 11, int columna = 0, double widthcolumna = 0)
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
                case 7:
                    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 112, 173, 71);
                    break;
                case 8:
                    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 237, 125, 49);
                    break;
                case 9:
                    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 68, 114, 196);
                    break;

            }

            //if (backcolor)
            //    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 51, 63, 79);
            //else
            //    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 198, 89, 17);

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

        private void mergesimple(string rango, string valor, bool merge)
        {
            // RANGO COMBINADO
            workSheet.Cells[rango].Merge = merge;
            // VALOR
            workSheet.Cells[rango].Value = valor;

            //workSheet.Cells[rango].Style.Fill.PatternType = ExcelFillStyle.Solid;
            // TAMAÑO DE LETRA
            //workSheet.Cells[rango].Style.Font.Size = tamanoletra;

            // AJUSTAR TEXTO A COLUMNA
            //workSheet.Cells[rango].Style.WrapText = wraptext;

            // TAMAÑO DE CELDA
            //if (columna != 0)
            //{
            //    workSheet.Column(columna).Width = widthcolumna;
            //}


            // BACKGROUND
            //switch (backcolor)
            //{
            //    case 1:
            //        workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 51, 63, 79);
            //        break;
            //    case 2:
            //        workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 128, 128, 128);
            //        break;
            //    case 3:
            //        workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 55, 86, 35);
            //        break;
            //    case 4:
            //        workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 128, 96, 0);
            //        break;
            //    case 5:
            //        workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 198, 89, 17);
            //        break;
            //    case 6:
            //        workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 82, 82, 82);
            //        break;

            //}

            //if (backcolor)
            //    workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 51, 63, 79);
            //else
            //workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 198, 89, 17);

            // COLOR DE TEXTO
            //workSheet.Cells[rango].Style.Font.Color.SetColor(fontcolor);

            // NEGRITA
            //workSheet.Cells[rango].Style.Font.Bold = negrita;
            // ALINEAMIENTO
            workSheet.Cells[rango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells[rango].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // BORDES
            //if (bordes)
            //{
            workSheet.Cells[rango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[rango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[rango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[rango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            //}

        }

        private void cells(int fila, int columna, string valor, Color backcolor, Color fontcolor, bool negrita = true, bool bordes = true, bool porcentaje = false)
        {

            // FORMATO DE PORCENTAJE
            if (porcentaje)
            {
                ExcelRange rango = workSheet.SelectedRange[fila, columna];
                rango.Style.Numberformat.Format = formatoporcentaje;

            }

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

        private void cellsformula(int fila, int columna, string valor, Color backcolor, Color fontcolor, bool negrita = true, bool bordes = true, bool porcentaje = false)
        {
            //decimal numero = 0;
            //if (!decimal.TryParse(valor, out numero))
            //    workSheet.Cells[fila, columna].Value = valor;
            //else
            //    workSheet.Cells[fila, columna].Value = numero;

            workSheet.Cells[fila, columna].Formula = valor;

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

        private void cells(int fila, int columna, double valor, Color backcolor, Color fontcolor, bool negrita = true, bool bordes = true, bool porcentaje = false, int decimales = 0)
        {

            // FORMATO DE PORCENTAJE
            if (porcentaje)
            {
                ExcelRange rango = workSheet.SelectedRange[fila, columna];
                rango.Style.Numberformat.Format = formatoporcentaje;

            }
            //else
            //{
            //    string news = string.Format("{0:n3}", valor);
            //    workSheet.Cells[fila, columna].Value = Convert.ToDouble(news);
            //}

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


        // CEROS DECIMALES
        private string getcerosdecimales(int deci)
        {
            string devolver = "0.";

            for (int x = 1; x <= deci; x++)
            {
                devolver += "0";
            }

            return devolver;
        }

        #endregion

    }
}