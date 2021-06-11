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
using TSC_WEB.Models.Entidades.Gerencia.ReporteCambio;

namespace TSC_WEB.Models.Modelos.Gerencia.ReporteCambio
{
    public class ReporteCambioModelo
    {
        public async Task<List<CambioReporte>> ListarCambio(string pedido, string estado, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_PEDIDO", pedido, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_ESTADO", estado, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_FECHAINI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("P_FECHAFIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("P_OPCION", 1, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add(name: ":PCURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<CambioReporte>(sql: "USYSTEX.GETSYS_COME_RPTCAMBIO_PRECIO",
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


        public async Task<List<Estado>> ListarEstado()
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_PEDIDO", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_ESTADO", null, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_FECHAINI", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("P_FECHAFIN", null, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("P_OPCION", 2, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add(name: ":PCURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = await conexion.QueryAsync<Estado>(sql: "USYSTEX.GETSYS_COME_RPTCAMBIO_PRECIO",
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

        public List<CambioReporte> ListaReporte(string pedido, string estado, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (OracleConnection conexion = DBAccess.ObtenerConexion())
                {
                    OracleDynamicParameters parametros = new OracleDynamicParameters();
                    parametros.Add("P_PEDIDO", pedido, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_ESTADO", estado, OracleMappingType.Varchar2, ParameterDirection.Input);
                    parametros.Add("P_FECHAINI", fechaInicio.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("P_FECHAFIN", fechaFin.Date, OracleMappingType.Date, ParameterDirection.Input);
                    parametros.Add("P_OPCION", 1, OracleMappingType.Int32, ParameterDirection.Input);
                    parametros.Add(name: ":PCURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                    var resultado = conexion.Query<CambioReporte>(sql: "USYSTEX.GETSYS_COME_RPTCAMBIO_PRECIO",
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



        public MemoryStream ReporteExcel(List<CambioReporte> listado)
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

                workSheet.Cells["A1"].Value = "CLIENTE";
                workSheet.Cells["B1"].Value = "PEDIDO";
                workSheet.Cells["C1"].Value = "COLOR";
                workSheet.Cells["D1"].Value = "ANTIGUO PRECIO";
                workSheet.Cells["E1"].Value = "NUEVO PRECIO";
                workSheet.Cells["F1"].Value = "ESTADO";
                workSheet.Cells["G1"].Value = "USUARIO SOLICITUD";
                workSheet.Cells["H1"].Value = "FECHA SOLICITUD";
                workSheet.Cells["I1"].Value = "MOTIVO SOLICITUD";
                workSheet.Cells["J1"].Value = "MOTIVO APROBACION / RECHAZO"; 

                workSheet.Column(1).Width = 30;     // CLIENTE";
                workSheet.Column(2).Width = 8;     // PEDIDO";
                workSheet.Column(3).Width = 30;     // COLOR";
                workSheet.Column(4).Width = 20;     // ANTIGUO PRECIO";
                workSheet.Column(5).Width = 20;     // NUEVO PRECIO";
                workSheet.Column(6).Width = 20;     // ESTADO";
                workSheet.Column(7).Width = 20;     // USUARIO SOLICITUD";
                workSheet.Column(8).Width = 20;     // FECHA SOLICITUD";
                workSheet.Column(9).Width = 50;     // MOTIVO SOLICITUD";
                workSheet.Column(10).Width = 50;     // MOTIVO APROBACION RECHAZO"; 

                int i = 2;
                 
                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.CLIENTE;
                    workSheet.Cells[i, 2].Value = item.PEDIDO;
                    workSheet.Cells[i, 3].Value = item.COLOR;
                    workSheet.Cells[i, 4].Value = item.ANTIGUO_PRECIO;
                    workSheet.Cells[i, 5].Value = item.NUEVO_PRECIO;
                    workSheet.Cells[i, 6].Value = item.ESTADO;
                    workSheet.Cells[i, 7].Value = item.USUARIO_SOLICITUD;
                    workSheet.Cells[i, 8].Value = item.FECHA_SOLICITUD;
                    workSheet.Cells[i, 9].Value = item.MOTIVO_SOLICITUD;
                    workSheet.Cells[i, 10].Value = item.MOTIVO_APROBACION_RECHAZO; 

                    i++;
                }

                package.Save();
            }

            stream.Position = 0;
            return stream;

        }
         
    }
}