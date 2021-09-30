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
        private const string formatoporcentaje = "0.00%";
        private const string formatonumero = "0.00";
        public string[] LETRAS = new[] 
        { 
             "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" ,
             "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
             "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ" ,
             "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ" 
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
                int colinicio = 9;

                // TALLAS
                var tallas = cantregistros > 0 ? listadatos.GroupBy(std => std.talla)
                    .Select(cl => new ReporteEntidad
                    {
                        talla = cl.First().talla,
                        ordentalla = cl.First().ordentalla
                    }).OrderBy(or => or.ordentalla).ToList() : new List<ReporteEntidad>();

                var canttallas = tallas.Count;

                //  LISTAS
                var lista = cantregistros > 0 ? listadatos.GroupBy(
                        grp => new
                        {
                            grp.usuariocrea,
                            grp.fechamod,
                            grp.ficha,
                            grp.partida,
                            grp.pedido,
                            grp.combo,
                            grp.estilotsc,
                            grp.estilocliente,
                            grp.tipo,
                            grp.mermahilos,
                            grp.mermarecorte
                        }
                    ).Select(sel => new ReporteEntidad
                    {
                        usuariocrea = sel.First().usuariocrea,
                        fechamod = sel.First().fechamod,
                        ficha = sel.First().ficha,
                        partida = sel.First().partida,
                        pedido = sel.First().pedido,
                        combo = sel.First().combo,
                        estilotsc = sel.First().estilotsc,
                        estilocliente = sel.First().estilocliente,
                        tipo = sel.First().tipo,
                        mermahilos = sel.First().mermahilos,
                        mermarecorte = sel.First().mermarecorte

                    }).OrderBy(obj => obj.tipo).ThenBy(t => t.ficha).ToList() : new List<ReporteEntidad>();

                lista = lista.Distinct().ToList();



                // CABECERAS
                merge("A1:A2", "Tipo", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge("B1:B2", "Usuario", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge("C1:C2", "Fecha de Liquidación", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge("D1:D2", "Ficha", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge("E1:E2", "Partida R.", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge("F1:F2", "Pedido", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge("G1:G2", "Combo", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge("H1:H2", "Estilo TSC", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge("I1:I2", "Estilo Cliente", true, 1, Color.White, true, true, true, 11, 1, 12);

                // PROGRAMADO GIRADO
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio + (canttallas - 1)] + "1", "Programado/Girado (Unidades)", true, 1, Color.White, true, true, true, 11, 1, 12);
                // TALLAS
                setTallas(colinicio, tallas, canttallas);
                colinicio += canttallas;

                // LIQUIDADO UNIDADES
                merge(LETRAS[colinicio]+"1:" + LETRAS[colinicio + (canttallas - 1)] + "1", "Liquidado (Unidades)", true, 1, Color.White, true, true, true, 11, 1, 12);
                // TALLAS
                setTallas(colinicio, tallas, canttallas);
                colinicio += canttallas;

                // PENDIENTE DE LIQUIDACION
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio + (canttallas - 1)] + "1", "Pendiente Liquidación por talla (Unidades)", true, 1, Color.White, true, true, true, 11, 1, 12);
                // TALLAS
                setTallas(colinicio, tallas, canttallas);
                colinicio += canttallas;

                // TOTALES
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio] + "2", "Total", true, 1, Color.White, true, true, true, 11, 1, 12);
                colinicio += 1;

                // % LIQUIDACION POR TALLAS
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio + (canttallas - 1)] + "1", "% Liquidación por talla", true, 1, Color.White, true, true, true, 11, 1, 12);
                // TALLAS
                setTallas(colinicio, tallas, canttallas);
                colinicio += canttallas;
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio] + "2", "Total", true, 1, Color.White, true, true, true, 11, 1, 12);
                colinicio += 1;

                // Programado (kg)
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio + (canttallas - 1)] + "1", "Programado (kg)", true, 1, Color.White, true, true, true, 11, 1, 12);
                // TALLAS
                setTallas(colinicio, tallas, canttallas);
                colinicio += canttallas;
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio] + "2", "Total", true, 1, Color.White, true, true, true, 11, 1, 12);
                colinicio += 1;

                // Liquidado (kg)
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio + (canttallas - 1)] + "1", "Liquidado (kg)", true, 1, Color.White, true, true, true, 11, 1, 12);
                // TALLAS
                setTallas(colinicio, tallas, canttallas);
                colinicio += canttallas;
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio] + "2", "Total", true, 1, Color.White, true, true, true, 11, 1, 12);
                colinicio += 1;

                // Pendiente Liquidación por talla (kg)
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio + (canttallas - 1)] + "1", "Pendiente Liquidación por talla (kg)", true, 1, Color.White, true, true, true, 11, 1, 12);
                // TALLAS
                setTallas(colinicio, tallas, canttallas);
                colinicio += canttallas;
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio] + "2", "Total", true, 1, Color.White, true, true, true, 11, 1, 12);
                colinicio += 1;

                // % Liquidación por talla (kg)
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio + (canttallas - 1)] + "1", "% Liquidación por talla(kg)", true, 1, Color.White, true, true, true, 11, 1, 12);
                // TALLAS
                setTallas(colinicio, tallas, canttallas);
                colinicio += canttallas;
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio] + "2", "Total", true, 1, Color.White, true, true, true, 11, 1, 12);
                colinicio += 1;


                // MERMA REAL
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio + 1] + "1", "Merma Real", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge(LETRAS[colinicio] + "2", "Recorte", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge(LETRAS[colinicio + 1] + "2", "Hilo", true, 1, Color.White, true, true, true, 11, 1, 12);

                colinicio += 2;

                // BALANCE GENERAL
                merge(LETRAS[colinicio] + "1:" + LETRAS[colinicio + 7] + "1", "Balance General", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge(LETRAS[colinicio] + "2", "Programado total (UN)", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge(LETRAS[colinicio + 1] + "2", "Liquidado total (UN)", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge(LETRAS[colinicio + 2] + "2", "Diferencia (UN)", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge(LETRAS[colinicio + 3] + "2", "Diferencia (%)", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge(LETRAS[colinicio + 4] + "2", "Programado total (Kg)", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge(LETRAS[colinicio + 5] + "2", "Liquidado total (Kg)", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge(LETRAS[colinicio + 6] + "2", "Diferencia (kg)", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge(LETRAS[colinicio + 7] + "2", "Diferencia (%)", true, 1, Color.White, true, true, true, 11, 1, 12);


                int filainicio = 3;
                // RECORREMOS VALORES
                foreach (var item in lista)
                {   

                    // DATOS GENERALES
                    cells(filainicio, 1, item.tipo, Color.White, Color.Black, false, true);
                    cells(filainicio, 2, item.usuariocrea , Color.White, Color.Black, false, true);
                    cells(filainicio, 3, item.fechamod, Color.White, Color.Black, false, true);
                    cells(filainicio, 4, item.ficha, Color.White, Color.Black, false, true);
                    cells(filainicio, 5, item.partida, Color.White, Color.Black, false, true);
                    cells(filainicio, 6, item.pedido, Color.White, Color.Black, false, true);
                    cells(filainicio, 7, item.combo, Color.White, Color.Black, false, true);
                    cells(filainicio, 8, item.estilotsc, Color.White, Color.Black, false, true);
                    cells(filainicio, 9, item.estilocliente, Color.White, Color.Black, false, true);

                    #region VARIABLES
                        int iniciocolumnadatos = 9;
                        decimal totalliquidado = 0;
                        decimal totalprogramado = 0;
                        decimal totalpendientesun = 0;
                        decimal totalporcentajeliquidaciontalla = 0;
                        decimal totalprogramadokg = 0;
                        decimal totalpesonetoreal = 0;
                        decimal totalpendienteliquidacionkg = 0;
                        decimal totalporcentajeliquidaciontallakg = 0;

                    #endregion

                    // CANTIDAD PROGRAMADA
                    foreach (var talla in tallas)
                    {
                        iniciocolumnadatos++;

                        var filtro = listadatos.Where(
                                obj => obj.ficha == item.ficha && obj.partida == item.partida
                                && obj.pedido == item.pedido && obj.talla == talla.talla
                        ).FirstOrDefault();

                        totalprogramado += filtro.programado;

                        cells(filainicio, iniciocolumnadatos, filtro.programado.ToString(), Color.White, Color.Black, false, true);

                    }

                    // LIQUIDADO
                    foreach (var talla in tallas)
                    {
                        iniciocolumnadatos++;

                        var filtro = listadatos.Where(
                                obj => obj.ficha == item.ficha && obj.partida == item.partida
                                && obj.pedido == item.pedido && obj.talla == talla.talla
                        ).FirstOrDefault();

                        totalliquidado += filtro.realprimera;

                        cells(filainicio, iniciocolumnadatos, filtro.realprimera.ToString(), Color.White, Color.Black, false, true);

                    }

                    // PENDIENTES
                    foreach (var talla in tallas)
                    {
                        iniciocolumnadatos++;

                        var filtro = listadatos.Where(
                                obj => obj.ficha == item.ficha && obj.partida == item.partida
                                && obj.pedido == item.pedido && obj.talla == talla.talla
                        ).FirstOrDefault();
                        totalpendientesun += filtro.pendiente;
                        cells(filainicio, iniciocolumnadatos, filtro.pendiente.ToString(), Color.White, Color.Black, false, true);

                    }

                    // TOTAL PENDIENTES
                    iniciocolumnadatos++;
                    cells(filainicio, iniciocolumnadatos, totalpendientesun.ToString(), Color.White, Color.Black, false, true);


                    // % LIQUIDACION POR TALLAS
                    foreach (var talla in tallas)
                    {
                        iniciocolumnadatos++;

                        var filtro = listadatos.Where(
                                obj => obj.ficha == item.ficha && obj.partida == item.partida
                                && obj.pedido == item.pedido && obj.talla == talla.talla
                        ).FirstOrDefault();
                        totalporcentajeliquidaciontalla += filtro.porcentajeliquidaciontalla;
                        cells(filainicio, iniciocolumnadatos, filtro.porcentajeliquidaciontalla.ToString(), Color.White, Color.Black, false, true,true);

                    }

                    // TOTAL LIQUIDACION POR TALLAS
                    iniciocolumnadatos++;
                    cells(filainicio, iniciocolumnadatos, (totalporcentajeliquidaciontalla / canttallas).ToString(), Color.White, Color.Black, false, true,true);

                    // PROGRAMADO KG
                    foreach (var talla in tallas)
                    {
                        iniciocolumnadatos++;

                        var filtro = listadatos.Where(
                                obj => obj.ficha == item.ficha && obj.partida == item.partida
                                && obj.pedido == item.pedido && obj.talla == talla.talla
                        ).FirstOrDefault();
                        totalprogramadokg += filtro.pesoprogramado;
                        cells(filainicio, iniciocolumnadatos, filtro.pesoprogramado.ToString(), Color.White, Color.Black, false, true);

                    }

                    // TOTAL LIQUIDACION POR TALLAS
                    iniciocolumnadatos++;
                    cells(filainicio, iniciocolumnadatos, totalprogramadokg.ToString(), Color.White, Color.Black, false, true);


                    // REAL KG
                    foreach (var talla in tallas)
                    {
                        iniciocolumnadatos++;

                        var filtro = listadatos.Where(
                                obj => obj.ficha == item.ficha && obj.partida == item.partida
                                && obj.pedido == item.pedido && obj.talla == talla.talla
                        ).FirstOrDefault();
                        totalpesonetoreal += filtro.pesonetoreal;
                        cells(filainicio, iniciocolumnadatos, filtro.pesonetoreal.ToString(), Color.White, Color.Black, false, true);

                    }

                    // TOTAL
                    iniciocolumnadatos++;
                    cells(filainicio, iniciocolumnadatos, totalpesonetoreal.ToString(), Color.White, Color.Black, false, true);

                    // PENDIENTE LIQUIDACION (KG)
                    foreach (var talla in tallas)
                    {
                        iniciocolumnadatos++;

                        var filtro = listadatos.Where(
                                obj => obj.ficha == item.ficha && obj.partida == item.partida
                                && obj.pedido == item.pedido && obj.talla == talla.talla
                        ).FirstOrDefault();
                        totalpendienteliquidacionkg += filtro.pendienteliquidacionkg;
                        cells(filainicio, iniciocolumnadatos, filtro.pendienteliquidacionkg.ToString(), Color.White, Color.Black, false, true);

                    }

                    // TOTAL PENDIENTE LIQUIDACION (KG)
                    iniciocolumnadatos++;
                    cells(filainicio, iniciocolumnadatos, totalpendienteliquidacionkg.ToString(), Color.White, Color.Black, false, true);


                    // % LIQUIDACION POR TALLAS (KG)
                    foreach (var talla in tallas)
                    {
                        iniciocolumnadatos++;

                        var filtro = listadatos.Where(
                                obj => obj.ficha == item.ficha && obj.partida == item.partida
                                && obj.pedido == item.pedido && obj.talla == talla.talla
                        ).FirstOrDefault();
                        totalporcentajeliquidaciontallakg += filtro.porcentajeliquidaciontallakg;
                        cells(filainicio, iniciocolumnadatos, filtro.porcentajeliquidaciontallakg.ToString(), Color.White, Color.Black, false, true,true);

                    }

                    // TOTAL % LIQUIDACION POR TALLAS (KG)
                    iniciocolumnadatos++;
                    cells(filainicio, iniciocolumnadatos, (totalporcentajeliquidaciontallakg / canttallas) .ToString(), Color.White, Color.Black, false, true,true);

                    // MERMA
                    var sumatotal = listadatos.Where(
                                       obj =>
                                          obj.partida == item.partida && obj.tipo == item.tipo
                                    ).Sum(s => s.realprimera);

                    decimal porcentajeficha = totalliquidado / sumatotal;

                    // MERMA RECORTE
                    item.mermarealrecorte = porcentajeficha * item.mermarecorte;
                    iniciocolumnadatos++;
                    cells(filainicio, iniciocolumnadatos, item.mermarealrecorte.ToString(), Color.White, Color.Black, false, true);

                    // MERMA HILOS
                    item.mermarealhilo = porcentajeficha * item.mermahilos;
                    iniciocolumnadatos++;
                    cells(filainicio, iniciocolumnadatos, item.mermahilos.ToString(), Color.White, Color.Black, false, true);

                    //iniciocolumnadatos++;

                    decimal difunidades = totalliquidado - totalprogramado;
                    decimal difkilos = totalpesonetoreal - totalprogramadokg;

                    cells(filainicio, iniciocolumnadatos + 1, totalprogramado.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, iniciocolumnadatos + 2, totalliquidado.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, iniciocolumnadatos + 3, difunidades.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, iniciocolumnadatos + 4, (totalprogramado > 0 ? difunidades / totalprogramado : 0).ToString() , Color.White, Color.Black, false, true,true);
                    cells(filainicio, iniciocolumnadatos + 5, totalprogramadokg.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, iniciocolumnadatos + 6, totalpesonetoreal.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, iniciocolumnadatos + 7, difkilos.ToString(), Color.White, Color.Black, false, true);
                    cells(filainicio, iniciocolumnadatos + 8, (totalprogramadokg > 0 ? difkilos / totalprogramadokg : 0).ToString(), Color.White, Color.Black, false, true,true);



                    filainicio++;

                }








                package.Save();
            }

            stream.Position = 0;
            return stream;

        }


        #region EXCELES

        private void setTallas(int colinicio, List<ReporteEntidad> tallas, int canttallas)
        {
            // ARMAMOS TALLAS
            for (int x = colinicio, index = 0; x < (colinicio + canttallas); x++)
            {
                merge(LETRAS[x] + "2", tallas[index].talla, true, 1, Color.White, true, true, true, 11, 1, 12);
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