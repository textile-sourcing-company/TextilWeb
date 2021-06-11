using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.AprobacionOC
{
    public class Lista3_RechTercFirma
    {
        public int PEDIDO { get; set; }
        public string FECHA { get; set; }
        public string DESCRIPCIONPAGO { get; set; }
        public string PROVEEDOR { get; set; }
        public string COMPRADOR { get; set; }
        public string SIMBOLO_MOEDA { get; set; }
        public string USUARIO { get; set; }
        public string FECHA_APROB { get; set; }
        public decimal TOTAL_PEDIDO { get; set; }
        public decimal VALOR_DOLAR { get; set; }
        public string MOTIVO { get; set; }
    }
}