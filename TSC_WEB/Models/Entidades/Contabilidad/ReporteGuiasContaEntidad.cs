using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TSC_WEB.Models.Entidades.Contabilidad.ReporteGuiasConta

{
    public class ReporteGuiasContaEntidad
    {
        public string EMPRESA { get; set; }
        public int NRO_GUIA { get; set; }
        public string SERIE_GUIA { get; set; }
        public string FECHA_EMISION_GUIA { get; set; }
        public decimal CANTIDAD_GUIA { get; set; }
        public int NRO_FACTURA { get; set; }
        public string SERIE_FACTURA { get; set; }
        public string FECHA_EMISION_FACTURA { get; set; }
        public int CANTIDAD_FACTURA { get; set; }
        public decimal VALOR_FACTURADO { get; set; } 
        public decimal VALOR_FACTURADO_AUTOMATICO { get; set; }
        public decimal VALOR_IGV_FAC { get; set; }
        public decimal VALOR_TOTAL_FAC { get; set; }
        public string CLIENTE { get; set; }
        public string CONCEPTO { get; set; }
        public string ESTADO { get; set; }
        public decimal DIFERENCIA { get; set; } 
    }
}