using Dapper;
using Dapper.Oracle;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Logistica.OCPendienteCierre;

namespace TSC_WEB.Models.Modelos.Logistica.OCPendienteCierre
{
    public class OCPendCierreModel
    {

        public async Task<List<Lista1_OCPendCierre>> ListarPendCierre(string niveles, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_I_OPCION", 1, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("P_CH_NIVELES_ITEM", niveles, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Lista1_OCPendCierre>(sql: "USYSTEX.SPU_GET_PENDIENTECIERRE",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);

                    return resultado.OrderByDescending(x => x.PEDIDO).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Lista2_ListaExcel> ListaReporteExcel(string niveles, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_I_OPCION", 2, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("P_CH_NIVELES_ITEM", niveles, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = conexion.Query<Lista2_ListaExcel>(sql: "USYSTEX.SPU_GET_PENDIENTECIERRE",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure);
                    return resultado.OrderByDescending(x => x.PEDIDO).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Lista3_NivelItem> ListarNivelItems()
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_I_OPCION", 3, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("P_CH_NIVELES_ITEM", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    return conexion.Query<Lista3_NivelItem>(sql: "USYSTEX.SPU_GET_PENDIENTECIERRE",
                                                                          param: parametros,
                                                                          commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MemoryStream ReporteExcel(List<Lista2_ListaExcel> listado)
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
                workSheet.Cells[1, 1, 1, 12].Style.Border.BorderAround(ExcelBorderStyle.Medium);

              
                workSheet.Cells["A1"].Value = "PEDIDO";
                workSheet.Cells["B1"].Value = "FECHA";
                workSheet.Cells["C1"].Value = "DIAS_ABIERTO";
                workSheet.Cells["D1"].Value = "TRANSACCION";
                workSheet.Cells["E1"].Value = "DESCRIPCION PAGO";
                workSheet.Cells["F1"].Value = "PROVEEDOR";
                workSheet.Cells["G1"].Value = "COMPRADOR";
                workSheet.Cells["H1"].Value = "MONEDA";
                workSheet.Cells["I1"].Value = "TOTAL OC";
                workSheet.Cells["J1"].Value = "CANTIDAD OC";
                workSheet.Cells["K1"].Value = "CANTIDAD INGRESADO";
                workSheet.Cells["L1"].Value = "DIFERENCIA";

                workSheet.View.FreezePanes(2, 1);
                workSheet.Cells["A1:L1"].AutoFilter = true;


                //workSheet.Column(1).Width = 10;     // "PEDIDO";
                //workSheet.Column(2).Width = 10;     // "FECHA";
                //workSheet.Column(3).Width = 10;     // "DIAS_ABIERTO";
                //workSheet.Column(4).Width = 20;     // "TRANSACCION";
                //workSheet.Column(5).Width = 20;     // "DESCRIPCION PAGO";
                //workSheet.Column(6).Width = 30;     // "PROVEEDOR";
                //workSheet.Column(7).Width = 30;     // "COMPRADOR";
                //workSheet.Column(8).Width = 10;     // "MONEDA";
                //workSheet.Column(9).Width = 10;     // "TOTAL OC";
                //workSheet.Column(10).Width = 14;    // "CANTIDAD OC";
                //workSheet.Column(11).Width = 15;    // "CANTIDAD INGRESADO";
                //workSheet.Column(12).Width = 15;    // "DIFERENCIA";

                int i = 2;

                foreach (var item in listado)
                {
                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.PEDIDO;
                    var vFecha = DateTime.Parse(item.FECHA);
                    workSheet.Cells[i, 2].Value = vFecha.Date;
                    workSheet.Cells[i, 2].Style.Numberformat.Format = "yyyy-mm-dd";

                    workSheet.Cells[i, 3].Value = item.DIAS_ABIERTO;
                    workSheet.Cells[i, 4].Value = item.TRANSACCION;
                    workSheet.Cells[i, 5].Value = item.DESCRIPCIONPAGO;
                    workSheet.Cells[i, 6].Value = item.PROVEEDOR;
                    workSheet.Cells[i, 7].Value = item.COMPRADOR;
                    workSheet.Cells[i, 8].Value = item.MONEDA;
                    workSheet.Cells[i, 9].Value = item.TOTAL_PEDIDO;
                    workSheet.Cells[i, 10].Value = item.CANTIDAD_OC;
                    workSheet.Cells[i, 11].Value = item.CANT_INGRESA;
                    workSheet.Cells[i, 12].Value = item.DIFERENCIA;


                    workSheet.Cells[i, 9].Style.Numberformat.Format = "#,##0.00";
                    workSheet.Cells[i, 10].Style.Numberformat.Format = "#,##0.00";
                    workSheet.Cells[i, 11].Style.Numberformat.Format = "#,##0.00";
                    workSheet.Cells[i, 12].Style.Numberformat.Format = "#,##0.00";

                    i++;
                }


                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                package.Save();
            }

            stream.Position = 0;
            return stream;

        }


    }
}