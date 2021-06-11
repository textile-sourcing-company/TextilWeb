using Dapper;
using Dapper.Oracle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Almacenes.ReporteStockFecha;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace TSC_WEB.Models.Modelos.Logistica.ReporteStockFecha
{
    public class ReporteStockFechaModelo
    {
        //INSTANCIA CONEXION
        DBAccess conexion = new DBAccess();

        public List<ReporteStockFechaEntidad> ListarReporteStockFecha(string v_partida, string v_ubicacion, string v_tipotela, string v_centrocosto, string v_almacenes)
        {
            List<ReporteStockFechaEntidad> ReporteStockFechaLista = new List<ReporteStockFechaEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPGET_STOCK_FECHA_ALMACENES", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("PPARTIDA", v_partida));
            comando.Parameters.Add(new OracleParameter("PUBICACION", v_ubicacion));
            comando.Parameters.Add(new OracleParameter("PTIPOTELA", v_tipotela));
            comando.Parameters.Add(new OracleParameter("PCENTROCOSTO", v_centrocosto));
            comando.Parameters.Add(new OracleParameter("PCODALMACENES", v_almacenes));
            comando.Parameters.Add(new OracleParameter("PCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();
            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    ReporteStockFechaEntidad objReporteStockFechaE = new ReporteStockFechaEntidad();
                    objReporteStockFechaE.NIVEL_ = registros["NIVEL"].ToString();
                    objReporteStockFechaE.GRUPO_ = registros["GRUPO"].ToString();
                    objReporteStockFechaE.SUBGRUPO_ = registros["SUBGRUPO"].ToString();
                    objReporteStockFechaE.DESC_TALLA_ = registros["DESC_TALLA"].ToString();
                    objReporteStockFechaE.ITEM_ = registros["ITEM"].ToString();
                    objReporteStockFechaE.LOTE_ACOMP_ = registros["LOTE_ACOMP"].ToString();
                    objReporteStockFechaE.PARTIDA_ = registros["PARTIDA"].ToString();
                    objReporteStockFechaE.COD_ALMACEN_ = registros["COD_ALMACEN"].ToString();
                    objReporteStockFechaE.ALMACEN_ = registros["ALMACEN"].ToString();
                    objReporteStockFechaE.CENTRO_COSTO_ = registros["CENTRO_COSTO"].ToString();
                    objReporteStockFechaE.DESC_CENTRO_CUSTO_ = registros["DESC_CENTRO_CUSTO"].ToString();
                    objReporteStockFechaE.DESC_PRODUCTO_ = registros["DESC_PRODUCTO"].ToString();
                    objReporteStockFechaE.COLOR_ = registros["COLOR"].ToString();
                    objReporteStockFechaE.STOCK_ANT_ = registros["STOCK_ANT"] != DBNull.Value ? Convert.ToDecimal(registros["STOCK_ANT"].ToString()) : (decimal?)null;
                    objReporteStockFechaE.UM_ = registros["UM"].ToString();
                    objReporteStockFechaE.COD_PROVEEDOR_ = registros["COD_PROVEEDOR"].ToString();
                    objReporteStockFechaE.PROVEEDOR_ = registros["PROVEEDOR"].ToString();
                    objReporteStockFechaE.CLIENTE_ = registros["CLIENTE"].ToString();
                    objReporteStockFechaE.ARTICULO_PRODUCTO_ = registros["ARTICULO_PRODUCTO"].ToString();
                    objReporteStockFechaE.ESTILO_CLIENTE_ = registros["ESTILO_CLIENTE"].ToString();
                    objReporteStockFechaE.TIPO_TELA_ = registros["TIPO_TELA"].ToString();
                    objReporteStockFechaE.FECHA_INGRESO_GR_ = registros["FECHA_INGRESO_GR"].ToString();
                    objReporteStockFechaE.OC_ = registros["OC"].ToString();
                    objReporteStockFechaE.ULT_FECHA_INGRESO_ = registros["ULT_FECHA_INGRESO"].ToString();
                    objReporteStockFechaE.ULT_FECHA_SALIDA_ = registros["ULT_FECHA_SALIDA"].ToString();
                    objReporteStockFechaE.CANTIDAD_ENTRADA_ = registros["CANTIDAD_ENTRADA"] != DBNull.Value ? Convert.ToDecimal(registros["CANTIDAD_ENTRADA"].ToString()) : (decimal?)null;
                    objReporteStockFechaE.CANTIDAD_SALIDA_ = registros["CANTIDAD_SALIDA"] != DBNull.Value ? Convert.ToDecimal(registros["CANTIDAD_SALIDA"].ToString()) : (decimal?)null;
                    objReporteStockFechaE.UBICACION_ = registros["UBICACION"].ToString();
                    objReporteStockFechaE.PROGRAMA_ = registros["PROGRAMA"].ToString();
                    objReporteStockFechaE.OBS_OC_ = registros["OBS_OC"].ToString();
                    ReporteStockFechaLista.Add(objReporteStockFechaE);
                }
            }
            conexion.Desconectar();

            return ReporteStockFechaLista.Where(x => x.STOCK2_ > 0).ToList();

            // .OrderByDescending(x => x.NRO_FACTURA).ToList();
        }


        // METODO PARA EXPORTAR EXCEL 
        public MemoryStream ExportarExcelReporteStockF(List<ReporteStockFechaEntidad> listado)
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

                workSheet.Cells["A1"].Value = "Cod. Item";
                workSheet.Cells["B1"].Value = "C. Costo";
                workSheet.Cells["C1"].Value = "Desc. C.Costo";
                workSheet.Cells["D1"].Value = "Cod. Almacén";
                workSheet.Cells["E1"].Value = "Desc. Almacén";
                workSheet.Cells["F1"].Value = "Partida";
                workSheet.Cells["G1"].Value = "Lote";
                workSheet.Cells["H1"].Value = "Tipo Tela";
                workSheet.Cells["I1"].Value = "Desc. Producto";
                workSheet.Cells["J1"].Value = "Articulo Producto";
                workSheet.Cells["K1"].Value = "Talla";
                workSheet.Cells["L1"].Value = "Color";
                workSheet.Cells["M1"].Value = "Inicial";
                workSheet.Cells["N1"].Value = "Entradas";
                workSheet.Cells["O1"].Value = "Salidas";
                workSheet.Cells["P1"].Value = "Stock";
                workSheet.Cells["Q1"].Value = "UM";
                workSheet.Cells["R1"].Value = "Cod. Proveedor";
                workSheet.Cells["S1"].Value = "Proveedor";
                workSheet.Cells["T1"].Value = "Programa";
                workSheet.Cells["U1"].Value = "Ubicación";
                workSheet.Cells["V1"].Value = "Ult. Fecha Ing.";
                workSheet.Cells["W1"].Value = "Ult. Fecha Sal.";
                workSheet.Cells["X1"].Value = "N° OC";
                workSheet.Cells["Y1"].Value = "Fecha Ing";
                workSheet.Cells["Z1"].Value = "Cliente";
                workSheet.Cells["AA1"].Value = "Estilo Cliente";
                workSheet.Cells["AB1"].Value = "Observación OC";

                ////workSheet.Column(1).Width = 10;     // MODULO
                ////workSheet.Column(2).Width = 10;     // CODIGO AUTORIZA
                ////workSheet.Column(3).Width = 10;     // CODIGO
                ////workSheet.Column(4).Width = 25;     // PROVEEDOR
                ////workSheet.Column(5).Width = 15;     // FECHA
                ////workSheet.Column(6).Width = 30;     // CENTRO DE COSTO
                ////workSheet.Column(7).Width = 25;     // TIPO PAGO
                ////workSheet.Column(8).Width = 20;     // VALOR SOLES
                ////workSheet.Column(9).Width = 20;     // VALOR DOLAR
                ////workSheet.Column(10).Width = 20;    // VALOR DOLAR

                int i = 2;

                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.COD_ITEMS_;
                    workSheet.Cells[i, 2].Value = item.CENTRO_COSTO_;
                    workSheet.Cells[i, 3].Value = item.DESC_CENTRO_CUSTO_;
                    workSheet.Cells[i, 4].Value = item.COD_ALMACEN_;
                    workSheet.Cells[i, 5].Value = item.ALMACEN_;
                    workSheet.Cells[i, 6].Value = item.PARTIDA_;
                    workSheet.Cells[i, 7].Value = item.LOTE_ACOMP_;
                    workSheet.Cells[i, 8].Value = item.TIPO_TELA_;
                    workSheet.Cells[i, 9].Value = item.DESC_PRODUCTO_;
                    workSheet.Cells[i, 10].Value = item.ARTICULO_PRODUCTO_;
                    workSheet.Cells[i, 11].Value = item.DESC_TALLA_;
                    workSheet.Cells[i, 12].Value = item.COLOR_;
                    workSheet.Cells[i, 13].Value = item.STOCK_ANT_;
                    workSheet.Cells[i, 14].Value = item.CANTIDAD_ENTRADA_;
                    workSheet.Cells[i, 15].Value = item.CANTIDAD_SALIDA_;
                    workSheet.Cells[i, 16].Value = item.STOCK2_;
                    workSheet.Cells[i, 17].Value = item.UM_;
                    workSheet.Cells[i, 18].Value = item.COD_PROVEEDOR_;
                    workSheet.Cells[i, 19].Value = item.PROVEEDOR_;
                    workSheet.Cells[i, 20].Value = item.PROGRAMA_;
                    workSheet.Cells[i, 21].Value = item.UBICACION_;
                    workSheet.Cells[i, 22].Value = item.ULT_FECHA_INGRESO_;
                    workSheet.Cells[i, 23].Value = item.ULT_FECHA_SALIDA_;
                    workSheet.Cells[i, 24].Value = item.OC_;
                    workSheet.Cells[i, 25].Value = item.FECHA_INGRESO_GR_;
                    workSheet.Cells[i, 26].Value = item.CLIENTE_;
                    workSheet.Cells[i, 27].Value = item.ESTILO_CLIENTE_;
                    workSheet.Cells[i, 28].Value = item.OBS_OC_;

                    i++;
                }

                workSheet.Cells.AutoFitColumns();
                package.Save();
            }

            stream.Position = 0;
            return stream;

        }

    }
}