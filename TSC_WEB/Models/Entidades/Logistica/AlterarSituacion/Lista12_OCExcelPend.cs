using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.AlterarSituacion
{
    public class Lista12_OCExcelPend
    {

        public string ESTADO_OC { get; set; }
        public int PEDIDO { get; set; }
        public string FECHA { get; set; }
        public string DESCRIPCIONPAGO { get; set; }
        public string MONEDA { get; set; }
        public decimal TOTAL_PEDIDO { get; set; }
        public string PROVEEDOR { get; set; }
        public string COMPRADOR { get; set; }
        public int CENTRO_COSTO { get; set; }

    }
}