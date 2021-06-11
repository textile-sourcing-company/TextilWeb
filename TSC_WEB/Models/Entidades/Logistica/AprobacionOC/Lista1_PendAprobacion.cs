using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.AprobacionOC
{
    public class Lista1_PendAprobacion
    {
        public int PEDIDO { get; set; }
        public string CECO { get; set; }
        public string FECHA { get; set; }
        public string PROVEEDOR { get; set; }
        public string COMPRADOR { get; set; }
        public string DESCRIPCIONPAGO { get; set; }
        public string SIMBOLO_MOEDA { get; set; }
        public string TOTAL_PEDIDO { get; set; }

    }
}