using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Almacenes.ReporteStockFecha
{
    public class ReporteStockFechaEntidad
    {
        public string NIVEL_ { get; set; }
        public string GRUPO_ { get; set; }
        public string SUBGRUPO_ { get; set; }
        public string DESC_TALLA_ { get; set; }
        public string ITEM_ { get; set; }
        public string LOTE_ACOMP_ { get; set; }
        public string PARTIDA_ { get; set; }
        public string COD_ALMACEN_ { get; set; }
        public string ALMACEN_ { get; set; }
        public string CENTRO_COSTO_ { get; set; }
        public string DESC_CENTRO_CUSTO_ { get; set; }
        public string DESC_PRODUCTO_ { get; set; }
        public string COLOR_ { get; set; }
        public decimal? STOCK_ANT_ { get; set; }
        public string UM_ { get; set; }
        public string COD_PROVEEDOR_ { get; set; }
        public string PROVEEDOR_ { get; set; }
        public string CLIENTE_ { get; set; }
        public string ARTICULO_PRODUCTO_ { get; set; }
        public string ESTILO_CLIENTE_ { get; set; }
        public string TIPO_TELA_ { get; set; }
        public string FECHA_INGRESO_GR_ { get; set; }
        public string OC_ { get; set; }
        public string ULT_FECHA_INGRESO_ { get; set; }
        public string ULT_FECHA_SALIDA_ { get; set; }
        public decimal? CANTIDAD_ENTRADA_ { get; set; }
        public decimal? CANTIDAD_SALIDA_ { get; set; }
        public string UBICACION_ { get; set; }
        public string PROGRAMA_ { get; set; }
        public string OBS_OC_ { get; set; }

        //CAMPOS CALCULADOS
        public decimal? STOCK2_
        {
            get
            {
                return STOCK_ANT_ + CANTIDAD_ENTRADA_ - CANTIDAD_SALIDA_;
            }
        }

        public string COD_ITEMS_
        {
            get
            {
                return NIVEL_ + "." + GRUPO_ + "." + SUBGRUPO_ + "." + ITEM_;
            }
        }
    }

}