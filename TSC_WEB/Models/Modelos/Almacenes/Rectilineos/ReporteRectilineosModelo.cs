using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos;


namespace TSC_WEB.Models.Modelos.Almacenes.Rectilineos
{
    public class ReporteRectilineosModelo
    {

        private const string formatoporcentaje = "0.00%";
        private const string formatonumero = "0.00";
        public string[] LETRAS = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string iniciocolumna    = "";
        string fincolumna       = "";
        string PARTIDATELA      = "";
        string RANGOANTERIOR    = "";


        ExcelWorksheet workSheet;

        // REPORTE
        public MemoryStream getReporteExcel(List<ReporteIngresoRectilineosAlmacenEntidad> listadatos)
        {
            
            int cantregistros = listadatos.Count;


            // LISTA DE PARTIDAS DE RECTILINEOS CUELLO
            var listacuello = cantregistros > 0 ? listadatos.GroupBy(
                    grp => new
                    {
                        grp.partidatela,
                        grp.partidarectilineo,
                        grp.tiporectilineo
                    }
                ).Select(sel => new ReporteIngresoRectilineosAlmacenEntidad
                {
                    partidatela = sel.First().partidatela,
                    partidarectilineo = sel.First().partidarectilineo,
                    tiporectilineo = sel.First().tiporectilineo

                }).Where(obj => obj.tiporectilineo == "CUELLO").ToList() : new List<ReporteIngresoRectilineosAlmacenEntidad>();

            listacuello = listacuello.Distinct().ToList();

            // LISTA DE PARTIDAS DE RECTILINEOS PUÑO
            var listapuno = cantregistros > 0 ? listadatos.GroupBy(
                    grp => new
                    {
                        grp.partidatela,
                        grp.partidarectilineo,
                        grp.tiporectilineo
                    }
                ).Select(sel => new ReporteIngresoRectilineosAlmacenEntidad
                {
                    partidatela = sel.First().partidatela,
                    partidarectilineo = sel.First().partidarectilineo,
                    tiporectilineo = sel.First().tiporectilineo

                }).Where(obj => obj.tiporectilineo == "PUNO").ToList() : new List<ReporteIngresoRectilineosAlmacenEntidad>();

            listacuello = listacuello.Distinct().ToList();

            // TALLAS
            var tallas = cantregistros > 0 ? listadatos.GroupBy(
                grp => new
                {
                    grp.orden,
                    grp.talla
                }
            )
            .Select(cl => new ReporteIngresoRectilineosAlmacenEntidad
            {
                talla = cl.First().talla,
                orden = cl.First().orden
            }).OrderBy(or => or.orden).ToList() : new List<ReporteIngresoRectilineosAlmacenEntidad>();


            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                workSheet = package.Workbook.Worksheets.Add("Reporte Ingreso de Rectilineos");


                // CABECERA DE PARTIDA DE CUELLOS GENERAL
                merge("A3:"+LETRAS[tallas.Count + 2]+"3", "RECTILINEOS CUELLOS", true, 1, Color.White, true, true, true, 11, 1, 12);

               
                // CABECERAS FIJAS
                cells(4, 1, "Partida Tela", Color.White, Color.Black, false, true);
                cells(4, 2, "Partida Rectilineo", Color.White, Color.Black, false, true);


                // TALLAS CABECERAS
                int icolumna = 3;
                foreach (var item in tallas)
                {
                    cells(4, icolumna, item.talla, Color.White, Color.Black, false, true);
                    icolumna++;
                }

                // CANTIDAD TOTAL CABECERA
                cells(4, icolumna, "TOTAL", Color.White, Color.Black, false, true);

                int ifila = 5;
                // RECORREMOS
                foreach (var item in listacuello)
                {

                    // ################
                    // ### PRIMERAS ###
                    // ################

                    // PARTIDA DE TELA
                    if (PARTIDATELA != item.partidatela)
                    {
                        cells(ifila, 1, item.partidatela, Color.White, Color.Black, false, true);
                        if (RANGOANTERIOR != string.Empty)
                        {
                            mergesimple(RANGOANTERIOR+":"+ "A" + (ifila - 1).ToString(),  PARTIDATELA,true);
                        }
                        RANGOANTERIOR = "A" + ifila;
                    }

                    // PARTIDA DE RECTILINEO
                    cells(ifila, 2, item.partidarectilineo, Color.White, Color.Black, false, true);

                    int icolumnap = 3;
                    iniciocolumna = LETRAS[icolumnap - 1];

                    // TALLAS
                    foreach (var talla in tallas)
                    {
                        var primeras = listadatos.Where(
                                       fil => fil.talla == talla.talla &&
                                       fil.partidatela == item.partidatela &&
                                       fil.partidarectilineo == item.partidarectilineo &&
                                       fil.tiporectilineo == item.tiporectilineo
                                   ).FirstOrDefault();

                        cells(ifila, icolumnap, primeras.cantidadprimera.ToString(), Color.White, Color.Black, false, true);
                        icolumnap++;
                    }

                    fincolumna = LETRAS[icolumnap - 2];

                    string suma = "SUMA("+iniciocolumna+ ifila+":"+fincolumna + ifila+")";
                    cellsformula(ifila, icolumnap, suma, Color.White, Color.Black, false, true);

                    // ################
                    // ### SEGUNDAS ###
                    // ################


                    ifila++;
                    if (ifila == (listacuello.Count * 2) + 4)
                    {
                        mergesimple(RANGOANTERIOR + ":" + "A" + (ifila).ToString(), item.partidatela, true);
                    }

                    merge("B" + ifila, "Segundas", true, 1, Color.White, true, true, true, 11, 1, 12);


                    int icolumnas = 3;
                    foreach (var talla in tallas)
                    {
                        var segundas = listadatos.Where(
                                       fil => fil.talla == talla.talla &&
                                       fil.partidatela == item.partidatela &&
                                       fil.partidarectilineo == item.partidarectilineo &&
                                       fil.tiporectilineo == item.tiporectilineo
                                   ).FirstOrDefault();

                        cells(ifila, icolumnas, segundas.cantidadsegunda.ToString(), Color.White, Color.Black, false, true);
                        icolumnas++;
                    }

                    string sumasegundas = "SUMA(" + iniciocolumna + ifila + ":" + fincolumna + ifila + ")";
                    cellsformula(ifila, icolumnas, sumasegundas, Color.White, Color.Black, false, true);

                    // ASIGNAMOS PARTIDA DE TELA
                    PARTIDATELA = item.partidatela;

                    ifila++;
                }

                // ####################################
                // ### CABECERA DE PARTIDA DE PUÑOS ###
                // ####################################
                string  iniciocolpunostring = LETRAS[tallas.Count + 4];
                string  iniciocolpunostring2 = LETRAS[tallas.Count + 5];

                int iniciocolpunoint = tallas.Count + 5;


                merge(iniciocolpunostring+"3:" + LETRAS[iniciocolpunoint + tallas.Count + 1] + "3", "RECTILINEOS PUÑOS", true, 1, Color.White, true, true, true, 11, 1, 12);

                // COLUMNAS
                cells(4, iniciocolpunoint, "Partida Tela", Color.White, Color.Black, false, true);
                cells(4, iniciocolpunoint + 1, "Partida Rectilineo", Color.White, Color.Black, false, true);

                // TALLAS
                int icolumnanew = iniciocolpunoint + 2;
                foreach (var item in tallas)
                {
                    cells(4, icolumnanew, item.talla, Color.White, Color.Black, false, true);
                    icolumnanew++;
                }
                // CANTIDAD TOTAL
                cells(4, icolumnanew, "TOTAL", Color.White, Color.Black, false, true);


                int ifilanew = 5;
                PARTIDATELA = string.Empty;
                RANGOANTERIOR = string.Empty;

                // RECORREMOS
                foreach (var item in listapuno)
                {
                    // PRIMERAS

                    if (PARTIDATELA != item.partidatela)
                    {
                        cells(ifilanew, iniciocolpunoint, item.partidatela, Color.White, Color.Black, false, true);
                        if (RANGOANTERIOR != string.Empty)
                        {
                            mergesimple(RANGOANTERIOR + ":" + LETRAS[iniciocolpunoint-1] + (ifilanew - 1).ToString(), PARTIDATELA, true);
                        }
                        RANGOANTERIOR = LETRAS[iniciocolpunoint - 1] + ifilanew;
                    }

                    cells(ifilanew, iniciocolpunoint + 1, item.partidarectilineo, Color.White, Color.Black, false, true);

                    int icolumnap = iniciocolpunoint +2;

                    iniciocolumna = LETRAS[icolumnap - 1];

                    foreach (var talla in tallas)
                    {
                        var primeras = listadatos.Where(
                                       fil => fil.talla == talla.talla &&
                                       fil.partidatela == item.partidatela &&
                                       fil.partidarectilineo == item.partidarectilineo &&
                                       fil.tiporectilineo == item.tiporectilineo
                                   ).FirstOrDefault();

                        cells(ifilanew, icolumnap, primeras.cantidadprimera.ToString(), Color.White, Color.Black, false, true);
                        icolumnap++;
                    }

                    fincolumna = LETRAS[icolumnap - 2];
                    string suma = "SUMA(" + iniciocolumna + ifilanew + ":" + fincolumna + ifilanew + ")";

                    cellsformula(ifilanew, icolumnap, suma, Color.White, Color.Black, false, true);

                    // SEGUNDAS
                    ifilanew++;

                    // MERGA A LA ULTIMA FILA
                    if (ifilanew == (listapuno.Count * 2) + 4)
                    {
                        mergesimple(RANGOANTERIOR + ":" + LETRAS[iniciocolpunoint - 1] + (ifilanew).ToString(), item.partidatela, true);

                    }

                    merge(iniciocolpunostring2 + ifilanew, "Segundas", true, 1, Color.White, true, true, true, 11, 1, 12);


                    int icolumnas = iniciocolpunoint + 2;
                    foreach (var talla in tallas)
                    {
                        var segundas = listadatos.Where(
                                       fil => fil.talla == talla.talla &&
                                       fil.partidatela == item.partidatela &&
                                       fil.partidarectilineo == item.partidarectilineo &&
                                       fil.tiporectilineo == item.tiporectilineo
                                   ).FirstOrDefault();

                        cells(ifilanew, icolumnas, segundas.cantidadsegunda.ToString(), Color.White, Color.Black, false, true);
                        icolumnas++;
                    }

                    string sumasegundas = "SUMA(" + iniciocolumna + ifilanew + ":" + fincolumna + ifilanew + ")";
                    cellsformula(ifilanew, icolumnas, sumasegundas, Color.White, Color.Black, false, true);

                    ifilanew++;

                    // ASIGNAMOS PARTIDA DE TELA
                    PARTIDATELA = item.partidatela;
                }


                // ANCHO DE COLUMNAS 
                workSheet.Column(1).Width = 11;
                workSheet.Column(2).Width = 17;

                workSheet.Column(iniciocolpunoint).Width = 11;
                workSheet.Column(iniciocolpunoint +1).Width = 17;


                //package.Workbook.CalculateMode = CalculateMode.Auto;
                //package.Workbook.CalculationOnSave = true;

                //package.Workbook.CalcMode = ExcelCalcMode.Automatic;
                //package.Workbook.FullCalcOnLoad = true;
                
                //workSheet.Calculate();
                //package.Workbook.ca



                package.Save();
            }

            stream.Position = 0;
            return stream;
        }


        #region EXCELES

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