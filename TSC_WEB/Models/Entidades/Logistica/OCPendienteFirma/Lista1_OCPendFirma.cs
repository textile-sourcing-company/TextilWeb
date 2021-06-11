using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.OCPendienteFirma
{
    public class Lista1_OCPendFirma
    {
        public string PEDIDO { get; set; }
        public string FECHA { get; set; }
        public string DESCRIPCIONPAGO { get; set; }
        public decimal TOTAL_PEDIDO { get; set; }
        public string PROVEEDOR { get; set; }
        public string COMPRADOR { get; set; }
        public string SIMBOLO_MOEDA { get; set; }
        public int NIVEL { get; set; }

    }
}