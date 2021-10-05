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
using TSC_WEB.Models.Entidades.Facturacion.ReporteVentas;

namespace TSC_WEB.Models.Modelos.Facturacion.ReporteVentas
{
    public class ReporteVentasModelo
    {
        public async Task<List<Ventas>> ListarVentas(string series, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_I_OPCION", 1, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("CH_SERIE", series, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Ventas>(sql: "USYSTEX.SPU_GET_FACTURACION_VENTAS_V1",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado
                              .OrderByDescending(x => x.NRO_FACTURA).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<Series>> ListarSeries()
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_I_OPCION", 2, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("CH_SERIE", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Series>(sql: "USYSTEX.SPU_GET_FACTURACION_VENTAS_V1",
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

        public List<VentasReporte> ListaReporte(string series, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_I_OPCION", 3, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("CH_SERIE", series, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = conexion.Query<VentasReporte>(sql: "USYSTEX.SPU_GET_FACTURACION_VENTAS_V1",
                                                                    param: parametros,
                                                                    commandType: CommandType.StoredProcedure);

                    return resultado
                              .OrderByDescending(x => x.SERIE_FACTURA)
                                .ThenByDescending(x => x.NRO_FACTURA).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public MemoryStream ReporteExcel(List<VentasReporte> listado)
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

                workSheet.Cells["A1"].Value = "FECHA";
                workSheet.Cells["B1"].Value = "AÑO";
                workSheet.Cells["C1"].Value = "MES";
                workSheet.Cells["D1"].Value = "CLIENTE FACTURA";
                workSheet.Cells["E1"].Value = "CLIENTE GUIA";
                workSheet.Cells["F1"].Value = "TIPO PRENDA";
                workSheet.Cells["G1"].Value = "OBSERVACIÓN";
                workSheet.Cells["H1"].Value = "SERIE FACTURA";
                workSheet.Cells["I1"].Value = "NRO FACTURA";
                workSheet.Cells["J1"].Value = "SERIE GUIA";
                workSheet.Cells["K1"].Value = "NUM GUIA";
                workSheet.Cells["L1"].Value = "MONEDA";
                workSheet.Cells["M1"].Value = "PRECIO REAL";
                workSheet.Cells["N1"].Value = "CANTIDAD";
                workSheet.Cells["O1"].Value = "IGV";
                workSheet.Cells["P1"].Value = "VALOR REAL";
                workSheet.Cells["Q1"].Value = "COD.CONDICIÓN PAGO";
                workSheet.Cells["R1"].Value = "CONDICIÓN PAGO";

                workSheet.Column(1).Width = 10;     // FECHA";
                workSheet.Column(2).Width = 10;     // AÑO";
                workSheet.Column(3).Width = 8;     // MES";
                workSheet.Column(4).Width = 30;     // CLIENTE FACTURA";
                workSheet.Column(5).Width = 25;     // CLIENTE GUIA";
                workSheet.Column(6).Width = 30;     // TIPO PRENDA";
                workSheet.Column(7).Width = 30;     // OBSERVACION";
                workSheet.Column(8).Width = 15;     // NRO FACTURA";
                workSheet.Column(9).Width = 15;     // SERIE FACTURA";
                workSheet.Column(10).Width = 15;     // SERIE GUIA";
                workSheet.Column(11).Width = 10;    // NUM GUIA";
                workSheet.Column(12).Width = 10;     // MONEDA";
                workSheet.Column(13).Width = 15;     // SUBTOTAL";
                workSheet.Column(14).Width = 15;     // CANTIDAD";
                workSheet.Column(15).Width = 15;     // IGV";
                workSheet.Column(16).Width = 15;     // TOTAL";
                workSheet.Column(17).Width = 15;     // IGV";
                workSheet.Column(18).Width = 25;     // TOTAL";

                int i = 2;


                foreach (var item in listado)
                {
                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.FECHA;
                    workSheet.Cells[i, 2].Value = item.ANO;
                    workSheet.Cells[i, 3].Value = item.MES;
                    workSheet.Cells[i, 4].Value = item.CLIENTE_FACTURA;
                    workSheet.Cells[i, 5].Value = item.CLIENTE_GUIA;
                    workSheet.Cells[i, 6].Value = item.TIPO_PRENDA;
                    workSheet.Cells[i, 7].Value = item.OBSERVACION;
                    workSheet.Cells[i, 8].Value = item.SERIE_FACTURA;
                    workSheet.Cells[i, 9].Value = item.NRO_FACTURA;
                    workSheet.Cells[i, 10].Value = item.SERIE_GUIA;
                    workSheet.Cells[i, 11].Value = item.NUM_GUIA;
                    workSheet.Cells[i, 12].Value = item.MONEDA;
                    workSheet.Cells[i, 13].Value = item.PRECIO_REAL;
                    workSheet.Cells[i, 14].Value = item.CANTIDAD_REAL;
                    workSheet.Cells[i, 15].Value = item.IGV;
                    workSheet.Cells[i, 16].Value = item.VALOR_REAL;
                    workSheet.Cells[i, 17].Value = item.CONDPAGO;
                    workSheet.Cells[i, 18].Value = item.DESCCONDPAGO;

                    i++;
                }
                    
                package.Save();
            }

            stream.Position = 0;
            return stream;

        }



    }
}