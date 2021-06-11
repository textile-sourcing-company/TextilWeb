using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela;
using Dapper;
using Dapper.Oracle;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela
{
    public class ReporteLiquidacionModelo
    {
        private const string formatoporcentaje = "0.00%";


        MemoryStream stream;
        ExcelPackage package;
        ExcelWorksheet workSheet;
        DBAccess conexion = new DBAccess();


        // OBTENEMOS REPORTE DE CORTE LIQUIDACION
        public List<ReporteLiquidacionEntidad> getReporteLiquidacion(string i_fechai, string i_fechaf, string i_partida, string i_turno, string i_programa, string i_cliente, int? i_ficha,int? i_combo,string i_estadotendido,string i_estadocorte)
        {
            List<ReporteLiquidacionEntidad> objLista = new List<ReporteLiquidacionEntidad>();

            OracleCommand comando = new OracleCommand("systextilrpt.SPU_LIQUIDACION_REPORTE_NEW", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_fechai", i_fechai == string.Empty ? null : i_fechai));
            comando.Parameters.Add(new OracleParameter("i_fechaf", i_fechaf == string.Empty ? null : i_fechaf));

            comando.Parameters.Add(new OracleParameter("i_ficha", i_ficha == 0 ? null : i_ficha));
            comando.Parameters.Add(new OracleParameter("i_turno", i_turno == string.Empty ? null : i_turno));
            comando.Parameters.Add(new OracleParameter("i_partida", i_partida == string.Empty ? null : i_partida));
            comando.Parameters.Add(new OracleParameter("i_programa", i_programa == string.Empty ? null : i_programa));
            comando.Parameters.Add(new OracleParameter("i_cliente", i_cliente == string.Empty ? null : i_cliente));

            comando.Parameters.Add(new OracleParameter("i_combo", i_combo == 0 ? null : i_combo));
            comando.Parameters.Add(new OracleParameter("i_estadotendido", i_estadotendido == string.Empty ? null : i_estadotendido));
            comando.Parameters.Add(new OracleParameter("i_estadocorte", i_estadocorte == string.Empty ? null : i_estadocorte));



            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ReporteLiquidacionEntidad objeto = new ReporteLiquidacionEntidad();

                        objeto.fechaliquidacion = Convert.ToDateTime(registros["fechaliquidacion"].ToString()).ToShortDateString();
                        objeto.fichas = Convert.ToInt32(registros["fichas"].ToString());
                        objeto.pedido = Convert.ToInt32(registros["pedido"].ToString());
                        objeto.partida = Convert.ToString(registros["partida"].ToString());
                        objeto.programa = Convert.ToString(registros["programa"].ToString());
                        objeto.estilo = Convert.ToString(registros["estilo"].ToString());
                        objeto.color = Convert.ToString(registros["color"].ToString());
                        objeto.codigotela = Convert.ToString(registros["codigotela"].ToString());
                        objeto.descripciontela = Convert.ToString(registros["descripciontela"].ToString());
                        objeto.prendasprogramadas = Convert.ToDouble(registros["prendasprogramadas"].ToString());
                        objeto.prendasreales = Convert.ToDouble(registros["prendasreales"].ToString());
                        objeto.diferencia1 = Convert.ToDouble(registros["diferencia1"].ToString());
                        objeto.kilosprogramados = Convert.ToDouble(registros["kilosprogramados"].ToString());
                        objeto.kilostizados = Convert.ToDouble(registros["kilostizados"].ToString());
                        objeto.kilosreales = Convert.ToDouble(registros["kilosreales"].ToString());
                        objeto.largotizado = Convert.ToDouble(registros["largotizado"].ToString());
                        objeto.largopanoreal = Convert.ToDouble(registros["largopanoreal"].ToString());
                        objeto.tallas = Convert.ToDouble(registros["tallas"].ToString());
                        objeto.panos = Convert.ToDouble(registros["panos"].ToString());
                        objeto.pesopanoprogramado = Convert.ToDouble(registros["pesopanoprogramado"].ToString());
                        objeto.pesopanoreal = Convert.ToDouble(registros["pesopanoreal"].ToString());
                        objeto.consnetoprogramado = Convert.ToDouble(registros["consnetoprogramado"].ToString());
                        objeto.consnetoreal = Convert.ToDouble(registros["consnetoreal"].ToString());
                        objeto.mermatendprog = Convert.ToDouble(registros["mermatendprog"].ToString());
                        objeto.mermatendreal = Convert.ToDouble(registros["mermatendreal"].ToString());
                        objeto.consbrutprog = Convert.ToDouble(registros["consbrutprog"].ToString());
                        objeto.consbrutoreal = Convert.ToDouble(registros["consbrutoreal"].ToString());
                        objeto.diferencia2 = Convert.ToDouble(registros["diferencia2"].ToString());
                        objeto.eficprog = Convert.ToDouble(registros["eficprog"].ToString());
                        objeto.eficreal = Convert.ToDouble(registros["eficreal"].ToString());
                        objeto.diferencia3 = Convert.ToDouble(registros["diferencia3"].ToString());
                        objeto.puntas = Convert.ToDouble(registros["puntas"].ToString());
                        objeto.retazos = Convert.ToDouble(registros["retazos"].ToString());
                        objeto.empalmes = Convert.ToDouble(registros["empalmes"].ToString());
                        objeto.mermaprog = Convert.ToDouble(registros["mermaprog"].ToString());
                        objeto.mermareal = Convert.ToDouble(registros["mermareal"].ToString());
                        objeto.entrecorte = Convert.ToDouble(registros["entrecorte"].ToString());
                        objeto.conos = Convert.ToDouble(registros["conos"].ToString());
                        objeto.bolsas = Convert.ToDouble(registros["bolsas"].ToString());
                        objeto.dev1ra = Convert.ToDouble(registros["dev1ra"].ToString());
                        objeto.dev2da = Convert.ToDouble(registros["dev2da"].ToString());
                        objeto.deverp = registros["deverp"].ToString() == "" ? 0 : Convert.ToDouble(registros["deverp"].ToString());
                        objeto.coderp = Convert.ToString(registros["coderp"].ToString());
                        objeto.anchprog = Convert.ToDouble(registros["anchprog"].ToString());
                        objeto.anchreal = Convert.ToDouble(registros["anchreal"].ToString());
                        objeto.direfencia4 = Convert.ToDouble(registros["direfencia4"].ToString());
                        objeto.anchotizadoutil = Convert.ToDouble(registros["anchotizadoutil"].ToString());
                        objeto.diferenciametros = Convert.ToDouble(registros["diferenciametros"].ToString());
                        objeto.diferenciametrosp = Convert.ToDouble(registros["diferenciametrosp"].ToString());
                        objeto.densprog = Convert.ToDouble(registros["densprog"].ToString());
                        objeto.diferencia5 = Convert.ToDouble(registros["diferencia5"].ToString());
                        objeto.cuadretela = Convert.ToDouble(registros["cuadretela"].ToString());
                        objeto.diferencia6 = Convert.ToDouble(registros["diferencia6"].ToString());
                        objeto.adicional = Convert.ToDouble(registros["adicional"].ToString());
                        objeto.collareta = Convert.ToDouble(registros["collareta"].ToString());

                        objeto.realesliquidados = Convert.ToDouble(registros["realesliquidados"].ToString());
                        objeto.kilosdespachados = Convert.ToDouble(registros["despachado"].ToString());

                        objeto.consumoponderado = Convert.ToDouble(registros["consumoponderado"].ToString());


                        objeto.estadotendido = registros["estadotendido"].ToString();
                        objeto.estadocorte = registros["estadocorte"].ToString();
                        objeto.dev1ramotivo = registros["motdevolucionprimera"].ToString();
                        objeto.dev2damotivo = registros["motdevolucionsegunda"].ToString();

                        objeto.motadicional = registros["motadicional"].ToString();

                        objeto.version = registros["version"].ToString();
                        objeto.combo = registros["combo"].ToString();

                        objeto.usuario = registros["usuario"].ToString();
                        objeto.celula = registros["celula"].ToString();
                        objeto.turno = registros["turno"].ToString();


                objLista.Add(objeto);
                
            }

            conexion.Desconectar();

            return objLista;

        }

        //  CREAR REPORTE
        private void merge(string rango, string valor, bool merge, bool backcolor, Color fontcolor, bool negrita = true, bool bordes = true)
        {
            workSheet.Cells[rango].Merge = merge;
            workSheet.Cells[rango].Value = valor;

            workSheet.Cells[rango].Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheet.Cells[rango].Style.WrapText = true;

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

        private void cells(int fila,int columna, string valor, Color backcolor, Color fontcolor, bool negrita = true, bool bordes = true,bool porcentaje = false)
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

        private void cells(int fila, int columna, double valor, Color backcolor, Color fontcolor, bool negrita = true, bool bordes = true,bool porcentaje = false,int decimales = 0)
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


        // EXPORTAMOS
        public MemoryStream ReporteExcel(List<ReporteLiquidacionEntidad> listado)
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                workSheet = package.Workbook.Worksheets.Add("Reporte Liquidación");

                // PRINCIPALES
                merge("A1:A2", "Fecha de Liquidación",true, true, Color.White);
                merge("B1:B2", "Turno",true, true, Color.White);
                merge("C1:C2", "Célula",true, true, Color.White);
                merge("D1:D2", "Usuario",true, true, Color.White);
                merge("E1:E2", "Ficha",true,true,Color.White);
                merge("F1:F2", "Pedido",true,true,Color.White);
                merge("G1:G2", "Partida",true,true,Color.White);
                merge("H1:H2", "Combo", true, true, Color.White);
                merge("I1:I2", "Programa",true,true,Color.White);
                merge("J1:J2", "Estilo",true,true,Color.White);
                merge("K1:K2", "Color",true,true,Color.White);
                merge("L1:L2", "Código de Tela",true,true,Color.White);
                merge("M1:M2", "Descripción de Tela",true,true,Color.White);


                // BALANCE DE PRENDAS
                merge("N1:Q1", "Balance de Prendas",true,true,Color.White);
                merge("N2:N2", "Prendas Programadas por PCP",true,true,Color.White);
                merge("O2:O2", "Prendas Liquidadas",true,true,Color.White);
                merge("P2:P2", "Diferencia",true,true,Color.White);
                merge("Q2:Q2", "%",true,true,Color.White);
             

                // CONSUMO  POR PRENDA
                merge("R1:W1", "Consumo por Prenda",true,true,Color.White);
                merge("R2:R2", "Consumo neto programado (kg)",true,true,Color.White);
                merge("S2:S2", "Consumo neto real (kg)", true,true,Color.White);
                merge("T2:T2", "Consumo bruto según tizado (kg)", true,true,Color.White);
                merge("U2:U2", "Consumo según tizado(Ponderado)",true,true,Color.White);
                merge("V2:V2", "Consumo bruto real",true,true,Color.White);
                merge("W2:W2", "Diferencia (Consumo Bruto real  vs Bruto Programado)", true,true,Color.White);
                
                //KILOS TENDIDO + COLLARETAS
                merge("X1:Y1", "Kilos Tendidos + Collaretas",true,true,Color.White);
                merge("X2:X2" , "Tela Tendida (kg)",true,true,Color.White);
                merge("Y2:Y2", "Tela de collareta(kg)",true,true,Color.White);

                // MERMAS DE TENDIDO
                merge("Z1:AG1", "Mermas de tendido",true,true,Color.White);
                merge("Z2:Z2", "Puntas (kg)",true,true,Color.White);
                merge("AA2:AA2", "Retazos (kg)", true,true,Color.White);
                merge("AB2:AB2", "Empalmes (kg)", true,true,Color.White);
                merge("AC2:AC2", "Conos", true,true,Color.White);
                merge("AD2:AD2", "Bolsas",true,true,Color.White);
                merge("AE2:AE2", "Total Merma de tendido (kg)",true,true,Color.White);
                merge("AF2:AF2", "Merma de tendido programada (kg)", true,true,Color.White);
                merge("AG2:AG2", "Merma de tendido real (kg)",true,true,Color.White);

                // TELA ADICIONAL
                merge("AH1:AI1", "Tela Adicional",true,true,Color.White);
                merge("AH2:AH2", "Tela Adicional(kg)",true,true,Color.White);
                merge("AI2:AI2", "Motivo Requerimiento",true,true,Color.White);

                //DEVOLUCION DE TELA
                merge("AJ1:AO1", "Devolución de tela",true,true,Color.White);
                merge("AJ2:AJ2", "Devolución de primeras (kg)",true,true,Color.White);
                merge("AK2:AK2", "Motivo", true, true, Color.White);
                merge("AL2:AL2", "Devolución de segundas (kg)",true,true,Color.White);
                merge("AM2:AM2", "Motivo",true,true,Color.White);
                merge("AN2:AN2", "Devolución ERP",true,true,Color.White);
                merge("AO2:AO2", "Código ERP",true,true,Color.White);

                // BALANCE DE TELA
                merge("AP1:AV1", "Balance de tela",true,false,Color.White);
                merge("AP2:AP2", "Tela Programada (kg)",true,false, Color.White);
                merge("AQ2:AQ2", "Tela Asignada por Tizado (kg)",true,false, Color.White);
                merge("AR2:AR2", "Tela Despachada (kg)",true,false, Color.White);
                merge("AS2:AS2", "Tela Despachada + Adicional (kg)", true,false, Color.White);
                merge("AT2:AT2", "Tela Liquidada",true,false, Color.White); // RAA
                merge("AU2:AU2", "Diferencia (kg)",true,false, Color.White);
                merge("AV2:AV2", "% Liquidación", true, false, Color.White);
            
                //MERMA DE CORTE
                merge("AW1:BB1", "Merma de corte",true,true,Color.White);
                merge("AW2:AW2", "Entrecorte (kg)",true,true,Color.White);
                merge("AX2:AX2", "Orillos (kg)", true,true,Color.White);
                merge("AY2:AY2", "Extremos (kg)", true,true,Color.White);
                merge("AZ2:AZ2", "Total Merma de Corte (kg)", true,true,Color.White);
                merge("BA2:BA2", "Merma Programada",true,true,Color.White);
                merge("BB2:BB2", "Merma Real",true,true,Color.White);

                // EFICIENCIA DE TIZADO
                merge("BC1:BE1", "Eficiencia de tizado",true,true,Color.White);
                merge("BC2:BC2", "Eficiencia programada", true,true,Color.White);
                merge("BD2:BD2", "Eficiencia real",true,true,Color.White);
                merge("BE2:BE2", "Diferencia %",true,true,Color.White);

                // DATOS DE TELA
                merge("BF1:BY1", "Datos de tela",true,true,Color.White);
                merge("BF2:BF2", "Ancho total programado (m)",true,true,Color.White);
                merge("BG2:BG2", "Ancho total real (m)", true,true,Color.White);
                merge("BH2:BH2", "Diferencia Ancho (m)", true,true,Color.White);
                merge("BI2:BI2", "% Variación Ancho",true,true,Color.White);
                merge("BJ2:BJ2", "Ancho tizado util",true,true,Color.White);
                merge("BK2:BK2", "Diferencia (m) (Real vs Programado)", true, true, Color.White);
                merge("BL2:BL2", "Diferencia (%) (Real vs Programado)", true,true,Color.White);
                merge("BM2:BM2", "Densidad programada",true,true,Color.White);
                merge("BN2:BN2", "Densidad real",true,true,Color.White);
                merge("BO2:BO2", "Variación de densidad",true,true,Color.White);
                merge("BP2:BP2", "Largo según tizado (Ponderado)",true,true,Color.White);
                merge("BQ2:BQ2", "Largo de paño real (Ponderado)", true,true,Color.White);
                merge("BR2:BR2", "Diferencia (m)", true, true, Color.White);
                merge("BS2:BS2", "% Variación de Largo",true,true,Color.White);
                merge("BT2:BT2", "Tallas",true,true,Color.White);
                merge("BU2:BU2", "Paños",true,true,Color.White);
                merge("BV2:BV2", "Peso Paño programado (kg)", true,true,Color.White);
                merge("BW2:BW2", "Peso Paño real (kg)", true,true,Color.White);
                merge("BX2:BX2", "Diferencia (kg)", true, true, Color.White);
                merge("BY2:BY2", "% Variación",true,true,Color.White);


                merge("BZ1:CA1", "Estados",true,true,Color.White);
                merge("BZ2:BZ2", "Tendido",true,true,Color.White);
                merge("CA2:CA2", "Corte",true,true,Color.White);

                int i = 3;
                // ASIGNAMOS DATOS
                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    cells(i, 1, item.fechaliquidacion, Color.White, Color.Black, false, true);
                    cells(i, 2, item.turno, Color.White, Color.Black, false, true);
                    cells(i, 3, item.celula, Color.White, Color.Black, false, true);
                    cells(i, 4, item.usuario, Color.White, Color.Black, false, true);
                    cells(i, 5, item.fichas.ToString(), Color.White, Color.Black, false, true);
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
                    cells(i, 15, item.prendasreales, Color.White, Color.Black, false, true);
                    
                    double dif1 = item.prendasreales - item.prendasprogramadas;
                    cells(i, 16, dif1, Color.White, Color.Black, false, true);

                    double porcentaje = item.prendasprogramadas > 0 ? (item.prendasreales / item.prendasprogramadas)  : 0;
                    cells(i, 17, porcentaje , Color.White, Color.Black, false, true,true);
                   

                    // CONSUMO POR PRENDA
                    cells(i, 18, item.consnetoprogramado, Color.White, Color.Black, false, true,false,3);
                    cells(i, 19, item.consnetoreal, Color.White, Color.Black, false, true, false, 3);
                    cells(i, 20, item.consbrutprog, Color.White, Color.Black, false, true, false, 3);
                    cells(i, 21, item.consumoponderado, Color.White, Color.Black, false, true, false, 3);
                    cells(i, 22, item.consbrutoreal, Color.White, Color.Black, false, true, false, 3);

                    double dif2 = item.consbrutprog > 0 ? ((item.consbrutoreal - item.consbrutprog) / item.consbrutprog) : 0;
                    cells(i, 23, dif2, Color.White, Color.Black, false, true,true);

                    // KILOS TENDIDOS + COLLARETAS
                    cells(i, 24, item.kilosreales, Color.White, Color.Black, false, true,false,2);
                    cells(i, 25, item.collareta, Color.White, Color.Black, false, true,false,2);

                    // MERMAS DE TENDIDO
                    cells(i, 26, item.puntas, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 27, item.retazos, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 28, item.empalmes, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 29, item.conos, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 30, item.bolsas, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 31, item.totalmerma, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 32, item.mermatendprog / 100, Color.White, Color.Black, false, true,true);
                    cells(i, 33, item.mermatendreal / 100, Color.White, Color.Black, false, true,true);

                    // TELA ADICIONAL
                    cells(i, 34, item.adicional, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 35, item.motadicional, Color.White, Color.Black, false, true);

                    //DEVOLUCIONES DE TELA
                    cells(i, 36, item.dev1ra, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 37, item.dev1ramotivo, Color.White, Color.Black, false, true);
                    cells(i, 38, item.dev2da, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 39, item.dev2damotivo, Color.White, Color.Black, false, true);
                    cells(i, 40, item.deverp, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 41, item.coderp, Color.White, Color.Black, false, true);

                    // BALANCE DE TELA
                    cells(i, 42, item.kilosprogramados, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 43, item.kilostizados, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 44, item.kilosdespachados, Color.White, Color.Black, false, true, false, 2);

                    double despaadicional = item.kilosdespachados + item.adicional;
                    cells(i, 45, despaadicional, Color.White, Color.Black, false, true, false, 2);

                    cells(i, 46, item.realesliquidados, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 47, item.realesliquidados - (despaadicional) , Color.White, Color.Black, false, true, false, 2);

                    if (despaadicional != 0)
                    {
                        cells(i, 48, (item.realesliquidados / (despaadicional)) , Color.White, Color.Black, false, true,true);
                    }else
                    {
                        cells(i, 48, 0, Color.White, Color.Black, false, true,true);
                    }

                    // MERMAS DE CORTE
                    cells(i, 49, item.entrecorte, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 50, item.orillos, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 51, item.extremos, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 52, item.totalmermacorte, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 53, item.mermaprog /100, Color.White, Color.Black, false, true,true);
                    cells(i, 54, item.mermareal /100, Color.White, Color.Black, false, true,true);

                    // EFICIENCIA DE TIZADO
                    cells(i, 55, item.eficprog / 100 , Color.White, Color.Black, false, true,true);
                    cells(i, 56, item.eficreal / 100, Color.White, Color.Black, false, true,true);
                    double dif3 = 0;
                    if (item.eficprog != 0)
                    {
                        dif3 = ((item.eficreal - item.eficprog) / item.eficprog);
                    }
                    cells(i, 57, dif3, Color.White, Color.Black, false, true,true);

                    // DATOS DE TELA
                    cells(i, 58, item.anchprog, Color.White, Color.Black, false, true);
                    cells(i, 59, item.anchreal, Color.White, Color.Black, false, true);
                    double dif4 = item.anchreal - item.anchprog;
                    cells(i, 60, dif4, Color.White, Color.Black, false, true);

                    if (item.anchprog != 0)
                    {
                        cells(i, 61, (dif4 / item.anchprog) , Color.White, Color.Black, false, true,true);
                    }else
                    {
                        cells(i, 61, 0, Color.White, Color.Black, false, true,true);
                    }

                    cells(i, 62, item.anchotizadoutil, Color.White, Color.Black, false, true);

                    double difmetros = item.anchotizadoutil - item.anchreal;
                    cells(i, 63, difmetros , Color.White, Color.Black, false, true);

                    if (item.anchreal != 0)
                    {
                        cells(i, 64, (difmetros / item.anchreal), Color.White, Color.Black, false, true,true);
                    }
                    else
                    {
                        cells(i, 64, 0, Color.White, Color.Black, false, true,true);
                    }

                    cells(i, 65, item.densprog, Color.White, Color.Black, false, true);
                    double densidadreal = 0;
                    if (item.largopanoreal == 0)
                    {
                        densidadreal = 0;
                    }
                    else
                    {
                        densidadreal = item.pesopanoreal / (item.anchreal * item.largopanoreal);
                    }
                    cells(i, 66, Math.Round(densidadreal,3) , Color.White, Color.Black, false, true);

                    cells(i, 67, ((densidadreal - item.densprog ) / item.densprog), Color.White, Color.Black, false, true,true);
                    cells(i, 68, item.largotizado, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 69, item.largopanoreal, Color.White, Color.Black, false, true, false, 2);
                    double dif5 = item.largopanoreal - item.largotizado;
                    cells(i, 70, dif5, Color.White, Color.Black, false, true, false, 2);

                    if (item.largotizado != 0)
                    {
                        cells(i, 71, (dif5 / item.largotizado), Color.White, Color.Black, false, true,true);
                    }else
                    {
                        cells(i, 71, 0, Color.White, Color.Black, false, true,true);
                    }

                    cells(i, 72, item.tallas, Color.White, Color.Black, false, true);
                    cells(i, 73, item.panos, Color.White, Color.Black, false, true);
                    cells(i, 74, item.pesopanoprogramado, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 75, item.pesopanoreal, Color.White, Color.Black, false, true, false, 2);
                    double dif6 = item.pesopanoreal - item.pesopanoprogramado;
                    cells(i, 76, dif6, Color.White, Color.Black, false, true, false, 2);
                    cells(i, 77, item.pesopanoprogramado > 0 ? (dif6 / item.pesopanoprogramado) : 0, Color.White, Color.Black, false, true,true);

                    // ESTADOS
                    cells(i, 78, item.estadotendido, Color.White, Color.Black, false, true);
                    cells(i, 79, item.estadocorte, Color.White, Color.Black, false, true);

                    i++;
                }


                // ANCHO DE COLUMNAS
                for (int x = 1;x <= 79;x++)
                {
                    workSheet.Column(x).Width = 21;
                }


                package.Save();

            }
            stream.Position = 0;
            return stream;

        }

    }
}