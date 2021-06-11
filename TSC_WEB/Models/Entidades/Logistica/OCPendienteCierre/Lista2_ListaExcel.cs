using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.OCPendienteCierre
{
    public class Lista2_ListaExcel
    {
        public int PEDIDO { get; set; }
        public string FECHA { get; set; }
        public int DIAS_ABIERTO { get; set; }
        public string TRANSACCION { get; set; }
        public string DESCRIPCIONPAGO { get; set; }
        public string PROVEEDOR { get; set; }
        public string COMPRADOR { get; set; }
        public string MONEDA { get; set; }
        public decimal TOTAL_PEDIDO { get; set; }
        public decimal CANTIDAD_OC { get; set; }
        public decimal CANT_INGRESA { get; set; }
        public decimal DIFERENCIA { get; set; }
    }
}