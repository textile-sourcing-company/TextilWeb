using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica
{
    public class EBSegOCMes
    {
        public string CLIENTE { get; set; }
        public string MES { get; set; }
        public string PROGRAMA { get; set; }
        public string FAMILIA { get; set; }
        public string COD_PROD { get; set; }
        public string DSC_PROD   { get; set; }
        public int OC     { get; set; }
        public string PROVEEDOR { get; set; }
        public string COMPRADOR { get; set; }
        public string FECHA_EMI_OC { get; set; }
        public string FECHA_COMP_OC { get; set; }
        public string DESCRIPCIONPAGO { get; set; }
        public decimal? CNT_OC { get; set; }
        public decimal? PREC_UNIT { get; set; }
        public decimal? TOTAL { get; set; }
        public int COD_CENTROCOSTO { get; set; }
        public string DESC_CENTROCOSTO { get; set; }

    }
}