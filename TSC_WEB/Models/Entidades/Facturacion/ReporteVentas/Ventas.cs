using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TSC_WEB.Models.Entidades.Facturacion.ReporteVentas
{
    public class Ventas
    {
        public string FECHA { get; set; }
        public string ANO { get; set; }
        public string MES { get; set; }
        public string CLIENTE_FACTURA { get; set; }
        public string CLIENTE_GUIA { get; set; }
        public string TIPO_PRENDA { get; set; }
        public string OBSERVACION { get; set; }
        public string NRO_FACTURA { get; set; }
        public string SERIE_FACTURA { get; set; }
        public string SERIE_GUIA { get; set; }
        public string NUM_GUIA { get; set; }
        public string MONEDA { get; set; }
        public string SIMBOLO { get; set; }
        public decimal PRECIO_REAL { get; set; }
        public int CANTIDAD_REAL { get; set; }
        public decimal IGV { get; set; }
        public decimal VALOR_REAL { get; set; }

        public string CONDPAGO { get; set; }
        public string DESCCONDPAGO { get; set; }
    }
}