using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela.reporteliquidacion;
using OfficeOpenXml;
using OfficeOpenXml.Style;


namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela.reporteliquidacion
{
    public class EjecutaReporteModelo
    {
        DBAccess conexion = new DBAccess();
        private const string formatoporcentaje  = "0.00%";
        private const string formatonumero      = "0.00";

        ExcelWorksheet workSheet;

        // GET REPORTE
        public List<datosReporteLiquidacionEntidad> getReporteLiquidacion(string fechainicio,string fechafin,int? ficha,int? turno,string partida,string programa,string cliente,string estadotendido,string estadocorte,int? combo)
        {

            List<datosReporteLiquidacionEntidad> objLista = new List<datosReporteLiquidacionEntidad>();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_REPORTELIQUIDACION_WEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_fechainicio", fechainicio == string.Empty ? null : fechainicio));
            comando.Parameters.Add(new OracleParameter("i_fechafin", fechafin == string.Empty ? null : fechafin));
            comando.Parameters.Add(new OracleParameter("i_ficha", ficha ));
            comando.Parameters.Add(new OracleParameter("i_turno", turno ));
            comando.Parameters.Add(new OracleParameter("i_partida", partida == string.Empty ? null : partida));
            comando.Parameters.Add(new OracleParameter("i_programa", programa == string.Empty ? null : programa));
            comando.Parameters.Add(new OracleParameter("i_cliente", cliente == string.Empty ? null : cliente));
            comando.Parameters.Add(new OracleParameter("i_estadotendido", estadotendido == string.Empty ? null : estadotendido));
            comando.Parameters.Add(new OracleParameter("i_estadocorte", estadocorte == string.Empty ? null : estadocorte));
            comando.Parameters.Add(new OracleParameter("i_combo", combo ));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                datosReporteLiquidacionEntidad obj = new datosReporteLiquidacionEntidad();
                string fecha    = Convert.ToDateTime(registros["fechaliquidacion"].ToString()).ToShortDateString();
                string hora     = Convert.ToDateTime(registros["fechaliquidacion"].ToString()).ToShortTimeString();

                obj.fechaliquidacion = fecha + " " + hora;
                obj.turno = Convert.ToInt32(registros["turno"].ToString());
                obj.celula = registros["celula"].ToString();
                obj.usuario = registros["usuario"].ToString();
                obj.ficha = Convert.ToInt32(registros["ficha"].ToString());
                obj.pedido = Convert.ToInt32(registros["pedido"].ToString());
                obj.partida = registros["partida"].ToString();
                obj.combo = Convert.ToInt32(registros["combo"].ToString());
                obj.version = Convert.ToInt32(registros["version"].ToString());
                obj.programa = registros["programa"].ToString().ToString();
                obj.estilo = registros["estilo"].ToString().ToString();
                obj.color = registros["color"].ToString().ToString();
                obj.codigotela = registros["codigotela"].ToString();
                obj.descripciontela = registros["descripciontela"].ToString();
                obj.prendasprogramadas = Convert.ToDouble(registros["prendasprogramado"].ToString());
                obj.prendasliquidadas = Convert.ToDouble(registros["prendasliquidadas"].ToString());
                obj.consumonetoreal = Convert.ToDouble(registros["consumoneto"].ToString());
                obj.consumobrutoprogramado = Convert.ToDouble(registros["consumoprogramado"].ToString());
                obj.consumobrutotizados = Convert.ToDouble(registros["consumotizado"].ToString());
                obj.consumobrutoreal = Convert.ToDouble(registros["consumopesobruto"].ToString());

                obj.telatendidakg = Convert.ToDouble(registros["telatendidaneta"].ToString());
                obj.telacollaretakg = Convert.ToDouble(registros["collareta"].ToString());
                obj.puntas = Convert.ToDouble(registros["puntas"].ToString());
                obj.retazos = Convert.ToDouble(registros["retazos"].ToString());
                obj.empalmes = Convert.ToDouble(registros["empalmes"].ToString());
                obj.conos = Convert.ToDouble(registros["conos"].ToString());
                obj.bolsas = Convert.ToDouble(registros["bolsas"].ToString());
                obj.telarealcombo = Convert.ToDouble(registros["telarealcombo"].ToString());
                obj.totalmermacombo = Convert.ToDouble(registros["totalmermacombo"].ToString());


                obj.telaadicional = Convert.ToDouble(registros["adicional"].ToString());
                obj.devolucionprimera = Convert.ToDouble(registros["devoprimera"].ToString());
                obj.devolucionsegunda = Convert.ToDouble(registros["devosegunda"].ToString());
                obj.telaprogramadakg = Convert.ToDouble(registros["kilosprogramado"].ToString());
                obj.telatizadakg = Convert.ToDouble(registros["kilostizado"].ToString());
                obj.teladespachadakg = Convert.ToDouble(registros["kilosdespachado"].ToString());
                obj.telaliquidada = Convert.ToDouble(registros["telaliquidada"].ToString());
                obj.entrecorte = Convert.ToDouble(registros["entrecorte"].ToString());
                obj.orillos = Convert.ToDouble(registros["orillos"].ToString());
                obj.extremos = Convert.ToDouble(registros["extremos"].ToString());

                obj.eficienciaprogramadatizados = Convert.ToDouble(registros["eficienciatizadosprogramado"].ToString());
                obj.eficienciarealtizados = Convert.ToDouble(registros["eficienciatizadosreal"].ToString());



                obj.anchototalprogramado = Convert.ToDouble(registros["anchotelaprogramado"].ToString());
                obj.anchototalreal = Convert.ToDouble(registros["anchotelareal"].ToString());
                obj.densidadprogramada = Convert.ToDouble(registros["densidadprogramado"].ToString());
                obj.densidadreal = Convert.ToDouble(registros["densidadreal"].ToString());

                // DATOS EXTRAS
                obj.consumobrutocotizacion = Convert.ToDouble(registros["consumobrutocotizacion"].ToString());
                obj.consumobrutoexplosion = Convert.ToDouble(registros["consumobrutoexplosion"].ToString());
                obj.motivoadicional = registros["motivoadicional"].ToString();
                obj.motivoprimera = registros["motivoprimera"].ToString();
                obj.motivosegunda = registros["motivosegunda"].ToString();
                obj.devolucionerp = Convert.ToDouble(registros["devolucionerp"].ToString());
                obj.codigoerp = registros["codigoerp"].ToString();
                obj.eficienciacotizada = Convert.ToDouble(registros["eficienciacotizada"].ToString());
                obj.eficienciaexplosion = Convert.ToDouble(registros["eficienciaexplosion"].ToString());
                obj.anchototalcotizado = Convert.ToDouble(registros["anchototalcotizado"].ToString());
                obj.anchototalexplosion = Convert.ToDouble(registros["anchototalexplosion"].ToString());
                obj.densidadcotizacion = Convert.ToDouble(registros["densidadcotizacion"].ToString());
                obj.densidadexplosion = Convert.ToDouble(registros["densidadexplosion"].ToString());
                obj.consumolinealcotizacion = Convert.ToDouble(registros["consumolinealcotizacion"].ToString());
                obj.consumolinealexplosion = Convert.ToDouble(registros["consumolinealexplosion"].ToString());
                obj.consumolinealprogramado = Convert.ToDouble(registros["consumolinealprogramado"].ToString());
                obj.consumolinealreal = Convert.ToDouble(registros["consumolinealreal"].ToString());


                objLista.Add(obj);

            }

            conexion.Desconectar();

            return objLista;
        }


        //  CREAR REPORTE
        private void merge(string rango, string valor, bool merge, int backcolor, Color fontcolor, bool negrita = true, bool bordes = true,bool wraptext = false,int tamanoletra = 11,int columna = 0,double widthcolumna = 0)
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

                //if (decimales == 2)
                //{
                //    ExcelRange rango = workSheet.SelectedRange[fila, columna];
                //    rango.Style.Numberformat.Format = formatonumero;
                //    workSheet.Cells[fila, columna].Value = valor;


                //}
                //else
                //{
                //    workSheet.Cells[fila, columna].Value = Math.Round(valor, decimales);
                //}

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



        // EXPORTAR EXCEL
        public MemoryStream getReporteExcel(List<datosReporteLiquidacionEntidad> listadatos)
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                workSheet = package.Workbook.Worksheets.Add("Reporte Liquidación");

                // PRINCIPALES
                merge("A1:A2", "Fecha de Liquidación", true, 1, Color.White, true, true, true, 11, 1, 12);
                merge("B1:B2", "Turno", true, 1, Color.White, true, true, true, 11, 2, 8.43);
                merge("C1:C2", "Célula", true, 1, Color.White, true, true, true, 11, 3, 8.43);
                merge("D1:D2", "Usuario", true, 1, Color.White, true, true, true, 11, 4, 9.29);
                merge("E1:E2", "Ficha", true, 1, Color.White, true, true, true, 11, 5, 8.43);
                merge("F1:F2", "Pedido", true, 1, Color.White, true, true, true, 11, 6, 8.43);
                merge("G1:G2", "Partida", true, 1, Color.White, true, true, true, 11, 7, 8.43);
                merge("H1:H2", "Combo", true, 1, Color.White, true, true, true, 11, 8, 8.43);
                merge("I1:I2", "Programa", true, 1, Color.White, true, true, true, 11, 9, 8.43);
                merge("J1:J2", "Estilo", true, 1, Color.White, true, true, true, 11, 10, 8.43);
                merge("K1:K2", "Color", true, 1, Color.White, true, true, true, 11, 11, 8.43);
                merge("L1:L2", "Código de Tela", true, 1, Color.White, true, true, true, 11, 12, 13.71);
                merge("M1:M2", "Descripción de Tela", true, 1, Color.White, true, true, true, 11, 13, 20.86);

                // BALANCE DE PRENDAS
                merge("N1:Q1", "Balance de Prendas", true, 2, Color.White);
                merge("N2:N2", "Prendas Programadas por PCP", true, 2, Color.White,true,true,true,9,14,15.43);
                merge("O2:O2", "Prendas Liquidadas", true, 2, Color.White,true,true,true,9,15,9.86);
                merge("P2:P2", "Diferencia", true, 2, Color.White, true, true, true, 9, 16, 7.86);
                merge("Q2:Q2", "%", true, 2, Color.White, true, true, true, 9, 17, 6.86);

                // CONSUMO  POR PRENDA
                merge("R1:Y1", "Consumo por Prenda", true, 1, Color.White);
                merge("R2:R2", "Consumo neto programado (kg)", true, 1, Color.White, true, true, true, 9, 18, 13.29);
                merge("S2:S2", "Consumo neto real (kg)", true, 1, Color.White, true, true, true, 9, 19, 10.86);
                merge("T2:T2", "Consumo bruto cotización", true, 1, Color.White, true, true, true, 9, 20, 12.71);
                merge("U2:U2", "Consumo bruto explosión", true, 1, Color.White, true, true, true, 9, 21, 14);
                merge("V2:V2", "Consumo bruto Prog (kg)", true, 1, Color.White, true, true, true, 9, 22, 14);
                merge("W2:W2", "Consumo bruto Tizados (kg)", true, 1, Color.White, true, true, true, 9, 23, 11.71);
                merge("X2:X2", "Consumo bruto real", true, 1, Color.White, true, true, true, 9, 24, 10.14);
                merge("Y2:Y2", "Diferencia (Consumo Bruto real  vs Bruto Programado)", true, 1, Color.White, true, true, true, 9, 25, 21.71);

                // KILOS TENDIDO
                merge("Z1:AA1", "Kilos Tendidos + Collaretas", true, 3, Color.White);
                merge("Z2:Z2", "Tela Tendida Neta(kg)", true, 3, Color.White, true, true, true, 9, 26, 12.43);
                merge("AA2:AA2", "Tela de Collareta(kg)", true, 3, Color.White, true, true, true, 9, 27, 11.29);

                // MERMAS DE TENDIDO
                merge("AB1:AI1", "Mermas de Tendido", true, 1, Color.White);
                merge("AB2:AB2", "Puntas(kg)", true, 1, Color.White, true, true, true, 9, 28, 8.71);
                merge("AC2:AC2", "Retazos(kg)", true, 1, Color.White, true, true, true, 9, 29, 10);
                merge("AD2:AD2", "Empalmes(kg)", true, 1, Color.White, true, true, true, 9, 30, 10.71);
                merge("AE2:AE2", "Conos(kg)", true, 1, Color.White, true, true, true, 9, 31, 9.14);
                merge("AF2:AF2", "Bolsas(kg)", true, 1, Color.White, true, true, true, 9, 32, 8.86);
                merge("AG2:AG2", "Total merma de tendido(kg)", true, 1, Color.White, true, true, true, 9, 33, 11.86);
                merge("AH2:AH2", "Merma de tendido programada %", true, 1, Color.White, true, true, true, 9, 34, 15.71);
                merge("AI2:AI2", "Merma de tendido real %", true, 1, Color.White, true, true, true, 9, 35, 11.71);

                // TELA ADICIONAL
                merge("AJ1:AK1", "Tela Adicional", true, 4, Color.White);
                merge("AJ2:AJ2", "Tela Adicional (kg)", true, 4, Color.White, true, true, true, 9, 36, 11.14);
                merge("AK2:AK2", "Motivo Requerimiento", true, 4, Color.White, true, true, true, 9, 37, 12.57);

                // DEVOLUCION DE TELA
                merge("AL1:AQ1", "Devolución de tela", true, 1, Color.White);
                merge("AL2:AL2", "Devolución de primeras (kg)", true, 1, Color.White, true, true, true, 9, 38, 12.14);
                merge("AM2:AM2", "Motivo", true, 1, Color.White, true, true, true, 9, 39, 11.43);
                merge("AN2:AN2", "Devolución de segundas (kg)", true, 1, Color.White, true, true, true, 9, 40, 10.71);
                merge("AO2:AO2", "Motivo", true, 1, Color.White, true, true, true, 9, 41, 6.71);
                merge("AP2:AP2", "Devolución ERP", true, 1, Color.White, true, true, true, 9, 42, 11);
                merge("AQ2:AQ2", "Código ERP", true, 1, Color.White, true, true, true, 9, 43, 10.14);

                // BALANCE DE TELA
                merge("AR1:AX1", "Balance de tela", true, 5, Color.White);
                merge("AR2:AR2", "Tela Programada (kg)", true, 5, Color.White, true, true, true, 9, 44, 13.29);
                merge("AS2:AS2", "Tela Asignada por Tizado (kg)", true, 5, Color.White, true, true, true, 9, 45, 13.43);
                merge("AT2:AT2", "Tela Despachada (kg)", true, 5, Color.White, true, true, true, 9, 46, 13.14);
                merge("AU2:AU2", "Tela Despachada + Adicional (kg)", true, 5, Color.White, true, true, true, 9, 47, 16.86);
                merge("AV2:AV2", "Tela Liquidada (kg)", true, 5, Color.White, true, true, true, 9, 48, 11.14);
                merge("AW2:AW2", "Diferencia (kg)", true, 5, Color.White, true, true, true, 9, 49, 12);
                merge("AX2:AX2", "% Liquidación", true, 5, Color.White, true, true, true, 9, 50, 10.29);

                // MERMA DE CORTE
                merge("AY1:BB1", "Merma de Corte", true, 1, Color.White);
                merge("AY2:AY2", "Entrecorte (kg)", true, 1, Color.White, true, true, true, 9, 51, 8.43);
                merge("AZ2:AZ2", "Orillos (kg)", true, 1, Color.White, true, true, true, 9, 52, 8.14);
                merge("BA2:BA2", "Extremos (kg)", true, 1, Color.White, true, true, true, 9, 53, 9.57);
                merge("BB2:BB2", "Total Merma Corte (kg)", true, 1, Color.White, true, true, true, 9, 54, 11.71);

                // EFICIENCIA DE TIZADO
                merge("BC1:BG1", "Eficiencia de tizado", true, 6, Color.White);
                merge("BC2:BC2", "Eficiencia cotizada", true, 6, Color.White, true, true, true, 9, 55, 9.86);
                merge("BD2:BD2", "Eficiencia de explosión", true, 6, Color.White, true, true, true, 9, 56, 13);
                merge("BE2:BE2", "Eficiencia programada", true, 6, Color.White, true, true, true, 9, 57, 10.86);
                merge("BF2:BF2", "Eficiencia real", true, 6, Color.White, true, true, true, 9, 58, 8.71);
                merge("BG2:BG2", "Diferencia %", true, 6, Color.White, true, true, true, 9, 59, 8.29);

                // DATOS DE TELA
                merge("BH1:BR1", "Datos de tela", true, 1, Color.White);
                merge("BH2:BH2", "Ancho Total Cotizado (m)", true, 1, Color.White, true, true, true, 9, 60, 11.86);
                merge("BI2:BI2", "Ancho Total de explosión (m)", true, 1, Color.White, true, true, true, 9, 61, 11.86);
                merge("BJ2:BJ2", "Ancho Total programado (m)", true, 1, Color.White, true, true, true, 9, 62, 11.86);
                merge("BK2:BK2", "Ancho Total real (m)", true, 1, Color.White, true, true, true, 9, 63, 11.86);
                merge("BL2:BL2", "Diferencia Ancho (m)", true, 1, Color.White, true, true, true, 9, 64, 11.86);
                merge("BM2:BM2", "% Variación Ancho", true, 1, Color.White, true, true, true, 9, 65, 11.86);
                merge("BN2:BN2", "Densidad de Cotización", true, 1, Color.White, true, true, true, 9, 66, 11.14);
                merge("BO2:BO2", "Densidad de Explosión", true, 1, Color.White, true, true, true, 9, 67, 9.57);
                merge("BP2:BP2", "Densidad Programada", true, 1, Color.White, true, true, true, 9, 68, 11.14);
                merge("BQ2:BQ2", "Densidad Real", true, 1, Color.White, true, true, true, 9, 69, 8.29);
                merge("BR2:BR2", "Variación de densidad (prog. vs real)", true, 1, Color.White, true, true, true, 9, 70, 15.71);
                //merge("BS2:BS2", "Consumo lineal - Cotización", true, 1, Color.White, true, true, true, 9, 71, 13.71);
                //merge("BT2:BT2", "Consumo lineal - Explosión", true, 1, Color.White, true, true, true, 9, 72, 13.71);
                //merge("BU2:BU2", "Consumo lineal - Programado", true, 1, Color.White, true, true, true, 9, 73, 13.71);
                //merge("BV2:BV2", "Consumo lineal - Real", true, 1, Color.White, true, true, true, 9, 74, 13.71);

                // ALTURA 
                workSheet.Row(2).Height = 24.75;


                int i = 3;
                // ASIGNAMOS DATOS
                foreach (var item in listadatos)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    // DATOS GENERALES
                    cells(i, 1, item.fechaliquidacion, Color.White, Color.Black, false, true);
                    cells(i, 2, item.turno, Color.White, Color.Black, false, true);
                    cells(i, 3, item.celula, Color.White, Color.Black, false, true);
                    cells(i, 4, item.usuario, Color.White, Color.Black, false, true);
                    cells(i, 5, item.ficha.ToString(), Color.White, Color.Black, false, true);
                    cells(i, 6, item.pedido.ToString(), Color.White, Color.Black, false, true);
                    cells(i, 7, item.partida, Color.White, Color.Black, false, true);
                    cells(i, 8, item.combo, Color.White, Color.Black, false, true);
                    cells(i, 9, item.programa, Color.White, Color.Black, false, true);
                    cells(i, 10, item.estilo, Color.White, Color.Black, false, true);
                    cells(i, 11, item.color, Color.White, Color.Black, false, true);
                    cells(i, 12, item.codigotela, Color.White, Color.Black, false, true);
                    cells(i, 13, item.descripciontela, Color.White, Color.Black, false, true);

                    // BALANCE DE PRENDAS
                    cells(i, 14, item.prendasprogramadas, Color.White, Color.Black, false, true);
                    cells(i, 15, item.prendasliquidadas, Color.White, Color.Black, false, true);
                    cells(i, 16, item.difprendasprog, Color.White, Color.Black, false, true);
                    cells(i, 17, item.difprendasprogporc, Color.White, Color.Black, false, true,true);

                    // CONSUMO POR PRENDAS
                    cells(i, 18, item.consumonetoprog, Color.White, Color.Black, false, true,false,4);
                    cells(i, 19, item.consumonetoreal, Color.White, Color.Black, false, true,false,4);
                    cells(i, 20, item.consumobrutocotizacion, Color.White, Color.Black, false, true, false, 4);
                    cells(i, 21, item.consumobrutoexplosion, Color.White, Color.Black, false, true, false, 4);
                    cells(i, 22, item.consumobrutoprogramado, Color.White, Color.Black, false, true, false, 4);
                    cells(i, 23, item.consumobrutotizados, Color.White, Color.Black, false, true, false, 4);
                    cells(i, 24, item.consumobrutoreal, Color.White, Color.Black, false, true, false, 4);
                    cells(i, 25, item.difconsbrutorealprogpor, Color.White, Color.Black, false, true,true);

                    // KILOS TENDIDOS
                    cells(i, 26, item.telatendidakg, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 27, item.telacollaretakg, Color.White, Color.Black, false, true, false, 2);

                    // MERMAS DE TENDIDO
                    cells(i, 28, item.puntas, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 29, item.retazos, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 30, item.empalmes, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 31, item.conos, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 32, item.bolsas, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 33, item.totalmermatendido, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 34, item.totalmermatendidoprogpor, Color.White, Color.Black, false, true,true);
                    cells(i, 35, item.totalmermatendidorealpor, Color.White, Color.Black, false, true, true);

                    // TELA ADICIONAL
                    cells(i, 36, item.telaadicional, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 37, item.motivoadicional, Color.White, Color.Black, false, true);

                    // DEVOLUCION DE TELA
                    cells(i, 38, item.devolucionprimera, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 39, item.motivoprimera, Color.White, Color.Black, false, true);
                    cells(i, 40, item.devolucionsegunda, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 41, item.motivosegunda, Color.White, Color.Black, false, true);
                    cells(i, 42, item.devolucionerp, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 43, item.codigoerp, Color.White, Color.Black, false, true);

                    // BALANCE DE TELA
                    cells(i, 44, item.telaprogramadakg, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 45, item.telatizadakg, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 46, item.teladespachadakg, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 47, item.teladespachadaadicional, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 48, item.telaliquidada, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 49, item.difteladespadictelaliqui, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 50, item.porcentajeliquidacion, Color.White, Color.Black, false, true,true);

                    // MERMAS DE CORTE
                    cells(i, 51, item.entrecorte, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 52, item.orillos, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 53, item.extremos, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 54, item.totalmermacorte, Color.White, Color.Black, false, true, false, 2);
                
                    // EFICIENCIA DE TIZADO
                    cells(i, 55, item.eficienciacotizada_new, Color.White, Color.Black, false, true,true);
                    cells(i, 56, item.eficienciaexplosion, Color.White, Color.Black, false, true,true);
                    cells(i, 57, item.eficienciaprogramadatizados, Color.White, Color.Black, false, true,true);
                    cells(i, 58, item.eficienciarealtizados, Color.White, Color.Black, false, true,true);
                    cells(i, 59, item.difefiprorealtizadospor, Color.White, Color.Black, false, true,true);

                    // DATOS DE TELA
                    cells(i, 60, item.anchototalcotizado, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 61, item.anchototalexplosion, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 62, item.anchototalprogramado, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 63, item.anchototalreal, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 64, item.difanchproreal, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 65, item.variacionancho, Color.White, Color.Black, false, true,true);
                    cells(i, 66, item.densidadcotizacion_gramos, Color.White, Color.Black, false, true, false, 3);
                    cells(i, 67, item.densidadexplosion_gramos, Color.White, Color.Black, false, true, false, 3);
                    cells(i, 68, item.densidadprogramada, Color.White, Color.Black, false, true, false, 3);
                    cells(i, 69, item.densidadreal, Color.White, Color.Black, false, true, false, 3);
                    cells(i, 70, item.variaciondedensidad, Color.White, Color.Black, false, true, true);
                    //cells(i, 71, item.consumolinealcotizacion, Color.White, Color.Black, false, true, false, 4);
                    //cells(i, 72, item.consumolinealexplosion, Color.White, Color.Black, false, true, false, 4);
                    //cells(i, 73, item.consumolinealprogramado, Color.White, Color.Black, false, true, false, 4);
                    //cells(i, 74, item.consumolinealreal, Color.White, Color.Black, false, true, false, 4);


                    i++;
                }

                //workSheet.Cells["A1:BV2"].AutoFitColumns();
                //workSheet.Cells.AutoFitColumns();

                package.Save();
            }

            stream.Position = 0;
            return stream;
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


    }
}