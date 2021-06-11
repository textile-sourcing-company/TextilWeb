using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Facturacion.FacturaProveedores
{
    public class Factura
    {
        public string RS { get; set; }
        public string SERIE_NUM { get; set; }
        public string FECHA_EMI { get; set; }
        public string FECHA_VEN { get; set; }
        public string COND_PAGO { get; set; }
        public string OC { get; set; }
        public string GR { get; set; }
        public string DESCRIPCION { get; set; }
        public string MONEDA { get; set; }
        public string UNI_MED { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal V_UNIT { get; set; }
        public decimal V_VENTA { get; set; }
        public decimal TOTAL { get; set; }
        public string RUC { get; set; }
        public decimal IGV { get; set; }
    }
}