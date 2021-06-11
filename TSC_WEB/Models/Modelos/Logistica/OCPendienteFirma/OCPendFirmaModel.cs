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
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Logistica.OCPendienteFirma;

namespace TSC_WEB.Models.Modelos.Logistica.OCPendienteFirma
{
    public class OCPendFirmaModel
    {
        public async Task<List<Lista1_OCPendFirma>> ListarPendienteFirma(string tipoFirmas,
                                                                         string usuario,
                                                                         string empresa,
                                                                         DateTime fechaInicio,
                                                                         DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_I_OPCION", 1, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("P_I_FILTRO_1", null, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_1", tipoFirmas, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_2", usuario, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_3", empresa, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Lista1_OCPendFirma>(sql: "USYSTEX.SPU_GET_OCPENDIENTESFIRMA",
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

        public async Task<List<Lista2_OCDetalle>> ListarDetalleOC(int oc)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_I_OPCION", 2, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("P_I_FILTRO_1", oc, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_1", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_2", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_3", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Lista2_OCDetalle>(sql: "USYSTEX.SPU_GET_OCPENDIENTESFIRMA",
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

        public async Task<List<Lista3_OCFirmas>> ListarCboFirmas()
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_I_OPCION", 3, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("P_I_FILTRO_1", null, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_1", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_2", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_3", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Lista3_OCFirmas>(sql: "USYSTEX.SPU_GET_OCPENDIENTESFIRMA",
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

        public List<Lista4_ListaExcel> ListaReporteExcel(string firmas,
                                                         string usuario,
                                                         string empresa,
                                                         DateTime fechaInicio,
                                                         DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_I_OPCION", 4, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("P_I_FILTRO_1", null, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_1", firmas, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_2", usuario, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_CH_FILTRO_3", empresa, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_INI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("DT_FECHA_FIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add(name: ":O_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = conexion.Query<Lista4_ListaExcel>(sql: "USYSTEX.SPU_GET_OCPENDIENTESFIRMA",
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


        public MemoryStream ReporteExcel(List<Lista4_ListaExcel> listado)
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

                workSheet.Cells["A1"].Value = "PEDIDO";
                workSheet.Cells["B1"].Value = "FECHA";
                workSheet.Cells["C1"].Value = "FIRMAS REALIZADAS";
                workSheet.Cells["D1"].Value = "DIAS PENDIENTES FIRMA";
                workSheet.Cells["E1"].Value = "CECO";
                workSheet.Cells["F1"].Value = "DESCRIPCION PAGO";
                workSheet.Cells["G1"].Value = "PROVEEDOR";
                workSheet.Cells["H1"].Value = "COMPRADOR";
                workSheet.Cells["I1"].Value = "MONEDA";
                workSheet.Cells["J1"].Value = "TOTAL OC";
                workSheet.Cells["K1"].Value = "FIRMANTE PENDIENTE";
                workSheet.Cells["L1"].Value = "FIRMA 1";
                workSheet.Cells["M1"].Value = "FIRMA 2";
                workSheet.Cells["N1"].Value = "FIRMA 3";

                workSheet.Column(1).Width = 10;     // PEDIDO";
                workSheet.Column(2).Width = 10;     // FECHA";
                workSheet.Column(3).Width = 20;     // FIRMAS";
                workSheet.Column(4).Width = 25;     // DIAS PENDIENTES FIRMA";
                workSheet.Column(5).Width = 30;     // CECO";
                workSheet.Column(6).Width = 25;     // DESCRIPCION PAGO
                workSheet.Column(7).Width = 30;     // PROVEEDOR";
                workSheet.Column(8).Width = 30;     // COMPRADOR";
                workSheet.Column(9).Width = 15;     // MONEDA";
                workSheet.Column(10).Width = 15;     // TOTAL OC";
                workSheet.Column(11).Width = 50;     // FIRMA PENDIENTE";
                workSheet.Column(12).Width = 8;     // FIRMA 1";
                workSheet.Column(13).Width = 8;     // FIRMA 2";
                workSheet.Column(14).Width = 8;     // FIRMA 3";

                int i = 2;

                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.PEDIDO;
                    workSheet.Cells[i, 2].Value = item.FECHA;
                    workSheet.Cells[i, 3].Value = item.FIRMAS;
                    workSheet.Cells[i, 4].Value = item.CANT_DIAS;
                    workSheet.Cells[i, 5].Value = item.CECO;
                    workSheet.Cells[i, 6].Value = item.DESCRIPCIONPAGO;
                    workSheet.Cells[i, 7].Value = item.PROVEEDOR;
                    workSheet.Cells[i, 8].Value = item.COMPRADOR;
                    workSheet.Cells[i, 9].Value = item.MONEDA;
                    workSheet.Cells[i, 10].Value = item.TOTAL_PEDIDO;
                    workSheet.Cells[i, 11].Value = item.FIRMANTE;
                    workSheet.Cells[i, 12].Value = item.FIRMA_1;
                    workSheet.Cells[i, 13].Value = item.FIRMA_2;
                    workSheet.Cells[i, 14].Value = item.FIRMA_3;

                    i++;
                }

                package.Save();
            }

            stream.Position = 0;
            return stream;

        }



    }
}