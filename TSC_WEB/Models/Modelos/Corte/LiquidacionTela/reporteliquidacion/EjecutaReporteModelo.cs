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
        private const string formatoporcentaje = "0.00%";
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
        private void merge(string rango, string valor, bool merge, bool backcolor, Color fontcolor, bool negrita = true, bool bordes = true,bool autofit = false)
        {
            workSheet.Cells[rango].Merge = merge;
            workSheet.Cells[rango].Value = valor;

            workSheet.Cells[rango].Style.Fill.PatternType = ExcelFillStyle.Solid;
            // 
            //workSheet.Cells[rango].Style.WrapText = false;
            // AUTO FIT
            //if (autofit)
            //{
            //    workSheet.Cells[rango].AutoFitColumns();
            //}

            if (backcolor)
                workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 51, 63, 79);
            else
                workSheet.Cells[rango].Style.Fill.BackgroundColor.SetColor(1, 198, 89, 17);


            workSheet.Cells[rango].Style.Font.Color.SetColor(fontcolor);
            workSheet.Cells[rango].Style.Font.Bold = true;
            workSheet.Cells[rango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells[rango].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


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
                workSheet.Cells[fila, columna].Value = Math.Round(valor, decimales);
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
                merge("A1:A2", "Fecha de Liquidación", true, true, Color.White);
                merge("B1:B2", "Turno", true, true, Color.White);
                merge("C1:C2", "Célula", true, true, Color.White);
                merge("D1:D2", "Usuario", true, true, Color.White);
                merge("E1:E2", "Ficha", true, true, Color.White);
                merge("F1:F2", "Pedido", true, true, Color.White);
                merge("G1:G2", "Partida", true, true, Color.White);
                merge("H1:H2", "Combo", true, true, Color.White);
                merge("I1:I2", "Programa", true, true, Color.White);
                merge("J1:J2", "Estilo", true, true, Color.White);
                merge("K1:K2", "Color", true, true, Color.White);
                merge("L1:L2", "Código de Tela", true, true, Color.White);
                merge("M1:M2", "Descripción de Tela", true, true, Color.White);

                // BALANCE DE PRENDAS
                merge("N1:Q1", "Balance de Prendas", true, true, Color.White);
                merge("N2:N2", "Prendas Programadas por PCP", true, true, Color.White);
                merge("O2:O2", "Prendas Liquidadas", true, true, Color.White);
                merge("P2:P2", "Diferencia", true, true, Color.White);
                merge("Q2:Q2", "%", true, true, Color.White);

                // CONSUMO  POR PRENDA
                merge("R1:Y1", "Consumo por Prenda", true, true, Color.White);
                merge("R2:R2", "Consumo neto programado (kg)", true, true, Color.White);
                merge("S2:S2", "Consumo neto real (kg)", true, true, Color.White);
                merge("T2:T2", "Consumo bruto cotización", true, true, Color.White);
                merge("U2:U2", "Consumo bruto explosión", true, true, Color.White);
                merge("V2:V2", "Consumo bruto Prog (kg)", true, true, Color.White);
                merge("W2:W2", "Consumo bruto Tizados (kg)", true, true, Color.White);
                merge("X2:X2", "Consumo bruto real", true, true, Color.White);
                merge("Y2:Y2", "Diferencia (Consumo Bruto real  vs Bruto Programado)", true, true, Color.White);

                // KILOS TENDIDO
                merge("Z1:AA1", "Kilos Tendidos + Collaretas", true, true, Color.White);
                merge("Z2:Z2", "Tela Tendida Neta(kg)", true, true, Color.White);
                merge("AA2:AA2", "Tela de Collareta(kg)", true, true, Color.White);

                // MERMAS DE TENDIDO
                merge("AB1:AI1", "Mermas de Tendido", true, true, Color.White);
                merge("AB2:AB2", "Puntas(kg)", true, true, Color.White);
                merge("AC2:AC2", "Retazos(kg)", true, true, Color.White);
                merge("AD2:AD2", "Empalmes(kg)", true, true, Color.White);
                merge("AE2:AE2", "Conos(kg)", true, true, Color.White);
                merge("AF2:AF2", "Bolsas(kg)", true, true, Color.White);
                merge("AG2:AG2", "Total merma de tendido(kg)", true, true, Color.White);
                merge("AH2:AH2", "Merma de tendido programada %", true, true, Color.White);
                merge("AI2:AI2", "Merma de tendido real %", true, true, Color.White);

                // TELA ADICIONAL
                merge("AJ1:AK1", "Tela Adicional", true, true, Color.White);
                merge("AJ2:AJ2", "Tela Adicional (kg)", true, true, Color.White);
                merge("AK2:AK2", "Motivo Requerimiento", true, true, Color.White);

                // DEVOLUCION DE TELA
                merge("AL1:AQ1", "Devolución de tela", true, true, Color.White);
                merge("AL2:AL2", "Devolución de primeras (kg)", true, true, Color.White);
                merge("AM2:AM2", "Motivo", true, true, Color.White);
                merge("AN2:AN2", "Devolución de segundas (kg)", true, true, Color.White);
                merge("AO2:AO2", "Motivo", true, true, Color.White);
                merge("AP2:AP2", "Devolución ERP", true, true, Color.White);
                merge("AQ2:AQ2", "Código ERP", true, true, Color.White);

                // BALANCE DE TELA
                merge("AR1:AX1", "Balance de tela", true, true, Color.White);
                merge("AR2:AR2", "Tela Programada (kg)", true, true, Color.White);
                merge("AS2:AS2", "Tela Asignada por Tizado (kg)", true, true, Color.White);
                merge("AT2:AT2", "Tela Despachada (kg)", true, true, Color.White);
                merge("AU2:AU2", "Tela Despachada + Adicional (kg)", true, true, Color.White);
                merge("AV2:AV2", "Tela Liquidada (kg)", true, true, Color.White);
                merge("AW2:AW2", "Diferencia (kg)", true, true, Color.White);
                merge("AX2:AX2", "% Liquidación", true, true, Color.White);

                // MERMA DE CORTE
                merge("AY1:BB1", "Merma de Corte", true, true, Color.White);
                merge("AY2:AY2", "Entrecorte (kg)", true, true, Color.White);
                merge("AZ2:AZ2", "Orillos (kg)", true, true, Color.White);
                merge("BA2:BA2", "Extremos (kg)", true, true, Color.White);
                merge("BB2:BB2", "Total Merma Corte (kg)", true, true, Color.White);

                // EFICIENCIA DE TIZADO
                merge("BC1:BG1", "Eficiencia de tizado", true, true, Color.White);
                merge("BC2:BC2", "Eficiencia cotizada", true, true, Color.White);
                merge("BD2:BD2", "Eficiencia de explosión", true, true, Color.White);
                merge("BE2:BE2", "Eficiencia programada", true, true, Color.White);
                merge("BF2:BF2", "Eficiencia real", true, true, Color.White);
                merge("BG2:BG2", "Diferencia %", true, true, Color.White);

                // DATOS DE TELA
                merge("BH1:BV1", "Datos de tela", true, true, Color.White);
                merge("BH2:BH2", "Ancho Total Cotizado (m)", true, true, Color.White);
                merge("BI2:BI2", "Ancho Total de explosión (m)", true, true, Color.White);
                merge("BJ2:BJ2", "Ancho Total programado (m)", true, true, Color.White);
                merge("BK2:BK2", "Ancho Total real (m)", true, true, Color.White);
                merge("BL2:BL2", "Diferencia Ancho (m)", true, true, Color.White);
                merge("BM2:BM2", "% Variación Ancho", true, true, Color.White);
                merge("BN2:BN2", "Densidad de Cotización", true, true, Color.White);
                merge("BO2:BO2", "Densidad de Explosión", true, true, Color.White);
                merge("BP2:BP2", "Densidad Programada", true, true, Color.White);
                merge("BQ2:BQ2", "Densidad Real", true, true, Color.White);
                merge("BR2:BR2", "Variación de densidad (prog. vs real)", true, true, Color.White);
                merge("BS2:BS2", "Consumo lineal - Cotización", true, true, Color.White);
                merge("BT2:BT2", "Consumo lineal - Explosión", true, true, Color.White);
                merge("BU2:BU2", "Consumo lineal - Programado", true, true, Color.White);
                merge("BV2:BV2", "Consumo lineal - Real", true, true, Color.White);


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
                    cells(i, 18, item.consumonetoprog, Color.White, Color.Black, false, true,false,5);
                    cells(i, 19, item.consumonetoreal, Color.White, Color.Black, false, true,false,5);
                    cells(i, 20, item.consumobrutocotizacion, Color.White, Color.Black, false, true, false, 5);
                    cells(i, 21, item.consumobrutoexplosion, Color.White, Color.Black, false, true, false, 5);
                    cells(i, 22, item.consumobrutoprogramado, Color.White, Color.Black, false, true, false, 5);
                    cells(i, 23, item.consumobrutotizados, Color.White, Color.Black, false, true, false, 5);
                    cells(i, 24, item.consumobrutoreal, Color.White, Color.Black, false, true, false, 5);
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
                    cells(i, 36, item.adicional, Color.White, Color.Black, false, true, false, 2);
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
                    cells(i, 55, item.eficienciacotizada, Color.White, Color.Black, false, true,true);
                    cells(i, 56, item.eficienciaexplosion, Color.White, Color.Black, false, true,true);
                    cells(i, 57, item.eficienciaprogramadatizados, Color.White, Color.Black, false, true);
                    cells(i, 58, item.eficienciarealtizados, Color.White, Color.Black, false, true);
                    cells(i, 59, item.difefiprorealtizadospor, Color.White, Color.Black, false, true,true);

                    // DATOS DE TELA
                    cells(i, 60, item.anchototalcotizado, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 61, item.anchototalexplosion, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 62, item.anchototalprogramado, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 63, item.anchototalreal, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 64, item.difanchproreal, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 65, item.variacionancho, Color.White, Color.Black, false, true,true);
                    cells(i, 66, item.densidadcotizacion, Color.White, Color.Black, false, true, false, 3);
                    cells(i, 67, item.densidadexplosion, Color.White, Color.Black, false, true, false, 3);
                    cells(i, 68, item.densidadprogramada, Color.White, Color.Black, false, true, false, 3);
                    cells(i, 69, item.densidadreal, Color.White, Color.Black, false, true, false, 3);
                    cells(i, 70, item.variaciondedensidad, Color.White, Color.Black, false, true, true);
                    cells(i, 71, item.consumolinealcotizacion, Color.White, Color.Black, false, true, false, 5);
                    cells(i, 72, item.consumolinealexplosion, Color.White, Color.Black, false, true, false, 5);
                    cells(i, 73, item.consumolinealprogramado, Color.White, Color.Black, false, true, false, 5);
                    cells(i, 74, item.consumolinealreal, Color.White, Color.Black, false, true, false, 5);


                    i++;
                }

                //workSheet.Cells["A1:BV2"].AutoFitColumns();
                workSheet.Cells.AutoFitColumns();

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }

    }
}